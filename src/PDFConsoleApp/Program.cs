using System;
using System.Drawing;

using Kaltek.PDF;
using Kaltek.PDF.SkiaSharp;

namespace PDFConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IPdfDocument doc = new PdfDocument
            {
                Author = "Kaltek",
                Creator = "Kaltek",
                Keywords = "Kaltek, PDF, SkiaSharp",
                Subject = "Creating PDF Document",
                Title = "The PDF Document"
            };

            PageDimensions dimensions = new PageDimensions(PaperType.Letter, PageOrientation.Portrait);

            Page page = new Page(dimensions)
            {
                BorderColor = Color.DarkGray,
                BorderWidth = 2
            };

            AddHeaderAndFooter(page);
            AddTestTable(page);
            doc.Pages.Add(page);

            PageDimensions dim2 = new PageDimensions(PaperType.Letter, PageOrientation.Landscape);

            page = new Page(dim2)
            {
                BorderColor = Color.Red,
                BorderWidth = 3
            };

            AddHeaderAndFooter(page);

            AddImage(page);
            doc.Pages.Add(page);

            doc.Save($@"C:\Development\Tests\HelloWorld_{DateTime.Now:yyyyMMdd_hhmm_tt}.pdf");
        }

        private static void AddImage(Page page)
        {
            var image = new Image(@"pexels-photo-254770.jpg")
            {
                Left = 100,
                Top = 200,
                Width = 200,
                Height = 100
            };

            page.PageElements.Add(image);
        }

        private static void AddHeaderAndFooter(Page page)
        {
            Header header = new Header(page)
            {
                LeftText = "Hello",
                CenterText = $"{DateTime.Today:MM/dd/yyyy}",
                RightText = "World"
            };

            Footer footer = new Footer(page)
            {
                LeftText = $"Page 1",
                CenterText = "VER 1.0",
                RightText = $"{DateTime.Today:MM/dd/yyyy}"
            };

            page.PageElements.Add(header);
            page.PageElements.Add(footer);
        }

        private static void AddTestTable(Page page)
        {
            Table table = new Table(page, 10, 100);

            TableCell cell;
            TableRow row;

            for (int i = 0; i < 3; i++)
            {
                row = new TableRow(table);

                if (i == 2)
                {
                    row.BackColor = Color.FromArgb(246, 246, 255);
                }

                cell = new TableCell(row, 123456.78, "{0:#,####.00}", true)
                {
                    Font = new Font
                    {
                        TextSize = 10,
                        FontFamily = Fonts.Arial,
                    },

                    Width = 100,
                    ForeColor = Color.Red,
                    DrawRightBorder = true,

                    TextJustify = TextJustify.Right
                };

                row.AddCell(cell);

                string text = "Hello";

                if (i == 0)
                {
                    text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr,  sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
                }

                cell = new TableCell(row, text, string.Empty, false)
                {
                    Font = new Font
                    {
                        TextSize = 10,
                        FontFamily = Fonts.Arial,
                    }
                };

                if (i == 0)
                {
                    cell.Font.TextSize = 6;
                }

                cell.Width = 200;

                if (i == 2)
                {
                    cell.DrawRightBorder = true;
                    cell.TextJustify = TextJustify.Center;
                }

                row.AddCell(cell);

                cell = new TableCell(row, "World", string.Empty, false)
                {
                    Font = new Font
                    {
                        TextSize = 10,
                        FontFamily = Fonts.Arial,
                    },

                    Width = 50
                };

                if (i == 1)
                {
                    cell.TextJustify = TextJustify.Center;
                }

                row.AddCell(cell);

                row.Width = 350;
                table.Rows.Add(row);
            }

            page.PageElements.Add(table);
        }
    }
}
