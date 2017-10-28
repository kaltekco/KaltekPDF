using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Kaltek.PDF
{
    public class TableRow
    {
        public TableRow(Table table)
        {
            Table = table;
            Cells = new List<TableCell>();
            BackColor = Color.White;
            BorderColor = Color.Black;
            BorderWidth = 0;
        }
   
        public int Height { get; set; }

        public int Width { get; set; }

        public Table Table { get; }

        public List<TableCell> Cells { get; set; }

        public int BorderWidth { get; set; }

        public Color BorderColor { get; set; }

        public Color BackColor { get; set; }

        public void AddCell(TableCell cell)
        {
            Cells.Add(cell);
        }
    }
}
