using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ZL.Forms;
using ZL.Sop.Framework;

namespace ZL.Sop
{
    /// <summary>
    /// 专门用于显示 SOP 图片的业务单元
    /// </summary>
    public class SimpleImageUnit : PictureBox, ISopBusinessUnit
    {
        public bool RequireCountdown => false;
        public event Action<bool, string> OnStatusChanged;

        public SimpleImageUnit(Color BackColor)
        {
            this.Dock = DockStyle.Fill;
            this.SizeMode = PictureBoxSizeMode.Zoom;
            this.BackColor = BackColor;
        }

        public void Initialize(SopContext context)
        {
            // 这里恢复了之前的图片加载逻辑
            string imageName = context.PicturePath;
            string imgPath = Path.Combine(context.GetExtra<string>("SopImgDir", ""), imageName ?? "");

            if (string.IsNullOrEmpty(imageName) || !File.Exists(imgPath))
            {
                imgPath = context.GetExtra<string>("SopDefaultImg", "");
            }

            ImageHelper.SafeLoadBackgroundImage(this, imgPath);
        }

        public void Activate()
        {
            // 图片是静态的，一加载就算“合格”
            // 可以在这里立即触发，或者等待用户点击下一步（取决于宿主逻辑）
            // 如果需要自动通过，可以调用：
            // OnStatusChanged?.Invoke(true, "请参照图示操作");
        }

        public void Deactivate()
        {
            // 图片没啥好释放的，除非需要释放 Image 内存
            if (this.Image != null)
            {
                // 注意：根据你的 ImageHelper 实现决定是否需要 Dispose
            }
        }
    }
}
