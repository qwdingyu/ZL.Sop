using Demo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZL.Forms.Extension;
using ZL.Sop.Framework;

namespace Demo
{
    public partial class SbrSensorCheckControl : UserControl, ISopBusinessUnit
    {
        public bool RequireCountdown => true;
        // 将UI控件作为成员变量，方便访问
        private bool _isSeatOk, _isBackrestOk, _isCushionOk;
        private SensorSpec _seatSpec;
        private SensorSpec _backrestSpec;
        private SensorSpec _cushionSpec;

        Color MainBackColor = Color.FromArgb(88, 107, 118);
        private readonly Color SuccessColor = Color.Lime;
        private readonly Color ErrorColor = Color.Red;
        private readonly Color NormalColor = Color.Gray;


        public event Action<bool, string> OnStatusChanged;
        public SbrSensorCheckControl()
        {
            InitializeComponent();
            lblTitle.CenterLabelInPanel();
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
        public void Initialize(SopContext context)
        {
            InitializeDisplay();
            // 重置状态
            _isSeatOk = _seatSpec != null;
            _isBackrestOk = _backrestSpec != null;
            _isCushionOk = _cushionSpec != null;
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
        }
        public void Activate()
        {
            //UiPlcEvents.OnSensorDataeChanged += OnSensorDataeChanged;
        }

        public void Deactivate()
        {
            //UiPlcEvents.OnSensorDataeChanged -= OnSensorDataeChanged;
        }

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
                    lblSeatActual.BackColor =NormalColor;
                    _isSeatOk = false;
                }
                else
                {
                    _isSeatOk = (values.SeatPosition >= _seatSpec.LCL && values.SeatPosition <= _seatSpec.UCL);
                    lblSeatActual.BackColor = _isSeatOk ?SuccessColor : ErrorColor;
                    if (!_isSeatOk)
                        lblTitle.Text = "请调整座椅前后到指定位置！";
                }

                // 2. 更新靠背位置
                lblBackrestActual.Text = values.BackrestPosition.ToString();
                if (_backrestSpec == null)
                {
                    lblBackrestActual.BackColor = NormalColor;
                    _isBackrestOk = false;
                }
                else
                {
                    _isBackrestOk = (values.BackrestPosition >= _backrestSpec.LCL && values.BackrestPosition <= _backrestSpec.UCL);
                    lblBackrestActual.BackColor = _isBackrestOk ? SuccessColor : ErrorColor;
                    if (!_isBackrestOk)
                        lblTitle.Text = "请调整靠背前后到指定位置！";
                }

                // 3. 更新座垫位置
                lblCushionActual.Text = values.CushionPosition.ToString();
                if (_cushionSpec == null)
                {
                    lblCushionActual.BackColor =NormalColor;
                    _isCushionOk = false;
                }
                else
                {
                    _isCushionOk = (values.CushionPosition >= _cushionSpec.LCL && values.CushionPosition <= _cushionSpec.UCL);
                    lblCushionActual.BackColor = _isCushionOk ?SuccessColor : ErrorColor;
                    if (!_isCushionOk)
                        lblTitle.Text = "请调整座垫到指定位置！";
                }
                // 核心修改：当所有条件满足时，通过事件通知 SopStepHost
                if (_isSeatOk && _isBackrestOk && _isCushionOk)
                {
                    OnStatusChanged?.Invoke(true, "传感器检测合格！");
                }
                else
                {
                    // 如果有一个不合格，也通知 SopStepHost 状态为不合格，停止可能的合格延时
                    OnStatusChanged?.Invoke(false, lblTitle.Text);
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

        private void ClearDisplay()
        {
            lblTitle.Text = "等待开始测试";
            // 清理其他标签...
            //// 校验结束或未开始时隐藏
            //this.Visible = false; 
            _seatSpec = null;
            _backrestSpec = null;
            _cushionSpec = null;
        }
    }
}
