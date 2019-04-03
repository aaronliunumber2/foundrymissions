using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionModels.Enums
{
    public enum MissionLength
    {
        [Description("Less than 15 minutes")]
        [Display(Name="Less than 15 minutes")]
        LessThanFifteenMinutes,
        [Description("15-30 minutes")]
        [Display(Name = "15-30 minutes")]
        FifteenToThirtyMinutes,
        [Description("30 minutes - 1 hour")]
        [Display(Name = "30 minutes - 1 hour")]
        ThirtyMinutesToOneHour,
        [Description("1 hour - 2 hours")]
        [Display(Name = "1 hour - 2 hours")]
        OneToTwoHours,
        [Description("More than 2 hours")]
        [Display(Name = "More than 2 hours")]
        OverTwoHours,
    }
}