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
        $('#ChangePasswordModel_OldPassword').val('')
        $('#ChangePasswordModel_NewPassword').val('')
        $('#ChangePasswordModel_ConfirmPassword').val('')
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
        type: "POST",
        data: { id: id },
    }).done(function () {
        rowParent.slideUp(100)
    });
});
