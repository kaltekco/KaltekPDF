using System.Collections.Generic;
using System.Text;
using SkiaSharp;

using Kaltek.PDF;
using Kaltek.SkiaSharp.Extensions;
using System;

namespace Kaltek.PDF.SkiaSharp.Renderers
{
    internal class PageRenderer : IRenderer
    {
        private readonly PdfPage _page;

        public PageRenderer(PdfPage page)
        {
            _page = page;
        }

        public void Render(SKCanvas canvas)
        {
            if ((_page == null) || (canvas == null))
            {
                return;
            }

            var bounds = _page.Dimensions.GetDrawingBounds().ToSKRect();

            if (_page.BorderWidth > 0)
            {
                SKPaint borderPaint = new SKPaint
                {
                    Color = _page.BorderColor.ToSKColor(),
                    StrokeWidth = _page.BorderWidth,
                    Style = SKPaintStyle.Stroke,
                };

                canvas.DrawRect(bounds, borderPaint);
            }

            foreach (var control in _page.PageElements)
            {
                switch (control)
                {
                    case PdfHeader header:
                         RenderHeader(canvas, bounds, header);
                        break;
                    case PdfFooter footer:
                        RenderFooter(canvas, bounds, footer);
                        break;
                    case PdfTable table:
                        RenderTable(canvas, table);
                        break;
                    case PdfImage image:
                        RenderImage(canvas, image);
                        break;
                    default:
                        break;
                }
            }
        }

        private void RenderImage(SKCanvas canvas, PdfImage image)
        {
            var renderer = new ImageRenderer(image);
            renderer.Render(canvas);
        }

        private void RenderTable(SKCanvas canvas, PdfTable table)
        {
            var renderer = new TableRenderer(table);
            renderer.Render(canvas);
        }

        private static void RenderHeader(SKCanvas canvas, SKRect bounds, PdfHeader header)
        {
            int headerHeight = header.Height;
            var headerRect = new SKRect(bounds.Left, bounds.Top, bounds.Right, bounds.Top + headerHeight);
            var renderer = new HeaderFooterRenderer(header, headerRect);
            renderer.Render(canvas);
        }

        private static void RenderFooter(SKCanvas canvas, SKRect bounds, PdfFooter footer)
        {
            int headerHeight = footer.Height;
            var headerRect = new SKRect(bounds.Left, bounds.Bottom - headerHeight, bounds.Right, bounds.Bottom);
            var renderer = new HeaderFooterRenderer(footer, headerRect);
            renderer.Render(canvas);
        }
    }
}
