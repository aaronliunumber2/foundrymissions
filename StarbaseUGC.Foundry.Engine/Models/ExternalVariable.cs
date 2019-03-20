using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models
{
    public class ExternalVariable
    {
        public string Name { get; internal set; }
        public string Type { get; internal set; }
        public SpecificValue SpecificValue { get; internal set; }
    }

    public class SpecificValue
    {
        public string Type { get; internal set; }
        public string FloatVal { get; internal set; }
        public string StringVal { get; internal set; }
    }
}
