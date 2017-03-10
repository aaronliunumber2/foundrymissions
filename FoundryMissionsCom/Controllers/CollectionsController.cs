using FoundryMissionsCom.Models;
using FoundryMissionsCom.Models.FoundryMissionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoundryMissionsCom.Controllers
{
    public class CollectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Collections
        public ActionResult Index()
        {
            var collections = new List<ListCollectionViewModel>();

            collections.Add(new ListCollectionViewModel()
            {
                CollectionLink = "marvel",
                Description = "Missions by Zorbane that are inspired by the Marvel Cinematic Universe.  These missions can be played in any order except for the finale mission, UNKNOWN MISSION NAME.",
                Id = 1,
                ImageLink = "~/collections/1/pic.jpg",
                Name = "Marvel",
                Owner = db.Users.FirstOrDefault(u => u.CrypticTag.Equals("Zorbane")),
            });

            collections.Add(new ListCollectionViewModel()
            {
                CollectionLink = "bad-missions",
                Description = "These missions are not very good, they are by RogueEnterprise and should not be played.  THIS IS A WARNING.  Sometimes you will get stuck in tehe floor along with your boffs.  Also he doesn't need more dilithium.",
                Id = 2,
                ImageLink = "~/collections/2/pic.jpg",
                Name = "Bad Missions",
                Owner = db.Users.FirstOrDefault(u => u.CrypticTag.Equals("Zorbane")),
            });

            return View(collections);
        }

        public ActionResult Submit()
        {
            return View();
        }

        public ActionResult Details()
        {
            return RedirectToAction("index");
        }

        public ActionResult Details(string link)
        {
            return View();
        }
    }
}