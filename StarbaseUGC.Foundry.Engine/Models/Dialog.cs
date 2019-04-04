using StarbaseUGC.Foundry.Engine.Helpers;
using StarbaseUGC.Foundry.Engine.Models.Components;
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

        public List<DialogAction> Action { get { return GetFoundryObjectsByTitle(Constants.Dialog.Action.Title).Cast<DialogAction>().ToList(); } }
    }

    public class DialogAction : FoundryObject
    {
        public DialogAction(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                ActionName = name;
            }
        }

        public string ActionName { get; internal set; } = "Continue";

        public List<Trigger> ShowWhen { get; } = new List<Trigger>();
        public List<Trigger> HideWhen { get; } = new List<Trigger>();

        public string NextPromptID { get { return GetFieldValue(Constants.Dialog.Action.NextPromptID); } }
    }
}