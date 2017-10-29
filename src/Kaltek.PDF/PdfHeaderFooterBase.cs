using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Kaltek.Drawing;

namespace Kaltek.PDF
{
    public abstract class PdfHeaderFooterBase : IPageElement
    {
        public PdfHeaderFooterBase(PdfPage page)
        {
            Page = page;
            Height = (int)Page.Dimensions.ConvertUnits(0.5f, UnitOfMeasure.Inch, UnitOfMeasure.Point);
            Color = Color.CadetBlue;
            Font = new Font
            {
                FontFamily = Fonts.TimesNewRoman,
                TextSize = 8,
            };
        }

        public string LeftText { get; set; }

        public string CenterText { get; set; }

        public string RightText { get; set; }

        public Font Font { get; set; }

        public int Height { get; set; }

        public Color Color { get; set; }

        public PdfPage Page { get; private set; }

        public int BorderWidth { get; set; }

        public Color BorderColor { get; set; }
    }
}
