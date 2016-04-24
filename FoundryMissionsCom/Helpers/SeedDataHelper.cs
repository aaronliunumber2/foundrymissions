using FoundryMissionsCom.Models;
using FoundryMissionsCom.Models.FoundryMissionModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Helpers
{
    public class SeedDataHelper
    {

        public static void AddUsersAndRoles(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var usersToAdd = new List<ApplicationUser>();

            //check if zorbane exists
            ApplicationUser zorbane = context.Users.FirstOrDefault(u => u.UserName.Equals("Zorbane"));
            if (zorbane == null)
            {
                zorbane = new ApplicationUser()
                {
                    UserName = "Zorbane",
                    TwitterUsername = "Zorbane",
                    AutoApproval = true,
                    CrypticTag = "Zorbane",
                    Email = "aaron.liu.bc@gmail.com",
                    JoinDate = new DateTime(2016, 4, 20),
                };
                userManager.Create(zorbane, "359Battlewolffoundrymissions");
            }

            usersToAdd.Add(zorbane);
            CreateRole(context, "Administrator", usersToAdd);
        }

        public static List<MissionTagType> GetMissionTagTypes()
        {
            List<MissionTagType> tags = new List<MissionTagType>()
            {
                new Models.FoundryMissionModels.MissionTagType() { TagName = "Space Combat" },
                new Models.FoundryMissionModels.MissionTagType() { TagName = "Ground Combat" },
                new Models.FoundryMissionModels.MissionTagType() { TagName = "Single Player" },
                new Models.FoundryMissionModels.MissionTagType() { TagName = "Team Play" },
                new Models.FoundryMissionModels.MissionTagType() { TagName = "Puzzle" },
                new Models.FoundryMissionModels.MissionTagType() { TagName = "Quarters" },
            };


            return tags;
        }

        private static void CreateRole(ApplicationDbContext context, string role, List<ApplicationUser> usersToAdd)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!RoleManager.RoleExists(role))
            {
                RoleManager.Create(new IdentityRole(role));
            }

            foreach (var user in usersToAdd)
            {
                UserManager.AddToRole(user.Id, role);
            }
        }
    }
}