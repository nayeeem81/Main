$(btnviewid).click(function (e) {
    e.preventDefault();
    $(this).tab('show');
    $(edittabpaneidmsg).removeClass("active");
    $(viewtabpaneidmsg).addClass("active");
});

$(btneditid).click(function (e) {
    e.preventDefault();
    $(this).tab('show');
    $(viewtabpaneidmsg).removeClass("active");
    $(edittabpaneidmsg).addClass("active");
});

$(function () {
    $('[data-manage-post="collapse"]').collapse(
        {
            parent: "#accordioninbox",
            toggle: true
        });

    $('[data-manage-post="collapse"]').collapse(
        {
            parent: "#accordionoutbox",
            toggle: true
        });

    $(document).on("click", ".btn-submit-view-inbox-msg", function (e) {
        e.preventDefault();
        var $frm = $(this).closest("form.reply-form").eq(0);
        resetValidation($frm);

        var ReceiverUserID = $frm.find("#ReceiverUserID").eq(0).val();
        var SenderUserID = $frm.find("#SenderUserID").eq(0).val();
        var MessageID = $frm.find("#MessageID").eq(0).val();
        var ParentMessageID = $frm.find("#ParentMessageID").eq(0).val();

        var msg = $frm.find("#replymessage").eq(0).val();

        var isValid = true;

        if (msg == "" || msg == null) {
            addCustomErrorMessage($frm.find("#replymessage").eq(0), PLEASE_ENTER_MSG_TO_REPLY);
            isValid = false;
        }


        if (isValid == true) {
            notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + REPLYING + '</strong>&nbsp;' + PLEASE_WAIT_FOR_A_WHILE, {
                type: 'info'
            });
            //send email to seleasy admin
            var requestData = {
                "ReceiverUserID": $.trim(SenderUserID),
                "SenderUserID": $.trim(ReceiverUserID),
                "MessageID": $.trim(MessageID),
                "ParentMessageID": $.trim(ParentMessageID),
                "Message": $.trim(msg),
            };

            $.ajax({
                url: savereplymsgurl, //Server script to process data
                type: 'POST',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    if (result == "Success") {
                        notify.update({
                            type: 'success',
                            message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + MESSAGE_SENT + '</strong> ' + MESSAGE_SENT_SUCCESSFUL,
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
                        //alert("Mesage has been sent successfully. We will reply to you as soon as possible.");
                        return false;
                    }
                }
            });
        }
        return false;
    });

    function updateMessageStatus(id) {
        var requestData = { "MessageID": id };
        $.ajax({
            url: updatemsgstatusurl,
            type: 'POST',
            data: requestData,
            cache: false,
            done: function (result) {
            }
        });
    }

    $(document).on("click", ".single-msg-inbox", function (e) {
        e.preventDefault();
        var id = $(this).attr("data-id");
        updateMessageStatus(id);
    });
});