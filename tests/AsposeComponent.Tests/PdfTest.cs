using Xunit.Abstractions;

namespace AsposeComponent.Tests
{
    public class PdfTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private static string BaseDirectory => AppDomain.CurrentDomain.BaseDirectory;
        public PdfTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("Assets\\Example-of-a-weekly-shopping-list.pdf", "Results\\ExportPdfToExcel.xlsx")]
        public void PdfToExcel(string inputfileName, string outputfileName)
        {
            PdfProcess process = new();
            var dir = Directory.GetParent(outputfileName);
            if (!Directory.Exists(dir.FullName))
                Directory.CreateDirectory(dir.FullName);
            process.PdfToExcel(BaseDirectory + inputfileName, BaseDirectory + outputfileName);
            Assert.True(File.Exists(BaseDirectory + outputfileName));
        }

        [Theory]
        [InlineData("Assets\\Example-of-a-weekly-shopping-list.pdf", "Results\\ExportPdfToHtml.html")]
        public void PdfToHtml(string inputfileName, string outputfileName)
        {
            var dir = Directory.GetParent(outputfileName);
            if (!Directory.Exists(dir.FullName))
                Directory.CreateDirectory(dir.FullName);
            PdfProcess.PdfToHtml(BaseDirectory + inputfileName, BaseDirectory + outputfileName);
            Assert.True(File.Exists(BaseDirectory + outputfileName));
        }

        [Theory]
        [InlineData("Assets\\Example-of-a-weekly-shopping-list.pdf", "Results\\TableToText.txt")]
        public void ExtractTable(string inputfileName, string outputfileName)
        {
            _testOutputHelper.WriteLine("Start extract table...");
            var strList = PdfProcess.ExtractTable(BaseDirectory + inputfileName);
            //foreach (var str in strList)
            //{
            //    _testOutputHelper.WriteLine($"{str}");
            //}
            var dir = Directory.GetParent(outputfileName);
            if (!Directory.Exists(dir.FullName))
                Directory.CreateDirectory(dir.FullName);
            System.IO.File.WriteAllLines(BaseDirectory + outputfileName, strList);
            Assert.True(File.Exists(BaseDirectory + outputfileName));
            _testOutputHelper.WriteLine("End extract table...");
        }

        [Theory]
        [InlineData("Assets\\HtmlTemplate.html", "Results\\ExportHtmlToExcel.xls")]
        public void HtmlToExcel(string inputfileName, string outputfileName)
        {
            var dir = Directory.GetParent(outputfileName);
            if (!Directory.Exists(dir.FullName))
                Directory.CreateDirectory(dir.FullName);
            CellProcess.HtmlToExcel(BaseDirectory + inputfileName, BaseDirectory + outputfileName);
            Assert.True(File.Exists(BaseDirectory + outputfileName));
        }
    }
}