using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZL.Forms.UC;
using ZL.Sop;
using ZL.Sop.Framework;

namespace SeatTest.UI
{
    public partial class PreFlightCheckControl : UserControl, ISopBusinessUnit
    {
        public bool RequireCountdown => true;
        // 将UI控件作为成员变量，方便访问
        private bool _isSeatOk, _isBackrestOk, _isCushionOk;
        private SensorSpec _seatSpec;
        private SensorSpec _backrestSpec;
        private SensorSpec _cushionSpec;

        Color MainBackColor = Color.FromArgb(88, 107, 118);
        private readonly Color SuccessColor = Color.LimeGreen;
        private readonly Color ErrorColor = Color.Red;


        public event Action<bool, string> OnStatusChanged;

        public List<UiMoniter> PlcUiMoniterList = new List<UiMoniter>();
        public PreFlightCheckControl()
        {
            InitializeComponent();
            ClearDisplay();
        }
        /// <summary>
        /// 异步校验方法
        /// </summary>
        /// <param name="seatSpec">座椅位置规格</param>
        /// <param name="backrestSpec">靠背位置规格</param>
        /// <param name="cushionSpec">座垫位置规格</param>
        /// <param name="timeoutMs">超时时间（毫秒）</param>
        public void PerformCheckAsync(SensorSpec seatSpec, SensorSpec backrestSpec, SensorSpec cushionSpec)
        {
            _seatSpec = seatSpec;
            _backrestSpec = backrestSpec;
            _cushionSpec = cushionSpec;
            InitializeDisplay();
        }
        public void Initialize(SopContext config)
        {
            PlcUiMoniterList = new List<UiMoniter>();
            PlcUiMoniterList.Add(new UiMoniter() { Key = "1", Name = "托盘到位" });
            PlcUiMoniterList.Add(new UiMoniter() { Key = "2", Name = "产品在位" });
            PlcUiMoniterList.Add(new UiMoniter() { Key = "3", Name = "气缸顶升" });
            PlcUiMoniterList.Add(new UiMoniter() { Key = "4", Name = "产品测量" });
            PlcUiMoniterList.Add(new UiMoniter() { Key = "5", Name = "测量结果" });
            PlcUiMoniterList.Add(new UiMoniter() { Key = "6", Name = "产品方向" });
            InitializeDisplay();
        }
        private void InitializeDisplay()
        {
            lblTitle.Text = "请调整座椅至指定位置...";
            lblSeatLCL.Text = _seatSpec?.LCL.ToString() ?? "N/A";
            lblSeatUCL.Text = _seatSpec?.UCL.ToString() ?? "N/A";
            lblBackrestLCL.Text = _backrestSpec?.LCL.ToString() ?? "N/A";
            lblBackrestUCL.Text = _backrestSpec?.UCL.ToString() ?? "N/A";
            lblCushionLCL.Text = _cushionSpec?.LCL.ToString() ?? "N/A";
            lblCushionUCL.Text = _cushionSpec?.UCL.ToString() ?? "N/A";

            lblSeatActual.Text = "---";
            lblBackrestActual.Text = "---";
            lblCushionActual.Text = "---";

            lblSeatActual.BackColor = SystemColors.Control;
            lblBackrestActual.BackColor = SystemColors.Control;
            lblCushionActual.BackColor = SystemColors.Control;

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
                //// 检查 _validationTcs 是否在 BeginInvoke 排队期间已完成
                //if (_validationTcs.Task.IsCompleted) return;

                // 1. 更新座椅位置
                lblSeatActual.Text = values.SeatPosition.ToString();
                if (_seatSpec == null)
                {
                    lblSeatActual.BackColor = Color.Gray;
                    //// 如果没有规格，默认为不需要检测
                    //_isSeatOk = true; 
                }
                else
                {
                    _isSeatOk = (values.SeatPosition >= _seatSpec.LCL && values.SeatPosition <= _seatSpec.UCL);
                    lblSeatActual.BackColor = _isSeatOk ? Color.Lime : ErrorColor;
                    if (!_isSeatOk)
                        lblTitle.Text = "请调整座椅前后到指定位置！";
                }

                // 2. 更新靠背位置
                lblBackrestActual.Text = values.BackrestPosition.ToString();
                if (_backrestSpec == null) lblBackrestActual.BackColor = Color.Gray;
                else
                {
                    _isBackrestOk = (values.BackrestPosition >= _backrestSpec.LCL && values.BackrestPosition <= _backrestSpec.UCL);
                    lblBackrestActual.BackColor = _isBackrestOk ? Color.Lime : ErrorColor;
                    if (!_isBackrestOk)
                        lblTitle.Text = "请调整靠背前后到指定位置！";
                }

                // 3. 更新座垫位置
                lblCushionActual.Text = values.CushionPosition.ToString();
                if (_cushionSpec == null)
                {
                    lblCushionActual.BackColor = Color.Gray;
                    _isCushionOk = true;
                }
                else
                {
                    _isCushionOk = (values.CushionPosition >= _cushionSpec.LCL && values.CushionPosition <= _cushionSpec.UCL);
                    lblCushionActual.BackColor = _isCushionOk ? Color.Lime : ErrorColor;
                    if (!_isCushionOk)
                        lblTitle.Text = "请调整座垫到指定位置！";
                }
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
            // 清理其他标签...
            //// 校验结束或未开始时隐藏
            //this.Visible = false; 
            _seatSpec = null;
            _backrestSpec = null;
            _cushionSpec = null;
            SetSameStatus(false);
        }

    }
}
