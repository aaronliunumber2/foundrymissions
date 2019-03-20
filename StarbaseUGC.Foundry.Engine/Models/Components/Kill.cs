using StarbaseUGC.Foundry.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models.Components
{
    class Kill : Component
    {
        public string ChildIDs { get { return GetFieldValue(Constants.Component.Kill.ChildIDs); } }
        public string FSMRef { get { return GetFieldValue(Constants.Component.Kill.FSMRef); } }

        public Kill(int number) : base (number)
        {

        }
    }
}
