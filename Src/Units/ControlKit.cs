using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZL.Forms.UC;

namespace ZL.Sop
{
    public class UiMoniter
    {
        public string Key { get; set; }
        public string Name { get; set; }
    }
    public class ControlKit
    {

        /// <summary>
        /// 创建设备监控的标签
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="spacing"></param>
        public static void AddControlsToPanel(Panel panel, List<UiMoniter> tags, int spacing = 10)
        {
            Uc_DeviceSignalBig sampleControl = new Uc_DeviceSignalBig();
            panel.Controls.Clear(); // 清空 panel 中已有的控件
            int panelWidth = panel.Width;
            int panelHeight = panel.Height;

            int controlWidth = sampleControl.Width;
            int controlHeight = sampleControl.Height;

            int columns = (panelWidth + spacing) / (controlWidth + spacing);
            int rows = (panelHeight + spacing) / (controlHeight + spacing);

            int x = 0;
            int y = 0;
            int controlCount = 0;
            if (tags == null || tags.Count == 0) return;

            foreach (var tag in tags)
            {
                // 超出 panel 可容纳的控件数量时跳出
                if (controlCount >= columns * rows) break;

                if (y + controlHeight > panelHeight)
                {
                    break; // 超出 panel 高度时跳出
                }

                panel.Controls.Add(new Uc_DeviceSignalBig(tag.Key, tag.Name, Color.White) { Size = sampleControl.Size, Location = new Point(x, y) });

                x += controlWidth + spacing;
                controlCount++;

                if (x + controlWidth > panelWidth)
                {
                    x = 0;
                    y += controlHeight + spacing;
                }
            }
        }

    }
}
