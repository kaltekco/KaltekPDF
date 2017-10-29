using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Kaltek.Drawing;

namespace Kaltek.PDF
{
    public class PdfParagraph : IPageElement
    {
        public PdfParagraph()
        {
            Font = Fonts.Arial;
            Color = Color.Black;
        }

        public string Font { get; set; }

        public Color Color { get; set; }

        public string Text { get; set; }

        public Rectangle Bounds { get; set; }
    }
}
