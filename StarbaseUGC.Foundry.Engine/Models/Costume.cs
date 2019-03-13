using StarbaseUGC.Foundry.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models
{
    public class Costume : FoundryObject
    {
        public string Description { get { return GetFieldValue(Constants.Costume.Description); } }
        public string DisplayName { get { return GetFieldValue(Constants.Costume.DisplayName); } }
    }
}
