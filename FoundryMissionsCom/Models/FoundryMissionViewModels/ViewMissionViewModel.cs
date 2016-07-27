using FoundryMissionsCom.Models.FoundryMissionModels;
using FoundryMissionsCom.Models.FoundryMissionModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionViewModels
{
    public class ViewMissionViewModel
    {
        public int Id { get; set; }

        public ApplicationUser Author { get; set; }

        public string CrypticId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Faction Faction { get; set; }

        public string FactionImageUrl { get; set; }

        public int MinimumLevel { get; set; }

        public string MinimumLevelImageUrl { get; set; }

        public MissionStatus Status { get; set; }

        public DateTime DateLastUpdated { get; set; }

        public MissionLength Length { get; set; }

        public List<MissionTagType> Tags { get; set; }

        public List<string> Videos { get; set; }

        public List<string> Images { get; set; }

        public string MissionLink { get; set; }
    }
}