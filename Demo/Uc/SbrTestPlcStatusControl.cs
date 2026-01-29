using Demo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZL.Forms.Extension;
using ZL.Forms.UC;
using ZL.Sop;
using ZL.Sop.Framework;

namespace Demo
{
    public partial class SbrTestPlcStatusControl : UserControl, ISopBusinessUnit
    {
        public bool RequireCountdown => false;
        Color MainBackColor = Color.FromArgb(88, 107, 118);
        public event Action<bool, string> OnStatusChanged;
        public List<UiMoniter> PlcUiMoniterList = new List<UiMoniter>();
        public SbrTestPlcStatusControl()
        {
            InitializeComponent();
            ClearDisplay();
        }
        public void Initialize(SopContext context)
        {
            lblTitle.CenterLabelInPanel();
            //lblTitle.Text = "...";
            PlcUiMoniterList = new List<UiMoniter>();
            PlcUiMoniterList.Add(new UiMoniter() { Key = "1", Name = "托盘到位" });
            PlcUiMoniterList.Add(new UiMoniter() { Key = "2", Name = "产品在位" });
            PlcUiMoniterList.Add(new UiMoniter() { Key = "3", Name = "气缸顶升" });
            PlcUiMoniterList.Add(new UiMoniter() { Key = "4", Name = "产品测量" });
            PlcUiMoniterList.Add(new UiMoniter() { Key = "5", Name = "测量结果" });
            PlcUiMoniterList.Add(new UiMoniter() { Key = "6", Name = "产品方向" });
            ControlKit.AddControlsToPanel(pan_State, PlcUiMoniterList, 12);
        }
        public void Activate()
        {
            //UiPlcEvents.OnSensorDataeChanged += OnSensorDataeChanged;
            //GlobalEvents.OnSbrStepTipChanged += OnSbrStepTipChanged;
            //UiPlcEvents.OnTwoHandStartChanged += OnTwoHandStartChanged;
        }

        public void Deactivate()
        {
            //UiPlcEvents.OnSensorDataeChanged -= OnSensorDataeChanged;
            //GlobalEvents.OnSbrStepTipChanged -= OnSbrStepTipChanged;
            //UiPlcEvents.OnTwoHandStartChanged -= OnTwoHandStartChanged;
        }
        void OnSbrStepTipChanged(int level, string msg) => SetTip(level, msg);

        void OnTwoHandStartChanged(bool isPress) => SetTip(0, "");
        /// <summary>
        /// 核心方法：从外部接收新的传感器数据并更新UI
        /// </summary>
        public void OnSensorDataeChanged(SensorValues values, Dictionary<string, object> moniterDic)
        {
            // 如果当前没有在执行校验（_validationTcs为null或已完成），则不处理
            //if (this.IsDisposed || !this.IsHandleCreated || _validationTcs == null || _validationTcs.Task.IsCompleted) return;
            if (this.IsDisposed || !this.IsHandleCreated) return;

            // 确保在UI线程上更新
            this.BeginInvoke((Action)(() =>
            {
                foreach (var it in moniterDic)
                {
                    OnTagValChanged(it.Key, it.Value);
                }
            }));
        }
        public void SetTip(int level, string msg)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;
            Invoke(new Action(() =>
            {
                lblTitle.BackColor = level == 2 ? Color.Red : MainBackColor;
                lblTitle.Text = msg;
            }));
        }
        /// <summary>
        /// 更新标签的状态显示
        /// </summary>
        /// <param name="tagid"></param>
        /// <param name="state"></param>
        private void OnTagValChanged(string tagid, dynamic state)
        {
            if (!(state is bool)) return;
            OnTagValChanged(tagid, state, true);
        }

        private void OnTagValChanged(string tagid, dynamic state, bool logMsg = false)
        {
            try
            {
                var _state = Convert.ToBoolean(state);
                foreach (Control control in pan_State.Controls)
                {
                    if (control is Uc_DeviceSignalBig ucDeviceSignal && ucDeviceSignal.Id == tagid)
                    {
                        ucDeviceSignal.state = _state; break;
                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                throw;
            }
        }

        /// <summary>
        /// 将全部信号 设置为一种状态
        /// </summary>
        /// <param name="state"></param>
        private void SetSameStatus(bool state = false)
        {
            foreach (Control control in pan_State.Controls)
            {
                if (control is Uc_DeviceSignalBig ucDeviceSignal)
                {
                    ucDeviceSignal.state = state;
                }
            }
        }
        private void ClearDisplay()
        {
            lblTitle.Text = "等待开始测试";
            SetSameStatus(false);
        }

    }
}
