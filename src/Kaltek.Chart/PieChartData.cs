using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Kaltek.Drawing;

namespace Kaltek.Chart
{
    public class PieChartData : ChartData
    {
        public PieChartData()
        {
            ExplodeOffset = 10;
        }

        public int LegendWidth { get; set; }

        public int ExplodeOffset { get; set; }
    }
}
