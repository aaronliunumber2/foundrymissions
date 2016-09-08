using FoundryMissionsCom.Models.FoundryMissionModels;
using FoundryMissionsCom.Models.FoundryMissionModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoundryMissionsCom.Models.FoundryMissionViewModels
{
    public class SubmitMissionViewModel
    {
        [Required]
        [Display(Name = "Cryptic Id")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Cryptic Id must be 9 characters long.")]
        [RegularExpression(@"(^[0-9a-zA-Z]*$)", ErrorMessage = "Cryptic Id must only contain uppercase alphanumeric characters.")]
        public string CrypticId { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Mission title can only be up to 32 characters long.")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public MissionLength Length { get; set; }

        [Required]
        public Faction Faction { get; set; }

        [Required]
        [Range(1, 60, ErrorMessage = "Minimum level must be between 1 and 60.")]
        [Display(Name = "Minimum Level")]
        public int MinimumLevel { get; set; }

        [Required]
        public bool Spotlit { get; set; }

        [Required]
        public bool Published { get; set; }

        public List<string> Tags { get; set; }

        public List<HttpPostedFileBase> Images { get; set; }

        public List<string> Videos { get; set; }

        public SubmitMissionViewModel()
        {
        }
    }
}