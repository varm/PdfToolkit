using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace PdfPigComponent
{
    public class HtmlProcess
    {
        public static void PdfToHtml(string inputfileName, string outputfileName)
        {
            using (var pdf = PdfDocument.Open(inputfileName))
            {
                var num = 1;
                List<string> strList = [];
                foreach (var page in pdf.GetPages())
                {
                    num++;
                    if (num >= 1)
                    {
                        // Either extract based on order in the underlying document with newlines and spaces.
                        var text = ContentOrderTextExtractor.GetText(page);

                        // Or based on grouping letters into words.
                        var otherText = string.Join("|", page.GetWords());

                        // Or the raw text of the page's content stream.
                        var rawText = page.Text;
                        var dic = page.Dictionary;

                        Console.WriteLine(text);
                        strList.Add(otherText);

                    }
                }
                System.IO.File.WriteAllLines(outputfileName, strList, System.Text.Encoding.UTF8);

            }
        }
    }
}
