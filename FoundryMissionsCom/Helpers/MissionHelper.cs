using FoundryMissionsCom.Models;
using FoundryMissionsCom.Models.FoundryMissionModels;
using FoundryMissionsCom.Models.FoundryMissionModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FoundryMissionsCom.Helpers
{
    public static class MissionHelper
    {
        private static string[] RestrictedMissionNames = { "SUBMIT", "DETAILS", "RANDOM", "EDIT", "SEARCH", "ADVSEARCH" };

        /// <summary>
        /// Get what the mission's link should be
        /// </summary>
        /// <param name="mission"></param>
        /// <returns>Link</returns>
        public static string GetMissionLink(ApplicationDbContext context, Mission mission)
        {
            var link = mission.Name;
   
            var counter = 1;
            link = link.Replace(' ', '-'); // change spaces
            link = Regex.Replace(link, @"\p{P}", ""); //remove punctuation
            link = link.ToLower();//lowercase

            var originalLink = link;


            if (MissionHelper.IsRestrictedMissionName(link))
            {
                link = originalLink + counter;
                counter++;
            }

            //check if the link already exists, and isn't this mission
            while (context.Missions.Any(m => m.MissionLink.Equals(link) && m.Id.Equals(mission.Id)))
            {
                link = originalLink + counter;
                counter++;
            }

            return link;            
        }

        public static bool IsRestrictedMissionName(string name)
        {
            return RestrictedMissionNames.Contains(name.ToUpper());
        }

        public static string GetLevelImageUrl(int minimumLevel, Faction faction)
        {
            return "";
        }

        public static string GetFactionImageUrl(Faction faction)
        {
            return "";
        }
    }
}