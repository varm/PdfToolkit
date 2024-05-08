using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AsposeComponent
{
    public class PdfProcess
    {
        public static void PdfToExcel(string inputfileName, string outputfileName)
        {
            Document pdfDocument = new Document(inputfileName);
            ExcelSaveOptions options = new ExcelSaveOptions();
            options.Format = ExcelSaveOptions.ExcelFormat.XLSX;
            options.MinimizeTheNumberOfWorksheets = true;
            pdfDocument.Save(outputfileName, options);
        }

        public static void PdfToHtml(string inputfileName, string outputfileName)
        {
            // Open the source PDF document
            Document pdfDocument = new Document(inputfileName);

            // Save the file into MS document format
            pdfDocument.Save(outputfileName, SaveFormat.Html);
        }

        public static List<string> ExtractTable(string inputfileName)
        {
            List<string> strList = [];
            // Load source PDF document
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputfileName);
            foreach (var page in pdfDocument.Pages)
            {
                Aspose.Pdf.Text.TableAbsorber absorber = new Aspose.Pdf.Text.TableAbsorber();
                absorber.Visit(page);
                foreach (AbsorbedTable table in absorber.TableList)
                {
                    foreach (AbsorbedRow row in table.RowList)
                    {
                        foreach (AbsorbedCell cell in row.CellList)
                        {
                            TextFragment textfragment = new TextFragment();
                            TextFragmentCollection textFragmentCollection = cell.TextFragments;
                            foreach (TextFragment fragment in textFragmentCollection)
                            {
                                string txt = "";
                                foreach (TextSegment seg in fragment.Segments)
                                {
                                    txt += seg.Text;
                                }
                                Console.WriteLine(txt);
                                strList.Add(txt);
                            }
                        }
                    }
                }
            }
            return strList;
        }


    }
}
