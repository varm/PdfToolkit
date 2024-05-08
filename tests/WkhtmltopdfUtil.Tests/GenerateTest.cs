using System.Text;

namespace WkhtmltopdfUtil.Tests
{
    public class GenerateTest
    {
        [Theory]
        [InlineData("Results\\Generated.pdf", "Assets\\template.html", "Assets\\avatar.jpg")]
        public void Generate(string outputfileName, string templatePath, string imgSrc)
        {
            StringBuilder sklst = new();
            string[] skills = ["C#", "Java", "Python", "Javascript"];
            for (int i = 0; i < skills.Length; i++)
            {
                sklst.Append($"<li>{skills[i]}</li>");
            }
            string[] exps = ["USTS", "PJU", "HSTS", "WUT"];
            StringBuilder expstr = new();
            for (int i = 0; i < exps.Length; i++)
            {
                expstr.Append($"<li>{exps[i]}</li>");
            }

            var htmlTemp = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + templatePath);
            htmlTemp = htmlTemp.Replace("[lilist]", sklst.ToString());
            htmlTemp = htmlTemp.Replace("[explist]", expstr.ToString());
            htmlTemp = htmlTemp.Replace("[imgsrc]", AppDomain.CurrentDomain.BaseDirectory + imgSrc);
            Assert.True(PdfGenerate.HtmlToPdf(htmlTemp, AppDomain.CurrentDomain.BaseDirectory + outputfileName));
        }
    }
}