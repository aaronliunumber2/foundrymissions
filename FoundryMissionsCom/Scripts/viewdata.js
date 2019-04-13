//load the initial data
var url = window.location.href;
var sections = url.split("/")
var viewIndex = getViewIndex(sections);
var missionlink = sections[viewIndex - 1];

var projectId = "project";
var objectivesId = "story";
var mapsId = "maps";
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
            var json =  response;
            var project = json.Project;
            //get the stuff
            var mission = json.Mission;
            var objectives = mission.Objectives;
            var components = json.Components;
            var dialogs = getDialogs(components);
            var maps = json.Maps;
            var costumes = json.Costumes;

            //project
            var projectDiv = getProjectsDivHtml(project);
            $("#" + projectId).append(projectDiv);

            //objectives
            var objectivesDiv = getObjectivesDivHtml(objectives);
            $("#" + objectivesId).append(objectivesDiv);

            //dialog is complicated
            doDialogs(components, costumes);

            //maps
            var mapsDiv = getMapsDivHtml(maps);
            $("#" + mapsId).append(mapsDiv);
            
            //costumes
            var costumesDiv = getCostumesDivHtml(costumes);
            $("#" + costumeId).append(costumesDiv);
            
        },
        error: function (error) {
            //var errorMessage = "Error loading mission data.";
            //alert(error)
            //$("#" + missiondata).html(errorMessage);
        }
    });
});

//load function
function getViewIndex(sections) {
    for (viewIndex = 0; viewIndex < sections.length; viewIndex++) {
        if (sections[viewIndex] == "view") {
            return viewIndex;
        }
    }
}

//ui events
$("#" + dialogMenuId).click(function () {
    $(".dialog-list-item").show();
})

$(".notDialogButton").click(function () {
    $(".dialog-list-item").hide();
})