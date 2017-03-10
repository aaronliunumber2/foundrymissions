using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FoundryMissionsCom.Helpers
{
    public static class MissionCollectionHelper
    {
        public static string GetImageLink(string image, int collectionid)
        {
            return Path.Combine(
                    "~/collections/images/",
                    collectionid.ToString(),
                    image);
        }
    }
}