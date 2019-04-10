using StarbaseUGC.Foundry.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models
{
    public class Mission : FoundryObject
    {
        public string Name
        {
            get { return Fields[Constants.FoundryObject.Name] as string; }
        }

        public GrantBlock GrantBlock { get { return (GrantBlock)FoundryObjects.Find(f => f.Title.Equals(Constants.Mission.GrantBlock.Title)); } }

        public List<Objective> Objectives { get { return GetFoundryObjectsByTitle(Constants.Mission.Objective.Title).Cast<Objective>().OrderBy(o => o.Number).ToList(); } }        

        public List<MapLink> MapLinks { get { return GetFoundryObjectsByTitle(Constants.Mission.MapLink.Title).Cast<MapLink>().ToList();  } }
    }

    public class GrantBlock : Dialog
    {

    }

    public class Objective : FoundryObject
    {
        public int Number { get; private set; }

        public Objective(int objectiveNumber)
        {
            Number = objectiveNumber;
        }

        public string UIString { get { return GetFieldValue(Constants.Mission.Objective.UIString); } }
        public string WaypointMode { get { return GetFieldValue(Constants.Mission.Objective.WaypointMode); } }
        public string ComponentMapName { get { return GetFieldValue(Constants.Mission.Objective.ComponentMapName); } }
        public string ComponentId { get { return GetFieldValue(Constants.Mission.Objective.ComponentId); } }
        [System.ComponentModel.DefaultValue("")]
        public string InteractText { get { return GetFieldValue(Constants.Mission.Objective.InteractText); } }
        [System.ComponentModel.DefaultValue("")]
        public string InteractAnim { get { return GetFieldValue(Constants.Mission.Objective.InteractAnim); } }
        [System.ComponentModel.DefaultValue("")]
        public string InteractDuration { get { return GetFieldValue(Constants.Mission.Objective.InteractDuration); } }
    }

    public class MapLink : FoundryObject
    {
        public string DoorComponent { get { return GetFieldValue(Constants.Mission.MapLink.DoorComponent); } }
        public string SpawnComponent { get { return GetFieldValue(Constants.Mission.MapLink.SpawnComponent); } }
        public string SpawnInternalMapName { get { return GetFieldValue(Constants.Mission.MapLink.SpawnInternalMapName); } }
        public string OpenMissionName { get { return GetFieldValue(Constants.Mission.MapLink.OpenMissionName); } }

        public DialogBlock DialogBlock {  get { return (DialogBlock)GetFoundryObjectByTitle(Constants.Mission.MapLink.DialogBlock.Title); } }
    }

    public class DialogBlock : Dialog
    {

    }
}

