using System;
using System.Drawing;
using System.Windows.Forms;
using ZL.Forms.Extension;
using Timer = System.Windows.Forms.Timer;

namespace ZL.Sop.Framework
{
    public partial class SopStepHost : UserControl
    {
        // UI 组件
        private readonly Panel _contentPanel;
        private readonly Label _lblTitle;
        private readonly Label _lblMessage;
        private readonly Panel _statusBar;
        private readonly Label _lblCountdown;

        // 逻辑组件
        private Timer _debounceTimer;
        private Timer _timeoutTimer;
        private ISopBusinessUnit _currentUnit;
        private SopContext _currentSopContext;
        private int _remainingSeconds;

        // 事件
        public event Action OnStepPassed;
        public event Action OnStepTimeout;

        // 【新增】属性控制 StatusBar
        public bool ShowStatusBar
        {
            get => _statusBar.Visible;
            set => _statusBar.Visible = value;
        }

        public SopStepHost()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ColorsDef.BackColor;
            //this.Padding = new Padding(2);

            // --- UI 构建 ---
            // 1. 标题 (Top)
            _lblTitle = new Label
            {
                Dock = DockStyle.Top,
                Height = 45,
                Font = new Font("微软雅黑", 16, FontStyle.Bold),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "初始化..."
            };

            // 2. 倒计时 (嵌入在 Title 右侧)
            _lblCountdown = new Label
            {
                Dock = DockStyle.Right,
                Width = 80,
                Font = new Font("Consolas", 14, FontStyle.Bold),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 0, 10, 0),
                BackColor = Color.Transparent
            };
            _lblTitle.Controls.Add(_lblCountdown); // 加入标题栏

