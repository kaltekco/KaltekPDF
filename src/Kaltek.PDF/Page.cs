using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Kaltek.PDF
{
    public class Page
    {
        private readonly PageDimensions _pageDimensions;

        public Page(PageDimensions pageDimensions)
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
