using FoundryMissionsCom.Models.FoundryMissionModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionViewModels
{
    public class EditMissionViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Cryptic Id")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Cryptic Id must be 9 characters long.")]
        [RegularExpression("^[A-Z0-9_]*$", ErrorMessage = "Cryptic Id must only contain uppercase alphanumeric characters.")]
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

        public ApplicationUser Author { get; set; }
        public bool AutoApprove { get; set; }
        public MissionStatus Status { get; set; }
    }
}