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
        public static char[] SplitSpace = new char[] { ' ' };

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

        public class Component
        {
            public const string Title = "Component";
            public const string VisibleName = "VisibleName";
            public const string Type = "Type";
            public const string MapType = "MapType";            
            public const string DisplayNameWasFixed = "DisplayNameWasFixed";
            public const string RoomDoor = "RoomDoor";

            #region Component Types
            /*
            Component Types

            OBJECT
            DIALOG_TREE
            WHOLE_MAP
            KILL
            ACTOR
            CONTACT
            PATROL_POINT
            ROOM_MARKER
            EXTERNAL_DOOR

            Also a unnamed one which I believe is a spawn point
            */

            public class Spawn
            {
                public const string Title = "SPAWN";
            }

            public class WholeMap
            {
                public const string Title = "WHOLE_MAP";
            }

            public class ExternalDoor
            {
                public const string Title = "EXTERNAL_DOOR";
            }

            public class RoomMarker
            {
                public const string Title = "ROOM_MARKER";
                public const string VolumeRadius = "VolumeRadius";               
            }

            public class Contact
            {
                public const string Title = "CONTACT";
                public const string CostumeName = "CostumeName";
                public const string FSMRef = "FSMRef";

            }

            public class Actor
            {
                public const string Title = "Actor";
                public const string CostumeName = "CostumeName";
            }

            public class Object
            {
                public const string Title = "OBJECT";
                public const string ObjectID = "ObjectID";
            }

            public class Kill
            {
                public const string Title = "KILL";

                public const string ChildIDs = "ChildIDs";
                public const string FSMRef = "FSMRef";
            }

            public class DialogTree
            {
                public const string Title = "DIALOG_TREE";
                
                public class Prompt
                {
                    public const string Title = "Prompt";
                }
            }

            #endregion

            public class InteractTriggerGroup
            {
                public const string Title = "InteractTriggerGroup";
                public const string InteractText = "InteractText";
                public const string InteractAnim = "InteractAnim";
                public const string InteractDuration = "InteractDuration";
            }

            public class Placement
            {
                public const string Title = "Placement";
                public const string MapName = "MapName";
                public const string RoomID = "RoomID";
                public const string Position = "Position";
                public const string Rotation = "Rotation";
                public const string RoomLevel = "RoomLevel";
                public const string Snap = "Snap";
            }

            public class When
            {
                public const string Title = "When";
            }

            public class ShowWhen
            {
                public const string Title = "ShowWhen";
            }

            public class HideWhen
            {
                public const string Title = "HideWhen";
            }

            public class ExternalVar
            {
                public const string Title = "ExternalVar";
                public const string End = "End";
                public const string Type = "Type";

                public class SpecificValue
                {
                    public const string Title = "SpecificValue";
                    public const string End = "End";

                    public const string Type = "Type";
                    public const string FloatVal = "FloatVal";
                    public const string StringVal = "StringVal";
                }
            }
        }

        public class Costume
        {
            public const string Title = "Costume";
            public const string Description = "Description";
            public const string DisplayName = "DisplayName";

            public class CachedPlayerCostume
            {
                public const string Skeleton = "Skeleton";
                public const string Species = "Species";
                public const string CostumeType = "CostumeType";
                public const string DefaultColorLinkAll = "DefaultColorLinkAll";
                public const string Height = "Height";
                public const string ColorSkin = "ColorSkin";
                public const string ReegionCategory = "RegionCategory";
                public const string RegionCategoryAll = "RegionCategory All";
                public const string RegionCategoryHidden = "RegionCategory Hidden";
            }
                
            public class Part
            {
                public class Movable
                {


                }

                public class CustumeColors
                {

                }
            }

        }

        public class Dialog
        {
            public const string PromptBody = "PromptBody";
            public const string PromptPetCostume = "PromptPetCostume";
            public const string PromptStyle = "PromptStyle";
            public const string PromptTitle = "PromptTitle";
            public const string PromptCostume = "PromptCostume";

            public class Action
            {
                public const string Title = "Action";
                public const string NextPromptID = "NextPromptID";
            }
        }

        public class Trigger
        {
            public const string When = "When";
            public const string HideWhen = "HideWhen";

            public const string MapStart = "MAP_START";
            public const string Manual = "MANUAL";
            public const string ObjectiveStart = "OBJECTIVE_START";
            public const string MissionStart = "MISSION_START";


            public class Component
            {
                public const string ComponentReachedTitle = "COMPONENT_REACHED";
                public const string ComponentCompleteTitle = "COMPONENT_COMPLETE";
                public const string CurrentComponentComplete = "CURRENT_COMPONENT_COMPLETE";
                public const string ComponentID = "ComponentID";
            }

            public class ObjectiveComplete
            {
                public const string Title = "OBJECTIVE_COMPLETE";
                public const string ObjectiveID = "ObjectiveID";
            }

            public class DialogPromptReached
            {
                public const string Title = "DIALOG_PROMPT_REACHED";
            }

            public class DialogLink
            {                
                public const string Title = "DialogPrompt";
                public const string DialogID = "DialogID";
                public const string PromptID = "PromptID";
            }
        }
    }
}
