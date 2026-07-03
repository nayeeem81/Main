var notify;

function submitUserLikeComment(id, actionType) {
    $.ajax({
        url: commentLikeUrl + '?commentID=' + id + '&actionType=' + actionType,
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
        }
    });

}

$(document).on("click",
    "[data-like-comment]",
    function () {
        var jObject = $(this);
        var curCount = jObject.data("count-comment");
        var postId = jObject.data("id");
        var jLike = $("[data-like-comment][data-id=" + postId + "]");
        var jCount = $("[data-like-count-comment][data-id=" + postId + "]");

        if (jObject.hasClass("active")) {
            curCount = Math.max(curCount - 1, 0);
            jLike.data("count-comment", curCount);
            jCount.text(curCount);
            jLike.removeClass("active");
            jLike.removeClass("green-icon-active");
            jLike.addClass("green-icon-inactive");
            submitUserLikeComment(postId, "Minus");
        } else {
            curCount = Math.max(curCount + 1, 0);
            jLike.data("count-comment", curCount);
            jCount.text(curCount);
            jLike.removeClass("green-icon-inactive");
            jLike.addClass("active");
            jLike.addClass("green-icon-active");
            submitUserLikeComment(postId, "Plus");
        }
    });

$(document).on("click", "#btnComment", function () {
    var that = this;
    var $comment = $("#Comment");
    var commentText = $comment.val();
    var postID = $(that).data("id");

    if (commentText != "" && commentText != undefined && commentText != null) {
        $.ajax({
            url: commentUrl + '?comment=' + commentText + '&postID=' + postID,
            type: 'GET',
            cache: false,
            success: function (todayDate) {
                $("#noComment").remove();
                $(".comment-list").prepend('<li>' +
                    '<p class="comment">' + commentText + '</p>' +
                    '<p class="date">' + todayDate + '</p>' +
                    '</li>');
                $comment.val("");
            },
            error: function () {
            }
        });
    }
});

$(function () {

    $('[data-contact="collapse"]').collapse(
        {
            parent: "#accordion",
            toggle: false
        });

    $('[data-contact="collapse"]').collapse(
        {
            parent: "#accordioncontactdetail",
            toggle: false
        });

    function isFormPasswordValidSingleItemPage() {
        return isPasswordRulesValid($passwordObject, $repasswordObject);
    }

    $(document).on("change", "#contactemail", function () {
        resetValidation($("#rootContactSingleItemPage"));
        var email = $(this).val();
        var requestData = {
            "Email": $.trim(email),
        };
        $.ajax({
            url: checkUserAccountExistByEmail,
            type: 'POST',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result == "EmailInvalid") {
                    resetValidation($("#rootContactSingleItemPage"));
                    addCustomErrorMessage($("#contactemail"), PLEASE_ENTER_VALID_EMAI);
                }
                if (result == "EmailNotFound") {
                    var $passobject = $("#divRePassword");
                    $passobject.show();
                    return false;
                }
                if (result == "EmailFound") {
                    var $passobject1 = $("#divRePassword");
                    $passobject1.hide();
                    return false;
                }
            }
        });
    });

    $(document).on("click", "#btnSubmitContactAdvertiser", function () {
        resetValidation($("#rootContactSingleItemPage"));
        var postID = $("#PostID").val();
        var phone = $("#contactphone").val();        
        var email = $("#contactemail").val();        
        var isValid = true;        
        if (phone == "" || phone == null) {
            addCustomErrorMessage($("#contactphone"), PLEASE_ENTER_PHONE);
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
        if (isValid == true) {
            //notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + SUBMITTING + '</strong>&nbsp;' + PLEASE_WAIT_FOR_A_WHILE, {
            //    type: 'info'
            //});
            $("#requestForm").hide();
            $("#accordioncontactdetail").show();
            var requestData = {
                "Email": $.trim(email),
                "PostID": postID,
                "Phone": phone
            };
            $.ajax({
                url: savecontactsingleitempageurl, //Server script to process data
                type: 'POST',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                async: true,
                cache: false,
                success: function (result) {
                    if (result == "EmailInvalid") {
                        resetValidation($("#rootContactSingleItemPage"));
                        addCustomErrorMessage($("#contactemail"), PLEASE_ENTER_VALID_EMAIL);
                    }
                    else if (result == "Success") {
                        //notify.update({
                        //    type: 'success',
                        //    message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + MESSAGE_SENT + '</strong>' + MESSAGE_SENT_TO_ADVERTISER_SUCCESSFULLY,
                        //    allow_dismiss: true,
                        //    placement: {
                        //        from: "top",
                        //        align: "right"
                        //    },
                        //    delay: { show: 500, hide: 800 },
                        //    template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                        //        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                        //        '<span data-notify="icon"></span>' +
                        //        '<span data-notify="title">{1}</span>' +
                        //        '<span data-notify="message">{2}</span>' +
                        //        '</div>'
                        //});                        
                    }                    
                    return false;
                }
            });
        }
        return false;
    });
});