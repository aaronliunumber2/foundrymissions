var defaultDialogName = "Dialog Tree";
var holocostumePrefix = "Holodeck_Ugc_";
var dialogStartChars = "<&";
var dialogEndChars = "&>"
var promptWidth = 300;

var addedPrompts = [];

function getDialogs(components) {
    return components.filter(function (component) {
        return component.Type == "DIALOG_TREE";
    });
}

function doDialogs(components, costumes) {

    var dialogs = getDialogs(components);

    for (i = 0; i < dialogs.length; i++) {
        addedPrompts = []; //reset the added prompts
        var dialog = dialogs[i];
        var name = getDialogDisplayName(dialog);
        var id = getDialogId(dialog);
        var dialogPrompts = dialog.DialogPrompts;
        //create the listitem (li) html and add it to the menu
        var html = '<li class="dialog-list-item"><a data-toggle="tab" href="#' + id + '">' + name + '</a></li>';
        //add it before the costumes button
        $("#" + costumeMenuId).before(html);

        //create the tab and data and add it to the tab panes
        html = getDialogTabPageHtml(dialog, costumes, components, dialogPrompts);
        $("#" + costumeId).before(html);
    }
}

var displayNameLength = 30;
function getDialogDisplayName(dialog) {
    var dialogName = dialog.VisibleName;

    //if its the default name or empty grab the first 10 characters from the prompt body
    if (dialogName.includes(defaultDialogName) || !dialogName) {
        var promptBody = dialog.PromptBody;

        //replace new lines with space
        promptBody = promptBody.replace(/\r\n|\n|\r/g, " ");

        //show the first 20 characters
        dialogName = promptBody.substring(0, displayNameLength);
    }
    return dialogName;
}

function getDialogId(dialog) {
    return "dialog" + dialog.Number;
}

function getDialogTabPageHtml(dialog, costumes, components, dialogPrompts) {
    var id = getDialogId(dialog);
    var html = "<div id='" + id + "' class='tab-pane'>";
    //start with the starting dialog call the recursive getDialogPromptDiv function
    html += getDialogPromptDiv(dialog, costumes, components, dialogPrompts, dialog);

    html += "</div>";
    return html;
}

function getDialogPromptDiv(prompt, costumes, components, dialogPrompts, parent) {
    /*prompt contains these things
      1. Costume
      2. Animation
      3. Title
      4. Text
      5. Actions (buttons)
    */

    var costume = getCostumeName(prompt, costumes, components, parent);
    var animation = formatPromptStyle(prompt.PromptStyle);
    var title = formatPromptTitle(prompt.PromptTitle);
    var text = formatPromptBody(prompt.PromptBody);
    var actions = prompt.Action;
    var promptNumber = getPromptNumber(prompt);
    var width = actions.length * promptWidth;

    var promptHtml = "<div class='prompt' style='width: "+ width +"px'>";
    promptHtml += "<div class='prompt-body' id='prompt-body-" + promptNumber + "'>";
    promptHtml += "<div>Costume: " + costume + "</div>";
    promptHtml += "<div>Title: " + title + "</div>";
    promptHtml += "<div>Text</div>";
    promptHtml += "<div class='promptText'>" + text + "</div>";
    promptHtml += "</div>"; //promptbody
    promptHtml += getActionHtml(actions);
    promptHtml += "</div>"; //prompt


    //now add all the next prompts doing its actions first (recursively)
    for (var nextAction = 0; nextAction < actions.length; nextAction++) {
        var action = actions[nextAction];
        var nextPromptId = action.NextPromptID;
        if (nextPromptId == -1) { //-1 brings it back to the top level
            nextPromptId = 0;
        }

        //first if next promptId is "" that means its an end dialog action
        if (nextPromptId != "") {
            if (!addedPrompts.includes(nextPromptId)) {
                //only do it if we didn't add this prompt yet
                addedPrompts.push(nextPromptId);
                var nextPrompt = getNextPromptById(dialogPrompts, nextPromptId);
                promptHtml += getDialogPromptDiv(nextPrompt, costumes, components, dialogPrompts, parent);
            }
        }

    }


    return promptHtml;
}

function getActionHtml(actions) {
    var actionHtml = "<div class='action-body'>";
    for (iAction = 0; iAction < actions.length; iAction++) {       
        action = actions[iAction];
        actionHtml += "<div class='action'>";
        actionHtml += action.ActionName;
        actionHtml += "</div>";
    }


    actionHtml += "</div>";
    return actionHtml;
}

function getNextPromptById(dialogPrompts, nextPromptId) {
    for (var i = 0; i < dialogPrompts.length; i++) {
        // look for the entry with a matching value
        var prompt = dialogPrompts[i];
        if (prompt.Number == nextPromptId) {
            // we found it
            return prompt;
        }
    }
}

function getCostumeName(prompt, costumes, components, parent) {
    //get the costume, or pet costume or parent costume
    var costume = prompt.PromptCostume;

    //first check if it has a holoID.  If it does go to the costumes and get the costume name
    if (costume && costume.includes(holocostumePrefix)) {
        var costumeObj = getCostumeByName(costumes, costume);
        costume = costumeObj.DisplayName;
    }

    if (costume == "") {
        costume = prompt.PromptPetCostume;
    }

    if (costume == "" && parent) { //if there is a parent check it for the costume
        costume = getCostumeName(parent, costumes, components);

    }
    else if (costume == "" && !parent) { //if there is no parent check if there is a actor ID
        if (prompt.ActorID) {
            var actorID = prompt.ActorID;
            //get the componentID that matches the Actor ID
            var component = getComponentById(components, actorID);
            costume = component.VisibleName;
        }
    }

    return costume;

}

function getPromptNumber(prompt) {
    var number = 0;

    if (prompt.Number) {
        number = prompt.Number;
    }

    return number;
}

function formatPromptStyle(style) {
    if (!style) {
        return "[Default]";
    }
    else {
        return style;
    }
}

function formatPromptTitle(title) {
    if (!title) {
        return "";
    }
    else {
        return title;
    }
}

function formatPromptBody(promptBody) {
    /*formatting 
      1. replaces \r\n with <br>
    */


    promptBody = promptBody.replace(/\r\n|\n|\r/g, '<br />');

    return promptBody;
}