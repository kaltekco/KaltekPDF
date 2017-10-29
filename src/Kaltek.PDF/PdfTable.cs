using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Kaltek.PDF
{
    public class PdfTable : IPageElement
    {
        public PdfTable(PdfPage page, int left, int top)
        {
            Page = page;
            Left = left;
            Top = top;
            BorderColor = Color.Black;
            BorderWidth = 1;
            Rows = new List<PdfTableRow>();
        }

        public PdfPage Page { get; }

        public int Left { get; }

        public int Top { get; }

        public List<PdfTableRow> Rows { get; set; }

        public Color BorderColor { get; set; }

        public int BorderWidth { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}
