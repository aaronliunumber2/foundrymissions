using StarbaseUGC.Foundry.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Helpers
{
    internal static class FoundryObjectFactory
    {
        internal static FoundryObject CreateFoundryObject(string title)
        {
            switch (title)
            {
                case Constants.Project.Title:
                    return new Project();
                case Constants.Project.RestrictionProperties.Title:
                    return new RestrictionProperties();
                case Constants.Map.Title:
                    return new Map();
                case Constants.Map.Prefab.Title:
                    return new Prefab();
                case Constants.Map.Space.Title:
                    return new Space();
                case Constants.Mission.Title:
                    return new Mission();
                case Constants.Mission.GrantBlock.Title:
                    return new GrantBlock();
                case Constants.Mission.MapLink.Title:
                    return new MapLink();
                case Constants.Mission.MapLink.DialogBlock.Title:
                    return new DialogBlock();
                default:
                    //special case stuff
                    if (title.Contains(Constants.Mission.Objective.Title))
                    {
                        //get the objective number
                        var split = title.Split(new char[] { ' ' });
                        var strnumber = split[1];
                        var number = Convert.ToInt32(strnumber);
                        return new Objective(number);
                    }

                    return new FoundryObject();
            }
        }
    }
}
