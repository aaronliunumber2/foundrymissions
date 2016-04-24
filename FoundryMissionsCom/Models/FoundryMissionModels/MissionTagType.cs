using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionModels
{
    public class MissionTagType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Tag Name")]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string TagName { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }

        public override string ToString()
        {
            return TagName;
        }
    }
}