namespace SpireExcel.Tests
{
    public class ExcelTest
    {
        [Theory]
        [InlineData("Assets\\Example-of-a-weekly-shopping-list.pdf", "Results\\ExportPdfToExcel.xlsx")]
        public void PdfToExcel(string inputfileName, string outputfileName)
        {
            var dir = Directory.GetParent(outputfileName);
            if (!Directory.Exists(dir.FullName))
                Directory.CreateDirectory(dir.FullName);
            DocProcess.PdfToExcel(AppDomain.CurrentDomain.BaseDirectory + inputfileName, AppDomain.CurrentDomain.BaseDirectory + outputfileName);
            Assert.True(File.Exists(AppDomain.CurrentDomain.BaseDirectory + outputfileName));
        }

    }
}