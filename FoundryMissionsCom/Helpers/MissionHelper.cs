using FoundryMissionsCom.Models;
using FoundryMissionsCom.Models.FoundryMissionModels;
using FoundryMissionsCom.Models.FoundryMissionModels.Enums;
using FoundryMissionsCom.Models.FoundryMissionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace FoundryMissionsCom.Helpers
{
    public static class MissionHelper
    {
        private static string[] RestrictedMissionNames = { "SUBMIT", "DETAILS", "RANDOM", "EDIT", "SEARCH", "ADVANCED-SEARCH", "SEARCHRESULTS" };

        /// <summary>
        /// Get what the mission's link should be
        /// </summary>
        /// <param name="mission"></param>
        /// <returns>Link</returns>
        public static string GetMissionLink(ApplicationDbContext context, Mission mission)
        {
            var link = mission.Name;
   
            var counter = 1;
            link = Regex.Replace(link, @"\p{P}", ""); //remove punctuation
            link = link.Replace(' ', '-'); // change spaces
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

        public static string GetSmallLevelImageUrl(int minimumLevel, Faction faction)
        {
            var url = "small_";

            if (minimumLevel >= 60)
            {
                url += "fleetadmiral_";
            }
            else if (minimumLevel >= 50)
            {
                url += "viceadmiral_";
            }
            else if (minimumLevel >= 40)
            {
                url += "rearadmiral_";
            }
            else if (minimumLevel >= 30)
            {
                url += "captain_";
            }
            else if (minimumLevel >= 20)
            {
                url += "commander_";
            }
            else if (minimumLevel >= 10)
            {
                url += "ltcommander_";
            }
            else
            {
                url += "lieutenant_";
            }

            url += faction.ToString().ToLower();

            url += ".png";

            return url;
        }

        public static string GetSmallFactionImageUrl(Faction faction)
        {
            var url = "small_" + faction.ToString().ToLower() + ".png";

            return url;
        }

        public static string GetBigLevelImageUrl(int minimumLevel, Faction faction)
        {
            return "big_captain_federation.png";
        }

        public static string GetBigFactionImageUrl(Faction faction)
        {
            return "big_federation.png";
        }

        public static string GetTagImageUrl(MissionTagType tag)
        {
            return tag.TagName.ToLower().Replace(' ', '-') + ".jpg";
        }

        public static List<ListMissionViewModel> GetListMissionViewModels(List<Mission> missions)
        {
            List<ListMissionViewModel> listMissions = new List<ListMissionViewModel>();
            foreach (var mission in missions)
            {
                var listMission = new ListMissionViewModel()
                {
                    Id = mission.Id,
                    Name = mission.Name,
                    CrypticId = mission.CrypticId,
                    Author = mission.Author,
                    MinimumLevel = mission.MinimumLevel,
                    Faction = mission.Faction,
                    DateLastUpdated = mission.DateLastUpdated,
                    FactionImageUrl = GetSmallFactionImageUrl(mission.Faction),
                    LevelImageUrl = GetSmallLevelImageUrl(mission.MinimumLevel, mission.Faction),
                    MissionLink = mission.MissionLink,
                    Status = mission.Status
                };

                listMissions.Add(listMission);
            }

            return listMissions;
        }

        public static List<SelectListItem> GetYesNoSelectList()
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
            return publishedSelectItems;
        }
    }
}