namespace PdfPigComponent.Tests
{
    public class DocTest
    {
        [Theory]
        [InlineData("Assets\\Example-of-a-weekly-shopping-list.pdf", "Results\\ExportPdfToHtml.html")]
        public void PdfToHtml(string inputfileName, string outputfileName)
        {
            var dir = Directory.GetParent(outputfileName);
            if (!Directory.Exists(dir.FullName))
                Directory.CreateDirectory(dir.FullName);
            HtmlProcess.PdfToHtml(AppDomain.CurrentDomain.BaseDirectory + inputfileName, AppDomain.CurrentDomain.BaseDirectory + outputfileName);
            Assert.True(File.Exists(AppDomain.CurrentDomain.BaseDirectory + outputfileName));
        }
    }
}