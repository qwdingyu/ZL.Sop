using System.Collections.Generic;

namespace ZL.Sop.Framework
{
    /// <summary>
    /// SOP 步骤上下文 (DTO)
    /// 用于解耦复杂的业务实体，仅传递 UI 渲染所需的数据
    /// </summary>
    public class SopContext
    {
        // 基础信息
        public string StepName { get; set; }
        public string Command { get; set; }
        public int TimeoutMs { get; set; } = 30000;

        /// <summary>
        /// 步骤合格后，延时指定毫秒自动关闭。默认为1000ms (1秒)。
        /// 设置为0或负数表示立即关闭（不延时）。
        /// </summary>
        public int PassDelayMs { get; set; } = 1000;

        /// <summary>
        /// 决定此步骤是否允许超时并自动关闭。
        /// SimpleImageUnit 这类展示型步骤应设置为 false。
        /// 如果 RequireCountdown 为 false，此值通常也为 false。
        /// </summary>
        public bool AllowStepTimeout { get; set; } = true;

        /// <summary>
        /// 决定这个步骤怎么显示
        /// </summary>
        public string DisplayMode { get; set; } = "Embedded";
        /// <summary>
        /// 图片专用
        /// </summary>
        public string PicturePath { get; set; }

        public Dictionary<string, object> Extras { get; set; } = new Dictionary<string, object>();

        // 辅助方法：快速获取扩展参数
        public T GetExtra<T>(string key, T defaultValue = default)
        {
            if (Extras != null && Extras.ContainsKey(key) && Extras[key] is T val)
            {
                return val;
            }
            return defaultValue;
        }
    }
}
