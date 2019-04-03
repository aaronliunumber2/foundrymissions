using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionModels.Enums
{
    public enum MissionStatus
    {
        [Display(Name ="Unpublished")]
        Unpublished,
        [Display(Name = "In Review")]
        InReview,
        [Display(Name = "Published")]
        Published,
        [Display(Name = "Removed")]
        Removed,
    }
}