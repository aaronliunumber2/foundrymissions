using StarbaseUGC.Foundry.Engine.Helpers;
using StarbaseUGC.Foundry.Engine.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models
{
    public class FoundryObject
    {
        internal virtual string Title { get; set; } = string.Empty;
        internal Dictionary<string, object> Fields { get; } = new Dictionary<string, object>();
        internal List<FoundryObject> FoundryObjects { get; } = new List<FoundryObject>();
        internal Dictionary<string, ExternalVariable> ExternalVariables { get; } = new Dictionary<string, ExternalVariable>();

        protected string GetFieldValue(string fieldValue)
        {
            if (Fields.ContainsKey(fieldValue))
            {
                var value = Fields[fieldValue] as string;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    value = FoundryObjectFormatter.FormatRegularText(value);
                }
                return value;
            }
            else
            {
                return string.Empty;
            }
        }

        protected FoundryObject GetFoundryObjectByTitle(string title)
        {
            return FoundryObjects.Find(f => f.Title.Equals(title));
        }

        protected List<FoundryObject> GetFoundryObjectsByTitle(string title)
        {
            return FoundryObjects.Where(f => f.Title.Equals(title)).ToList();
        }
    }
}
