using StarbaseUGC.Foundry.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models
{
    public class Project : FoundryObject
    {
        public string Name
        {
            get { return Fields[Constants.FoundryObject.Name] as string; }
        }

        public string AccountName
        {
            get { return Fields[Constants.Project.AccountName] as string; }
        }

        public string CreationTime
        {
            get { return Fields[Constants.Project.CreationTime] as string; }
        }

        public string FromContainer
        {
            get { return Fields[Constants.Project.FromContainer] as string; }
        }

        public string PublicName
        {
            get { return Fields[Constants.Project.PublicName] as string; }
        }

        public string Description
        {
            get { return Fields[Constants.Project.Description] as string; }
        }

        public string Language
        {
            get { return Fields[Constants.Project.Language] as string; }
        }

        public string LifetimeTipsReceived
        {
            get
            {
                if (Fields.ContainsKey(Constants.Project.LifetimeTipsReceived))
                {
                    return Fields[Constants.Project.LifetimeTipsReceived] as string;
                }
                else
                {
                    return "0";
                }            
            }
        }

        public string AverageRating
        {
            get { return Fields[Constants.Project.AverageRating] as string; }
        }

        public RestrictionProperties RestrictionProperties
        {
            get { return (RestrictionProperties)FoundryObjects.Find(f => f.Title.Equals(Constants.Project.RestrictionProperties.Title)); }
        }
    }
    
    public class RestrictionProperties : FoundryObject
    {
        public string Faction
        {
            get { return Fields[Constants.Project.RestrictionProperties.Faction] as string; }
        }

        public string MinLevel
        {
            get { return GetFieldValue(Constants.Project.RestrictionProperties.MinLevel); }
        }
    }
}
