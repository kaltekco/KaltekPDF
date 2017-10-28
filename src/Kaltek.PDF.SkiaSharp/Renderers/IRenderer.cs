using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace Kaltek.PDF.SkiaSharp.Renderers
{
    public interface IRenderer
    {
        void Render(SKCanvas canvas);
    }
}
