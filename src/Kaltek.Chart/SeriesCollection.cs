using System.Collections.Generic;

namespace Kaltek.Chart
{
    public class SeriesCollection
    {
        public SeriesCollection()
        {
            DataPoints = new List<DataPoint>();
        }

        public List<DataPoint> DataPoints { get; set; }

        public string Title { get; set; }

        public string ValueDisplayFormat { get; set; }
    }
}
