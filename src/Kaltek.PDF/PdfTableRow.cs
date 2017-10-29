using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Kaltek.PDF
{
    public class PdfTableRow
    {
        public PdfTableRow(PdfTable table)
        {
            Table = table;
            Cells = new List<PdfTableCell>();
            BackColor = Color.White;
            BorderColor = Color.Black;
            BorderWidth = 0;
        }
   
        public int Height { get; set; }

        public int Width { get; set; }

        public PdfTable Table { get; }

        public List<PdfTableCell> Cells { get; set; }

        public int BorderWidth { get; set; }

        public Color BorderColor { get; set; }

        public Color BackColor { get; set; }

        public void AddCell(PdfTableCell cell)
        {
            Cells.Add(cell);
        }
    }
}
