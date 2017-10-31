using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SkiaSharp;

using Kaltek.SkiaSharp.Extensions;

namespace Kaltek.Chart.SkiaSharp
{
    public abstract class ChartBase : IChart
    {
        private readonly ChartData _chartData;

        public ChartBase(ChartData chartData)
        {
            _chartData = chartData;

            TextPaint = new SKPaint
            {
                IsAntialias = true,
                TextAlign = SKTextAlign.Left,
                TextSize = _chartData.TextFont.TextSize,
                Color = _chartData.TextColor.ToSKColor(),
            };

            LegendBoxBorderPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                Color = SKColors.Black,
            };

            LegendBoxPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
            };

            TitlePaint = new SKPaint
            {
                Color = _chartData.TextColor.ToSKColor(),
                IsAntialias = true,
                TextAlign = SKTextAlign.Center,
                FakeBoldText = true,
                TextSize = _chartData.TitleFont.TextSize,
                Typeface = SKTypeface.FromFamilyName(_chartData.TitleFont.FontFamily),
            };

            ChartItemFillPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            ChartItemOutlinePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                IsAntialias = true,
                Color = SKColors.Black
            };
        }

        public void Save(string path)
        {
            File.WriteAllBytes(path, Save());
        }

        public byte[] Save()
        {
            using (var surface = SKSurface.Create(_chartData.Width, _chartData.Height, SKImageInfo.PlatformColorType, SKAlphaType.Premul))
            {
                SKCanvas canvas = surface.Canvas;

                Render(canvas);

                return surface.Snapshot().Encode().ToArray();
            }
        }

        public abstract void Render(SKCanvas canvas);

        public SKPaint TextPaint { get; }
        public SKPaint LegendBoxBorderPaint { get; }
        public SKPaint LegendBoxPaint { get; }
        public SKPaint TitlePaint { get; }
        public SKPaint ChartItemFillPaint { get; }
        public SKPaint ChartItemOutlinePaint { get; }

        public void DrawTitle(SKCanvas canvas)
        {
            int x = _chartData.Width / 2;
            int y = _chartData.Padding.Top + _chartData.TitleFont.TextSize;

            canvas.DrawText(_chartData.Title, x, y, TitlePaint);
        }

        public void Dispose()
        {
            TextPaint?.Dispose();
            LegendBoxBorderPaint?.Dispose();
            LegendBoxPaint?.Dispose();
            TitlePaint?.Dispose();
            ChartItemFillPaint?.Dispose();
            ChartItemOutlinePaint?.Dispose();
        }
    }
}
