using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoundryMissionsCom.Models.FoundryMissionViewModels
{
    public class ListAuthorViewModel
    {
        public string Username { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name="Date Registered")]
        [DataType(DataType.Date)]
        public DateTime DateRegistered { get; set; }
        public bool Admin { get; set; }

        [Display(Name = "Auto Approve")]
        public bool AutoApprove { get; set; }

        [Display(Name = "Locked Out")]
        public bool Lockedout { get; set; }
    }
}