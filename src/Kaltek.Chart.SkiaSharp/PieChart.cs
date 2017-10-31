using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SkiaSharp;

using Kaltek.SkiaSharp.Extensions;

namespace Kaltek.Chart.SkiaSharp
{
    public class PieChart : ChartBase, IPieChart
    {
        private readonly PieChartData _chartData;

        public PieChart(PieChartData chartData) : base(chartData)
        {
            _chartData = chartData;
        }
 
        public override void Render(SKCanvas canvas)
        {
            if (!_chartData.SeriesCollection.Any())
            {
                return;
            }

            canvas.Clear(_chartData.BackColor.ToSKColor());
            var datapoints = _chartData.SeriesCollection[0].DataPoints;

            DrawTitle(canvas);
            DrawLegends(canvas, datapoints);

            float totalValues = datapoints.Sum(d => d.Value);

            var chartSize = GetChartRect();

            SKPoint center = new SKPoint(0, 0);
            float explodeOffset = 10;
            float radius = Math.Min(chartSize.Width / 2, chartSize.Height / 2) - 2 * _chartData.ExplodeOffset;
            SKRect rect = new SKRect(center.X - radius, center.Y - radius,
                                     center.X + radius, center.Y + radius);

            float startAngle = 0;
            int colorIndex = 0;

            var actualCenter = new SKPoint(chartSize.Left + chartSize.Width / 2, chartSize.Top + chartSize.Height / 2);

            canvas.Save();
            canvas.Translate(actualCenter.X, actualCenter.Y);

            foreach (var item in datapoints)
            {
                float sweepAngle = 360f * item.Value / totalValues;

                using (SKPath path = new SKPath())
                {
                    path.MoveTo(center);
                    path.ArcTo(rect, startAngle, sweepAngle, false);
                    path.Close();

                    ChartItemFillPaint.Color = _chartData.Colors[colorIndex++].ToSKColor();

                    // Calculate "explode" transform
                    float angle = startAngle + 0.5f * sweepAngle;
                    float x = explodeOffset * (float)Math.Cos(Math.PI * angle / 180);
                    float y = explodeOffset * (float)Math.Sin(Math.PI * angle / 180);

                    canvas.Save();
                    canvas.Translate(x, y);

                    // Fill and stroke the path
                    canvas.DrawPath(path, ChartItemFillPaint);
                    canvas.DrawPath(path, ChartItemOutlinePaint);
                    canvas.Restore();
                }

                startAngle += sweepAngle;
            }

            canvas.Restore();
        }

        private void DrawLegends(SKCanvas canvas, List<DataPoint> datapoints)
        {
            int x = (int)(_chartData.Width * 0.7f);
            int y = _chartData.Padding.Top * 2 + _chartData.TitleFont.TextSize;

            int index = 0;

            var legendRect = new SKRect(0, 0, _chartData.TextFont.TextSize, _chartData.TextFont.TextSize);
            var textLocation = new SKPoint(x + _chartData.TextFont.TextSize * 2, y + _chartData.TextFont.TextSize);

            foreach (var item in datapoints)
            {
                LegendBoxPaint.Color = _chartData.Colors[index++].ToSKColor();

                legendRect = new SKRect(x, textLocation.Y - _chartData.TextFont.TextSize, x + _chartData.TextFont.TextSize, textLocation.Y);

                canvas.DrawRect(legendRect, LegendBoxPaint);
                canvas.DrawRect(legendRect, LegendBoxBorderPaint);

                var text = string.Format(_chartData.SeriesCollection[0].ValueDisplayFormat, item.Value);
                canvas.DrawText($"{item.Title} ({text})", textLocation.X, textLocation.Y, TextPaint);

                var offset = (int)(_chartData.TextFont.TextSize * 1.5f);
                y += offset;

                textLocation.Y += offset;
            }
        }

        private SKRect GetChartRect()
        {
            int x = _chartData.Padding.Left;
            int y = _chartData.Padding.Top * 2 + _chartData.TitleFont.TextSize;

            int possibleHeight = _chartData.Height - y - _chartData.Padding.Bottom;

            int possibleWidth = (int)(_chartData.Width - x - _chartData.Width * 0.3f);

            int chartSize = possibleHeight < possibleWidth ? possibleHeight : possibleWidth;

            return new SKRect(x, y, x + chartSize, y + chartSize);
        }
    }
}
