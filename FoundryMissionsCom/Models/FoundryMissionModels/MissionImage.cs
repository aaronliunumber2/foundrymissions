using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionModels
{
    public class MissionImage
    {
        [Key, Column(Order = 0)]
        public int MissionId { get; set; }
        [Key, Column(Order = 1)]
        public string Filename { get; set; }

        public int Order { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
    }
}