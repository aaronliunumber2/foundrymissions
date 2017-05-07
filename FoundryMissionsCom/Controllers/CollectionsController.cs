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
    public class CollectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Collections
        public ActionResult Index()
        {
            //temporary for testing
            var collections = new List<ListCollectionViewModel>();

            collections.Add(new ListCollectionViewModel()
            {
                CollectionLink = "superheroes-by-zorbane",
                Description = "Missions by Zorbane that are inspired by the Marvel Cinematic Universe.  These missions can be played in any order except for the finale mission, UNKNOWN MISSION NAME.",
                Id = 1,
                ImageLink = MissionCollectionHelper.GetImageLink("pic.jpg", 1),
                Name = "Superheroes by Zorbane",
                Owner = db.Users.FirstOrDefault(u => u.CrypticTag.Equals("Zorbane")),
            });

            collections.Add(new ListCollectionViewModel()
            {
                CollectionLink = "bad-missions",
                Description = "These missions are not very good, they are by RogueEnterprise and should not be played.  THIS IS A WARNING.  Sometimes you will get stuck in tehe floor along with your boffs.  Also he doesn't need more dilithium.",
                Id = 2,
                ImageLink = MissionCollectionHelper.GetImageLink("pic.jpg", 2),
                Name = "Bad Missions",
                Owner = db.Users.FirstOrDefault(u => u.CrypticTag.Equals("Zorbane")),
            });

            return View(collections);
        }

        public ActionResult Submit()
        {
            return View();
        }

        public ActionResult Details(string link)
        {
            //temporary for testing
            var collection = new ViewCollectionViewModel()
            {
                CollectionLink = "superheroes-by-zorbane",
                Description = "Missions by Zorbane that are inspired by the Marvel Cinematic Universe.  These missions can be played in any order except for the finale mission, UNKNOWN MISSION NAME. Missions by Zorbane that are inspired by the Marvel Cinematic Universe.These missions can be played in any order except for the finale mission, UNKNOWN MISSION NAME. Missions by Zorbane that are inspired by the Marvel Cinematic Universe.  These missions can be played in any order except for the finale mission, UNKNOWN MISSION NAME. Missions by Zorbane that are inspired by the Marvel Cinematic Universe.  These missions can be played in any order except for the finale mission, UNKNOWN MISSION NAME. Missions by Zorbane that are inspired by the Marvel Cinematic Universe.These missions can be played in any order except for the finale mission, UNKNOWN MISSION NAME. Missions by Zorbane that are inspired by the Marvel Cinematic Universe.  These missions can be played in any order except for the finale mission, UNKNOWN MISSION NAME.",
                Id = 1,
                ImageLink = MissionCollectionHelper.GetImageLink("pic.jpg", 1),
                Name = "Superheroes by Zorbane",
                AuthorTag = db.Users.FirstOrDefault(u => u.CrypticTag.Equals("Zorbane")).CrypticTag,
                Missions = new List<ListMissionViewModel>(),
            };

            var missions = new List<Mission>();

            missions.Add(db.Missions.Where(m => m.Name.Equals("The Improbable Bulk")).Single());
            missions.Add(db.Missions.Where(m => m.Name.Equals("Duritanium Man")).Single());

            collection.Missions = MissionHelper.GetListMissionViewModels(missions);

            return View(collection);
        }
    }
}