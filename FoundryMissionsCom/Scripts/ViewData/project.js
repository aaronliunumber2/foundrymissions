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
    return name;
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