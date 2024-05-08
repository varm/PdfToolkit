namespace AsposeComponent
{
    public class CellProcess
    {
        public static void HtmlToExcel(string inputfileName, string outputfileName)
        {
            //Row height https://forum.aspose.com/t/html-to-excel-and-pdf-column-width-and-row-height-not-proper-content-chopped-out-after-conversion/208491/2
            Aspose.Cells.HTMLLoadOptions hTMLLoad = new Aspose.Cells.HTMLLoadOptions(Aspose.Cells.LoadFormat.Html);
            hTMLLoad.IgnoreNotPrinted = true;

            // add this line to get better result
            hTMLLoad.AutoFitColsAndRows = true;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(inputfileName, hTMLLoad);
            Aspose.Cells.Worksheet worksheet = null;

            if (workbook != null)
            {
                worksheet = workbook.Worksheets[0];
                worksheet.PageSetup.PrintTitleRows = "$1:$3";
            }
            //workbook.Save(outputfileName,SaveFormat.Xlsx); 
            worksheet.PageSetup.Orientation = Aspose.Cells.PageOrientationType.Portrait;
            worksheet.Workbook.Save(outputfileName);
        }
    }
}
