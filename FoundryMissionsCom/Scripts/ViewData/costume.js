function getCostumesDivHtml(costumes) {
    var div = "<div id='costumesTab'><h3>Costumes</h3>";

    for (i = 0; i < costumes.length; i++) {
        var costume = costumes[i];
        var costumeName = costume.DisplayName;
        div += "<div class='costumeName '>" + costumeName + "</div>";
    }

    div += "</div>";
    return div;
}

function getCostumeByName(costumes, name) {
    //lazy  copied this from https://stackoverflow.com/a/19253830
    // iterate over each element in the array
    for (var i = 0; i < costumes.length; i++) {
        // look for the entry with a matching value
        var costume = costumes[i];
        if (costume.Name == name) {
            // we found it
            return costumes[i];
        }
    }
}