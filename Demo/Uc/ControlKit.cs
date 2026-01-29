using System.Drawing;
using System.Windows.Forms;
using ZL.Forms.UC;
using ZL.Gear.Drivers.Devices.Plc;

namespace SeatTest.UI.Uc
{
    public class ControlKit
    {

        /// <summary>
        /// 创建设备监控的标签
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="spacing"></param>
        public static void AddControlsToPanel(Panel panel, int spacing = 10)
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
            var tags = PlcDevice.uiTagMoniters;
            if (tags == null || tags.Count == 0) return;

            foreach (var tag in tags)
            {
                // 超出 panel 可容纳的控件数量时跳出
                if (controlCount >= columns * rows) break;

                if (y + controlHeight > panelHeight)
                {
                    break; // 超出 panel 高度时跳出
                }

                panel.Controls.Add(new Uc_DeviceSignalBig(tag.TagKey, tag.TagName, Color.White) { Size = sampleControl.Size, Location = new Point(x, y) });

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
