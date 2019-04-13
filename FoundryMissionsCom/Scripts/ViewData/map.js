function getMapsDivHtml(maps) {
    var div = "<div id='mapsTab'><h3>Maps</h3>";

    for (i = 0; i < maps.length; i++) {
        var map = maps[i];
        var mapName = map.DisplayName;
        div += "<div class='mapName '>" + mapName + "</div>";
    }

    div += "</div>";
    return div;
}