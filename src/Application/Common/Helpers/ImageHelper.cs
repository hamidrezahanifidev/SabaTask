using System;
using System.Drawing;
using System.IO;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Extentions;

namespace CleanArchitecture.Application.Common.Helpers
{
    public class ImageHelper
    {
        public static string ImageToBase64(Image image)
        {
            var thumb = image.GetThumbnailImage(400, 400, () => false, IntPtr.Zero);

            using (var memoryStream = new MemoryStream())
            {
                thumb.Save(memoryStream, image.RawFormat);
                var imageBytes = memoryStream.ToArray();

                // Convert byte[] to Base String
                var base64String = Convert.ToBase64String(imageBytes);
                image.Dispose();
                memoryStream.Dispose();
                return base64String;
            }
        }

        public static Image GetImageByPath(string path)
        {
            Image avatar;
            try
            {
                avatar = Image.FromFile(path);
            }
            catch (Exception)
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "default.jpeg");
                avatar = Image.FromFile(path);
            }
            return avatar;
        }

        public static Image BodyToImage(string body)
        {
            Image image;
            try
            {
                MemoryStream ms = new MemoryStream(body.GetBase64());
                image = Image.FromStream(ms);
            }
            catch (Exception)
            {
                throw new BodyFormatException();
            }
            return image;
        }
    }
}
