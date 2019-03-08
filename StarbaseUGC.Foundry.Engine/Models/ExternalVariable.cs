using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models
{
    public class ExternalVariable
    {
        public string Name { get; set; }
        public Dictionary<string, string> Variables { get; set; }
    }
}
