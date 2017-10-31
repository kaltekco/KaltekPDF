using System;
using System.Collections.Generic;
using System.Text;

namespace Kaltek.Chart
{
    public class ColumnChartData : ChartData
    {
        public ColumnChartData()
        {
            ColumnWidth = 10;
            ColumnSpacing = 10;
        }

        public int ColumnWidth { get; set; }

        public int ColumnSpacing { get; set; }
    }
}
