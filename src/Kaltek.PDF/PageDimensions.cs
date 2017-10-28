using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Kaltek.PDF
{
    public enum UnitOfMeasure
    {
        Point,
        Inch,
        cm,
        mm,
    }

    public enum PaperType
    {
        Letter,
        Legal,
        A4
    }

    public enum PageOrientation
    {
        Portrait,
        Landscape,
    }

    public static class PaperTypeSize
    {
        public static Size GetSize(PaperType paperType, int dpi = 72)
        {
            switch (paperType)
            {
                case PaperType.Legal:
                    return new Size((int)(8.5 * dpi), 14 * dpi);
                case PaperType.A4:
                    return new Size((int)(21.0f * 72 / 2.54f), (int)(29.7f * 72 / 2.54f));
                default:
                    return new Size((int)(8.5 * dpi), 11 * dpi);
            }
        }
    }

    public class PageDimensions
    {
        private readonly PaperType _paperType;
        private readonly PageOrientation _orientation;
        private readonly int _dpi;
        private static float[] _unitInPoints = null;

        private Size _size;

        private int _marginTop;
        private int _marginBottom;
        private int _marginLeft;
        private int _marginRight;

        public PageDimensions(PaperType paperType, PageOrientation orientation, int dpi = 72)
        {
            _paperType = paperType;
            _orientation = orientation;
            _dpi = dpi;

            _unitInPoints = new float[]
            {
                1.0f,			// Point
		        dpi,			// Inch
		        dpi / 2.54f,	// cm
		        dpi / 25.4f,    // mm
            };

            _size = PaperTypeSize.GetSize(_paperType, _dpi);

            if (_orientation == PageOrientation.Landscape)
            {
                _size = new Size(_size.Height, _size.Width);
            }

            _marginTop = (int)ConvertUnits(0.15f, UnitOfMeasure.Inch, UnitOfMeasure.Point);
            _marginBottom = _marginTop;

            _marginLeft = (int)ConvertUnits(0.5f, UnitOfMeasure.Inch, UnitOfMeasure.Point);
            _marginRight = _marginLeft;
        }

        public int Width => _size.Width;

        public int Height => _size.Height;

        public int MarginTop => _marginTop;

        public int MarginBottom => _marginBottom;

        public int MarginLeft => _marginLeft;

        public int MarginRight => _marginRight;

        public float ConvertUnits(float value, UnitOfMeasure source, UnitOfMeasure destination)
        {
            if (source == destination)
            {
                return value;
            }

            float sourceFactor = _unitInPoints[(int)source];
            float destinationFactor = _unitInPoints[(int)destination];

            return value * sourceFactor / destinationFactor;
        }

        public Rectangle GetContentRect()
        {
            var left = _marginLeft;
            var width = _size.Width - (MarginLeft + MarginRight);
            var top = _marginTop;
            var height = _size.Height - (MarginTop + MarginBottom);

            return new Rectangle(left, top, width, height);
        }

        public Rectangle GetDrawingBounds()
        {
            // Letter: Content dimension: 540 X 772. Drawing bounds (0, 0, 540, 772)

            var contentRect = GetContentRect();

            contentRect.X = 0;
            contentRect.Y = 0;

            return contentRect;
        }
    }
}
