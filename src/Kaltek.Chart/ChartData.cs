using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Kaltek.Drawing;

namespace Kaltek.Chart
{
    public abstract class ChartData
    {
        public ChartData()
        {
            SeriesCollection = new List<Chart.SeriesCollection>();

            TitleFont = new Font
            {
                FontFamily = Fonts.Arial,
                TextSize = 24,
            };

            TextFont = new Font
            {
                FontFamily = Fonts.Arial,
                TextSize = 12,
            };

            TextColor = Color.Black;

            Padding = new Padding(0, 0, 0, 0);
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public Color[] Colors => ChartColors.PastelColors;

        public Color BackColor { get; set; }

        public string Title { get; set; }

        public Font TitleFont { get; set; }

        public Color TextColor { get; set; }

        public Font TextFont { get; set; }

        public Padding Padding { get; set; }

        public List<SeriesCollection> SeriesCollection { get; set; }
    }
}
