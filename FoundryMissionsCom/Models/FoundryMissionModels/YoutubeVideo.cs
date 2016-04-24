using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionModels
{
    public class YoutubeVideo
    {
        public string YoutubeVideoId { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
    }
}