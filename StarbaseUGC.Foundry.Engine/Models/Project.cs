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
            get { return GetFieldValue(Constants.FoundryObject.Name); }
        }

        public string AccountName
        {
            get { return GetFieldValue(Constants.Project.AccountName); }
        }

        public string CreationTime
        {
            get { return GetFieldValue(Constants.Project.CreationTime); }
        }

        public string FromContainer
        {
            get { return GetFieldValue(Constants.Project.FromContainer); }
        }

        public string PublicName
        {
            get { return GetFieldValue(Constants.Project.PublicName); }
        }

        public string Description
        {
            get { return GetFieldValue(Constants.Project.Description); }
        }

        public string Language
        {
            get { return GetFieldValue(Constants.Project.Language); }
        }

        public string LifetimeTipsReceived
        {
            get
            {
                if (Fields.ContainsKey(Constants.Project.LifetimeTipsReceived))
                {
                    return GetFieldValue(Constants.Project.LifetimeTipsReceived);
                }
                else
                {
                    return "0";
                }            
            }
        }

        public string AverageRating
        {
            get { return GetFieldValue(Constants.Project.AverageRating); }
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
            get { return GetFieldValue(Constants.Project.RestrictionProperties.Faction); }
        }

        public string MinLevel
        {
            get { return GetFieldValue(Constants.Project.RestrictionProperties.MinLevel); }
        }
    }
}
