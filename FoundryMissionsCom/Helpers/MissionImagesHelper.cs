using FoundryMissionsCom.Models;
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
        private static readonly int MaxImageSize = 2 * 1024 * 1024;

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

            //first verify they're all images and under 2mb
            foreach(var file in files)
            {
                if (!IsImage(file))
                {
                    throw new Exception("File type is not an image.");
                }

                if (file.ContentLength > MaxImageSize)
                {
                    throw new Exception("File size is over 2MB.");
                }
            }

            foreach (var file in files)
            {
                string jpgFilename = Path.ChangeExtension(file.FileName, ".jpg");


                images.Add(jpgFilename, GetJpg(file));
            }
            return images;
        }

        public static void CheckForRemovedImages(ApplicationDbContext context, Mission mission, List<string> images)
        {
            var removedImages = new List<MissionImage>();

            foreach (var image in mission.Images)
            {
                if (!images.Contains(image.Filename))
                {
                    removedImages.Add(image);
                }
            }

            foreach (var image in removedImages)
            {
                mission.Images.Remove(image);
                DeleteImage(context,image);
            }
        }

        public static void AddImages(Dictionary<string, Image> images, Mission mission)
        {
            if (mission.Images == null)
            {
                mission.Images = new List<MissionImage>();
            }

            //all the images should be verified now
            var counter = mission.Images.Count;
            foreach(var file in images.Keys)
            {
                string fileName = GetFileName(mission.Id, file);

                string path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/missions/images/"),
                    mission.Id.ToString(),
                    fileName);

                Image image = images[file];
                Image thumbnail = GetThumbnail(image);

                SaveImage(image, path);

                path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/missions/images/"),
                    mission.Id.ToString(),
                    ThumbPrefix + fileName);

                SaveImage(image, path);

                if (mission.Images == null)
                {
                    mission.Images = new List<MissionImage>();
                }
                mission.Images.Add(new MissionImage() { MissionId = mission.Id, Filename = fileName, Order = counter });
                counter++;

                image.Dispose();
                thumbnail.Dispose();
            }
        }

        public static MissionImage GetRandomMissionImage(ApplicationDbContext context)
        {
            MissionImage image = context.MissionImages.OrderBy(m => Guid.NewGuid()).Take(1).FirstOrDefault();
            var mission = context.Missions.Where(m => m.Id == image.MissionId).FirstOrDefault();

            //in case the mission is invalid
            if (mission == null || mission.Status != Models.FoundryMissionModels.Enums.MissionStatus.Published)
            {
                image = GetRandomMissionImage(context);
            }

            return image;
        }

        private static string GetFileName(int id, string file)
        {
            string path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/missions/images/"),
                    id.ToString(),
                    file);

            if (File.Exists(path))
            {
                return GetFileName(id, file, 1);
            }
            else
            {
                return file;
            }
        }

        private static string GetFileName(int id, string file, int counter)
        {
            string path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/missions/images/"),
                    id.ToString(),
                    counter + file);

            if (File.Exists(path))
            {
                return GetFileName(id, file, counter+1);
            }
            else
            {
                return counter + file;
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

        private static void DeleteImage(ApplicationDbContext context, MissionImage image)
        {
            context.MissionImages.Remove(image);

            //delete the image and the thumbnail
            string path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/missions/images/"),
                    image.MissionId.ToString(),
                    image.Filename);

            File.Delete(path);

            path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/missions/images/"),
                    image.MissionId.ToString(),
                    ThumbPrefix + image.Filename);

            File.Delete(path);

        }

        private static bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".bmp" }; // add more if u like...

            foreach (var item in formats)
            {
                if (file.FileName.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        private static Image GetJpg(HttpPostedFileBase file)
        {
            var image = Image.FromStream(file.InputStream, true, true);
            var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            var jpgImage = Image.FromStream(stream);

            return jpgImage;
        }
    }
}