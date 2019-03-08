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
                    #region Component
                    else if (title.Contains(Constants.Component.Title))
                    {
                        return GetComponent(title, importLines, currentIndex);
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
                case Constants.Component.Spawn.Title:
                    return new Spawn(number);                
            }
            return new Component(number);
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
            }

            return Constants.Component.Spawn.Title;
        }
    }
}
