using FoundryMissionsCom.Models;
using FoundryMissionsCom.Models.FoundryMissionModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;

namespace FoundryMissionsCom.Helpers
{
    public static class EmailHelper
    {
        public static void ReportNewUser(ApplicationUser user)
        {
            SendEmail(GetAdminEmails(), "New User Registered", "<p>" + user.UserName + " has registered to the website.</p>");
        }

        public static void ReportMissionNeedsApproval(Mission mission)
        {
            var missionLink = "http://www.foundrymissions.com/missions/" + mission.MissionLink;

            SendEmail(GetAdminEmails(), "Mission " + mission.Name + " awaits approval", "<p>" + mission.Name + " awaits approval.</p><p><a href=\"" + missionLink + "\">Mission Link</a></p>");
        }

        public static void ReportApprovedMission(Mission mission)
        {
        }

        private static List<ApplicationUser> GetAdminEmails()
        {
            var ctx = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));
            var adminUsers = roleManager.Roles.Where(r => r.Name.Equals(ConstantsHelper.AdminRole)).FirstOrDefault().Users.Select(u => u.UserId);

            return ctx.Users.Where(u => adminUsers.Contains(u.Id)).ToList();
        }

        private static void SendEmail(List<ApplicationUser> recipients, string subject, string body)
        {


            foreach (var recipient in recipients)
            {
                var fullBody = "<p>Hello " + recipient.UserName + ",</p>" + body;
                var message = new MailMessage();
                message.To.Add(recipient.Email);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                }
            }
        }
    }
}