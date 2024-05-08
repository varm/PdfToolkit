using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;

namespace SpirePDF
{
    public class DocProcess
    {
        #region Generate PDF
        static void GeneratePdf()
        {
            PdfDocument doc = new PdfDocument();
            for (int i = 0; i < 4; i++)
            {
                PdfImage im = PdfImage.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Assets", (i + 1).ToString() + ".jpg"));
                float width = im.Width;
                float height = im.Height;
                PdfPageBase page = doc.Pages.Add(new SizeF(width, height), new PdfMargins(0, 0, 0, 0));
                page.Canvas.DrawImage(im, 0, 0, width, height);
            }

            PdfImage im2 = PdfImage.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Assets", "00.jpg"));
            float width2 = im2.Width;
            float height2 = im2.Height;
            PdfPageBase page2 = doc.Pages.Add(new SizeF(width2, height2), new PdfMargins(0, 0, 0, 0));
            page2.Canvas.DrawImage(im2, 0, 0, width2, height2);

            doc = AddTextContent(doc);

            //Save pdf file.
            doc.SaveToFile("PdfResult.pdf");
            doc.Close();
            System.Diagnostics.Process.Start("PdfResult.pdf");
        }

        static PdfDocument AddTextContent(PdfDocument doc)
        {
            //Add page
            PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, new PdfMargins(35f));

            //Specify title text
            String titleText = "Do not go gentle into that good night";

            //Create a brush
            PdfSolidBrush titleBrush = new PdfSolidBrush(new PdfRGBColor(Color.Blue));
            PdfSolidBrush paraBrush = new PdfSolidBrush(new PdfRGBColor(Color.Black));

            //Create true type font
            PdfTrueTypeFont titleFont = new PdfTrueTypeFont(new System.Drawing.Font("Courier", 18f, FontStyle.Bold), true);
            PdfTrueTypeFont paraFont = new PdfTrueTypeFont(new System.Drawing.Font("Courier", 12f, FontStyle.Regular), true);

            //Text alignment
            PdfStringFormat format = new PdfStringFormat();
            format.Alignment = PdfTextAlignment.Center;

            //Draw the title in the center of the page
            page.Canvas.DrawString(titleText, titleFont, titleBrush, page.Canvas.ClientSize.Width / 2, 20, format);

            //Gets the paragraph content from the.txt file
            string paraText = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Assets", "test.txt"));

            //Create a PdfTextWidget object to hold the paragraph content
            PdfTextWidget widget = new PdfTextWidget(paraText, paraFont, paraBrush);

            //Creates a rectangle to hold the content of the paragraph
            RectangleF rect = new RectangleF(0, 50, page.Canvas.ClientSize.Width, page.Canvas.ClientSize.Height);

            //Set PdfLayoutType to 'Paginate' to paginate the content automatically
            PdfTextLayout layout = new PdfTextLayout();
            layout.Layout = PdfLayoutType.Paginate;

            //Draws the widget on the page
            widget.Draw(page, rect, layout);
            return doc;
        }

        #endregion

        public static void PdfToHtml(string inputfileName, string outputfileName)
        {
            //Create a PdfDocument instance
            PdfDocument pdf = new PdfDocument();
            //Load a PDF document
            pdf.LoadFromFile(inputfileName);

            //Save the PDF document as HTML
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            pdf.SaveToFile(outputfileName, Spire.Pdf.FileFormat.HTML);
            pdf.Close();

        }

    }
}
