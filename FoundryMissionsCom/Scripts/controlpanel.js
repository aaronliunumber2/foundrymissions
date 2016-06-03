$(function () {
});

function changePasswordFail(error) {
    var data = JSON.parse(error.responseText)
    $("change-password-result").empty()
    $(".validation-summary-valid").empty()
    for (var key in data) {
        $(".validation-summary-valid").append('<ul><li>' + data[key] + '</li></ul>')
    }
}

function changePasswordSuccess() {
    //clear out the validation errors and textboxes
    $(".validation-summary-valid").empty()
    $("#ChangePasswordModel_OldPassword").val('')
    $("#ChangePasswordModel_NewPassword").val('')
    $("#ChangePasswordModel_ConfirmPassword").val('')
}