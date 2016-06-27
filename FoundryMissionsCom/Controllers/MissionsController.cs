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
using FoundryMissionsCom.Helpers;
using FoundryMissionsCom.Attributes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
        public ActionResult Details(string link)
        {
            if (string.IsNullOrEmpty(link))
            {
                return RedirectToAction("index", "home");
            }
            Mission mission = db.Missions.Where(m => m.MissionLink.Equals(link)).FirstOrDefault();
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

            ViewMissionViewModel viewMission = new ViewMissionViewModel()
            {
                Id = mission.Id,
                Author = mission.Author,
                CrypticId = mission.CrypticId.ToUpper(),
                Name = mission.Name,
                Description = mission.Description,
                Faction = mission.Faction,
                FactionImageUrl = MissionHelper.GetBigFactionImageUrl(mission.Faction),
                MinimumLevel = mission.MinimumLevel,
                MinimumLevelImageUrl = MissionHelper.GetBigLevelImageUrl(mission.MinimumLevel, mission.Faction),
                DateLastUpdated = mission.DateLastUpdated,
                Length = mission.Length,
                Tags = mission.Tags.OrderBy(t => t.TagName).ToList(),
                Videos = mission.Videos,
                Status = mission.Status,
                Images = new List<string>()
            };

            //It's okay to show the mission now

            return View(viewMission);
        }

        // GET: Missions/Submit
        [Authorize]
        public ActionResult Submit()
        {
            List<SelectListItem> publishedSelectItems = MissionHelper.GetYesNoSelectList();
            ViewBag.AvailableTags = db.MissionTagTypes.Select(t => t.TagName).ToList();
            ViewBag.PublishedSelectList = new SelectList(publishedSelectItems, "Value", "Text");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Submit([Bind(Include = "CrypticId,Name,Description,Length,Faction,MinimumLevel,Spotlit,Published,Tags")] SubmitMissionViewModel missionViewModel, string submitButton)
        {
            if (ModelState.IsValid)
            {
                //check if cryptic id is already used
                if (db.Missions.Any(m => m.CrypticId.Equals(missionViewModel.CrypticId)))
                {
                    ModelState.AddModelError("DuplicateCrypticID", "Duplicate Cryptic ID.");

                    List<SelectListItem> publishedSelectItems = MissionHelper.GetYesNoSelectList();
                    ViewBag.AvailableTags = db.MissionTagTypes.Select(t => t.TagName).ToList();
                    ViewBag.PublishedSelectList = new SelectList(publishedSelectItems, "Value", "Text");
                    return View(missionViewModel);
                }

                ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                Mission mission = new Mission();

                #region Copy Info

                mission.CrypticId = missionViewModel.CrypticId.ToUpper();
                mission.Description = missionViewModel.Description;
                mission.Faction = missionViewModel.Faction;
                mission.Length = missionViewModel.Length;
                mission.MinimumLevel = missionViewModel.MinimumLevel;
                mission.Name = missionViewModel.Name;
                mission.Published = missionViewModel.Published;
                mission.Spotlit = missionViewModel.Spotlit;

                #endregion

                mission.Tags = db.MissionTagTypes.Where(t => missionViewModel.Tags.Contains(t.TagName)).ToList();
                
                mission.MissionLink = MissionHelper.GetMissionLink(db, mission);
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
                        mission.Status = Models.FoundryMissionModels.Enums.MissionStatus.InReview;
                        //mission.Status = Models.FoundryMissionModels.Enums.MissionStatus.Published;
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
                return RedirectToAction("details", new { link = mission.MissionLink });
            }

            return View(missionViewModel);
        }

        public ActionResult Edit(string link)
        {
            if (string.IsNullOrEmpty(link))
            {
                return RedirectToAction("index", "home");
            }
            Mission mission = db.Missions.Where(m => m.MissionLink.Equals(link)).FirstOrDefault();
            if (mission == null)
            {
                return HttpNotFound();
            }

            //only people who can edit a mission are the author or an admin
            if (!mission.Author.UserName.Equals(User.Identity.Name) && !User.IsInRole(ConstantsHelper.AdminRole))
            {

                return HttpNotFound();
            }

            //if the user is not an admin and it is removed it doesn't exist
            if (mission.Status == MissionStatus.Removed && !User.IsInRole(ConstantsHelper.AdminRole))
            {
                return HttpNotFound();
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
            editModel.Status = mission.Status;
            editModel.Author = mission.Author;
            editModel.AutoApprove = mission.Author.AutoApproval;
            editModel.Tags = mission.Tags.Select(t => t.TagName).ToList();
            mission.MissionLink = MissionHelper.GetMissionLink(db, mission);

            ViewBag.AvailableTags = db.MissionTagTypes.Select(t => t.TagName).ToList();
            ViewBag.PublishedSelectList = new SelectList(publishedSelectItems, "Value", "Text");

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [MultipleButton(Name = "action", Argument = "publishmission")]
        public ActionResult PublishMission([Bind(Include = "Id,Author,CrypticId,Name,Description,Length,Faction,MinimumLevel,Spotlit,Published, Tags")] EditMissionViewModel missionViewModel)
        {
            var mission = db.Missions.Find(missionViewModel.Id);
            var author = mission.Author;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            if (author.AutoApproval || UserManager.IsInRole(author.Id, ConstantsHelper.AdminRole))
            {
                missionViewModel.Status = MissionStatus.Published;
            }
            else
            {
                missionViewModel.Status = MissionStatus.InReview;
            }

            return Edit(missionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [MultipleButton(Name = "action", Argument = "savemission")]
        public ActionResult SaveMission([Bind(Include = "Id,Author,CrypticId,Name,Description,Length,Faction,MinimumLevel,Spotlit,Published, Tags")] EditMissionViewModel missionViewModel)
        {
            return Edit(missionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [MultipleButton(Name = "action", Argument = "withdrawmission")]
        public ActionResult WithdrawMission([Bind(Include = "Id,Author,CrypticId,Name,Description,Length,Faction,MinimumLevel,Spotlit,Published, Tags")] EditMissionViewModel missionViewModel)
        {
            missionViewModel.Status = MissionStatus.Unpublished;
            return Edit(missionViewModel);
        }

        private ActionResult Edit(EditMissionViewModel missionViewModel)
        {
            if (ModelState.IsValid)
            {
                var mission = db.Missions.Find(missionViewModel.Id);
                var user = mission.Author;              
                mission.CrypticId = missionViewModel.CrypticId.ToUpper();
                mission.Name = missionViewModel.Name;
                mission.Description = missionViewModel.Description;
                mission.Length = missionViewModel.Length;
                mission.Faction = missionViewModel.Faction;
                mission.MinimumLevel = missionViewModel.MinimumLevel;
                mission.Spotlit = missionViewModel.Spotlit;
                mission.Status = missionViewModel.Status;
                mission.DateLastUpdated = DateTime.Today;
                mission.Tags = db.MissionTagTypes.Where(t => missionViewModel.Tags.Contains(t.TagName)).ToList();
                mission.MissionLink = MissionHelper.GetMissionLink(db, mission);

                db.SaveChanges();

                return RedirectToAction("details", new { link = mission.MissionLink });
            }
            return View(missionViewModel);
        }

        public ActionResult Random()
        {
            var missionLink = db.Missions.OrderBy(m => Guid.NewGuid()).Where(m=> m.Status == MissionStatus.Published).Select(m => m.MissionLink).FirstOrDefault();

            return RedirectToAction("details", new { link = missionLink });
        }

        public ActionResult Search(string q)
        {

            if (string.IsNullOrEmpty(q))
            {
                return RedirectToAction("Index", "Home");
            }

            string upperQuery = q.ToUpper();
            
            var missions = db.Missions.Where(m => (m.Author.UserName.ToUpper().Contains(upperQuery) ||
                                             m.CrypticId.ToUpper().Contains(upperQuery) ||
                                             m.Description.ToUpper().Contains(upperQuery) ||
                                             m.Name.ToUpper().Contains(upperQuery)) &&
                                             m.Status == MissionStatus.Published).ToList();
            
            List<ListMissionViewModel> listMissions = MissionHelper.GetListMissionViewModels(missions);

            return View(listMissions);
        }

        [ActionName("Advanced-Search")]
        public ActionResult AdvancedSearch()
        {
            return View();
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
