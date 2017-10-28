using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;
using SkiaSharp;

using Kaltek.PDF.SkiaSharp.Extensions;

namespace Kaltek.PDF.SkiaSharp.Renderers
{
    internal class RowRenderer : IRenderer
    {
        private List<CellRenderer> _cellRenderers = new List<CellRenderer>();

        public RowRenderer(TableRow row)
        {
            Row = row;

            foreach (var cell in Row.Cells)
            {
                _cellRenderers.Add(new CellRenderer(cell));
            }
        }

        public TableRow Row { get; }

        public int Left { get; set; }

        public int Top { get; set; }

        public void Render(SKCanvas canvas)
        {
            int maxHeight = Row.Cells.Max(m => m.Height);

            var borderPaint = new SKPaint
            {
                Color = Row.BorderColor.ToSKColor(),
                Style = SKPaintStyle.Stroke,
                StrokeWidth = Row.BorderWidth,
            };

            if (Row.BackColor != Color.White)
            {
                var rowBgPaint = new SKPaint
                {
                    Color = Row.BackColor.ToSKColor(),
                    Style = SKPaintStyle.StrokeAndFill,
                    StrokeWidth = 1,
                };
                canvas.DrawRect(new SKRect(Left, Top, Left + Row.Width, Top + maxHeight), rowBgPaint);
            }

            canvas.DrawRect(new SKRect(Left, Top, Left + Row.Width, Top + maxHeight), borderPaint);

            int cellCount = _cellRenderers.Count;
            int i = 0;

            var left = Left;

            foreach (var cell in _cellRenderers)
            {
                cell.Left = left;
                cell.Top = Top;

                cell.Render(canvas);
                left += cell.Cell.Width;

                if (cell.Cell.DrawRightBorder && i++ < cellCount)
                {
                    int right = left;

                    canvas.DrawLine(right, Top, right, Top + maxHeight, borderPaint);
                }
            }
        }

        public void UpdateData(SKCanvas canvas)
        {
            Row.Width = 0;

            foreach (var cell in _cellRenderers)
            {
                cell.UpdateData(canvas);
                Row.Width += cell.Cell.Width;
                if (cell.Cell.Height > Row.Height)
                {
                    Row.Height = cell.Cell.Height;
                }
            }
        }
    }
}
