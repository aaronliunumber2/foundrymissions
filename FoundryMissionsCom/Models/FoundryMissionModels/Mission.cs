using FoundryMissionsCom.Models.FoundryMissionModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionModels
{
    public class Mission
    {
        [Key]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string AuthorUserId { get; set; }
        [ScaffoldColumn(false)]
        public virtual ApplicationUser Author { get; set; }

        [Required]
        [Display(Name = "Cryptic Id")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Cryptic Id must be 9 characters long.")]
        [Index(IsUnique = true)]
        [RegularExpression("^[A-Z0-9_]*$", ErrorMessage = "Cryptic Id must only contain uppercase alphanumeric characters.")]
        public string CrypticId { get; set; }

        [Required]
        [Index(IsUnique = true)]
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
        [Display(Name="Minimum Level")]
        public int MinimumLevel { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Date Added")]
        [ScaffoldColumn(false)]
        public DateTime DateAdded { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Last Updated")]
        [ScaffoldColumn(false)]
        public DateTime DateLastUpdated { get; set; }

        [Required]
        public bool Spotlit { get; set; }

        [Required]
        public bool Published { get; set; }
        
        [Required]
        public MissionStatus Status { get; set; }

        [ScaffoldColumn(false)]
        public string MissionLink { get; set; }

        public virtual List<MissionTagType> Tags { get; set; }
        public virtual List<YoutubeVideo> Videos { get; set; }
    }
}