$(function () {

    $('#availabletags').on('click', '.gameplay-tag', function (event) {
        event.preventDefault();

        var tagitem = $(this).clone();
        tagitem.attr('name', 'selectedtag');
        tagitem.appendTo('#selectedtags');
        $(this).remove();       
    });

    $('#selectedtags').on('click', '.gameplay-tag', function () {
        event.preventDefault();

        var tagitem = $(this).clone();
        tagitem.removeAttr('name');
        tagitem.appendTo('#availabletags');
        $(this).remove();
    });


})