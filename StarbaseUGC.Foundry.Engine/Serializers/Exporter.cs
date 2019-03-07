using StarbaseUGC.Foundry.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace StarbaseUGC.Foundry.Engine.Serializers
{
    public static class Exporter
    {
        public static string ExportMissionToJson(FoundryMission mission)
        {
            var json = new JavaScriptSerializer().Serialize(mission);

            return json;
        }
    }
}
