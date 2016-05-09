using FoundryMissionsCom.Models;
using FoundryMissionsCom.Models.FoundryMissionModels;
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

            var qry = db.Missions.OrderBy(m => m.DateLastUpdated).Take(RandomUpdated);

            List<Mission> recentlyUpdatedMissions = qry.Where(m => m.Status == Models.FoundryMissionModels.Enums.MissionStatus.Published).OrderBy(m => Guid.NewGuid()).Take(FrontPageMissions).ToList();

            

            ViewBag.RandomMissions = randomMissions;
            ViewBag.RecentlyUpdatedMissions = recentlyUpdatedMissions;

            return View();
        }
    }
}