using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SkiaSharp;

namespace Kaltek.PDF.SkiaSharp.Renderers
{
    internal class TableRenderer : IRenderer
    {
        private readonly Table _table;
        private readonly int _left;
        private readonly int _top;
        private readonly SKPaint _linePaint;

        private List<RowRenderer> _rowRenderer = new List<RowRenderer>();

        public TableRenderer(Table table)
        {
            _table = table;
            _left = table.Left;
            _top = table.Top;

            _linePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = _table.BorderWidth,
            };

            foreach(var row in table.Rows)
            {
                _rowRenderer.Add(new RowRenderer(row));
            }
        }

        private void UpdateData(SKCanvas canvas)
        {
            foreach (var row in _rowRenderer)
            {
                row.UpdateData(canvas);
                _table.Height += row.Row.Height;

                if (_table.Width < row.Row.Width)
                {
                    _table.Width = row.Row.Width;
                }
            }
        }

        public void Render(SKCanvas canvas)
        {
            UpdateData(canvas);

            int rowTop = _top;
            int rowCount = _table.Rows.Count;

            foreach (var row in _rowRenderer)
            {
                row.Top = rowTop;
                row.Left = _left;

                row.Render(canvas);

                rowTop += row.Row.Height;
            }

            double rowBottom = _top;

            for (int i = 0; i < _table.Rows.Count - 1; i++)
            {
                var row = _table.Rows[i];
                rowBottom += row.Height;

                canvas.DrawLine(_left, _top, _left + row.Width, _top, _linePaint);
            }

            double tableBottom = _top + _table.Height;

            canvas.DrawRect(new SKRect(_left, _top, _left + _table.Width, _top + _table.Height), _linePaint);
        }
    }
}
