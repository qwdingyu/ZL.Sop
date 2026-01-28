using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ZL.Sop.Framework
{
    public class SopViewManager
    {
        private readonly Panel _embeddedContainer;
        /// <summary>
        /// 唯一的宿主实例
        /// </summary>
        private readonly SopStepHost _host;
        /// <summary>
        /// 唯一的弹窗实例
        /// </summary>
        private UniversalOverlayForm _popupForm;

        // 视图工厂字典：Key -> 创建控件的委托
        // 使用 Func<Control> 允许懒加载（Lazy Loading），或者返回单例
        private readonly Dictionary<string, Func<Control>> _viewFactories = new();

        /// <summary>
        /// 缓存已创建的实例，避免重复 new
        /// </summary>
        private readonly Dictionary<string, Control> _instanceCache = new();
        /// <summary>
        /// 【新增】记录当前活跃的命令Key
        /// </summary>
        private string _currentActiveCommand;
        Color BackColor = Color.Transparent;
        public SopViewManager(Panel embeddedContainer, Color backColor)
        {
            _embeddedContainer = embeddedContainer;

            // 初始化宿主 (Host)
            _host = new SopStepHost();
            _host.OnStepPassed += () => ClosePopup(DialogResult.OK);
            _host.OnStepTimeout += () => ClosePopup(DialogResult.Abort);
            BackColor = backColor;
        }

        /// <summary>
        /// 注册视图工厂
        /// </summary>
        /// <param name="key">Command 字符串</param>
        /// <param name="factory">创建控件的方法，例如 () => new MyControl()</param>
        public void RegisterFactory(string key, Func<Control> factory)
        {
            if (!_viewFactories.ContainsKey(key)) _viewFactories[key] = factory;
        }

        /// <summary>
        /// 注册单例视图 (兼容旧代码习惯)
        /// </summary>
        public void RegisterView(string key, Control instance)
        {
            // 包装成返回固定实例的委托
            RegisterFactory(key, () => instance);
        }

        public DialogResult ShowStep(SopContext sopContext)
        {
            _currentActiveCommand = sopContext.Command;
            // 1. 获取单元实例 (含防销毁检查)
            Control unitControl = GetUnitInstance(sopContext.Command, BackColor);

            // 2. 挂载到 Host
            _host.MountUnit(unitControl, sopContext);

            // 3. 根据模式显示
            if (sopContext.DisplayMode == "ModalPopup")
            {
                return ShowPopupMode(unitControl);
            }
            else
            {
                return ShowEmbeddedMode();
            }
        }

        private DialogResult ShowPopupMode(Control unitControl)
        {
            // 确保没有残留
            ClosePopup(DialogResult.None);

            // 创建新弹窗
            _popupForm = new UniversalOverlayForm();

            // 计算合适大小
            Size hostSize = unitControl.Size.Width < 200 ? new Size(800, 600) : unitControl.Size;
            // 补偿 Host 的 Header/Footer
            hostSize.Height += 100;
            _popupForm.SetContent(_host, hostSize);

            try
            {
                return _popupForm.ShowDialog();
            }
            finally
            {
                // =========================================================
                // 【白屏修复核心】：必须把 Host 救出来！
                // =========================================================
                if (_host.Parent != null)
                {
                    // 强制从 Form 的 Controls 集合中移除
                    _host.Parent.Controls.Remove(_host);
                }

                // 卸载业务，停止 Timer
                _host.UnmountCurrentUnit();

                // 销毁弹窗
                if (_popupForm != null && !_popupForm.IsDisposed)
                {
                    _popupForm.Dispose();
                }
                _popupForm = null;
                _currentActiveCommand = null;
            }
        }

        private DialogResult ShowEmbeddedMode()
        {
            // 嵌入模式也需要确保 Host 没有被困在其他 Form 里
            if (_host.Parent != _embeddedContainer)
            {
                if (_host.Parent != null) _host.Parent.Controls.Remove(_host);

                _embeddedContainer.Controls.Clear();
                _host.Dock = DockStyle.Fill;
                _embeddedContainer.Controls.Add(_host);
            }

            _host.Visible = true;
            _host.BringToFront();
            // 嵌入模式下，即使当前步骤不超时，也不会自动关闭主窗口，因此直接返回OK
            // 如果嵌入模式也需要外部关闭，则需要更复杂的逻辑，但通常嵌入模式是持续显示直到下一个步骤
            return DialogResult.OK;
        }
        private void ClosePopup(DialogResult result)
        {
            if (_popupForm != null && _popupForm.Visible)
            {
                if (result != DialogResult.None)
                    _popupForm.DialogResult = result;
                else
                    _popupForm.Close();
            }
        }

        private Control GetUnitInstance(string Command, Color BackColor)
        {
            // 1. 查找工厂
            string key = _viewFactories.ContainsKey(Command) ? Command : "___DEFAULT_IMG___";

            // 默认工厂逻辑
            if (key == "___DEFAULT_IMG___" && !_viewFactories.ContainsKey(key))
            {
                _viewFactories[key] = () => new SimpleImageUnit(BackColor);
            }

            // 2. 检查缓存
            if (_instanceCache.ContainsKey(key))
            {
                var cachedControl = _instanceCache[key];
                // 【白屏修复辅助】：如果缓存的控件被意外 Dispose 了，重建它
                if (cachedControl.IsDisposed)
                {
                    _instanceCache.Remove(key);
                }
                else
                {
                    return cachedControl;
                }
            }

            // 3. 创建新实例
            var control = _viewFactories[key].Invoke();
            _instanceCache[key] = control;
            return control;
        }

        /// <summary>
        /// 【新增】外部事件触发当前弹窗步骤完成/失败
        /// 注意：此方法仅对当前以 ModalPopup 模式显示的步骤有效，且会检查 commandKey。
        /// </summary>
        /// <param name="commandKey">要触发的步骤的Command Key。</param>
        /// <param name="isPassed">是否标记为合格。</param>
        /// <param name="message">触发消息。</param>
        public void TriggerCurrentStepCompletion(string commandKey, bool isPassed, string message = "外部触发完成")
        {
            // 确保有弹窗且当前Command匹配，避免误操作
            if (_popupForm != null && _popupForm.Visible && _currentActiveCommand == commandKey)
            {
                _host.ExternalTriggerCompletion(isPassed, message);
            }
            // 如果是嵌入模式，通常不需要外部触发器来 "关闭" 步骤，因为它只是一个内容切换
            // 如果嵌入模式也需要类似“步骤完成”的信号，可能需要设计一个更通用的事件机制
        }
    }
}