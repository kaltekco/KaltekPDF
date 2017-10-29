using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Kaltek.Drawing;

namespace Kaltek.PDF
{
    public class PdfTableCell
    {
        public PdfTableCell(PdfTableRow row, object data, string dataFormat, bool suppressIfZero)
        {
            Row = row;
            Data = data;
            DataFormat = dataFormat;
            SuppressIfZero = suppressIfZero;

            Padding = new Padding(5, 3, 3, 5);
            BackColor = Color.White;
            ForeColor = Color.Black;
            Font = new Font
            {
                FontFamily = Fonts.TimesNewRoman,
                TextSize = 8,
            };
            Width = 100;
            TextJustify = TextJustify.Left;
        }

        public int Width { get; set; }

        public PdfTableRow Row { get; }

        public object Data { get; }

        public string DataFormat { get; }

        public bool SuppressIfZero { get; }

        public int Height { get; set; }

        public Font Font { get; set; }

        public Color BackColor { get; set; }

        public Color ForeColor { get; set; }

        public Padding Padding { get; set; }

        public TextJustify TextJustify { get; set; }

        public bool DrawRightBorder { get; set; }
    }
}
