using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Helpers
{
    class Constants
    {
        public const string FoundryObjectStartCharacter = "{";
        public const string FoundryObjectEndCharacter = "}";

        public class FoundryMission
        {
            public const string NameSpace = "NameSpace";
        }

        public class FoundryObject
        {
            public const string Name = "Name";
        }

        public class Project
        {
            public const string Title = "Project";
            public const string AccountName = "AccountName";
            public const string CreationTime = "creationTime";
            public const string FromContainer = "FromContainer";
            public const string PublicName = "PublicName";
            public const string Description = "Description";
            public const string Language = "Language";
            public const string LifetimeTipsReceived = "uLifetimeTipsReceived";
            public const string AverageRating = "AverageRating";

            public class RestrictionProperties
            {
                public const string Title = "RestrictionProperties";
                public const string MinLevel = "MinLevel";
                public const string Faction = "Faction";
            }
        }

        public class Map
        {
            public const string Title = "Map";
            public const string DisplayName = "DisplayName";
            public const string Backdrop = "Backdrop";

            public class Prefab
            {
                public const string Title = "Prefab";
                public const string MapName = "MapName";
            }

            public class Space
            {
                public const string Title = "Space";
            }
        }

        public class Mission
        {
            public const string Title = "Mission";

            public class GrantBlock
            {
                public const string Title = "GrantBlock";
            }

            public class Objective
            {
                public const string Title = "Objective";
                public const string UIString = "UIString";
                public const string WaypointMode = "WaypointMode";
                public const string ComponentMapName = "ComponentMapName";
                public const string ComponentId = "ComponentID";
                public const string InteractText = "InteractText";
                public const string InteractAnim = "InteractAnim";
                public const string InteractDuration = "InteractDuration";
            }

            public class MapLink
            {
                public const string Title = "MapLink";
                public const string DoorComponent = "DoorComponent";
                public const string SpawnComponent = "SpawnComponent";
                public const string SpawnInternalMapName = "SpawnInternalMapName";
                public const string OpenMissionName = "OpenMissionName";

                public class DialogBlock
                {
                    public const string Title = "DialogBlock";
                }
            }
        }

        public class Dialog
        {
            public const string PromptBody = "PromptBody";
            public const string PromptPetCostume = "PromptPetCostume";
            public const string PromptStyle = "PromptStyle";
        }
    }
}
