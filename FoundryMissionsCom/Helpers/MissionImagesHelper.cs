using FoundryMissionsCom.Models.FoundryMissionModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Helpers
{
    public static class MissionImagesHelper
    {
        public const string ThumbPrefix = "thumb_";
        public const int ThumbWidth = 100;

        public static string GetImageLink(string image, int missionId)
        {
            return Path.Combine(
                    "~/missions/images/",
                    missionId.ToString(),
                    image);
        }

        public static string GetThumbnailLink(string image, int missionId)
        {
            return Path.Combine(
                    "~/missions/images/",
                    missionId.ToString(),
                    ThumbPrefix + image);
        }

        /// <summary>
        /// Make sure the HttpPostedFileBase files are images
        /// </summary>
        /// <param name="files"></param>
        /// <returns>The images and their filenames</returns>
        public static Dictionary<string, Image> ValidateImages(List<HttpPostedFileBase> files)
        {
            var images = new Dictionary<string, Image>();
            foreach (var file in files)
            {
                images.Add(Path.GetFileName(file.FileName), Image.FromStream(file.InputStream));
            }
            return images;
        }

        public static void CheckForRemovedImages()
        {
            
        }

        public static void AddImages(Dictionary<string, Image> images, Mission mission)
        {
            //all the images should be verified now
            foreach(var file in images.Keys)
            {
                string path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/missions/images/"),
                    mission.Id.ToString(),
                    file);

                Image image = images[file];
                Image thumbnail = GetThumbnail(image);

                SaveImage(image, path);

                path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/missions/images/"),
                    mission.Id.ToString(),
                    ThumbPrefix + file);

                SaveImage(image, path);

                if (mission.Images == null)
                {
                    mission.Images = new List<MissionImage>();
                }
                mission.Images.Add(new MissionImage() { MissionId = mission.Id, Filename = file });

                image.Dispose();
                thumbnail.Dispose();
            }
        }

        private static Image GetThumbnail(Image image)
        {
            var ratio = image.Height / 100;
            var newHeight = 100;
            var newWidth = image.Width / ratio;
            
            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;

        }

        private static void SaveImage(Image image, string path)
        {
            //lets check for the folder, if it doesn't exist create it
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    image.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }
}