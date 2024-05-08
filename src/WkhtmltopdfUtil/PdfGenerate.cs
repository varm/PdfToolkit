using WkhtmltopdfUtil.Utility;

namespace WkhtmltopdfUtil
{
    public class PdfGenerate
    {
        public static bool HtmlToPdf(string htmlContent, string outputfileName)
        {
            return WKhtmltopdfUtil.HtmlTextConvertToPdf(htmlContent, outputfileName);
        }
    }
}
