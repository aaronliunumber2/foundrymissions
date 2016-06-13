using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FoundryMissionsCom.Models;
using System.Collections.Generic;
using System.Net;
using FoundryMissionsCom.Helpers;
using FoundryMissionsCom.Models.FoundryMissionModels;
using FoundryMissionsCom.Models.FoundryMissionModels.Enums;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FoundryMissionsCom.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public ActionResult Index()
        {
            var model = new ManageIndexViewModel();

            var missions = db.Missions.Where(m => m.Author.UserName.Equals(User.Identity.Name) && m.Status != MissionStatus.Removed).ToList();
            model.Missions = MissionHelper.GetListMissionViewModels(missions);

            if (User.IsInRole(ConstantsHelper.AdminRole))
            {
                model.MissionsToApprove = MissionHelper.GetListMissionViewModels(db.Missions.Where(m => m.Status == Models.FoundryMissionModels.Enums.MissionStatus.InReview).OrderBy(m => m.DateLastUpdated).ToList());
                model.Authors = AccountHelper.GetAuthorViewModels(db.Users.OrderBy(u => u.UserName).ToList(), db);
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult SubmitMission(int id) 
        {
            //check if the user is auto approved, if so set it to published, otherwise set it to review
            string userId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id.Equals(userId));

            if (user == null)
            {
                return Json(new { success = "false", message = "Unable to find user info." });
            }

            if (user.AutoApproval || User.IsInRole(ConstantsHelper.AdminRole))
            {
                return SetMissionStatus(id, MissionStatus.Published);
            }
            else
            {
                return SetMissionStatus(id, MissionStatus.InReview);
            }
        }

        [HttpPost]
        public JsonResult WithdrawMission(int id)
        {
            return SetMissionStatus(id, MissionStatus.Unpublished);
        }

        private JsonResult SetMissionStatus(int id, MissionStatus status)
        {
            Mission mission = db.Missions.FirstOrDefault(m => m.Id.Equals(id));

            if (mission == null)
            {
                return Json(new { success = "false", message = "Unable to find mission." });
            }

            //need to check if the user is allowed to change the mission status.  Either mission owner or role is admin
            if (mission.Author.Id.Equals(User.Identity.GetUserId()) || User.IsInRole(ConstantsHelper.AdminRole))
            {
                mission.Status = status;
                db.SaveChanges();
                return Json(new { success = "true", message = status.DisplayName() });
            }
            else
            {
                return Json(new { success = "false", message = "Unauthorized User" });
            }
        }

        [HttpPost]
        [Authorize(Roles = ConstantsHelper.AdminRole)]
        public JsonResult ApproveMission(int id)
        {
            Mission mission = db.Missions.FirstOrDefault(m => m.Id.Equals(id));

            if (mission == null)
            {
                return Json("Unable to find mission.");
            }

            mission.Status = MissionStatus.Published;
            db.SaveChanges();

            return Json("Success");
        }

        [HttpPost]
        [Authorize(Roles = ConstantsHelper.AdminRole)]
        public JsonResult DenyMission(int id)
        {
            Mission mission = db.Missions.FirstOrDefault(m => m.Id.Equals(id));

            if (mission == null)
            {
                return Json("Unable to find mission.");
            }

            mission.Status = MissionStatus.Unpublished;
            db.SaveChanges();

            return Json("Success");
        }

        [HttpPost]
        [Authorize(Roles = ConstantsHelper.AdminRole)]
        public JsonResult SetRole(string username, string role, bool inRole)
        {
            //never change Zorbane or RogueEnterprise
            if (username.Equals("Zorbane") ||
                username.Equals("RogueEnterprise"))
            {
                return Json(new { success = "false", message = ConstantsHelper.ErrorReservedUser });
            }

            var user = db.Users.FirstOrDefault(u => u.UserName.Equals(username));

            if (user == null)
            {
                return Json(new { success = "false", message = "Unable to find user " + username });
            }

            var userId = user.Id;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var isInRole = userManager.IsInRole(userId, role);

            if (inRole && !isInRole)
            {
                userManager.AddToRole(userId, role);
            }
            else if (!inRole && isInRole)
            {
                userManager.RemoveFromRole(userId, role);
            }

            return Json(new { success = "true", inrole = inRole.ToString().ToLower() });
        }

        [HttpPost]
        [Authorize(Roles = ConstantsHelper.AdminRole)]
        public JsonResult SetAutoApprove(string username, bool autoapprove)
        {
            //never change Zorbane or RogueEnterprise
            if (username.Equals("Zorbane") ||
                username.Equals("RogueEnterprise"))
            {
                return Json(new { success = "false", message = ConstantsHelper.ErrorReservedUser });
            }

            var user = db.Users.FirstOrDefault(u => u.UserName.Equals(username));

            if (user == null)
            {
                return Json(new { success = "false", message = "Unable to find user " + username });
            }

            user.AutoApproval = autoapprove;
            db.SaveChanges();

            return Json(new { success = "true", autoapprove = autoapprove.ToString().ToLower() });
        }

        [HttpPost]
        [Authorize(Roles = ConstantsHelper.AdminRole)]
        public JsonResult SetLockout(string username, bool lockout)
        {
            //never change Zorbane or RogueEnterprise
            if (username.Equals("Zorbane") ||
                username.Equals("RogueEnterprise"))
            {
                return Json(new { success = "false", message = ConstantsHelper.ErrorReservedUser });
            }

            var user = db.Users.FirstOrDefault(u => u.UserName.Equals(username));

            if (user == null)
            {
                return Json(new { success = "false", message = "Unable to find user " + username });
            }

            user.LockoutEnabled = lockout;
            db.SaveChanges();

            return Json(new { success = "true", autoapprove = lockout.ToString().ToLower() });
        }


        #region Two Factor Authentication
        /*
        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }
        */
        #endregion

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangePassword(string oldPassword, string newPassword)
        {

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), oldPassword, newPassword);
            var errors = new List<string>();

            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return Json(new { success = "true", message = "Your password has been changed." } );
            }

            foreach (var error in result.Errors)
            {
                errors.Add(error);
            }

            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Json(new { success = "false", message = errors });
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}