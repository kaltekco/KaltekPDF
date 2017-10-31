using System;
using System.Collections.Generic;
using System.Text;

namespace Kaltek.Chart
{
    public interface IChartFactory
    {
        IPieChart CreatePieChart(PieChartData data);

        IColumnChart CreateColumnChart(ColumnChartData data);
    }
}
