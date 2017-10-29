using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kaltek.Drawing;

namespace Kaltek.PDF
{
    public class SummaryColumnDefinition
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string Text { get; set; }
        public TextJustify TextJustify { get; set; }
        public Font Font { get; set; }
        public double FontSize { get; set; }
    }
}
