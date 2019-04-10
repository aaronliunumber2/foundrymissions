using StarbaseUGC.Foundry.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models.Components
{
    public class DialogTree : Component
    {
        //these are copied directly from dialog type :(
        public string PromptBody { get { return GetFieldValue(Constants.Dialog.PromptBody); } }
        [System.ComponentModel.DefaultValue("")]
        public string PromptPetCostume { get { return GetFieldValue(Constants.Dialog.PromptPetCostume); } }
        [System.ComponentModel.DefaultValue("")]
        public string PromptStyle { get { return GetFieldValue(Constants.Dialog.PromptStyle); } }
        [System.ComponentModel.DefaultValue("")]
        public string PromptTitle { get { return GetFieldValue(Constants.Dialog.PromptTitle); } }
        [System.ComponentModel.DefaultValue("")]
        public string PromptCostume { get { return GetFieldValue(Constants.Dialog.PromptCostume); } }
        public List<DialogAction> Action { get { return GetFoundryObjectsByTitle(Constants.Dialog.Action.Title).Cast<DialogAction>().ToList(); } }

        public List<Prompt> DialogPrompts { get { return GetFoundryObjectsByTitle(Constants.Component.DialogTree.Prompt.Title).Cast<Prompt>().OrderBy(p => p.Number).ToList(); } }
        

        public DialogTree(int number) : base (number)
        {

        }
    }

    public class Prompt : Dialog
    {
        public int Number { get; private set; }


        public Prompt(int number)
        {
            Number = number;
        }
    }
}
