using System;

namespace ZL.Sop.Framework
{
    public interface ISopBusinessUnit
    {
        /// <summary>
        /// 决定是否显示倒计时 UI (核心修改) // 图片控件 -> false  检测控件 -> true
        /// </summary>
        bool RequireCountdown { get; }
        /// <summary>
        /// 初始化：传入配置
        /// </summary>
        /// <param name="context"></param>
        void Initialize(SopContext context);

        // 2. 激活：当控件显示在界面上时调用 (用于订阅全局事件，开启PLC扫描等)
        void Activate();

        // 3. 休眠：当控件从界面移除或被覆盖时调用 (用于解绑全局事件，停止不必要的UI刷新)
        // **这是解决后台CPU过高和内存泄漏的关键**
        void Deactivate();

        // 4. 状态变更事件：(bool isPassed, string message)
        event Action<bool, string> OnStatusChanged;
    }
}
