using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoundryMissionsCom.Models;
using FoundryMissionsCom.Models.FoundryMissionModels;
using FoundryMissionsCom.Models.FoundryMissionModels.Enums;
using FoundryMissionsCom.Models.FoundryMissionViewModels;

namespace FoundryMissionsCom.Controllers
{
    public class MissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Missions
        public ActionResult Index()
        {
            return View(db.Missions.ToList());
        }

        // GET: Missions/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Mission mission = db.Missions.Find(id);
            if (mission == null)
            {
                return HttpNotFound();
            }

            //if it is in review or unpubilshed only an aadministrator or the author can view it
            if (mission.Status == Models.FoundryMissionModels.Enums.MissionStatus.InReview ||
                mission.Status == Models.FoundryMissionModels.Enums.MissionStatus.Unpublished)
            {
                if (!mission.Author.UserName.Equals(User.Identity.Name) &&
                   (!User.IsInRole("Administrator")))
                {
                    return HttpNotFound();
                }
            }

            //if it is removed only an admnistrator can view it
            if (mission.Status == Models.FoundryMissionModels.Enums.MissionStatus.Removed)
            {
                if (!User.IsInRole("Administrator"))
                {
                    return HttpNotFound();
                }
            }

            return View(mission);
        }

        // GET: Missions/Submit
        [Authorize]
        public ActionResult Submit()
        {
            var publishedSelectItems = new List<SelectListItem>();
            #region Published Select List
            publishedSelectItems.Add(new SelectListItem()
            {
                Value = "false",
                Text = "No",
            });
            publishedSelectItems.Add(new SelectListItem()
            {
                Value = "true",
                Text = "Yes",
            });
            #endregion

            ViewBag.PublishedSelectList = new SelectList(publishedSelectItems, "Value", "Text");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Submit([Bind(Include = "CrypticId,Name,Description,Length,Faction,MinimumLevel,Spotlit,Published")] SubmitMissionViewModel missionViewModel, string submitButton)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                Mission mission = new Mission();

                #region Copy Info

                mission.CrypticId = missionViewModel.CrypticId;
                mission.Description = missionViewModel.Description;
                mission.Faction = missionViewModel.Faction;
                mission.Length = missionViewModel.Length;
                mission.MinimumLevel = missionViewModel.MinimumLevel;
                mission.Name = missionViewModel.Name;
                mission.Published = missionViewModel.Published;
                mission.Spotlit = missionViewModel.Spotlit;

                #endregion

                mission.Author = user;
                mission.DateAdded = DateTime.Today;
                mission.DateLastUpdated = DateTime.Today;
                if (submitButton.Equals("Save and Publish"))
                {
                    if (user.AutoApproval)
                    {
                        mission.Status = Models.FoundryMissionModels.Enums.MissionStatus.Published;
                    }
                    else
                    {
                        //mission.Status = Models.FoundryMissionModels.Enums.MissionStatus.InReview;
                        mission.Status = Models.FoundryMissionModels.Enums.MissionStatus.Published;
                    }
                }
                else //if (submitButton.Equals("Save"))
                {
                    //don't do anything, leave it at the current status (default = unpublished)
                    //mission.Status = Models.FoundryMissionModels.Enums.MissionStatus.Unpublished;
                }
                mission.Spotlit = false;                

                db.Missions.Add(mission);
                db.SaveChanges();
                return RedirectToAction("Details", new { mission.Id });
            }

            return View(missionViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Mission mission = db.Missions.Find(id);
            if (mission == null)
            {
                return HttpNotFound();
            }

            //only people who can edit a mission are the author or an admin
            if (!mission.Author.UserName.Equals(User.Identity.Name) && !User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            var publishedSelectItems = new List<SelectListItem>();
            #region Published Select List
            publishedSelectItems.Add(new SelectListItem()
            {
                Value = "false",
                Text = "No",
            });
            publishedSelectItems.Add(new SelectListItem()
            {
                Value = "true",
                Text = "Yes",
            });
            #endregion

            var editModel = new EditMissionViewModel();
            editModel.Id = mission.Id;
            editModel.CrypticId = mission.CrypticId;
            editModel.Name = mission.Name;
            editModel.Description = mission.Description;
            editModel.Length = mission.Length;
            editModel.Faction = mission.Faction;
            editModel.MinimumLevel = mission.MinimumLevel;
            editModel.Spotlit = mission.Spotlit;
            editModel.Published = mission.Published;

            ViewBag.PublishedSelectList = new SelectList(publishedSelectItems, "Value", "Text");

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,CrypticId,Name,Description,Length,Faction,MinimumLevel,Spotlit,Published")] EditMissionViewModel missionViewModel, string submitButton)
        {
            if (ModelState.IsValid)
            {
                var mission = db.Missions.Find(missionViewModel.Id);
                mission.CrypticId = missionViewModel.CrypticId;
                mission.Name = missionViewModel.Name;
                mission.Description = missionViewModel.Description;
                mission.Length = missionViewModel.Length;
                mission.Faction = missionViewModel.Faction;
                mission.MinimumLevel = missionViewModel.MinimumLevel;
                mission.Spotlit = missionViewModel.Spotlit;
                mission.Published = missionViewModel.Published;

                db.SaveChanges();

                return RedirectToAction("Details", new { mission.Id });
            }
            return View(missionViewModel);
        }

        public ActionResult Random()
        {
            int missionId = db.Missions.OrderBy(m => Guid.NewGuid()).Select(m => m.Id).FirstOrDefault();

            return RedirectToAction("Details", new { id = missionId });
        }

        public ActionResult Search(string q)
        {
            string upperQuery = q.ToUpper();
            List<ListMissionViewModel> listMissions = new List<ListMissionViewModel>();
            var missions = db.Missions.Where(m => m.Author.UserName.ToUpper().Contains(upperQuery) ||
                                             m.CrypticId.ToUpper().Contains(upperQuery) ||
                                             m.Description.ToUpper().Contains(upperQuery) ||
                                             m.Name.ToUpper().Contains(upperQuery)).ToList();


            foreach(var mission in missions)
            {
                var listMission = new ListMissionViewModel()
                {
                    Id = mission.Id,
                    Name = mission.Name,
                    CrypticId = mission.CrypticId,
                    Author = mission.Author.UserName,
                    MinimumLevel = mission.MinimumLevel,
                    Faction = mission.Faction,
                    DateLastUpdated = mission.DateLastUpdated,
                    FactionImageUrl = GetFactionImageUrl(mission.Faction),
                    LevelImageUrl = GetLevelImageUrl(mission.MinimumLevel),
                };

                listMissions.Add(listMission);
            }

            return View(listMissions);
        }

        private string GetLevelImageUrl(int minimumLevel)
        {
            return "";
        }

        private string GetFactionImageUrl(Faction faction)
        {
            return "";
        }



        #region  Auto generated

        // GET: Missions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Missions.Find(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            return View(mission);
        }

        // POST: Missions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mission mission = db.Missions.Find(id);
            db.Missions.Remove(mission);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
