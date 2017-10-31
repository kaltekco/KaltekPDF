using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using SkiaSharp;
using Kaltek.SkiaSharp.Extensions;

namespace Kaltek.Chart.SkiaSharp
{
    public class ColumnChart : ChartBase, IColumnChart
    {
        private readonly ColumnChartData _chartData;

        public ColumnChart(ColumnChartData chartData) : base(chartData)
        {
            _chartData = chartData;
        }

        public override void Render(SKCanvas canvas)
        {
            canvas.Clear(_chartData.BackColor.ToSKColor());

            DrawTitle(canvas);
            DrawLegends(canvas);

            var chartRect = GetChartRect();

            var unitHeight = DrawAxes(canvas, chartRect);

            DrawColumns(canvas, chartRect, unitHeight);
        }

        private void DrawColumns(SKCanvas canvas, SKRect chartRect, float unitHeight)
        {
            float chartAreaLeft = chartRect.Left + _chartData.TextFont.TextSize * 4;
            float chartAreaRight = chartRect.Right - _chartData.TextFont.TextSize;

            float availableWidth = chartAreaRight - chartAreaLeft;

            int numberOfSeries = _chartData.SeriesCollection.Count();
            int numberOfItems = _chartData.SeriesCollection[0].DataPoints.Count();

            int totalWidthOfBars = numberOfSeries * numberOfItems * _chartData.ColumnWidth + (numberOfItems - 1) * _chartData.ColumnSpacing;

            float left = chartAreaLeft + ((chartAreaRight - chartAreaLeft) - totalWidthOfBars) / 2;

            var index = 0;

            foreach (var series in _chartData.SeriesCollection)
            {
                ChartItemFillPaint.Color = _chartData.Colors[index].ToSKColor();

                var item = 0;

                var barLeft = left + index * _chartData.ColumnWidth;

                TextPaint.TextAlign = SKTextAlign.Center;

                foreach(var dataPoint in series.DataPoints)
                {
                    float height = dataPoint.Value * unitHeight;

                    var bottom = chartRect.Bottom;
                    var top = bottom - height;
                    var r = barLeft + _chartData.ColumnWidth;
                    var rect = new SKRect(barLeft, top, r, bottom);

                    canvas.DrawRect(rect, ChartItemFillPaint);
                    canvas.DrawRect(rect, ChartItemOutlinePaint);

                    if (index == 0)
                    {
                        float textWidth = TextPaint.MeasureText(dataPoint.Title);
                        var textLeft = barLeft + (_chartData.ColumnWidth * numberOfSeries) / 2;
                        var textBottom = bottom + TextPaint.TextSize * 2;

                        canvas.DrawText(dataPoint.Title, textLeft, textBottom, TextPaint);
                    }

                    barLeft += _chartData.ColumnWidth * numberOfSeries + _chartData.ColumnSpacing;
                    item++;
                }

                index++;
            }
        }

        private SKRect GetChartRect()
        {
            int x = _chartData.Padding.Left;
            int y = _chartData.Padding.Top * 2 + _chartData.TitleFont.TextSize;

            int possibleWidth = (int)(_chartData.Width - x - _chartData.Padding.Right);

            int possibleHeight = (int)(_chartData.Height - y - _chartData.Height * 0.2f);

            return new SKRect(x, y, x + possibleWidth, y + possibleHeight);
        }

        private void DrawLegends(SKCanvas canvas)
        {
            int x = _chartData.Padding.Left;

            float legendsTop = _chartData.Height * 0.8f;

            var height = _chartData.Height - legendsTop - _chartData.Padding.Bottom;

            var y = legendsTop + (height + _chartData.TextFont.TextSize) / 2;

            int index = 0;

            var legendRect = new SKRect(0, 0, _chartData.TextFont.TextSize, _chartData.TextFont.TextSize);
            var textLocation = new SKPoint(x + _chartData.TextFont.TextSize * 2, y + _chartData.TextFont.TextSize);

            foreach (var item in _chartData.SeriesCollection)
            {
                LegendBoxPaint.Color = _chartData.Colors[index++].ToSKColor();

                legendRect = new SKRect(x, textLocation.Y - _chartData.TextFont.TextSize, x + _chartData.TextFont.TextSize, textLocation.Y);

                canvas.DrawRect(legendRect, LegendBoxPaint);
                canvas.DrawRect(legendRect, LegendBoxBorderPaint);

                canvas.DrawText(item.Title, textLocation.X, textLocation.Y, TextPaint);

                var offset = TextPaint.MeasureText(item.Title) + 3 * _chartData.TextFont.TextSize;
                x += (int)offset;

                textLocation.X += offset;
            }
        }

        private float DrawAxes(SKCanvas canvas, SKRect chartRect)
        {
            float maxValue = 0;

            foreach (var series in _chartData.SeriesCollection)
            {
                var seriesMax = series.DataPoints.Max(d => d.Value);
                maxValue = Math.Max(maxValue, seriesMax);
            }

            int highest = (int)Math.Round(maxValue / 10.0, MidpointRounding.AwayFromZero) * 10;

            int nextHighest = highest;

            int height = (int)chartRect.Height;

            int markerValue = nextHighest / 4;
            float markerOffset = height / 4;

            float x = chartRect.Left;
            float y = chartRect.Top + _chartData.TextFont.TextSize;

            var axesPaint = new SKPaint
            {
                Color = SKColors.Black,
                StrokeWidth = 1,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true,
            };

            var linePaint = new SKPaint
            {
                Color = SKColors.LightGray,
                StrokeWidth = 1,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true,
            };


            List<string> texts = new List<string>();

            float maxWidth = BuildAxesLabels(nextHighest, markerValue, texts);

            canvas.Save();
            canvas.Translate(maxWidth, 0);
            TextPaint.TextAlign = SKTextAlign.Right;

            for (int i = 0; i < 5; i++)
            {
                y = chartRect.Top + i * markerOffset;
                canvas.DrawText(texts[i], x, y + _chartData.TextFont.TextSize / 2, TextPaint);

                if (i != 4)
                {
                    canvas.DrawLine(x + _chartData.TextFont.TextSize, y, chartRect.Right, y, linePaint);
                }
            }

            SKPath path = new SKPath();
            path.MoveTo(x + _chartData.TextFont.TextSize, chartRect.Top);
            path.LineTo(x + _chartData.TextFont.TextSize, chartRect.Bottom);
            path.LineTo(chartRect.Right - maxWidth, chartRect.Bottom);

            canvas.DrawPath(path, axesPaint);

            canvas.Restore();

            float unitHeight = height / (float)highest;

            return unitHeight;
        }

        private float BuildAxesLabels(int highest, int markerValue, List<string> texts)
        {
            float maxWidth = 0;

            for (int i = 0; i < 5; i++)
            {
                string text = string.Format(_chartData.SeriesCollection[0].ValueDisplayFormat, highest);

                maxWidth = Math.Max(TextPaint.MeasureText(text), maxWidth);

                highest = highest - markerValue;

                texts.Add(text);
            }

            return maxWidth;
        }
    }
}
