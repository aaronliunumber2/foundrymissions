using StarbaseUGC.Foundry.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models.Components
{
    class ComponentObject : Component
    {
        public string ObjectID { get { return GetFieldValue(Constants.Component.Object.ObjectID); } }

        public ComponentObject(int number) : base (number)
        {

        }
    }
}
