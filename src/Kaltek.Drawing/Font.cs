using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Kaltek.Drawing
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
