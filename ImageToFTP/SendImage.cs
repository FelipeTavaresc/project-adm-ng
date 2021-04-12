using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToFTP
{
    public static class SendImage
    {
        public static void SaveImageInDirectory(byte[] image, string produtoId, string slug)
        {
            using (var ms = new MemoryStream(image, 0, image.Length))
            {
                string folderPath = @"C:\Users\Felipe\Documents\Visual Studio 2017\Projects\AdmNerdGo\AdmNerdGo\wwwroot\img\produtos\";
                string fileName = produtoId + "-" + slug + ".jpg";
                string imagePath = folderPath + fileName;
                Image img = Image.FromStream(ms, true);
                img.Save(imagePath, ImageFormat.Jpeg);
            }
        }
    }
}
