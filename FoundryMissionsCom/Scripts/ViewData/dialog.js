var defaultDialogName = "Dialog Tree";
var holocostumePrefix = "Holodeck_Ugc_";
var dialogStartChars = "<&";
var dialogEndChars = "&>"

function getDialogs(components) {
    return components.filter(function (component) {
        return component.Type == "DIALOG_TREE";
    });
}

function doDialogs(components, costumes) {

    var dialogs = getDialogs(components);

    for (i = 0; i < dialogs.length; i++) {
        var dialog = dialogs[i];
        var name = getDialogDisplayName(dialog);
        var id = getDialogId(dialog);
        //create the listitem (li) html and add it to the menu
        var html = '<li class="dialog-list-item"><a data-toggle="tab" href="#' + id + '">' + name + '</a></li>';
        //add it before the costumes button
        $("#" + costumeMenuId).before(html);

        //create the tab and data and add it to the tab panes
        html = getDialogTabPageHtml(dialog, costumes, components);
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

function getDialogTabPageHtml(dialog, costumes, components) {
    var id = getDialogId(dialog);
    var html = "<div id='" + id + "' class='tab-pane'>";
    //do its own dialog
    html += getDialogPromptDiv(dialog,costumes, components);

    //now add all the next prompts (if there are any)
    var prompts = dialog.DialogPrompts;

    for (j = 0; j < prompts.length; j++) {

        var prompt = prompts[j];
        html += getDialogPromptDiv(prompt, costumes, components, dialog);
    }


    html += "</div>";
    return html;
}

function getDialogPromptDiv(prompt, costumes, components, parent) {
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

    var promptHtml = "<div class='prompt'>";
    promptHtml += "<div class='promptBody'>";
    promptHtml += "<div>Costume: " + costume + "</div>";
    promptHtml += "<div>Title: " + title + "</div>";
    promptHtml += "<div>Text</div>";
    promptHtml += "<div class='promptText'>" + text + "</div>";
    promptHtml += "</div>"; //promptbody
    promptHtml += getActionHtml(actions);
    promptHtml += "</div>"; //prompt


    return promptHtml;
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

function getActionHtml(actions) {
    return "";
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