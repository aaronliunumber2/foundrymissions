using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionViewModels
{
    public class SubmitCollectionViewModel
    {
        [Key]
        public int Key { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}