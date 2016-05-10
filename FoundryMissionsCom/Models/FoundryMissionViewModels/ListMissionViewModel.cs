using FoundryMissionsCom.Models.FoundryMissionModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionViewModels
{
    public class ListMissionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Cryptic Id")]
        public string CrypticId { get; set; }

        [Display(Name ="Author")]
        public ApplicationUser Author { get; set; }

        [Display(Name = "Minimum Level")]
        public int MinimumLevel { get; set; }

        public string LevelImageUrl { get; set; }

        public Faction Faction { get; set; }

        public string FactionImageUrl { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last Updated")]
        public DateTime DateLastUpdated { get; set; }

        public string MissionLink { get; set; }
    }
}