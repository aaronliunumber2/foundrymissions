$(function () {

    $('#available-tags').on('click', '.gameplay-tag', function (event) {
        event.preventDefault()

        var tagitem = $(this).clone()
        tagitem.appendTo('#selected-tags')
        $(this).remove()
        SetTags()
    });

    $('#selected-tags').on('click', '.gameplay-tag', function (event) {
        event.preventDefault()

        var tagitem = $(this).clone()
        tagitem.appendTo('#available-tags')
        $(this).remove()
        SetTags()
    });


    $('#submit-mission-image-list').on('click', '.delete-image', function (event) {
        event.preventDefault()
        $(this).closest('.row').remove()
        setImageListNumbers()
    });

    $('#submit-mission-image-list').on('click', '.delete-old-image', function (event) {
        event.preventDefault()
        $(this).closest('.row').remove()
        setOldImageListNumbers()
    });

    $('#submit-mission-video-list').on('click', '.delete-video', function (event) {
        event.preventDefault()
        $(this).closest('.row').remove()
        setImageListNumbers()
    });

})

$('#add-new-image').click(function (event) {
    event.preventDefault()

    //check the last image file, if it is not blank allow a new line to be added
    var lastImage = $('.submit-mission-image').last().val()
    if (lastImage != '') {
        $('#submit-mission-image-list').append('<div class="row"><div class="col-md-12"><input type="submit" class="delete-image" value="X"/><input type="file" class="submit-mission-image" accept="image/*" /></div></div>')
        setImageListNumbers()
    }
});

$('#add-new-video').click(function (event) {
    event.preventDefault()

    //check the last image file, if it is not blank allow a new line to be added
    var lastImage = $('.submit-mission-video').last().val()
    if (lastImage != '') {
        $('#submit-mission-video-list').append('<div class="row"><div class="col-md-12"><input type="submit" class="delete-video" value="X"/><input type="text" class="submit-mission-video" /></div></div>')
        setImageListNumbers()
    }
});

function setImageListNumbers() {
    var counter = 0
    $('.submit-mission-image').each(function () {
        $(this).attr({
            name: 'Images[' + counter + ']'
        })
        counter++
    });
}

function setOldImageListNumbers() {
    var counter = 0
    $('.old-image').each(function () {
        $(this).attr({
            name: 'OldImages[' + counter + ']'
        })
        counter++
    });
}

function setVideoListNumbers() {
    var counter = 0
    $('.submit-mission-video').each(function () {
        $(this).attr({
            name: 'Videos[' + counter + ']'
        })
        counter++
    });
}

function setOldVideoListNumbers() {
    var counter = 0
    $('.old-video').each(function () {
        $(this).attr({
            name: 'OldVideos[' + counter + ']'
        })
        counter++
    });
}


function SetTags() {
    var selectedTags = $('#selected-tags');
    var counter = 0;

    //remove all the hidden inputs
    selectedTags.children('.tag-input').each(function () {
        $(this.remove())
    })

    //read the hidden inputs
    selectedTags.children('.gameplay-tag').each(function () {
        $('<input>').attr({
            type: 'hidden',
            class: 'tag-input',
            name: 'Tags[' + counter + ']',
            value: $(this).attr('value')
        }).appendTo('#selected-tags')
        counter++
    })
}

