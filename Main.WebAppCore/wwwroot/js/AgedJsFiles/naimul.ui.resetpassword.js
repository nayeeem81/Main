$(function () {
    var $rootResetObject = $("#SigninRoot", document);
    var signinResetSubmitSelector = "#btnResetPassword";
    var $signinResetSubmitObject = $("#btnResetPassword", $rootResetObject);
    var $emailResetObject = $("#Email", $rootResetObject);

    function resetPassword(email) {
        var data = { "Email": email };
        $.ajax({
            url: emailresetpassword,
            type: 'POST',
            data: data,
            cache: false
        }).done(function (result) {
            if (result == "EmailInvalid") {
                resetValidation($rootResetObject);
                addCustomErrorMessage($emailResetObject, PLEASE_ENTER_VALID_EMAIL);
            }
            if (result == "Success") {
                alert(PLEASE_CHECK_YOUR_PASSWORD);
                window.location = signinaccurl;
            }
        });
    }

    $rootResetObject.on("click", signinResetSubmitSelector, function () {
        var isValid = true;
        resetValidation($rootResetObject);
        if (!validateRequiredTextData($emailResetObject, "Email")) {
            isValid = false;
        }
        var email = $emailResetObject.val();
        var isValidEmail = isValidEmailAddress(email);
        if (isValidEmail == false) {
            addCustomErrorMessage($emailResetObject, PLEASE_ENTER_VALID_EMAI);
            isValid = false;
        }
        if (isValid) {
            resetPassword(email);
        }
    });
});//end ready