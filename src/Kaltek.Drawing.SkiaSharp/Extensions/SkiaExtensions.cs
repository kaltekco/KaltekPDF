using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SkiaSharp;

namespace Kaltek.SkiaSharp.Extensions
{
    public static class SkiaExtensions
    {
        public static SKRect ToSKRect(this Rectangle rect)
        {
            return new SKRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static SKColor ToSKColor(this Color color)
        {
            return new SKColor(color.R, color.G, color.B, color.A);
        }

        public static SKSize ToSKSize(this Size size)
        {
            return new SKSize(size.Width, size.Height);
        }
    }
}
