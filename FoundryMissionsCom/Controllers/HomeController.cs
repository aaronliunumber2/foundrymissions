using FoundryMissionsCom.Helpers;
using FoundryMissionsCom.Models;
using FoundryMissionsCom.Models.FoundryMissionModels;
using FoundryMissionsCom.Models.FoundryMissionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoundryMissionsCom.Controllers
{
    public class HomeController : Controller
    {
        private const int FrontPageMissions = 3;
        private const int RandomUpdated = 10;

        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            Random rnd = new Random();

            List<Mission> randomMissions = db.Missions.Where(m => m.Status == Models.FoundryMissionModels.Enums.MissionStatus.Published).OrderBy(m => Guid.NewGuid()).Take(FrontPageMissions).ToList();

            var qry = db.Missions.Where(m => m.Status == Models.FoundryMissionModels.Enums.MissionStatus.Published).OrderByDescending(m => m.DateLastUpdated).Take(RandomUpdated);

            List<Mission> recentlyUpdatedMissions = qry.Where(m => m.Status == Models.FoundryMissionModels.Enums.MissionStatus.Published).OrderBy(m => Guid.NewGuid()).Take(FrontPageMissions).ToList();

            #region RandomMissionImageViewModel

            var randomImage = MissionImagesHelper.GetRandomMissionImage(db);
            RandomMissionImageViewModel randomMissionImage = null;
            if (randomImage != null)
            {
                var mission = db.Missions.Where(m => m.Id == randomImage.MissionId).FirstOrDefault();

                randomMissionImage = new RandomMissionImageViewModel();
                randomMissionImage.Author = mission.Author.CrypticTag;
                randomMissionImage.Faction = mission.Faction.ToString();
                randomMissionImage.ImageLink = MissionImagesHelper.GetImageLink(randomImage.Filename, mission.Id);
                randomMissionImage.ThumbnailLink = MissionImagesHelper.GetThumbnailLink(randomImage.Filename, mission.Id);
                randomMissionImage.MissionLink = mission.MissionLink;
                randomMissionImage.MissionName = mission.Name;
            }

            #endregion

            ViewBag.RandomMissions = randomMissions;
            ViewBag.RecentlyUpdatedMissions = recentlyUpdatedMissions;
            if (randomMissionImage != null)
            {
                ViewBag.RandomImage = randomMissionImage;
            }

            return View();
        }

        [ActionName("contact-us")]
        public ActionResult ContactUs()
        {
            return View();
        }
        
        [ActionName("test-email")]
        public ActionResult TestEmail()
        {
            EmailHelper.SendTestEmail();
            ViewBag.errorMessage = "Email sent successfully.";
            return View("Error");
        }
    }
}