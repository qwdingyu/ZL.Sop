using System.Drawing;
using System.Windows.Forms;

namespace ZL.Sop
{
    public partial class UniversalOverlayForm : Form
    {
        private Timer _fadeTimer;

        public UniversalOverlayForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.BackColor = Color.Black;
            this.Opacity = 0;
            // 黑色边框效果
            this.Padding = new Padding(2);
        }

        public void SetContent(Control content, Size preferredSize)
        {
            // 设置窗体大小
            this.Size = new Size(preferredSize.Width + 20, preferredSize.Height + 20);

            // 准备容器Panel（可选，为了更好的背景控制）
            Panel container = new Panel();
            container.Dock = DockStyle.Fill;
            container.BackColor = SystemColors.Control; // 恢复控件默认背景
            this.Controls.Add(container);

            // 添加内容
            content.Dock = DockStyle.Fill;
            container.Controls.Add(content);

            // 淡入动画
            _fadeTimer = new Timer { Interval = 20 };
            _fadeTimer.Tick += (s, e) =>
            {
                // 稍微留一点透
                if (this.Opacity < 0.95)
                    this.Opacity += 0.1;
                else
                    _fadeTimer.Stop();
            };
            _fadeTimer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _fadeTimer?.Stop();
            base.OnFormClosing(e);
        }
    }
}