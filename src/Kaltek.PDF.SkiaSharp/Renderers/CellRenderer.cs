using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

using Kaltek.Drawing;
using Kaltek.SkiaSharp.Extensions;

namespace Kaltek.PDF.SkiaSharp.Renderers
{
    internal class CellRenderer : IRenderer
    {
        private class CellItem
        {
            public string Text { get; set; }
            public float OffsetX { get; set; }
            public float OffsetY { get; set; }
        }

        private int _lineHeight;
        private List<CellItem> _cellItems = new List<CellItem>();
        private SKPaint _textPaint;

        public CellRenderer(PdfTableCell cell)
        {
            Cell = cell;
            
            _textPaint = new SKPaint
            {
                Color = Cell.ForeColor.ToSKColor(),
                TextSize = Cell.Font.TextSize,
                IsAntialias = true,
                Typeface = SKTypeface.FromFamilyName(Cell.Font.FontFamily),
            };
        }

        public PdfTableCell Cell { get; }

        public int Left { get; set; }

        public int Top { get; set; }

        public void Render(SKCanvas canvas)
        {
            foreach (var item in _cellItems)
            {
                var pos = new SKPoint(Left + item.OffsetX, Top + Cell.Padding.Top + item.OffsetY + (_lineHeight / 2));

                _textPaint.TextAlign = GetAlignment(Cell.TextJustify);
                canvas.DrawText(item.Text, pos.X, pos.Y, _textPaint);
            }
        }

        private SKTextAlign GetAlignment(TextJustify textJustify)
        {
            switch (textJustify)
            {
                case TextJustify.Center:
                    return SKTextAlign.Center;
                case TextJustify.Left:
                    return SKTextAlign.Left;
                case TextJustify.Right:
                    return SKTextAlign.Right;
                default:
                    return SKTextAlign.Left;
            }
        }

        public void UpdateData(SKCanvas canvas)
        {
            _lineHeight = Cell.Font.TextSize;

            if (Cell.Data == null || Cell.Data.GetType() == typeof(string))
            {
                string dataString = string.Empty;

                if (Cell.Data != null)
                {
                    dataString = Cell.Data.ToString();
                }

                AddCellItem(dataString);
            }
            else if (Cell.Data.GetType() == typeof(DateTime))
            {
                string dataString = Cell.Data.ToString();
                if (!string.IsNullOrEmpty(Cell.DataFormat))
                {
                    dataString = string.Format(Cell.DataFormat, Cell.Data);
                }

                AddCellItem(dataString);
            }
            else
            {
                AddNumericValueCellItem(Cell.Data, Cell.DataFormat, Cell.SuppressIfZero);
            }

            Cell.Height = _cellItems.Count * _lineHeight + Cell.Padding.Top + Cell.Padding.Bottom;
        }

        private void AddCellItem(string columnText)
        {
            if (string.IsNullOrEmpty(columnText))
            {
                columnText = string.Empty;
            }
            string[] lines = columnText.Split('\n');

            float xPosition = GetXPosition();
            float yPosition = Cell.Padding.Top;

            foreach (string line in lines)
            {
                int numberOfLines = AddLine(line, xPosition, yPosition);
                yPosition += (numberOfLines * _lineHeight);
            }
        }

        private int AddLine(string line, float xPosition, float yPosition)
        {
            double textWidth = _textPaint.MeasureText(line);

            int numberOfLines = 0;
            double allowedWidth = Cell.Width - (Cell.Padding.Left + Cell.Padding.Right);

            if (textWidth > allowedWidth)
            {
                string[] words = line.Split(' ');
                StringBuilder sb = new StringBuilder();

                for (int k = 0; k < words.Length; k++)
                {
                    string testStr = string.Format("{0} {1}", sb.ToString(), words[k]).Trim();
                    double widthToTest = _textPaint.MeasureText(testStr); 

                    if (widthToTest <= allowedWidth)
                    {
                        sb.Append(words[k]);
                        sb.Append(" ");
                    }
                    else
                    {
                        _cellItems.Add(new CellItem
                        {
                            OffsetX = xPosition,
                            OffsetY = yPosition,
                            Text = sb.ToString().TrimEnd(),

                        });
                        numberOfLines++;
                        yPosition += _lineHeight;
                        sb.Clear();
                        sb.Append(words[k]);
                        sb.Append(" ");
                    }
                }

                if (sb.Length > 0)
                {
                    _cellItems.Add(new CellItem
                    {
                        OffsetX = xPosition,
                        OffsetY = yPosition,
                        Text = sb.ToString().TrimEnd(),
                    });
                    numberOfLines++;
                    yPosition += _lineHeight;
                    sb.Clear();
                }
            }
            else
            {
                _cellItems.Add(new CellItem
                {
                    OffsetX = xPosition,
                    OffsetY = yPosition,
                    Text = line,
                });
                numberOfLines++;
            }

            return numberOfLines;
        }

        private void AddNumericValueCellItem(object value, string format, bool suppressIfZero)
        {
            float xPosition = GetXPosition();
            float yPosition = Cell.Padding.Top;

            //PointD location = GetPdfLocation(posX, positionY);
            bool skipValue = false;
            if (null != value)
            {
                string valueString = string.Empty;
                string valueFormat = "{0}";
                if (suppressIfZero)
                {
                    string valTest = value.ToString();
                    valTest = valTest.Replace(".", "");
                    valTest = valTest.Replace("0", "");
                    if (string.IsNullOrEmpty(valTest))
                    {
                        skipValue = true;
                    }
                }

                if (!skipValue)
                {
                    if (!string.IsNullOrEmpty(format))
                    {
                        valueFormat = format;
                    }
                    valueString = string.Format(valueFormat, value);
                    _cellItems.Add(new CellItem
                    {
                        OffsetX = xPosition,
                        OffsetY = yPosition,
                        Text = valueString,
                    });
                }
            }
        }

        private float GetXPosition()
        {
            float position = 0;

            switch (Cell.TextJustify)
            {
                case TextJustify.Right:
                    position = Cell.Width - Cell.Padding.Right;
                    break;
                case TextJustify.Center:
                    position = Cell.Width / 2;
                    break;
                default:
                    position = Cell.Padding.Left;
                    break;
            }

            return position;
        }
    }
}