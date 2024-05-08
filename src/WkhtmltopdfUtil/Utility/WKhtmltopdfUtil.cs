using System.Diagnostics;
using System.Text;

namespace WkhtmltopdfUtil.Utility
{
    /// <summary>
    /// WKhtmltopdf operation
    /// </summary>
    public class WKhtmltopdfUtil
    {
        /// <summary>
        /// Convert HTML text content to PDF
        /// </summary>
        /// <param name="strHtml">HTML content</param>
        /// <param name="savePath">Saving path of PDF file</param>
        /// <returns></returns>
        public static bool HtmlTextConvertToPdf(string strHtml, string savePath)
        {
            bool flag;
            try
            {
                string htmlPath = HtmlTextConvertFile(strHtml);

                flag = HtmlConvertToPdf(htmlPath, savePath);
            }
            catch
            {
                throw new Exception();
            }
            return flag;
        }
        /// <summary>
        /// Gets command line arguments
        /// </summary>
        /// <param name="htmlPath"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        private static string GetArguments(string htmlPath, string savePath)
        {
            if (string.IsNullOrEmpty(htmlPath))
            {
                throw new Exception("HTML local path or network address can not be empty.");
            }

            if (string.IsNullOrEmpty(savePath))
            {
                throw new Exception("The path saved by the PDF document can not be empty.");
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(" --page-height 279 ");
            stringBuilder.Append(" --page-width 215 ");
            //stringBuilder.Append(" --header-center I'm Page header ");  //Set the center display header
            //stringBuilder.Append(" --header-line ");         //A straight line appears between the header and the content
            //stringBuilder.Append(" --footer-center \"Page [page] of [topage]\" ");    //Set the footer to be centered
            //stringBuilder.Append(" --footer-line ");       //A straight line appears between the footer and the content
            stringBuilder.Append(" " + htmlPath + " ");       //The file path of the local HTML or the URL of the webpage HTML
            stringBuilder.Append(" " + savePath + " ");       //The path to save the generated PDF document
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Verify save path
        /// </summary>
        /// <param name="savePath"></param>
        private static void CheckFilePath(string savePath)
        {
            string ext = string.Empty;
            string path = string.Empty;
            string fileName = string.Empty;

            ext = Path.GetExtension(savePath);
            if (string.IsNullOrEmpty(ext) || ext.ToLower() != ".pdf")
            {
                throw new Exception("Extension error:This method is used to generate PDF files.");
            }

            fileName = Path.GetFileName(savePath);
            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("File name is empty.");
            }

            try
            {
                path = savePath.Substring(0, savePath.IndexOf(fileName));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch
            {
                throw new Exception("The file path does not exist.");
            }
        }
        /// <summary>
        /// Convert HTML to PDF
        /// </summary>
        /// <param name="htmlPath">The value can be a local path or a network address</param>
        /// <param name="savePath">Saving path of PDF file</param>
        /// <returns></returns>
        public static bool HtmlConvertToPdf(string htmlPath, string savePath)
        {
            bool flag = false;
            CheckFilePath(savePath);
            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "Assets", "wkhtmltopdf.exe");
            if (!File.Exists(exePath))
            {
                throw new Exception("No application wkhtmltopdf.exe was found.");
            }

            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = exePath;
                processStartInfo.WorkingDirectory = Path.GetDirectoryName(exePath);
                processStartInfo.UseShellExecute = false;
                processStartInfo.CreateNoWindow = true;
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.Arguments = GetArguments(htmlPath, savePath);

                Process process = new Process
                {
                    StartInfo = processStartInfo
                };
                process.Start();
                process.WaitForExit();

                ///Check whether an error message is displayed
                //StreamReader srone = process.StandardError;
                //StreamReader srtwo = process.StandardOutput;
                //string ss1 = srone.ReadToEnd();
                //string ss2 = srtwo.ReadToEnd();
                //srone.Close();
                //srone.Dispose();
                //srtwo.Close();
                //srtwo.Dispose();

                process.Close();
                process.Dispose();
                File.Delete(htmlPath);
                flag = true;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
        /// <summary>
        ///HTML text content to HTML file
        /// </summary>
        /// <param name="strHtml">HTML content</param>
        /// <returns>File path of HTML</returns>
        public static string HtmlTextConvertFile(string strHtml)
        {
            if (string.IsNullOrEmpty(strHtml))
            {
                throw new Exception("HTML text content cannot be empty.");
            }

            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\html\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = path + DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(1000, 10000) + ".html";
                FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
                streamWriter.Write(strHtml);
                streamWriter.Flush();

                streamWriter.Close();
                streamWriter.Dispose();
                fileStream.Close();
                fileStream.Dispose();
                return fileName;
            }
            catch
            {
                throw new Exception("HTML text content error.");
            }
        }
    }
}
