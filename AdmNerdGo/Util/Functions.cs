using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmNerdGo.Util
{
    public class Functions
    {
        private static readonly char[] s_Diacritics = GetDiacritics();

        public static byte[] ConvertImageToByte(List<IFormFile> Imagem)
        {
            byte[] image = null;
            foreach (var item in Imagem)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        item.CopyTo(stream);
                        image = stream.ToArray();
                    }
                }
            }
            return image;
        }

        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] > 255)
                    sb.Append(text[i]);
                else
                    sb.Append(s_Diacritics[text[i]]);
            }

            return sb.ToString();
        }

        private static char[] GetDiacritics()
        {
            char[] accents = new char[256];

            for (int i = 0; i < 256; i++)
                accents[i] = (char)i;

            accents[(byte)'á'] = accents[(byte)'à'] = accents[(byte)'ã'] = accents[(byte)'â'] = accents[(byte)'ä'] = 'a';
            accents[(byte)'Á'] = accents[(byte)'À'] = accents[(byte)'Ã'] = accents[(byte)'Â'] = accents[(byte)'Ä'] = 'A';

            accents[(byte)'é'] = accents[(byte)'è'] = accents[(byte)'ê'] = accents[(byte)'ë'] = 'e';
            accents[(byte)'É'] = accents[(byte)'È'] = accents[(byte)'Ê'] = accents[(byte)'Ë'] = 'E';

            accents[(byte)'í'] = accents[(byte)'ì'] = accents[(byte)'î'] = accents[(byte)'ï'] = 'i';
            accents[(byte)'Í'] = accents[(byte)'Ì'] = accents[(byte)'Î'] = accents[(byte)'Ï'] = 'I';

            accents[(byte)'ó'] = accents[(byte)'ò'] = accents[(byte)'ô'] = accents[(byte)'õ'] = accents[(byte)'ö'] = 'o';
            accents[(byte)'Ó'] = accents[(byte)'Ò'] = accents[(byte)'Ô'] = accents[(byte)'Õ'] = accents[(byte)'Ö'] = 'O';

            accents[(byte)'ú'] = accents[(byte)'ù'] = accents[(byte)'û'] = accents[(byte)'ü'] = 'u';
            accents[(byte)'Ú'] = accents[(byte)'Ù'] = accents[(byte)'Û'] = accents[(byte)'Ü'] = 'U';

            accents[(byte)'ç'] = 'c';
            accents[(byte)'Ç'] = 'C';

            accents[(byte)'ñ'] = 'n';
            accents[(byte)'Ñ'] = 'N';

            accents[(byte)'ÿ'] = accents[(byte)'ý'] = 'y';
            accents[(byte)'Ý'] = 'Y';

            return accents;
        }

        public static string SaveImageInDirectory(byte[] image, string produtoId, string slug)
        {
            string imagePath = string.Empty;
            using (var ms = new MemoryStream(image, 0, image.Length))
            {
                string folderPath = @"C:\Users\felip\source\repos\AdmNerdGo\AdmNerdGo\wwwroot\img\produtos\";
                string fileName = produtoId + "-" + slug + ".jpg";
                imagePath = folderPath + fileName;
                Image img = Image.FromStream(ms, true);
                img.Save(imagePath, ImageFormat.Jpeg);
            }
            return imagePath;
        }

        public static void UploadImageToFtp(string imagePath, string imageName)
        {
            var request = (System.Net.FtpWebRequest)System.Net.WebRequest.Create("ftp://nerdgo.com.br/www/wwwroot/img/produtos/" + imageName);
            request.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new System.Net.NetworkCredential("nerdgo", "brnd0804");

            var conteudoArquivo = System.IO.File.ReadAllBytes(imagePath);
            request.ContentLength = conteudoArquivo.Length;

            var requestStream = request.GetRequestStream();
            requestStream.Write(conteudoArquivo, 0, conteudoArquivo.Length);
            requestStream.Close();

            var response = (System.Net.FtpWebResponse)request.GetResponse();
            //Console.WriteLine("Upload completo. Status: {0}", response.StatusDescription);
            response.Close();
        }
    }
}
