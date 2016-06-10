using FoundryMissionsCom.Models;
using FoundryMissionsCom.Models.FoundryMissionViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Helpers
{
    public static class AccountHelper
    {
        public static List<ListAuthorViewModel> GetAuthorViewModels(List<ApplicationUser> users, ApplicationDbContext context)
        {
            List<ListAuthorViewModel> authors = new List<ListAuthorViewModel>();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            foreach (var user in users)
            {
                var author = new ListAuthorViewModel()
                {
                    Email = user.Email,
                    DateRegistered = user.JoinDate,
                    Admin = userManager.IsInRole(user.Id, ConstantsHelper.AdminRole),
                    AutoApprove = user.AutoApproval,
                    Username = user.UserName,
                    Lockedout = user.LockoutEnabled
                };

                authors.Add(author);
            }

            return authors;
        }
    }
}