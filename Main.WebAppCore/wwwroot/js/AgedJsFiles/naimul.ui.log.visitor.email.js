var visitActionName = "";
function getVisitorEmailCookei() {
    return $.cookie('visitoremail') != undefined ? $.cookie('visitoremail') : null;
}

function getVisitorPhoneCookei() {
    return $.cookie('visitorphone') != undefined ? $.cookie('visitorphone') : null;
}

function setVisitorEmailCookei(email) {
    $.cookie('visitoremail', email, { path: '/', expires: 500 });
}

function setVisitorPhoneCookei(phone) {
    $.cookie('visitorphone', phone, { path: '/', expires: 500 });
}

function doesVisitorCookeyExists() {
    return $.cookie('visitoremail') != undefined && $.cookie('visitoremail') != null && $.cookie('visitorphone') != undefined && $.cookie('visitorphone') != null ? true : false;
}

function getPostID() {
    return $("#PostID").val();
}

function SavePostVisitLogData(email, phone, postID, visitaction) {
    $.ajax({
        url: postVisitLogUrl + '?email=' + email + '&phone=' + phone + '&postID=' + postID + '&visitaction=' + visitaction,
        type: 'GET',
        cache: false,
        success: function (result) {
            
        },
        error: function () {
        }
    });

    return true;
}

function LogPostVisit() {
    if (doesVisitorCookeyExists()) {
        var emailVisitor = getVisitorEmailCookei();
        var phone = getVisitorPhoneCookei();
        var postID = getPostID();
        SavePostVisitLogData(emailVisitor, phone, postID, visitActionName);
    } else {
        //show dialog
        $("#requestVisitorEmailBackground").addClass("active");
        $("#requestVisitorEmailFormContent").addClass("active");
    }
}

$(function () {
    if (PAGE_NAME == "Item Details Page") {
        visitActionName = "PostVisit";
        setTimeout(
            LogPostVisit()
            , 3000);
    }
    else {
        visitActionName = "PostLiked";
    }

    $(document).on("click", "#visitorEmailClose", function () {
        var postID = getPostID();
        if (doesVisitorCookeyExists()) {
            var emailVisitor = getVisitorEmailCookei();
            var phone = getVisitorPhoneCookei();
            SavePostVisitLogData(emailVisitor, phone, postID, visitActionName);
        } else {
            SavePostVisitLogData("User Prefer to Not Share", "User Prefer to Not Share", postID, visitActionName);
        }
        $("#requestVisitorEmailFormContent").removeClass("active");
        $("#requestVisitorEmailBackground").removeClass("active");
    });

    $(document).on("click", "#btnSubmitVisitorEmail", function () {
        var email = $("#visitorEmail").val();
        var phone = $("#visitorPhone").val();
        var postID = getPostID();
        var notifyMe;
        notifyMe = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + SUBMITTING + '</strong>&nbsp;', {
            type: 'info'
        });
        SavePostVisitLogData(email, phone, postID, visitActionName);
        setVisitorEmailCookei(email);
        setVisitorPhoneCookei(phone);
        notifyMe.update({
            type: 'success',
            message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + MESSAGE_SENT + '</strong>' + MESSAGE_SENT_TO_ADVERTISER_SUCCESSFULLY,
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
        $("#requestVisitorEmailFormContent").removeClass("active");
        $("#requestVisitorEmailBackground").removeClass("active");
    });
});