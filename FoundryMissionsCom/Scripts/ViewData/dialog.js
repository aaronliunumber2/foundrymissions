var defaultDialogName = "Dialog Tree";
var dialogStartChars = "<&";
var dialogEndChars = "&>"

function getDialogs(components) {
    return components.filter(function (component) {
        return component.Type == "DIALOG_TREE";
    });
}

function doDialogs(dialogs) {

    for (i = 0; i < dialogs.length; i++) {
        var dialog = dialogs[i];
        var name = getDialogDisplayName(dialog);
        var id = getDialogId(dialog);
        //create the listitem (li) html and add it to the menu
        var html = '<li class="dialog-list-item"><a data-toggle="tab" href="#' + id + '">' + name + '</a></li>';
        //add it before the costumes button
        $("#" + costumeMenuId).before(html);

        //create the tab and data and add it to the tab panes
        html = getDialogTabPageHtml(dialog);
        $("#" + costumeId).before(html);
    }
}

var displayNameLength = 30;
function getDialogDisplayName(dialog) {
    var dialogName = dialog.VisibleName;

    //if its the default name or empty grab the first 10 characters from the prompt body
    if (dialogName.includes(defaultDialogName) || !dialogName) {
        var promptBody = dialog.PromptBody;
        if (promptBody.startsWith(dialogStartChars)) {
            //get rid of the first <& 
            promptBody = promptBody.substring(2);
        }
        //replace new lines with space
        promptBody = promptBody.replace(/\\n/g, " ");

        //show the first 20 characters
        dialogName = promptBody.substring(0, displayNameLength);

        //if it ends with &> remove it
        if (dialogName.endsWith(dialogEndChars)) {
            dialogName = dialogName.substring(0, dialogName.length - 2);
        }
    }
    else {
        //get rid of the quotation marks
        if (dialogName.startsWith('"') && dialogName.endsWith('"')) {
            dialogName = dialogName.substring(1, dialogName.length - 1);
        }
    }
    return dialogName;
}

function getDialogId(dialog) {
    return "dialog" + dialog.Number;
}

function getDialogTabPageHtml(dialog) {
    var id = getDialogId(dialog);
    var html = "<div id='" + id + "' class='tab-pane'>";
    //do its own dialog
    html += getDialogPromptDiv(dialog);

    //now add all the next prompts (if there are any)
    var prompts = dialog.DialogPrompts;

    for (j = 0; j < prompts.length; j++) {

        var prompt = prompts[j];
        html += getDialogPromptDiv(prompt);
    }


    html += "</div>";
    return html;
}

function getDialogPromptDiv(prompt) {
    /*prompt contains these things
      1. Costume
      2. Animation
      3. Title
      4. Text
      5. Actions (buttons)
    */

    var costume = prompt.PromptCostume;
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
      1. gets rid of <& and &> from the beginnings
      2. gets rid of " from the beginning and ending
      3. replaces \n with <br>
    */
    if (promptBody.startsWith(dialogStartChars) && promptBody.endsWith(dialogEndChars)) {
        promptBody = promptBody.substring(2, promptBody.length - 2);
    }
    else if (promptBody.startsWith('"') && promptBody.endsWith('"')) {
        promptBody = promptBody.substring(1, promptBody.length - 1);
    }

    promptBody = promptBody.replace(/\\n/g, "<br>");

    return promptBody;
}