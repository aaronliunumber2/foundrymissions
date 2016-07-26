using FoundryMissionsCom.Models.FoundryMissionModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Helpers
{
    public static class MissionImagesHelper
    {
        public static void SaveImages(List<HttpPostedFileBase> files, Mission mission)
        {
            //all the images should be verified now
            foreach(var imageFile in files)
            {
                using (Image image = Image.FromStream(imageFile.InputStream))
                {
                    string fileName = Path.GetFileName(imageFile.FileName);
                    string path = Path.Combine(
                        HttpContext.Current.Server.MapPath("~missions/images/"),
                        mission.Id.ToString(),
                        fileName);

                    Image thumbnail = GetThumbnail(image);
                    imageFile.SaveAs(path);

                    path = Path.Combine(
                        HttpContext.Current.Server.MapPath("~missions/images/"),
                        mission.Id.ToString(),
                        "thumb" + fileName);

                    thumbnail.Save()
                         
                }                
            }
        }

        private static Image GetThumbnail(Image image)
        {
            return null;
        }
    }
}