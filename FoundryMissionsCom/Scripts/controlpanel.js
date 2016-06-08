$(function () {
});

function changePasswordFail(error) {
    var data = JSON.parse(error.responseText)
    $('change-password-result').empty()
    $('.validation-summary-valid').empty()
    for (var key in data) {
        $('.validation-summary-valid').children('ul').append('<li>' + data[key] + '</li>')
    }
}

function changePasswordSuccess(data) {
    //clear out the validation errors and textboxes
    var success = data['success']
    $('.validation-summary-valid').empty()
    if (success == 'true') {
        $('#ChangePasswordModel_OldPassword').html('')
        $('#ChangePasswordModel_NewPassword').html('')
        $('#ChangePasswordModel_ConfirmPassword').html('')
        $('#change-password-result').append(data['message'])
    }
    else { //false
        var errors = data['message']
        var arrayLength = errors.length
        for (var i = 0; i < arrayLength; i++) {
            console.log(errors[i])
            if ($('.validation-summary-valid').children().length > 0) {
                $('.validation-summary-valid').children('ul').append('<li>' + errors[i] + '</li>')
            }
            else {
                $('.validation-summary-valid').append('<ul><li>' + errors[i] + '</li></ul>')
            }
        }
    }    
}

$('.approve-button').click(function () {
    var id = $(this).attr('id')
    var rowParent = $(this).closest('.search-result')
    $.ajax({
        url: 'manage/approvemission',
        type: 'POST',
        data: { id: id },
    }).done(function () {
        rowParent.slideUp(100)
    });
});

$('.deny-button').click(function () {
    var id = $(this).attr('id')
    var rowParent = $(this).closest('.search-result')
    $.ajax({
        url: 'manage/denymission',
        type: 'POST',
        data: { id: id },
    }).done(function () {
        rowParent.slideUp(100)
    });
});

$('.mission-actions').on('click', '.set-status-button', function () {
    var missionId = $(this).attr('mission')
    var buttonType = $(this).attr('value')
    var link = $(this).attr('link')
    var url = ''
    var button = $(this)
    if (buttonType == 'Submit') {
        url = 'manage/submitmission'
    }
    else if (buttonType == 'Unpublish' || buttonType == 'Withdraw')
    {
        url = 'manage/withdrawmission'
    }
    else {
        return;
    }

    $.ajax({
        url: url,
        type: 'POST',
        data: { id: missionId },
        success: function (data) {
            var success = data['success']
            var message = data['message']
            if (success == 'true') {
                //set the new status
                button.closest('.my-missions-result').children('.mission-status').html(message)
                var edithtml = '<a class="btn btn-default" href="/missions/edit/' + link + '">Edit</a>'
                var submithtml = '<input type="submit" value="Submit" class="btn btn-default set-status-button" mission="' + missionId + '" link="' + link + '">'
                var withdrawhtml = '<input type="submit" value="Withdraw" class="btn btn-default set-status-button" mission="' + missionId + '" link="' + link + '">'
                var unpublishhtml = '<input type="submit" value="Unpublish" class="btn btn-default set-status-button" mission="' + missionId + '" link="' + link + '">'

                //set the new buttons
                if (message == 'Unpublished') {
                    //edit and submit
                    button.parent().html(edithtml + submithtml);
                }
                else if (message == 'In Review') {
                    //edit and withdraw
                    button.parent().html(edithtml + withdrawhtml);
                }
                else if (message == 'Published') {
                    //Unpublish
                    button.parent().html(unpublishhtml);
                    
                }
            }
            else {
                alert(message)
            }

        }
    });
});
