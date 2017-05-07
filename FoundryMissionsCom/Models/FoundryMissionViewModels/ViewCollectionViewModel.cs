using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionViewModels
{
    public class ViewCollectionViewModel
    {
        public int Id { get; set; }
        public string AuthorTag { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ListMissionViewModel> Missions { get; set; }
        public string ImageLink { get; set; }
        public string CollectionLink { get; set; }
    }
}