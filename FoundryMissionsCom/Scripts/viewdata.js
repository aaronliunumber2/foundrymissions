//load the initial data
var url = window.location.href;
var sections = url.split("/")
var viewIndex = getViewIndex(sections);
var missionlink = sections[viewIndex - 1];

var projectId = "project";
var objectivesId = "story";
var dialogMenuId = "dialogsTabButton";
var costumeMenuId = "costumesTabButton";
var costumeId = "costumes";
$(document).ready(function () {

    var formData = new FormData();
    formData.append("link", missionlink);

    $.ajax({
        type: 'post',
        url: '/missions/jsonasync',
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {
            var json = JSON.parse(response.json);
            var project = json.Project;
            //get the stuff
            var mission = json.Mission;

            var objectives = mission.Objectives;
            var components = json.Components;
            var dialogs = getDialogs(components);

            //project
            var projectDiv = getProjectsDivHtml(project);
            $("#" + projectId).append(projectDiv);

            //objectives
            var objectivesDiv = getObjectivesDivHtml(objectives);
            $("#" + objectivesId).append(objectivesDiv);

            //dialog
            doDialogs(dialogs);
            //var dialogsDiv = getDialogsDivHtml(dialogs);
            //$("#" + missionDataId).append(dialogsDiv);

        },
        error: function (error) {
            //var errorMessage = "Error loading mission data.";
            //alert(error)
            //$("#" + missiondata).html(errorMessage);
        }
    });
});
//load functions
function getViewIndex(sections) {
    for (viewIndex = 0; viewIndex < sections.length; viewIndex++) {
        if (sections[viewIndex] == "view") {
            return viewIndex;
        }
    }
}

//get project functions
function getProjectsDivHtml(project) {
    var div = "";
    div += "<div class='row'><div class='col-md-2'>Name</div><div class='col-md-10'>" + formatPublicName(project.PublicName) + "</div></div>";
    div += "<div class='row'><div class='col-md-2'>Author</div><div class='col-md-10'>" + project.AccountName + "</div></div>";
    div += "<div class='row'><div class='col-md-2'>Language</div><div class='col-md-10'>" + project.Language + "</div></div>";
    div += "<div class='row'><div class='col-md-2'>Allegiance</div><div class='col-md-10'>" + formatFaction(project.RestrictionProperties.Faction) + "</div></div>";
    div += "<div class='row'><div class='col-md-2'>Level</div><div class='col-md-10'>" + formatMinLevel(project.RestrictionProperties.MinLevel) + "</div></div>";
    return div;
}

function formatPublicName(name) {
    //just get rid of the first and last character
    return name.substring(0, name.length - 1).substring(1);
}

function formatFaction(faction) {
    if (faction == "Allegiance_Starfleet") {
        return "Starfleet";
    }
    else {
        return "Klingon";
    }
}

function formatMinLevel(level) {
    if (level == "") {
        return "Any";
    }
    else {
        return level + "+";
    }
}

//get objective functions
function getObjectivesDivHtml(objectives) {
    var div = "<div id='objectives'><h3>Objectives</h3>";

    for (i = 0; i < objectives.length; i++) {
        var objective = objectives[i];
        var objectiveName = objective.UIString.substring(1, objective.UIString.length - 1);
        div += "<div class='objectiveName '>" + objectiveName + "</div>";
    }

    div += "</div>";
    return div;
}

//get dialog functions
var defaultDialogName = "Dialog Tree";
var dialogStartChars = "<&";
var dialogEndChars = "&>"

$("#" + dialogMenuId).click(function () {
    $(".dialog-list-item").show();
})

$(".notDialogButton").click(function () {
    $(".dialog-list-item").hide();
})

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
        dialogName = promptBody.substring(0, 20);

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
    var html = "<div id='"+ id +"' class='tab-pane'>";
    //do its own dialog
    html += "<div>" + dialog.PromptBody + "</div>";

    //now add all the next prompts (if there are any)
    var prompts = dialog.DialogPrompts;

    for (j = 0; j < prompts.length; j++) {

        var prompt = prompts[j];
        html += "<div>" + prompt.PromptBody + "</div>";
    }
    

    html += "</div>";
    return html;
}