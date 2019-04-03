using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionModels
{
    public class MissionCollection
    {
        [Key]
        public int Id { get; set; }    
        public string Name { get; set; }
        public string OwnerTag { get; set; }


    }
}