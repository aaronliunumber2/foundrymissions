using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Helpers
{
    public static class MissionCollectionHelper
    {
        private static string[] RestrictedMissionNames = { "SUBMIT", "DETAILS", "RANDOM", "EDIT" };

        public static string GetImageLink(string image, int collectionid)
        {
            return "~/content/collections/images/" + 
                collectionid.ToString() +
                "/" +                
                image;
        }
    }
}