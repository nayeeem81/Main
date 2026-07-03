$(function () {
    var notify;
    var $parentObject = $("#SigninRoot", document);
    var $form1 = $("#frmMamageAccount", document);
    var $UserID = $("#UserID", $form1);
    var $Phone = $("#Phone", $form1);
    var $ClientName = $("#ClientName", $form1);
    var $IsPrivate = $("#IsPrivateSeller");
    var $IsCompany = $("#IsCompanySeller");
    var $form2 = $("#frmChangePassword", document);
    var $currentPass = $("#CurrentPassword", $form2);
    var $newPass = $("#Password", $form2);
    var $rePass = $("#RePassword", $form2);

    function isFormPasswordValid() {
        return isPasswordRulesValid($newPass, $rePass);
    }

    function isFormRequiredFieldsValid() {
        var isValid = true;
        if (!validateRequiredTextData($Phone, "Phone")) {
            isValid = false;
        }
        if (!validateRequiredTextData($ClientName, "Full Name")) {
            isValid = false;
        }
        return isValid;
    }

    function isPasswordFormRequiredFieldsValid() {
        var isValid = true;
        if (!validateRequiredTextData($currentPass, "Current Password")) {
            isValid = false;
        }
        if (!validateRequiredTextData($newPass, "New Password")) {
            isValid = false;
        }
        if (!validateRequiredTextData($rePass, "Retype New Password")) {
            isValid = false;
        }
        return isValid;
    }

    function updateAccountValidation() {
        resetValidation($parentObject);
        isFormRequiredFieldsValid();
    }

    function updatePasswordValidation() {
        resetValidation($parentObject);
        isPasswordFormRequiredFieldsValid();
        isFormPasswordValid();
    }

    $(document).on("change", "#frmMamageAccount", function () {
        updateAccountValidation();
    });

    $(document).on("change", "#frmChangePassword", function () {
        updatePasswordValidation();
    });

    function isValidForm() {
        resetValidation($parentObject);
        if (isFormRequiredFieldsValid()) {
            return true;
        }
        return false;
    }

    function collectFormData() {
        var phone = $Phone.val();
        var name = $ClientName.val();
        var userid = $UserID.val();
        var currpass = $currentPass.val();
        var newpass = $newPass.val();
        var repass = $rePass.val();
        var requestData = {
            "ClientName": $.trim(name),
            "Phone": $.trim(phone),
            "UserID": $.trim(userid)
        };
        return requestData;
    }

    $("#btnSubmitAccount").click(function () {
        notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + SAVING_DATEBASE + '</strong>...', {
            type: 'info'
        });
        if (isValidForm()) {
            var requestData = collectFormData();
            $.ajax({
                url: updateaccount, 
                type: 'POST',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                error: function (xhr) {
                },
                success: function (result) {
                    if (result == "UnwantedAccessError") {
                        window.location = unwanteraccessurl;
                    }
                    else {
                        notify.update({
                            type: 'success',
                            message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + SUCCESS + '</strong> ' + YOUR_ACCOUNT_HAS_BEEN_UPDATED,
                            allow_dismiss: true,
                            placement: {
                                from: "top",
                                align: "right"
                            },
                            delay: { show: 500, hide: 100 },
                            template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                                          '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                                          '<span data-notify="icon"></span>' +
                                          '<span data-notify="title">{1}</span>' +
                                          '<span data-notify="message">{2}</span>' +
                                      '</div>'
                        });
                    }
                },
                processData: false
            });
        } else {
            notify.update({
                type: 'danger',
                message: '<strong>' + VALIDATION_FAILED + '</strong>&nbsp; ' + POST_NOT_SAVED +
                    PLEASE_ENTER_TEXT_FOR_INPUT,
                allow_dismiss: true,
                placement: {
                    from: "top",
                    align: "right"
                },
                delay: { show: 500, hide: 100 },
                template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                              '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                              '<span data-notify="icon"></span>' +
                              '<span data-notify="title">{1}</span>' +
                              '<span data-notify="message">{2}</span>' +
                          '</div>'
            });
        }
    });

    function collectPasswordFormData() {
        var userid = $UserID.val();
        var currpass = $currentPass.val();
        var newpass = $newPass.val();
        var repass = $rePass.val();
        var requestData = {
            "UserID": $.trim(userid),
            "CurrentPassword": currpass,
            "Password": newpass,
            "RePassword": repass
        };
        return requestData;
    }


    $("#btnChangePassword").click(function () {
        notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + SAVING_DATEBASE + '</strong>...', {
            type: 'info'
        });
        if (isFormPasswordValid() && isPasswordFormRequiredFieldsValid()) {
            var requestData = collectPasswordFormData();
            $.ajax({
                url: updateaccpassword, 
                type: 'POST',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                error: function (xhr) {
                },
                success: function (result) {
                    if (result == "UnwantedAccessError") {
                        window.location = unwanteraccessurl;
                    }
                    else if (result == true) {
                        notify.update({
                            type: 'success',
                            message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + SUCCESS + '</strong> ' + PASSWORD_HAS_BEEN_CHANGED,
                            allow_dismiss: true,
                            placement: {
                                from: "top",
                                align: "right"
                            },
                            delay: { show: 500, hide: 100 },
                            template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                                          '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                                          '<span data-notify="icon"></span>' +
                                          '<span data-notify="title">{1}</span>' +
                                          '<span data-notify="message">{2}</span>' +
                                      '</div>'
                        });
                        $currentPass.val("");
                        $newPass.val("");
                        $rePass.val("");
                    } else {
                        addCustomErrorMessage($rePass, PASSWORD_NOT_CHANGED);
                        notify.update({
                            type: 'danger',
                            message: '<strong></i>&nbsp;' + VALIDATION_FAILED + '</strong> ' + PASSWORD_CHANGE_FAILED,
                            allow_dismiss: true,
                            placement: {
                                from: "top",
                                align: "right"
                            },
                            delay: { show: 500, hide: 100 },
                            template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                                          '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                                          '<span data-notify="icon"></span>' +
                                          '<span data-notify="title">{1}</span>' +
                                          '<span data-notify="message">{2}</span>' +
                                      '</div>'
                        });
                        $currentPass.val("");
                        $newPass.val("");
                        $rePass.val("");
                    }

                },
                processData: false
            });
        } else {
            notify.update({
                type: 'danger',
                message: '<strong> ' + VALIDATION_FAILED + '</strong>&nbsp;' + PASSWORD_CANNOT_BE_CHANGED +
                    PLEASE_ENTER_TEXT_FOR_INPUT_FIELDS,
                allow_dismiss: true,
                placement: {
                    from: "top",
                    align: "right"
                },
                delay: { show: 500, hide: 100 },
                template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                              '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                              '<span data-notify="icon"></span>' +
                              '<span data-notify="title">{1}</span>' +
                              '<span data-notify="message">{2}</span>' +
                          '</div>'
            });
        }
    });

});