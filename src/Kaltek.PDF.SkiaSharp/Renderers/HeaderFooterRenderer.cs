using System;
using SkiaSharp;

using Kaltek.PDF;
using Kaltek.PDF.SkiaSharp.Extensions;

namespace Kaltek.PDF.SkiaSharp.Renderers
{
    public class HeaderFooterRenderer
    {
        private readonly PdfHeaderFooterBase _header;
        private readonly SKRect _bounds;

        public HeaderFooterRenderer(PdfHeaderFooterBase header, SKRect bounds)
        {
            _header = header;
            _bounds = bounds;
        }

        public void Render(SKCanvas canvas)
        {
            var paint = new SKPaint
            {
                Color = _header.Color.ToSKColor(),
                TextSize = _header.Font.TextSize,
                IsAntialias = true,
                Typeface = SKTypeface.FromFamilyName(_header.Font.FontFamily),
            };

            if (_header.BorderWidth > 0)
            {
                SKPaint borderPaint = new SKPaint
                {
                    Color = SKColors.FloralWhite,
                    StrokeWidth = 2,
                    Style = SKPaintStyle.Fill,
                };

                canvas.DrawRect(_bounds, borderPaint);
            }

            RenderLeftText(paint, canvas);
            RenderCenterText(paint, canvas);
            RenderRightText(paint, canvas);
        }

        private void RenderLeftText(SKPaint paint, SKCanvas canvas)
        {
            if (String.IsNullOrEmpty(_header.LeftText))
            {
                return;
            }

            float textSize = paint.MeasureText(_header.LeftText);

            var rect = GetRect(_bounds, 0);
            float left = rect.Left;

            float top = rect.Top + (rect.Height) / 2;
            canvas.DrawText(_header.LeftText, left + _header.Font.TextSize, top, paint);
        }

        private void RenderCenterText(SKPaint paint, SKCanvas canvas)
        {
            if (String.IsNullOrEmpty(_header.LeftText))
            {
                return;
            }

            float textSize = paint.MeasureText(_header.CenterText);

            var rect = GetRect(_bounds, 1);
            float left = _bounds.Width / 2;

            float top = rect.Top + (rect.Height) / 2;

            paint.TextAlign = SKTextAlign.Center;

            canvas.DrawText(_header.CenterText, left, top, paint);
        }

        private void RenderRightText(SKPaint paint, SKCanvas canvas)
        {
            if (String.IsNullOrEmpty(_header.LeftText))
            {
                return;
            }

            float textSize = paint.MeasureText(_header.RightText);

            var rect = GetRect(_bounds, 2);
            float left = rect.Right - textSize;

            float top = rect.Top + (rect.Height) / 2;

            paint.TextAlign = SKTextAlign.Right;
            canvas.DrawText(_header.RightText, rect.Right - _header.Font.TextSize, top, paint);
        }

        private static SKRect GetRect(SKRect bounds, int index)
        {
            float width = bounds.Width / 3;
            float left = bounds.Left + index * width;

            return new SKRect(
                left,
                bounds.Top,
                left + width,
                bounds.Bottom
                );
        }
    }
}