            // 3. 底部消息 (Bottom)
            _lblMessage = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                Font = new Font("微软雅黑", 10),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // 4. 状态条 (Bottom, 在消息上方)
            _statusBar = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 6,
                BackColor = Color.Gray
            };

            // 5. 内容容器 (Fill)
            _contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(1),
                BackColor = ColorsDef.BackColor
            };

            // 添加顺序决定布局 (Fill 最后添加)
            this.Controls.Add(_contentPanel);
            this.Controls.Add(_statusBar);
            this.Controls.Add(_lblMessage);
            this.Controls.Add(_lblTitle);
            _lblMessage.Visible = false;
            // 初始化 Timer
            _timeoutTimer = new Timer { Interval = 1000 };
            _timeoutTimer.Tick += TimeoutTimer_Tick;

            _debounceTimer = new Timer { Interval = 1000 };
            _debounceTimer.Tick += DebounceTimer_Tick;
        }

        public void MountUnit(Control control, SopContext sopContext)
        {
            // 1. 卸载旧核
            UnmountCurrentUnit();
            // 存储当前上下文
            _currentSopContext = sopContext;
            // 2. UI 重置
            _lblTitle.Text = sopContext.StepName;
            _lblTitle.BackColor = Color.Transparent;
            _lblTitle.ForeColor = Color.White;
            _lblMessage.Text = "准备就绪...";
            _statusBar.BackColor = Color.Gray;

            // 3. 准备倒计时数据
            int timeout = sopContext.TimeoutMs > 0 ? sopContext.TimeoutMs / 1000 : 30;
            _remainingSeconds = timeout;

            // 4. 挂载新核
            if (control is ISopBusinessUnit unit)
            {
                _currentUnit = unit;
                unit.OnStatusChanged += Unit_OnStatusChanged;

                // 配置合格延时
                _debounceTimer.Interval = sopContext.PassDelayMs > 0 ? sopContext.PassDelayMs : 1; // 至少1ms

                // 决定是否显示倒计时 UI 以及是否启动超时逻辑
                bool canDisplayCountdown = unit.RequireCountdown;
                bool canStepTimeout = sopContext.AllowStepTimeout && canDisplayCountdown; // 只有需要倒计时且允许超时才启动超时逻辑

                _lblCountdown.Visible = canDisplayCountdown;
                this.ShowStatusBar = canDisplayCountdown; // 图片类不显示状态条

                if (canDisplayCountdown)
                {
                    _lblCountdown.Text = $"{_remainingSeconds}s";
                }

                // 初始化业务
                unit.Initialize(sopContext);

                // 【白屏修复关键】: 强制重新布局
                if (control.Parent != _contentPanel)
                {
                    control.Parent = null;
                    _contentPanel.Controls.Clear();
                    control.Dock = DockStyle.Fill;
                    _contentPanel.Controls.Add(control);
                }
                control.Visible = true;
                control.BringToFront();

                // 激活业务
                unit.Activate();
                //// 5. 【修复BUG的关键】启动超时计时器！
                //// 无论是否显示倒计时UI，逻辑计时器必须启动，否则永远不会触发超时
                //_timeoutTimer.Start();
                // 5. 启动超时计时器 (仅在允许超时时)
                if (canStepTimeout)
                {
                    _timeoutTimer.Start();
                }
                else
                {
                    _timeoutTimer.Stop(); // 确保停止
                }
            }
            else // 如果不是 ISopBusinessUnit，例如 SimpleImageUnit
            {
                _lblCountdown.Visible = false;
                this.ShowStatusBar = false;
                _timeoutTimer.Stop();
                _debounceTimer.Stop(); // 确保停止

                // 默认的图片显示逻辑
                if (control.Parent != _contentPanel)
                {
                    control.Parent = null;
                    _contentPanel.Controls.Clear();
                    control.Dock = DockStyle.Fill;
                    _contentPanel.Controls.Add(control);
                }
                control.Visible = true;
                control.BringToFront();
            }
        }

        public void UnmountCurrentUnit()
        {
            // 停止所有计时器
            _debounceTimer.Stop();
            _timeoutTimer.Stop();

            if (_currentUnit != null)
            {
                _currentUnit.Deactivate();
                _currentUnit.OnStatusChanged -= Unit_OnStatusChanged;
                _currentUnit = null;
            }
            _currentSopContext = null; // 清除上下文
        }

        /// <summary>
        /// 【新增】外部触发步骤完成/失败的方法
        /// </summary>
        /// <param name="isPassed">是否标记为合格</param>
        /// <param name="message">触发消息</param>
        public void ExternalTriggerCompletion(bool isPassed, string message = "外部事件触发完成")
        {
            this.SafeInvoke(() =>
            {
                _timeoutTimer.Stop();
                _debounceTimer.Stop();

                _lblMessage.Text = message;

                if (isPassed)
                {
                    _statusBar.BackColor = Color.LimeGreen;
                    _lblTitle.ForeColor = Color.LimeGreen;
                    _lblTitle.Text = _currentSopContext?.StepName + " - " + "已合格";
                    OnStepPassed?.Invoke(); // 直接触发合格事件
                }
                else
                {
                    _statusBar.BackColor = Color.OrangeRed;
                    _lblTitle.ForeColor = Color.OrangeRed;
                    _lblTitle.Text = _currentSopContext?.StepName + " - " + "已中止";
                    OnStepTimeout?.Invoke(); // 外部失败也视为超时关闭
                }
                // 如果是嵌入模式，不会触发Form.DialogResult，但可以确保UI更新
                // 如果是弹窗模式，SopViewManager会收到OnStepPassed/OnStepTimeout并关闭弹窗
            });
        }
        private void TimeoutTimer_Tick(object sender, EventArgs e)
        {
            _remainingSeconds--;

            // 仅在需要显示时更新 UI
            if (_lblCountdown.Visible)
            {
                _lblCountdown.Text = $"{_remainingSeconds}s";
            }

            if (_remainingSeconds <= 0)
            {
                // 超时处理
                _timeoutTimer.Stop();
                _debounceTimer.Stop();

                _lblTitle.BackColor = Color.OrangeRed;
                _lblTitle.Text = (_currentSopContext?.StepName ?? "步骤") + " - 测试超时!";

                // 触发事件通知 Manager 关闭窗口
                OnStepTimeout?.Invoke();
            }
        }

        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            _debounceTimer.Stop();
            _timeoutTimer.Stop(); // 成功后也要停止超时计时
            _lblTitle.BackColor = Color.Transparent; // 恢复背景色
            _lblTitle.ForeColor = Color.Black; // 恢复文字颜色
            OnStepPassed?.Invoke();
        }

        private void Unit_OnStatusChanged(bool isOk, string msg)
        {
            this.SafeInvoke(() =>
            {
                _lblMessage.Text = msg;

                if (isOk)
                {
                    // 避免重复启动
                    if (!_debounceTimer.Enabled)
                    {
                        _statusBar.BackColor = Color.LimeGreen;
                        _lblTitle.ForeColor = Color.LimeGreen;
                        _lblTitle.Text = _currentSopContext?.StepName + " - " + "检测合格";
                        _debounceTimer.Start(); // 启动合格延时计时
                    }
                }
                else
                {
                    _debounceTimer.Stop();
                    _statusBar.BackColor = Color.IndianRed;
                    _lblTitle.ForeColor = Color.IndianRed;
                    _lblTitle.Text = _currentSopContext?.StepName + " - " + "请调整";
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnmountCurrentUnit();
                _debounceTimer?.Dispose();
                _timeoutTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}