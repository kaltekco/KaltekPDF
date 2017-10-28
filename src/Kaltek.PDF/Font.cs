using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Kaltek.PDF
{
    public class Font
    {
        public Font()
        {
            FontFamily = Fonts.TimesNewRoman;
        }

        public string FontFamily { get; set; }
        public int TextSize { get; set; }
    }
}
