using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kaltek.Drawing;

namespace Kaltek.PDF
{
    public class ColumnDefinition
    {
        public double Width { get; set; }

        public TextJustify TextJustify { get; set; }

        public string Title { get; set; }

        public string DataFieldName { get; set; }

        public string DataFieldFormatString { get; set; }

        public bool SuppressZero { get; set; }

        public bool AddTotalToFooter { get; set; }

        public string FooterFormatString { get; set; }

        public string FooterText { get; set; }

        public bool SkipFooterBorder { get; set; }
    }
}
