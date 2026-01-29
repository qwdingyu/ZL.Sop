using System;
using System.Drawing;
using System.Windows.Forms;
using ZL.Forms.Dgv;
using ZL.Sop.Framework;

namespace Demo
{
    public partial class SelfLearning : UserControl, ISopBusinessUnit
    {
        public bool RequireCountdown => true;
        // 2. 接口事件定义
        public event Action<bool, string> OnStatusChanged;

        public SelfLearning()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.Dock = DockStyle.Fill; // 自身默认Fill
            //this.Margin = new Padding(0); // 去掉外边距

            dgvTraffic.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            SetupTrafficGrid();
        }

        /// <summary>
        /// 【接口实现】初始化
        /// </summary>
        /// <param name="config"></param>
        public void Initialize(SopContext context)
        {
            // 清空日志，准备开始
            ClearLogs();
        }

        /// <summary>
        /// 【接口实现】激活
        /// </summary>
        public void Activate()
        {
            // 订阅事件
            GlobalEvents.OnKeyTrafficLogged += HandleKeyTraffic;
        }

        /// <summary>
        /// 【接口实现】休眠
        /// </summary>
        public void Deactivate()
        {
            // 取消订阅 (至关重要)
            GlobalEvents.OnKeyTrafficLogged -= HandleKeyTraffic;
        }
        /// <summary>
        /// 【修改点 3】实现清空日志逻辑
        /// </summary>
        public void ClearLogs()
        {
            // 线程安全检查
            if (this.dgvTraffic.InvokeRequired)
            {
                this.Invoke(new Action(ClearLogs));
                return;
            }

            // 直接清空行
            dgvTraffic.Rows.Clear();
        }
        private void SetupTrafficGrid()
        {
            dgvTraffic.AllowUserToAddRows = false;
            dgvTraffic.AllowUserToDeleteRows = false;
            dgvTraffic.ReadOnly = true;
            dgvTraffic.RowHeadersVisible = false;
            dgvTraffic.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTraffic.BackgroundColor = Color.White;
            dgvTraffic.BorderStyle = BorderStyle.Fixed3D;

            // =================================================================
            // 【修改 1】全局字体设置 (改为 18号)
            // =================================================================
            // 使用微软雅黑，显示中文更舒服
            var baseFont = new Font("Segoe UI", 12F, FontStyle.Regular);
            var headerFont = new Font("Segoe UI", 14F, FontStyle.Bold);

            dgvTraffic.DefaultCellStyle.Font = baseFont;
            dgvTraffic.ColumnHeadersDefaultCellStyle.Font = headerFont;

            // =================================================================
            // 【修改 2】调整行高以适应大字体 (非常重要，否则字会被切掉)
            // =================================================================
            // 数据行高度
            dgvTraffic.RowTemplate.Height = 28;
            // 标题行高度
            dgvTraffic.ColumnHeadersHeight = 38;
            // 禁止用户调整行高，保持整齐
            dgvTraffic.AllowUserToResizeRows = false;

            dgvTraffic.Columns.Clear();

            // 1. 步骤名称
            dgvTraffic.Columns.Add("colStep", "步骤");
            dgvTraffic.Columns["colStep"].Width = 110;

            // 2. 方向
            dgvTraffic.Columns.Add("colDir", "方向");
            dgvTraffic.Columns["colDir"].Width = 80;
            dgvTraffic.Columns["colDir"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 3. 报文数据 (核心)
            var colData = new DataGridViewTextBoxColumn();
            colData.Name = "colData";
            colData.HeaderText = "报文数据";
            colData.Width = 260;
            colData.DefaultCellStyle.Font = new Font("Consolas", 14F, FontStyle.Regular);
            dgvTraffic.Columns.Add(colData);

            // 4. 解析含义
            dgvTraffic.Columns.Add("colMeaning", "解析含义");
            dgvTraffic.Columns["colMeaning"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // 应用你的自定义主题
            // 注意：如果 SetCustomTheme 内部重置了字体，可能需要去查看那个扩展方法
            // 通常颜色设置不会影响字体大小
            dgvTraffic.SetCustomTheme(
                    backgroundColor: ColorsDef.BackColor,
                    fontColor: ColorsDef.FontColor,
                    selectionBackColor: ColorsDef.DgvSelectionBackColor,
                    selectionForeColor: ColorsDef.FontColor,
                    headerSelectionBackColor: ColorsDef.DgvHeaderSelectionColor,
                    headerSelectionForeColor: Color.Transparent
                    );
        }
        private void HandleKeyTraffic(KeyTrafficLogModel model)
        {
            if (this.dgvTraffic.InvokeRequired)
            {
                // 考虑到控件可能在事件触发时恰好被Dispose，加一层 try-catch 或检查
                if (this.IsDisposed || !this.IsHandleCreated) return;

                try
                {
                    this.Invoke(new Action<KeyTrafficLogModel>(HandleKeyTraffic), model);
                }
                catch (ObjectDisposedException) { /* 忽略 */ }
                return;
            }

            int index = dgvTraffic.Rows.Add();
            var row = dgvTraffic.Rows[index];

            row.Cells["colStep"].Value = model.StepName;
            row.Cells["colData"].Value = model.DataHex;
            row.Cells["colMeaning"].Value = model.Meaning;

            bool isTx = model.Direction.Contains("TX") || model.Direction.Contains("Send");
            row.Cells["colDir"].Value = isTx ? "TX ➜" : "RX ⬅";

            DataGridViewCellStyle style = new DataGridViewCellStyle();

            if (isTx)
            {
                style.ForeColor = Color.DarkSlateGray;
                style.BackColor = Color.FromArgb(245, 250, 255);
            }
            else
            {
                if (model.IsError)
                {
                    style.ForeColor = Color.Red;
                    style.Font = new Font(dgvTraffic.Font, FontStyle.Bold);
                    style.BackColor = Color.FromArgb(255, 240, 240);
                    row.Cells["colMeaning"].Value = "❌ " + model.Meaning;
                }
                else if (model.Meaning.Contains("Pending") || model.Meaning.Contains("78"))
                {
                    style.ForeColor = Color.DarkGoldenrod;
                    style.BackColor = Color.Ivory;
                    row.Cells["colMeaning"].Value = "⏳ " + model.Meaning;
                }
                else
                {
                    style.ForeColor = Color.DarkGreen;
                    style.BackColor = Color.FromArgb(240, 255, 240);
                    row.Cells["colMeaning"].Value = "✅ " + model.Meaning;
                }
            }

            row.DefaultCellStyle = style;
            dgvTraffic.FirstDisplayedScrollingRowIndex = index;

            if (dgvTraffic.Rows.Count > 500)
            {
                dgvTraffic.Rows.RemoveAt(0);
            }
            //// =================================================================
            //// 【新增建议】: 自动判断完成逻辑
            //// 如果你在自学习过程中能识别出“成功完成”的报文，可以在这里触发
            //// =================================================================
            //if (!isTx && model.Meaning.Contains("Learning Success"))
            //{
            //    lblTitle.Text = "自学习成功！";
            //    lblTitle.BackColor = Color.LimeGreen;

            //    // 延迟一会触发完成事件，让用户看清成功提示
            //    Timer t = new Timer { Interval = 1000 };
            //    t.Tick += (s, ev) =>
            //    {
            //        t.Stop(); t.Dispose();
            //        OnStepCompleted?.Invoke();
            //    };
            //    t.Start();
            //}
        }
    }
}