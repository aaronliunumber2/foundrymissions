using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Models.FoundryMissionViewModels
{
    public class ListCollectionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ApplicationUser Owner { get; set; }
        public string ImageLink { get; set; }
        public string CollectionLink { get; set; }

    }
}