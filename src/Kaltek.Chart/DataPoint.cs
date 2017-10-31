using System;
using System.Collections.Generic;
using System.Text;

namespace Kaltek.Chart
{
    public class DataPoint
    {
        public DataPoint()
        {

        }

        public DataPoint(float value, string title)
        {
            Value = value;
            Title = title;
        }

        public float Value { get; set; }

        public string Title { get; set; }
    }
}
