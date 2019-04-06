$("#submitbutton").click(function () {
    var fileCount = $("#exportfiles").prop("files").length;

    if (fileCount > 0)
    {
        //reset the uploading status div in case they hit upload multiple times
        $("#uploadresults").html("");

        var files = $("#exportfiles").prop("files");
        for (i = 0; i < fileCount; i++) {
            ajaxUploadFile(files[i]);
        }
            
    }
})

function ajaxUploadFile(file) {
    var uploadingText = "Uploading " + file.name + "...";
    var fileName =  file.name;
    var nameNoSpaces = formatFileNameToId(fileName)
    var uploadingDiv = "<div id='upload" + nameNoSpaces + "' class='uploadresult'>" + uploadingText + "</div>";

    $("#uploadresults").append(uploadingDiv);

    //prep form data
    var formData = new FormData();
    formData.append("exportFile", file);

    //report 


    //do the upload
    $.ajax({
        type: 'post',
        url: '/missions/uploadexportasync',
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (response) {

            if (response.Status) {
                var missionLink = response.missionLink
                var linkToView = "/missions/" + missionLink;
                completedText = "Upload of " + fileName + " successful! <a href='" + linkToView + "'>View</a>";

            }
            else {
                completedText = "Upload of " + fileName + " failed.  Please contact us so we can fix this!";
            }
            $("#upload" + nameNoSpaces).html(completedText);
        },
        error: function (error) {
            var completedText = "Upload failed.  Please contact us so we can fix this!";
            $("#upload" + nameNoSpaces).html(completedText);
        }
    });



}

function formatFileNameToId(fileName) {
    return fileName.replace(' ', '').replace('.', '');
}