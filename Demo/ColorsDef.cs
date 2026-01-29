using System.Drawing;

namespace Demo
{

    public class ColorsDef
    {

        public static Color FontColor = Color.White;

        public static Color DgvHeaderSelectionColor = Color.FromArgb(90, 150, 200); 
        public static Color DgvSelectionBackColor = Color.FromArgb(53, 64, 82);
        //Color MainBackColor = Color.FromArgb(88, 107, 118);
        /// <summary>
        /// 主背景色
        /// </summary>
        public static Color BackColor = Color.White;//  Color.FromArgb(17, 21, 41);
        public static Color ButtonBackColor = Color.FromArgb(53, 64, 82);
        /// <summary>
        /// 通过 (PASS)
        /// </summary>
        public static Color BackColorPASS = ColorTranslator.FromHtml("#10b981");
        /// <summary>
        /// 失败 (FAIL)
        /// </summary>
        public static Color BackColorFAIL = ColorTranslator.FromHtml("#ef4444");
        /// <summary>
        /// 测试中/激活 (Active)
        /// </summary>
        public static Color BackColorActive = ColorTranslator.FromHtml("#22d3ee");
        /// <summary>
        /// 待机 (Idle)
        /// </summary>
        public static Color BackColorIdle = ColorTranslator.FromHtml("#64748b");
    }
}
