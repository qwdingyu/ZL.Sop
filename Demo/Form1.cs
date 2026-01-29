using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ZL.Forms.Extension;
using ZL.Sop.Framework;

namespace Demo
{
    public partial class Form1 : Form
    {
        SbrSensorCheckControl sbrSensorCheckControl = new SbrSensorCheckControl();
        SbrTestPlcStatusControl sbrTestPlcStatusControl = new SbrTestPlcStatusControl();
        SelfLearning selfLearning = new SelfLearning();
        private SopViewManager _viewManager;
        public string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
        public string SopImgDir { get; set; }
        public string SopDefaultImg { get; set; }
        public Dictionary<string, object> Extras = new Dictionary<string, object>();
        public Form1()
        {
            InitializeComponent();
            // 启用双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeSopViews();
        }

        private void InitializeSopViews()
        {
            _viewManager = new SopViewManager(this.splitContainer2.Panel2, ColorsDef.BackColor);
            // 注册业务控件
            _viewManager.RegisterView("AutoSbrSensorCheck", this.sbrSensorCheckControl);
            _viewManager.RegisterView("AutoSbrResistance", this.sbrTestPlcStatusControl);
            _viewManager.RegisterView("StartSelfLearning", this.selfLearning);
            _viewManager.RegisterView("VerifyLearningResult", this.selfLearning);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SopImgDir = Path.Combine(BaseDir, "Photo");
            if (!Directory.Exists(SopImgDir))
                Directory.CreateDirectory(SopImgDir);
            SopDefaultImg = Path.Combine(SopImgDir, "Default.png");
            //设置缺省图片

            Extras = new Dictionary<string, object>() {
                        { "SopImgDir", SopImgDir },
                        { "SopDefaultImg", SopDefaultImg }
                    };
            SetSopTip(new SopContext() { Command = "", PicturePath = "", Extras = Extras });
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void SetSopTip(SopContext context)
        {
            this.SafeInvoke(() =>
            {
                var result = _viewManager.ShowStep(context);

                if (result == DialogResult.Abort)
                {
                    //Log.Error("测试步骤超时！");
                    // 处理超时逻辑，例如重试或跳过
                }
                else if (result == DialogResult.Cancel)
                {
                    //Log.Info("测试被中断");
                }
            });
        }
        private void btn_ImgChg_Click(object sender, EventArgs e)
        {
            var imgName = ImageUtils.GetRandomImageFileName(SopImgDir);
            SetSopTip(new SopContext() { Command = "", PicturePath = imgName, DisplayMode = "Embedded", Extras = Extras });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var context = new SopContext
            {
                StepName = "AutoSbrSensorCheck",
                Command = "AutoSbrSensorCheck",
                PicturePath = "3.png",
                TimeoutMs = 5000,
                PassDelayMs = 2000,       // 合格后2秒延时关闭
                AllowStepTimeout = true,  // 允许超时
                DisplayMode = "ModalPopup",
                Extras = Extras
            };
            SetSopTip(context);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var context = new SopContext
            {
                StepName = "AutoSbrResistance",
                Command = "AutoSbrResistance",
                PicturePath = "",
                TimeoutMs = 5000,
                PassDelayMs = 2000,       // 合格后2秒延时关闭
                AllowStepTimeout = true,  // 允许超时
                DisplayMode = "Embedded",
                Extras = Extras
            };
            SetSopTip(context);
        }
    }
}
