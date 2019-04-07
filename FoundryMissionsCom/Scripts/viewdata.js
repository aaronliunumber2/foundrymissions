//load the initial data
var url = window.location.href;
var sections = url.split("/" )
var missionlink = sections[sections.length - 2];
var missionDataId = "missiondata";

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
        
        //get the stuff
        var mission = json.Mission;
        var objectives = mission.Objectives;
        var components = json.Components;
        var dialogs = getDialogs(components);

        //objectives
        
        var objectivesDiv = getObjectivesDivHtml(objectives);
        $("#" + missionDataId).append(objectivesDiv);

        //dialog
        
        var dialogsDiv = getDialogsDivHtml(dialogs);
        $("#" + missionDataId).append(dialogsDiv);

    },
    error: function (error) {
        var error = "Error loading mission data.";
        $("#" + missiondata).html(error);
    }
});

//get objective functions
function getObjectivesDivHtml(objectives) {
    var div = "<div id='objectives' class='col-md-3'><h3>Objectives</h3>";

    for (i = 0; i < objectives.length; i++) {
        var objective = objectives[i];
        var objectiveName = objective.UIString.substring(1, objective.UIString.length-1);
        div += "<div class='objectiveName '>" + objectiveName + "</div>";
    }

    div += "</div>";
    return div;
}

//get dialog functions
var defaultDialogName = "Dialog Tree";
var dialogStartChars = "<&";
var dialogEndChars = "&>"

function getDialogs(components){
    return components.filter(function(component) {
        return component.Type == "DIALOG_TREE";
    });
}

function getDialogsDivHtml(dialogs){
    var div = "<div id='dialogs' class='col-md-3'><h3>Dialogs</h3>";

    for (i = 0; i < dialogs.length; i++) {
        var dialog = dialogs[i];
        var dialogName = dialog.VisibleName;

        //if its the default name or empty grab the first 10 characters from the prompt body
        if (dialogName.includes(defaultDialogName) || !dialogName) {
            var promptBody = dialog.PromptBody;
            if (promptBody.startsWith(dialogStartChars)) {
                //get rid of the first <& 
                promptBody = promptBody.substring(2);
            }
            //replace new lines with space
            promptBody.replace('\n\r', ' ');
            //show the first 20 characters
            dialogName = promptBody.substring(0, 20);
        }
        else {
            //get rid of the quotation marks
            dialogName = dialogName.substring(1, dialogName.length - 1);
        }
            
        div += "<div class='dialogTitle'>" + dialogName + "</div>";
    }

    div += "</div>";
    return div;
}