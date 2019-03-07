using StarbaseUGC.Foundry.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models
{
    public abstract class Dialog : FoundryObject
    {
        public string PromptBody {  get { return GetFieldValue(Constants.Dialog.PromptBody); } }
        public string PromptPetCostume { get { return GetFieldValue(Constants.Dialog.PromptPetCostume); } }
        public string PromptStyle { get { return GetFieldValue(Constants.Dialog.PromptStyle); } }
    }
}
