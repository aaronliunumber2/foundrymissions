function getObjectivesDivHtml(objectives) {
    var div = "<div id='objectives'><h3>Objectives</h3>";

    for (i = 0; i < objectives.length; i++) {
        var objective = objectives[i];
        var objectiveName = objective.UIString;
        div += "<div class='objectiveName '>" + objectiveName + "</div>";
    }

    div += "</div>";
    return div;
}