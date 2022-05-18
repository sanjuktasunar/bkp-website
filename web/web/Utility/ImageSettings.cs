using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace web.Utility
{
    public class ImageSettings
    {
        public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            byte[] imageData = null;
            if (file != null && file.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    imageData = binaryReader.ReadBytes(file.ContentLength);
                }
            }
            return imageData;
        }

        public Image ConvertToImage(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);

                System.Drawing.Image sourceimage =
                    System.Drawing.Image.FromStream(file.InputStream);
                return sourceimage;
            }
            return null;
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public string ConvertToString(HttpPostedFileBase file)
        {
            var image = ConvertToImage(file);
            if (image != null)
            {
                var resizeImage = ResizeImage(image, 600, 600, true);
                //var Imgstring = Convert.ToBase64String(ConvertToByte(file));
                var Imgstring = Convert.ToBase64String(ImageToByteArray(resizeImage));
                return Imgstring;
            }
            return null;
        }

        public Image ResizeImage(Image image, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            if (onlyResizeIfWider && image.Width <= newWidth) newWidth = image.Width;

            var newHeight = image.Height * newWidth / image.Width;
            if (newHeight > maxHeight)
            {
                // Resize with height instead  
                newWidth = image.Width * maxHeight / image.Height;
                newHeight = maxHeight;
            }

            var res = new Bitmap(newWidth, newHeight);

            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return res;
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public bool SaveImage(string ImgStr, string ImgName)
        {
            String path = System.Web.HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path

            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            //check if image exists or not
            var oldfilePath = System.Web.HttpContext.Current.Server.MapPath("~/ImageStorage/" + ImgName + ".jpg");
            if (File.Exists(oldfilePath))
                File.Delete(oldfilePath);

            string imageName = ImgName + ".jpg";
            //set the image path
            string imgPath = Path.Combine(path, imageName);

            ImgStr = ImgStr.Replace("data:image;base64,", "").Trim();
            byte[] imageBytes = Convert.FromBase64String(ImgStr);

            File.WriteAllBytes(imgPath, imageBytes);
            return true;
        }

        public bool IsBase64String(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return false;

            string base64 = base64String.Split(',')[1].ToString().Trim();
            return (base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }
    }
}