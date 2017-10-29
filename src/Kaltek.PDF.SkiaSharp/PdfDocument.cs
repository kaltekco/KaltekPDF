using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using SkiaSharp;

using Kaltek.PDF.SkiaSharp.Extensions;
using Kaltek.PDF.SkiaSharp.Renderers;

namespace Kaltek.PDF.SkiaSharp
{
    public class PdfDocument : IPdfDocument
    {
        public PdfDocument()
        {
            Pages = new List<PdfPage>();
            Author = "Kaltek";
            Creator = Author;
            Keywords = "Kaltek, PDF, SkiaSharp";
            Subject = "Creating PDF Document";
            Title = "The PDF Document";
        }

        public List<PdfPage> Pages { get; set; }

        public string Author { get; set; }

        public string Creator { get; set; }

        public string Keywords { get; set; }

        public string Producer { get; set; }

        public string Subject { get; set; }

        public string Title { get; set; }

        public void Save(string path)
        {
            using (var stream = new SKFileWStream(path))
            {
                SaveToStream(stream);
            }
        }

        public byte[] Save()
        {
            using (var strm = new SKDynamicMemoryWStream())
            {
                SaveToStream(strm);
                var data = strm.CopyToData();
                return data.ToArray();                
            }
        }

        private void SaveToStream(SKWStream stream)
        {
            var metadata = new SKDocumentPdfMetadata
            {
                Author = Author,
                Creation = DateTime.Now,
                Creator = Creator,
                Keywords = Keywords,
                Modified = DateTime.Now,
                Producer = Producer,
                Subject = Subject,
                Title = Title,
            };

            int dpi = 72;

            using (var document = SKDocument.CreatePdf(stream, metadata, dpi))
            {
                foreach (var page in Pages)
                {
                    var pdfCanvas = document.BeginPage(page.Width, page.Height, page.Dimensions.GetContentRect().ToSKRect());

                    var pageRenderer = new PageRenderer(page);
                    pageRenderer.Render(pdfCanvas);

                    document.EndPage();
                }

                // end the doc
                document.Close();
            }
        }
    }
}
