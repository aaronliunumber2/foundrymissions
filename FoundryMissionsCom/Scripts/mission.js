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

})

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