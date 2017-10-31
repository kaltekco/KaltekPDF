using System;
using System.Collections.Generic;
using System.Text;

namespace Kaltek.Chart
{
    public class SeriesDataPoint
    {
        public SeriesDataPoint()
        {

        }

        public SeriesDataPoint(float value)
        {
            Value = value;
        }

        public float Value { get; set; }
    }
}
