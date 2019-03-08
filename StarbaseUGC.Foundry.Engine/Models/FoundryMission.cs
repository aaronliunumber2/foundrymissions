using StarbaseUGC.Foundry.Engine.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models
{
    public class FoundryMission : FoundryObject
    {
        public string Namespace { get; set; }
        public Project Project { get; set; }
        public Mission Mission { get; set; }
        public List<Map> Maps { get; set; }
        public List<Component> Components { get; set; }
        public List<Costume> Costumes { get; set; }

    }
}
