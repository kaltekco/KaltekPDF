using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace Kaltek.PDF.SkiaSharp.Renderers
{
    internal class ImageRenderer : IRenderer
    {
        private readonly PdfImage _image;

        public ImageRenderer(PdfImage image)
        {
            _image = image;
        }

        public void Render(SKCanvas canvas)
        {
            var bitmap = SKBitmap.Decode(_image.Data);
            SKImage image = SKImage.FromBitmap(bitmap);
            
            var sourceRect = new SKRect(0, 0, image.Width, image.Height);

            SKRect destinationRect = GetDestinationRect(image.Width, image.Height);

            canvas.DrawImage(image, sourceRect, destinationRect);
        }

        private SKRect GetDestinationRect(int width, int height)
        {
            double ratioX = (double)_image.Width / (double)width;
            double ratioY = (double)_image.Height / (double)height;

            double ratio = ratioX < ratioY ? ratioX : ratioY;

            int newHeight = Convert.ToInt32(height * ratio);
            int newWidth = Convert.ToInt32(width * ratio);

            return new SKRect(_image.Left, _image.Top, _image.Left + newWidth, _image.Top + newHeight);
        }
    }
}
