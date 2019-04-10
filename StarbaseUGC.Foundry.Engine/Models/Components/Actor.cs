using StarbaseUGC.Foundry.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models.Components
{
    class Actor : Component
    {
        [System.ComponentModel.DefaultValue("")]
        public string CostumeName { get { return GetFieldValue(Constants.Component.Actor.CostumeName); } }

        public Actor(int number) : base (number)
        {

        }
    }
}
