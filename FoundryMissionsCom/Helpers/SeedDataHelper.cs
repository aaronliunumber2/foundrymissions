using FoundryMissionsCom.Models;
using FoundryMissionsCom.Models.FoundryMissionModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FoundryMissionsCom.Helpers
{
    public class SeedDataHelper
    {
        private static string[] DefaultTags = { "Story", "Combat", "No Combat", "Space", "Ground", "Small Craft", "Puzzles", "Diplomacy", "Exploration",
                                         "Time Travel", "Orions", "Klingons", "Tholians", "Na'Kuhl", "Mirror Federation", "Nausicaans", "Borg",
                                         "True Way", "Cardassians", "Iconians", "Custom Aliens", "Romulans", "Remans", "Gorn", "Q", "Federation", "Voth"};

        internal static void AddUsersAndRoles(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var usersToAdd = new List<ApplicationUser>();

            usersToAdd.Add(CreateAdminUser("Zorbane", "Zorbane", "Zorbane", "aaron.liu.bc@gmail.com", context, userManager));
            usersToAdd.Add(CreateAdminUser("RogueEnterprise", "RogueEnterprise", "RogueEnterprise", "sconom@gmail.com", context, userManager));

            CreateRole(context, "Administrator", usersToAdd);
        }

        private static ApplicationUser CreateAdminUser(string username, string twitter, string cryptic, string email, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {

            //check if user exists
            ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = username,
                    TwitterUsername = twitter,
                    AutoApproval = true,
                    CrypticTag = cryptic,
                    Email = email,
                    JoinDate = new DateTime(2016, 4, 20),
                };
                userManager.Create(user, "password");
            }

            return user;
        }

        internal static List<MissionTagType> GetMissionTagTypes()
        {
            List<MissionTagType> tags = new List<MissionTagType>();
            foreach(var tag in DefaultTags)
            {
                tags.Add(new MissionTagType() { TagName = tag });

            }

            return tags;
        }

        internal static void SetMissionLinks(ApplicationDbContext context)
        {
            var noLinkMissions = context.Missions.Where(m => string.IsNullOrEmpty(m.MissionLink)).ToList();

            foreach (var mission in noLinkMissions)
            {
                mission.MissionLink = MissionHelper.GetMissionLink(context, mission);
                context.SaveChanges();
            }
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
                if (!UserManager.IsInRole(user.Id, role))
                {
                    UserManager.AddToRole(user.Id, role);
                }
            }
        }
    }
}