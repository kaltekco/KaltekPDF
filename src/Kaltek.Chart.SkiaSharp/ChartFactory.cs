using System;
using System.Collections.Generic;
using System.Text;

namespace Kaltek.Chart.SkiaSharp
{
    public class ChartFactory : IChartFactory
    {
        public IPieChart CreatePieChart(PieChartData data)
        {
            return new PieChart(data);
        }

        public IColumnChart CreateColumnChart(ColumnChartData data)
        {
            return new ColumnChart(data);
        }
    }
}
