using StarbaseUGC.Foundry.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbaseUGC.Foundry.Engine.Models.Components
{

    public class Trigger : FoundryObject
    {
        public string TriggerType { get; private set; }

        public Trigger(string trigType)
        {
            TriggerType = trigType;
        }
    }

    public class ComponentTrigger : Trigger
    {
        public int ComponentId { get { return Convert.ToInt32((Constants.Trigger.Component.ComponentID)); } }

        public ComponentTrigger(string trigType) :  base(trigType)
        {

        }
    }

    public class ObjectiveCompleteTrigger : Trigger
    {
        public int ObjectiveId { get { return Convert.ToInt32(GetFieldValue(Constants.Trigger.ObjectiveComplete.ObjectiveID)); } }

        public ObjectiveCompleteTrigger(): base(Constants.Trigger.ObjectiveComplete.Title)
        {

        }
    }

    public class DialogPromptReachedTrigger: Trigger
    {
        public DialogPromptTriggerLink DialogPromptLink { get { return (DialogPromptTriggerLink)GetFoundryObjectByTitle(Constants.Trigger.DialogLink.Title); } }

        public DialogPromptReachedTrigger() : base(Constants.Trigger.DialogPromptReached.Title)
        {

        }
    }

    public class DialogPromptTriggerLink : FoundryObject
    {
        public string DialogID { get { return GetFieldValue(Constants.Trigger.DialogLink.DialogID); } }
        public string PromptID { get { return GetFieldValue(Constants.Trigger.DialogLink.PromptID); } }
    }
}
