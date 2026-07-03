$(document).on("click", ".button-template-comment", function () {
    var that = this;
    var $comment = $(this).closest(".new-comment-template").find("#Comment");
    var commentText = $comment.val();
    var postID = $(that).data("id");

    if (commentText != "" && commentText != undefined && commentText != null) {
        $.ajax({
            url: commentUrl + '?comment=' + commentText + '&postID=' + postID,
            type: 'GET',
            cache: false,
            success: function (todayDate) {
                $(that).closest(".comment-container-template").find("#noComment").remove();
                $(that).closest(".comment-container-template").find(".comment-list").prepend('<li>' +
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

function saveCategoryCookey(jsonstring) {
    $.cookie('categoryitemids', jsonstring, { path: '/', expires: 30 });
}

function getCategoryCookey() {
    var jsonstring = $.cookie('categoryitemids') != undefined ? $.cookie('categoryitemids') : '';
    var emptyObj = [];
    if (jsonstring != '') {
        var jsonObject = $.parseJSON(jsonstring);
        return jsonObject;
    }
    return emptyObj;
}

function doesExists(catId, jsonObj) {
    for (i in jsonObj) {
        if (jsonObj[i] == catId)
            return true;
    }
    return false;
}

function loadHomeData() {
    var jsonObject = getCategoryCookey();
    if (jsonObject.length > 0) {
        for (i = 0; i < jsonObject.length; i++) {
            var valClass = jsonObject[i];
            var $clones = $("." + valClass).clone();
            $("." + valClass).remove();
            $clones.prependTo("#homePageContent");
        }
    }
}

function loadData() {
    var jsonObject = getCategoryCookey();
    if (jsonObject.length > 0) {
        for (i = 0; i < jsonObject.length; i++) {
            var valClass = jsonObject[i];
            $(".int-" + valClass).removeClass("interest-item-color");
            $(".int-" + valClass).addClass("interest-item-selected-color");
        }
    }
}

function getInterestCategoryString() {
    var jsonObject = getCategoryCookey();
    var finalString = "";
    if (jsonObject.length > 0) {
        for (i = 0; i < jsonObject.length; i++) {
            var valClass = jsonObject[i];
            if (valClass != null) {
                finalString += "a" + valClass;
            }
        }
    }
    return finalString;
}

function createJSON(catId) {
    var jsonObj = getCategoryCookey();
    if (jsonObj.length > 0) {
        if (!doesExists(catId, jsonObj)) {
            jsonObj.push(catId);
            var jsonString = JSON.stringify(jsonObj);
            saveCategoryCookey(jsonString);
        }
    } else {
        jsonObj.push(catId);
        var jsonString1 = JSON.stringify(jsonObj);
        saveCategoryCookey(jsonString1);
    }
}

function removeJson(catId) {
    var jsonObj = getCategoryCookey();
    if (jsonObj.length > 0) {
        for (i = 0; i < jsonObj.length; i++) {
            if (jsonObj[i] == catId)
                delete jsonObj[i];
        }
        var jsonString1 = JSON.stringify(jsonObj);
        saveCategoryCookey(jsonString1);
    }
}

$(function () {
    //var notify;
    $('[data-toggle="tooltip"]').tooltip();

    function ResetButtonPositions() {
        $("i.fabia-btn").removeClass("orange");
        $("i.fabia-btn").addClass("white");
        $("i.left-stiky-btn").removeClass("orange");
        $("i.left-stiky-btn").addClass("white");
    }

    $("#btnRequest").click(function () {
        var that = this;
        ResetButtonPositions();
        if (!$(".left-request-side-menu").is(":visible")) {
            $(".fabia-panel").hide();
            $(".left-request-side-menu").show();
            $("#btnRequest").removeClass("request-not-shown");
            $("#btnRequest").addClass("request-shown");
        } else {
            $(".fabia-panel").hide();
            $("#btnRequest").removeClass("request-shown");
            $("#btnRequest").addClass("request-not-shown");
        }
    });

    $("#btnExport").click(function () {
        var that = this;
        ResetButtonPositions();
        if (!$(".left-export-side-menu").is(":visible")) {
            $(".fabia-panel").hide();
            $(".left-export-side-menu").show();
            $("#btnExport").removeClass("request-not-shown");
            $("#btnExport").addClass("request-shown");
        } else {
            $(".fabia-panel").hide();
            $("#btnExport").removeClass("request-shown");
            $("#btnExport").addClass("request-not-shown");
        }
    });

    $("#btnImport").click(function () {
        var that = this;
        ResetButtonPositions();
        if (!$(".left-import-side-menu").is(":visible")) {
            $(".fabia-panel").hide();
            $(".left-import-side-menu").show();
            $("#btnImport").removeClass("request-not-shown");
            $("#btnImport").addClass("request-shown");
        } else {
            $(".fabia-panel").hide();
            $("#btnImport").removeClass("request-shown");
            $("#btnImport").addClass("request-not-shown");
        }
    });

    $("#btnInterest").click(function () {
        var that = this;
        ResetButtonPositions();
        if (!$(".left-interest-side-menu").is(":visible")) {
            loadData();
            $(".fabia-panel").hide();
            $(".left-interest-side-menu").show();            
            $("#btnInterest").removeClass("white");
            $("#btnInterest").addClass("orange");
        } else {
            $(".fabia-panel").hide();
            $("#btnInterest").removeClass("orange");
            $("#btnInterest").addClass("white");
        }
    });

    $("#btnFabia").click(function () {
        var that = this;
        ResetButtonPositions();
        if (!$(".fabia-side-menu").is(":visible")) {
            $(".fabia-panel").hide();
            $(".fabia-side-menu").show();
            $(that).addClass("orange");
        } else {
            $(".fabia-panel").hide();
            $(that).removeClass("orange");
        }
    });

    $("#btnCat").click(function () {
        var that = this;
        ResetButtonPositions();
        if (!$(".cat-side-menu").is(":visible")) {
            $(".fabia-panel").hide();
            $(".cat-side-menu").show();
            $(that).addClass("orange");
        } else {
            $(".fabia-panel").hide();
            $(that).removeClass("orange");
        }
    });

    $("#btnCustomTitle").click(function () {
        var that = this;
        ResetButtonPositions();
        if (!$(".custom-side-menu").is(":visible")) {
            $(".fabia-panel").hide();
            $(".custom-side-menu").show();
            $(that).addClass("orange");
        } else {
            $(".fabia-panel").hide();
            $(that).removeClass("orange");
        }
    });

    $(document).on("click", ".interest-item", function () {
        var that = this;
        if (!$(that).hasClass("interest-item-selected-color")) {
            $(that).removeClass("interest-item-color");
            $(that).addClass("interest-item-selected-color");
            var categoryID = $(that).data("id");
            var categoryName = $(that).data("name");
            createJSON(categoryID, categoryName);
            var $clones = $("." + categoryID).clone();
            $("." + categoryID).remove();
            $clones.prependTo("#homePageContent");
        } else {
            $(that).removeClass("interest-item-selected-color");
            $(that).addClass("interest-item-color");
            var categoryIDDel = $(that).data("id");
            var categoryNameDel = $(that).data("name");
            removeJson(categoryIDDel, categoryNameDel);            
        }
    });

    $(document).on("click", ".fixed-right-fabia-btn", function () {
        $(".fabia-panel").hide();
        $(this).hide();
        $(".fabia-btn").not(".fixed-right-fabia-btn").show();
        $(".left-stiky-btn").show();
    });

    $(document).on("click", ".fixed-left-fabia-btn", function () {
        $(".fabia-btn").hide();
        $(".fabia-panel").hide();
        $(".fixed-right-fabia-btn").show();
        $(".left-stiky-btn").show();
    }); 

    $(document).on("click", ".alpha-item", function () {
        $(".alpha-item").closest("div").removeClass("fabia-icon-selected");
        $(this).closest("div").addClass("fabia-icon-selected");
        var postId = $(this).data("id");
        if ($(this).hasClass("clickable")) {
            $(".fa-service-list").hide();
            $(".sub-list-" + postId).show();
        } else {
            $(".sub-list-" + postId).hide();
        }
    });
    
    function CreateNotification() {
        var notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + SAVING_DATEBASE + '</strong>...', {
            type: 'info'
        });
        return notify;
    }

    function UpdateNotification(notify, message) {
        notify.update({
            type: 'success',
            message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + SUCCESS + '</strong> ' + message,
            allow_dismiss: true,
            placement: {
                from: "top",
                align: "right"
            },
            delay: { show: 900, hide: 100 },
            template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                '<span data-notify="icon"></span>' +
                '<span data-notify="title">{1}</span>' +
                '<span data-notify="message">{2}</span>' +
                '</div>'
        });
    }

    function SendRequest(phoneNumber, description, url, notify) {
        var objRequest = {
            "PhoneNumber": phoneNumber,
            "Message": description
        };
        $.ajax({
            url: url,
            type: 'POST',
            data: JSON.stringify(objRequest),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            error: function (xhr) {
            },
            success: function (result) {
                UpdateNotification(notify, 'Successfully sent your message, thanks!');
                $(".fabia-panel").hide();
                $(".fabia-panel").find("input[type=tel]").val("");
                $(".fabia-panel").find("textarea").val("");
            },
            processData: false,
            cache: false
        });
    }

    $(document).on("click", "#btnSubmitRequest", function () {
        var notify = CreateNotification();
        var description = $("#txtRequestDescription").val();
        var phoneNumber = $("#txtRequestPhoneNumber").val();
        SendRequest(phoneNumber, description, submitProductRequestURL, notify);        
    });

    $(document).on("click", "#btnSubmitImport", function () {
        var notify = CreateNotification();
        var description = $("#txtImportDescription").val();
        var phoneNumber = $("#txtImportPhoneNumber").val();
        SendRequest(phoneNumber, description, submitImportRequestURL, notify);
    });

    $(document).on("click", "#btnSubmitExport", function () {
        var notify = CreateNotification();
        var description = $("#txtExportDescription").val();
        var phoneNumber = $("#txtExportPhoneNumber").val();
        SendRequest(phoneNumber, description, submitExportRequestURL, notify);
    });

    $(document).on("click", "#btnNewProvider", function () {
        window.location = newProviderURL;
    });

    $(document).on("click", "#btnNewPost", function () {
        window.location = newPostURL;
    });

});