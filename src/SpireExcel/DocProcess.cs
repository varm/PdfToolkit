using Spire.Pdf;
using Spire.Pdf.Utilities;
using Spire.Xls;

namespace SpireExcel
{
    public class DocProcess
    {
        public static void PdfToExcel(string inputfileName, string outputfileName)
        {
            //Load a sample PDF document
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromFile(inputfileName);
            //Create a PdfTableExtractor instance
            PdfTableExtractor extractor = new PdfTableExtractor(pdf);
            //Extract tables from the first page
            PdfTable[] pdfTables = extractor.ExtractTable(0);
            //Create a Workbook object,
            Workbook wb = new Spire.Xls.Workbook();

            var Lic = new Spire.License.InternalLicense();
            Lic.LicenseType = Spire.License.LicenseType.Runtime;
            Lic.AssemblyList = new string[] { "Spire.Spreadsheet" };
            var InternalLicense = wb.GetType().GetProperty("InternalLicense", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            InternalLicense.SetValue(wb, Lic);

            //Remove default worksheets
            wb.Worksheets.Clear();
            //If any tables are found
            if (pdfTables != null && pdfTables.Length > 0)
            {
                //Loop through the tables
                for (int tableNum = 0; tableNum < pdfTables.Length; tableNum++)
                {
                    //Add a worksheet to workbook
                    String sheetName = String.Format("Table - {0}", tableNum + 1);
                    Worksheet sheet = wb.Worksheets.Add(sheetName);
                    //Loop through the rows in the current table
                    for (int rowNum = 0; rowNum < pdfTables[tableNum].GetRowCount(); rowNum++)
                    {
                        //Loop through the columns in the current table
                        for (int colNum = 0; colNum < pdfTables[tableNum].GetColumnCount(); colNum++)
                        {
                            //Extract data from the current table cell
                            String text = pdfTables[tableNum].GetText(rowNum, colNum);
                            //Insert data into a specific cell
                            sheet.Range[rowNum + 1, colNum + 1].Text = text;
                        }
                    }
                    //Auto fit column width
                    sheet.AllocatedRange.AutoFitColumns();
                }
            }
            //Save the workbook to an Excel file
            wb.SaveToFile(outputfileName, ExcelVersion.Version2016);
        }
    }
}
