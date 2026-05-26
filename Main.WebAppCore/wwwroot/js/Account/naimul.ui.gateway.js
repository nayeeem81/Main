var $rootGatewayObject = $("#GatewayRoot", document);
var signinSubmitSelector = "#btnLoginGateway";
var $signinSubmitObject = $("#btnLoginGateway", $rootGatewayObject);
var $emailPinObject = $("#Pin", $rootGatewayObject);
var $passwordObject = $("#Passcode", $rootGatewayObject);

$(function () {
    function loginAdmin(user, pass) {
        var data = { "Pin": user, "Passcode": pass };
        $.ajax({
            url: authnadmingateway,
            type: 'POST',
            data: data,
            cache: false
        }).done(function (result) {
            if (result == "BadTry") {
                window.location = adminsignaccount;
            } else {
                window.location = result;
            }
        }).fail(function (xhr, status, errorThrown) {
        });
    }

    $rootGatewayObject.on("click", signinSubmitSelector, function () {
        var isValid = true;
        resetValidation($rootGatewayObject);
        if (!validateRequiredTextData($emailPinObject, "PIN")) {
            isValid = false;
        }
        if (!validateRequiredTextData($passwordObject, "PASS CODE")) {
            isValid = false;
        }
        if (isValid) {
            var email = $emailPinObject.val();
            var pass = $passwordObject.val();
            loginAdmin(email, pass);
        }
    });
});