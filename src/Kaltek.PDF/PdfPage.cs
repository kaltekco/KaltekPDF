using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Kaltek.PDF
{
    public class PdfPage
    {
        private readonly PageDimensions _pageDimensions;

        public PdfPage(PageDimensions pageDimensions)
        {
            PageElements = new List<IPageElement>();
            _pageDimensions = pageDimensions;
        }

        public PageDimensions Dimensions => _pageDimensions;

        public float Width => _pageDimensions.Width;

        public float Height => _pageDimensions.Height;

        public int BorderWidth { get; set; }

        public Color BorderColor { get; set; }

        public List<IPageElement> PageElements { get; set; }
    }
}
