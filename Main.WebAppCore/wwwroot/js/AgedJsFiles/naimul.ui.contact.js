var notify;
$(function () {
    $(document).on("click", "#btnSubmitMessage", function () {
        resetValidation($("#rootContact"));
        var name = $("#contactname").val();
        var email = $("#contactemail").val();
        var subject = $("#contactsubject").val();
        var message = $("#contactmessage").val();
        var messagecategory = $("#MessageCategory").val();
        var isValid = true;
        if (name == "" || name == null) {
            addCustomErrorMessage($("#contactname"), PLEASE_ENTER_NAME);
            isValid = false;
        }
        if (email == "" || email == null) {
            addCustomErrorMessage($("#contactemail"), PLEASE_ENTER_EMAIL);
            isValid = false;
        }
        var isValidEmail = isValidEmailAddress(email);
        if (isValidEmail == false) {
            addCustomErrorMessage($("#contactemail"), PLEASE_ENTER_VALID_EMAI);
            isValid = false;
        }
        if (subject == "" || subject == null) {
            addCustomErrorMessage($("#contactsubject"), PLEASE_ENTER_SUBJECT_FOR_MESSAGE);
            isValid = false;
        }
        if (messagecategory == "" || messagecategory == null) {
            addCustomErrorMessage($("#MessageCategory"), PLEASE_ENTER_AREA_FOR_MESSAGE);
            isValid = false;
        }
        if (message == "" || message == null) {
            addCustomErrorMessage($("#contactmessage"), PLEASE_ENTER_YOUR_MESSAGE);
            isValid = false;
        }
        if (isValid == true) {
            notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + SUBMITTING + '</strong>&nbsp;' + PLEASE_WAIT_FOR_A_WHILE, {
                type: 'info'
            });
            var requestData = {
                "FullName": $.trim(name),
                "Email": $.trim(email),
                "Subject": $.trim(subject),
                "Message": $.trim(message),
                "MessageCategory": $.trim(messagecategory)
            };
            $.ajax({
                url: savecontacturl, 
                type: 'POST',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    if (result == "EmailInvalid") {
                        resetValidation($("#rootContact"));
                        addCustomErrorMessage($("#contactemail"), PLEASE_ENTER_VALID_EMAI);
                    }
                    if (result == "Success") {
                        notify.update({
                            type: 'success',
                            message: '<strong>' + MESSAGE_RECEIVED + '</strong>&nbsp;' + WE_WILL_CONTACT_AT_AN_EARLIEST,
                            allow_dismiss: true,
                            placement: {
                                from: "top",
                                align: "right"
                            },
                            delay: { show: 500, hide: 800 },
                            template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                                          '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                                          '<span data-notify="icon"></span>' +
                                          '<span data-notify="title">{1}</span>' +
                                          '<span data-notify="message">{2}</span>' +
                                      '</div>'
                        });
                        return false;
                    }
                }
            });
        }
        return false;
    });
});
