using StarbaseUGC.Foundry.Engine.Models;
using StarbaseUGC.Foundry.Engine.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Helpers
{
    internal static class FoundryObjectFactory
    {
        internal static FoundryObject CreateFoundryObject(string title, List<string> importLines, int currentIndex)
        {
            switch (title)
            {
                #region Project
                case Constants.Project.Title:
                    return new Project();
                case Constants.Project.RestrictionProperties.Title:
                    return new RestrictionProperties();
                #endregion
                #region Map
                case Constants.Map.Title:
                    return new Map();
                case Constants.Map.Prefab.Title:
                    return new Prefab();
                case Constants.Map.Space.Title:
                    return new Space();
                #endregion
                #region Mission
                case Constants.Mission.Title:
                    return new Mission();
                case Constants.Mission.GrantBlock.Title:
                    return new GrantBlock();
                case Constants.Mission.MapLink.Title:
                    return new MapLink();
                case Constants.Mission.MapLink.DialogBlock.Title:
                    return new DialogBlock();
                #endregion
                #region Component
                case Constants.Component.Placement.Title:
                    return new Placement();
                case Constants.Component.InteractTriggerGroup.Title:
                    return new InteractTriggerGroup();
                #endregion
                #region Triggers
                case Constants.Trigger.Component.ComponentCompleteTitle:
                case Constants.Trigger.Component.ComponentReachedTitle:
                    return new ComponentTrigger(title);                                
                case Constants.Trigger.ObjectiveComplete.Title:
                    return new ObjectiveCompleteTrigger();
                case Constants.Trigger.DialogPromptReached.Title:
                    return new DialogPromptReachedTrigger();
                case Constants.Trigger.DialogLink.Title:
                    return new DialogPromptTriggerLink();
                #endregion
                #region Costume
                case Constants.Costume.Title:
                    return new Costume();
                #endregion
                default:
                    //special case stuff
                    #region Objective
                    if (title.Contains(Constants.Mission.Objective.Title))
                    {
                        //get the objective number
                        var split = title.Split(new char[] { ' ' });
                        var strnumber = split[1];
                        var number = Convert.ToInt32(strnumber);
                        return new Objective(number);
                    }
                    #endregion
                    #region Prompt
                    else if (title.Contains(Constants.Component.DialogTree.Prompt.Title))
                    {
                        
                        var split = title.Split(new char[] { ' ' });
                        var strnumber = split[1];
                        var number = Convert.ToInt32(strnumber);
                        return new Prompt(number);
                    }
                    #endregion
                    #region Component
                    else if (title.Contains(Constants.Component.Title))
                    {
                        return GetComponent(title, importLines, currentIndex);
                    }
                    #endregion
                    #region Dialog                
                    else if (title.Contains(Constants.Dialog.Action.Title))
                    {
                        //get the action name
                        var name = title.Trim().Replace(Constants.Dialog.Action.Title, "");
                        return new DialogAction(name);
                    }
                    #endregion
                    return new FoundryObject();
            }
        }

        private  static Component GetComponent(string title, List<string> importLines, int currentIndex)
        {
            //get the component number
            var split = title.Split(new char[] { ' ' });
            var strnumber = split[1];
            var number = Convert.ToInt32(strnumber);
            //need to get the type 
            var componentType = GetComponentType(importLines, currentIndex);
            

            switch (componentType)
            {
                case Constants.Component.RoomMarker.Title:
                    return new RoomMarker(number);
                case Constants.Component.Contact.Title:
                    return new Contact(number);
                case Constants.Component.Object.Title:
                    return new ComponentObject(number);
                case Constants.Component.Actor.Title:
                    return new Actor(number);
                case Constants.Component.Kill.Title:
                    return new Kill(number);
                case Constants.Component.DialogTree.Title:
                    return new DialogTree(number);
                default:
                    return new Component(number);
            }

        }

        private static string GetComponentType(List<string> importLines, int currentIndex)
        {
            //get the component type by iterating until the Type field is found.  End if the } aka end object field is found.  That's a spawn component
            for(int i = currentIndex; i < importLines.Count - 1; i++)
            {
                var text = importLines[i];
                if (text.Contains(Constants.Component.Type))
                {
                    var split = text.Split(new char[] { ' ' });
                    var type = split[1];
                    return type;
                }
                else if (text.Contains(Constants.FoundryObjectEndCharacter))
                {
                    break;
                }
            }

            return Constants.Component.Spawn.Title;
        }
    }
}
