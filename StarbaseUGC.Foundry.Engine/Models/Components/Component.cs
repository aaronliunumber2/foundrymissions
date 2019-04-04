using StarbaseUGC.Foundry.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models.Components
{
    public class Component : FoundryObject
    {
        public int Number { get; private set; }
        public string Type { get { return GetFieldValue(Constants.Component.Type); } }
        public string MapType { get { return GetFieldValue(Constants.Component.MapType); } }
        public string DisplayNameWasFixed { get { return GetFieldValue(Constants.Component.DisplayNameWasFixed); } }
        public string VisibleName { get { return GetFieldValue(Constants.Component.VisibleName); } }
        public string RoomDoor { get { return GetFieldValue(Constants.Component.RoomDoor); } }

        public List<Trigger> When { get; } = new List<Trigger>();
        public List<Trigger> HideWhen { get; } = new List<Trigger>();

        public Placement Placement { get { return (Placement)GetFoundryObjectByTitle(Constants.Component.Placement.Title); } }
        public InteractTriggerGroup InteractTriggerGroup { get { return (InteractTriggerGroup)GetFoundryObjectByTitle(Constants.Component.InteractTriggerGroup.Title); } }
    


        public Component(int number)
        {
            Number = number;
        }
    }

    public class Placement : FoundryObject
    {
        public string MapName { get { return GetFieldValue(Constants.Component.Placement.MapName); } }
        public string RoomID { get { return GetFieldValue(Constants.Component.Placement.RoomID); } }
        public string Position { get { return GetFieldValue(Constants.Component.Placement.Position); } }
        public string Rotation { get { return GetFieldValue(Constants.Component.Placement.Rotation); } }
        public string RoomLevel { get { return GetFieldValue(Constants.Component.Placement.RoomLevel); } }
        public string Snap { get { return GetFieldValue(Constants.Component.Placement.Snap); } }
    }

    public class InteractTriggerGroup : FoundryObject
    {
        public string InteractText { get { return GetFieldValue(Constants.Component.InteractTriggerGroup.InteractText); } }
        public string InteractAnim { get { return GetFieldValue(Constants.Component.InteractTriggerGroup.InteractAnim); } }
        public string InteractDuration { get { return GetFieldValue(Constants.Component.InteractTriggerGroup.InteractDuration); } }
    }
}
