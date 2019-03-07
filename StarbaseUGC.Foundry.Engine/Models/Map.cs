using StarbaseUGC.Foundry.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models
{
    public class Map : FoundryObject
    {
        public string Name
        {
            get { return Fields[Constants.FoundryObject.Name] as string; }
        }

        public string MapId
        {
            get
            {
                var split = Name.Split(new char[] { ':' })[1];
                return split;
            }
        }

        public string DisplayName { get { return GetFieldValue(Constants.Map.DisplayName); } }


    }

    public abstract class MapSubType : FoundryObject
    {
        public string Backdrop { get { return GetFieldValue(Constants.Map.DisplayName); } }
    }

    public class Prefab : MapSubType
    {

    }

    public class Space : MapSubType
    {
        public string MapName { get { return GetFieldValue(Constants.Map.Prefab.MapName); } }
    }
}
