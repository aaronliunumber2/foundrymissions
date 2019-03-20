using StarbaseUGC.Foundry.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models.Components
{
    public class RoomMarker : Component
    {
        public string VolumeRadius { get { return GetFieldValue(Constants.Component.RoomMarker.VolumeRadius); } }

        public RoomMarker(int number) : base(number)
        {

        }
    }
}
