using StarbaseUGC.Foundry.Engine.Helpers;
using StarbaseUGC.Foundry.Engine.Models;
using StarbaseUGC.Foundry.Engine.Models.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace StarbaseUGC.Foundry.Engine.Serializers
{
    public static class FoundryMissionSerializer
    {
        public static FoundryMission ImportMission(string fileName)
        {
            var txt = File.ReadAllText(fileName);
            return ParseMissionText(txt);
        }

        public static string ExportMissionToJson(FoundryMission mission)
        {
            var json = new JavaScriptSerializer().Serialize(mission);

            return json;
        }

        private static FoundryMission ParseMissionText(string txt)
        {
            var importLines = new List<string>(txt.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));

            var mission = new FoundryMission();

            mission.Namespace = importLines.Find(s => s.StartsWith(Constants.FoundryMission.NameSpace)).Replace(Constants.FoundryMission.NameSpace, string.Empty);
            mission.Project = GetProject(importLines);
            mission.Mission = GetMission(importLines);
            mission.Maps = GetMaps(importLines);
            mission.Components = GetComponents(importLines);
            //costumes

            return mission;
        }

        private static Project GetProject(List<string> importLines)
        {
            var objects = GetFoundryObjectsByName(importLines, "Project");
            //since there is only one project object we can safely return the first one

            return (Project)objects[0];
        }

        private static Mission GetMission(List<string> importLines)
        {
            var objects = GetFoundryObjectsByName(importLines, "Mission");

            //since there is only one mission object we can safely return the first one

            return (Mission)objects[0];
        }

        private static List<Map> GetMaps(List<string> importLines)
        {
            var objects = GetFoundryObjectsByName(importLines, "Map");
            var maps = objects.Cast<Map>().ToList();
            return maps;
        }

        private static List<Component> GetComponents(List<string> importLines)
        {

            var objects = new List<FoundryObject>();

            //find all the lines that match component, but only like.  include tab at the beginning and  space at the end
            var matchIndexs = importLines.Select((text, index) => text.Contains($"\t{Constants.Component.Title} ") ? index : -1).Where(index => index != -1).ToArray();

            foreach (var matchIndex in matchIndexs)
            {
                var index = matchIndex;
                var text = importLines[index];

                //make sure its actually a component line, the first word must be component
                var split = text.Split(new char[] { ' ' });

                if (split.Length < 1)
                {
                    continue;
                }

                if (!split[0].Trim().Equals(Constants.Component.Title))
                {
                    continue;
                }


                var foundryObject = GetFoundryObjectByIndex(importLines, ref index);

                objects.Add(foundryObject);
            }

            var components = objects.Cast<Component>().ToList();
            return components;
        }

        private static List<FoundryObject> GetFoundryObjectsByName(List<string> importLines, string name)
        {
            var objects = new List<FoundryObject>();

            //find all the lines that match the name
            var matchIndexs = importLines.Select((text, index) => text.Equals(name) ? index : -1).Where(index => index != -1).ToArray();

            foreach (var matchIndex in matchIndexs)
            {
                var index = matchIndex;
                var foundryObject = GetFoundryObjectByIndex(importLines, ref index);

                objects.Add(foundryObject);
            }

            return objects;
        }

        /// <summary>
        /// Get the foundry object at the specific index
        /// </summary>
        /// <param name="importLines"></param>
        /// <param name="index">by reference because other foundry objects may be found inside and the index must keep going</param>
        /// <returns></returns>
        private static FoundryObject GetFoundryObjectByIndex(List<string> importLines, ref int currentIndex, string enforcedTitle = "")
        {
            var title = enforcedTitle; //allow us to set a title
            if (string.IsNullOrWhiteSpace(enforcedTitle))
            {
                title = importLines[currentIndex].Trim(); //this is the name of the object
            }

            var foundryObject = FoundryObjectFactory.CreateFoundryObject(title, importLines, currentIndex);

            //sometimes the title has extra data in it, the first word is always the title.  need to check for this
            var split = title.Split(Constants.SplitSpace);
            if (split.Length > 1)
            {
                title = split[0];
            }

            foundryObject.Title = title;
            var openText = importLines[currentIndex + 1]; //this represents the { 
            var closeText = openText.Replace(Constants.FoundryObjectStartCharacter, Constants.FoundryObjectEndCharacter); //this represents the } but keeps the indentation so we can find the correct "end"
            currentIndex = currentIndex + 2;          //this represents the first text that is actually a field

            for (; currentIndex < importLines.Count; currentIndex++)
            {
                var text = importLines[currentIndex];

                //first check for reserved words

                #region Empty Lines
                //skip empty lines
                if (string.IsNullOrEmpty(text.Trim()))
                {
                    continue;
                }
                #endregion

                #region Hit Close Text
                //go until it reaches the close text and then break                
                if (text.Equals(closeText))
                {
                    break;
                }
                #endregion

                #region Whens
                if (text.Trim().Equals(Constants.Component.When.Title) ||
                    text.Trim().Equals(Constants.Component.HideWhen.Title))
                {
                    //if it has a when taht means its a component
                    HandleWhen((Component)foundryObject, importLines, ref currentIndex);
                    continue;
                }
                #endregion

                #region External Variable
                if (text.Trim().Equals(Constants.Component.ExternalVar.Title))
                {

                }
                #endregion

                #region New Foundry Object
                //if it is not then next check if it is a new foundry object
                var foundryObjectCheckIndex = currentIndex + 1;
                //but first make sure we don't go over the text size, may be impossible to happen but should check anyways
                if (foundryObjectCheckIndex > importLines.Count - 1)
                {
                    break;
                }
                var foundryObjectCheckText = importLines[foundryObjectCheckIndex];
                if (foundryObjectCheckText.Contains(Constants.FoundryObjectStartCharacter))
                {
                    var newFoundryObject = GetFoundryObjectByIndex(importLines, ref currentIndex);

                    foundryObject.FoundryObjects.Add(newFoundryObject);
                }
                #endregion
                #region Add new Field
                else //it is neither the end or a new foundry object that means we have a new field!
                {
                    //trim the text, the first word is the field name, everything after that is the data
                    text = text.Trim();
                    split = text.Split(' ');
                    var fieldName = split[0];
                    var fieldValue = string.Empty;
                    if (split.Length > 1)
                    {
                        fieldValue = text.Substring(fieldName.Length + 1);
                    }
                    if (!foundryObject.Fields.ContainsKey(fieldName)) // some can be in ther emultiple times like "END"
                    {
                        foundryObject.Fields.Add(fieldName, fieldValue);
                    }
                }
                #endregion
            }

            return foundryObject;
        }

        private static void HandleWhen(Component foundryObject, List<string> importLines, ref int currentIndex)
        {
            //there are two types of Whens and those can be two types too
            //they are When and HideWhen
            //they are with parameters (MAP_START, MANUAL) or with parameters (everything else)

            Trigger whenObject = null;
            var split = importLines[currentIndex].Split(new char[] { ' ' });

            var whenType = split[0];
            var triggerType = Constants.Trigger.ObjectiveComplete.Title; //default is objective complete
            if (split.Length > 1)
            {
                triggerType = split[1];
            }
            
            whenObject = (Trigger)GetFoundryObjectByIndex(importLines, ref currentIndex, triggerType);

            //got the object now set it in the proper spot
            if (whenType.Equals(Constants.Trigger.When))
            {
                foundryObject.When = whenObject;
            }
            else //if (whenType.Equals(Constants.Trigger.HideWhen))
            {
                foundryObject.HideWhen = whenObject;
            }
        }

        //private static void HandleAction(FoundryObject foundryObject, List<string> importLines, ref int currentIndex)

        //for now if its external var then try to find the end and skip to it
        private static void HandleExternVar(FoundryObject foundryObject, List<string> importLines, ref int currentIndex)
        {
            var text = importLines[currentIndex];
            var endText = text.Replace(Constants.Component.ExternalVar.Title, Constants.Component.ExternalVar.End);
            //go until it finds the end
            for(; currentIndex < importLines.Count - 1; currentIndex++)
            {
                var currentText = importLines[currentIndex];
                if (currentText.Equals(endText))
                {
                    currentIndex++;
                    return;
                }
            }
        }
    }
}
