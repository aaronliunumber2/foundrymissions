using StarbaseUGC.Foundry.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models.Components
{
    public class Contact : Component
    {
        public string CostumeName { get { return GetFieldValue(Constants.Component.Contact.CostumeName); } }
        public string FSMRef { get { return GetFieldValue(Constants.Component.Contact.FSMRef); } }

        public Contact(int number) : base (number)
        {

        }
    }
}
