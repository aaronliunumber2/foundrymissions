using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionViewModels
{
    public class RandomMissionImageViewModel
    {
        public string ImageLink { get; set; }
        public string ThumbnailLink { get; set; }
        public string MissionName { get; set; }
        public string MissionLink { get; set; }
        public int MissionId { get; set; }
        public string Author { get; set; }
        public string Faction { get; set; }
    }
}