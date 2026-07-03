$(function () {
    var notify;
    $('[data-manage-post="collapse"]').collapse(
        {
            parent: "#accordion",
            toggle: true
        });

    function getPostDetails(postid, isedit) {
        var requestData = { "PostID": postid };
        var url = "";
        if (isedit)
            url = editpostbyidurlImage;
        else
            url = viewpostbyidurlImage;
        return $.ajax({
            url: url,
            type: 'POST',
            data: requestData,
            cache: false
        });
    }

    function getUploadImageURL(postid, serial) {
        var url = uploadimageurlImage + '?postId=' + postid + '&serialNo=' + serial;
        return url;
    }

    function getImageContentLoadURL(postId, serial) {
        var url = imageloaadmodifymodeurlImage + '?postId=' + postId + '&serialNo=' + serial;
        return url;
    }

    function loadImageContent(postId, serial, dialogedit) {
        $.ajax({
            type: 'GET',
            url: getImageContentLoadURL(postId, serial),
            cache: false,
            success: (function (result) {
                $("#imgDiv" + serial, $(dialogedit)).empty().html(result);
            }),
            error: (function (e) {
                notify = $.notify('<strong>' + Error + '</i>&nbsp;</strong>' + REQUEST_SEND_FAILED, {
                    type: 'danger',
                    allow_dismiss: true,
                    placement: {
                        from: "top",
                        align: "right"
                    },
                    delay: { show: 500, hide: 100 }
                });
            })
        });
    }

    function loadImagesForEdit(postid, dialogedit) {
        loadImageContent(postid, 1, dialogedit);
        setTimeout(function () {
            loadImageContent(postid, 2, dialogedit);
        }, 200);
        setTimeout(function () {
            loadImageContent(postid, 3, dialogedit);
        }, 400);
        setTimeout(function () {
            loadImageContent(postid, 4, dialogedit);
        }, 600);
    };

    $(".btn-viewid-tab").click(function (e) {
        e.preventDefault();
        $(this).tab('show');
        var $viewtabpane = $(this).closest("div.panel-body").find(".tab-pane-view-mode").eq(0);
        var $edittabpane = $(this).closest("div.panel-body").find(".tab-pane-edit-mode").eq(0);
        $viewtabpane.addClass("active");
        $edittabpane.removeClass("active");
        var postid = $(this).attr("data-id");
        var $dialogview = $("#dialogView-" + postid);
        var $dialogedit = $(this).closest("div.panel-body").find(".dialog-edit").eq(0);
        
        if (postid !== -1) {
            getPostDetails(postid, false).done(function (result) {
                $dialogedit.empty().hide();
                $dialogview.empty().html(result).show();
            });
        }
    });

    $(".btn-editid-tab").click(function (e) {
        e.preventDefault();
        var that = this;
        var $viewtabpane = $(this).closest("div.panel-body").find(".tab-pane-view-mode").eq(0);
        var $edittabpane = $(this).closest("div.panel-body").find(".tab-pane-edit-mode").eq(0);
        var $dialogview = $(this).closest("div.panel-body").find(".dialog-view").eq(0);
        var $dialogedit = $(this).closest("div.panel-body").find(".dialog-edit").eq(0);
        var postid = $(this).attr("data-id");
        if (postid !== -1) {
            getPostDetails(postid, true).done(function (result) {
                $dialogview.empty().hide();
                $dialogedit.empty().append(result).show();
                setTimeout(function () {
                    loadImagesForEdit(postid, $dialogedit);
                }, 300);
                $("#Upload1", $dialogedit).ajaxUpload({
                    url: getUploadImageURL(postid, 1),
                    name: "file",
                    data: true,
                    cache: false,
                    onSubmit: function () {
                        notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + LOADING + '</strong>...', {
                            type: 'info'
                        });
                    },
                    onComplete: function (response) {
                        loadImageContent(postid, 1, $dialogedit);
                        notify.update({
                            type: 'success',
                            message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + SUCCESS + '</strong> ' + IMAGE_LOAD_SUCCESSFUL,
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
                $("#Upload2", $dialogedit).ajaxUpload({
                    url: getUploadImageURL(postid, 2),
                    name: "file",
                    data: true,
                    cache: false,
                    onSubmit: function () {
                        notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + LOADING + '</strong>...', {
                            type: 'info'
                        });
                    },
                    onComplete: function (response) {
                        loadImageContent(postid, 2, $dialogedit);
                        notify.update({
                            type: 'success',
                            message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + SUCCESS + '</strong> ' + IMAGE_LOAD_SUCCESSFUL,
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
                $("#Upload3", $dialogedit).ajaxUpload({
                    url: getUploadImageURL(postid, 3),
                    name: "file",
                    data: true,
                    cache: false,
                    onSubmit: function () {
                        notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + LOADING + '</strong>...', {
                            type: 'info'
                        });
                    },
                    onComplete: function (response) {
                        loadImageContent(postid, 3, $dialogedit);
                        notify.update({
                            type: 'success',
                            message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + SUCCESS + '</strong>' + IMAGE_LOAD_SUCCESSFUL,
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
                $("#Upload4", $dialogedit).ajaxUpload({
                    url: getUploadImageURL(postid, 4),
                    name: "file",
                    data: true,
                    cache: false,
                    onSubmit: function () {
                        notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + LOADING + '</strong>...', {
                            type: 'info'
                        });
                    },
                    onComplete: function (response) {
                        loadImageContent(postid, 4, $dialogedit);
                        notify.update({
                            type: 'success',
                            message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + SUCCESS + '</strong> ' + IMAGE_LOAD_SUCCESSFUL,
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
        }
        setTimeout(function () {
            $(that).tab('show');
            $viewtabpane.removeClass("active");
            $edittabpane.addClass("active");
        }, 500);
    });

    function collectFormData(that) {
        var $parentObject = $(that).closest(".modify-post-root").eq(0);
        var $form1 = $parentObject.find("form.form1").eq(0);
        //form1
        var postId = $form1.find("#PostID").val();
        var requestData = {
            "PostID": $.trim(postId)
        };
        return requestData;
    }

    $(document).on("click", "input.btn-submit-post-modify-mode", function (e) {
        e.preventDefault();
        var that = this;
        var notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + UPDATEING + '</strong>...', {
            type: 'info'
        });
        var requestData = collectFormData(that);
        $.ajax({
            url: updateposturlImage, 
            type: 'POST',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            error: function (xhr) {
                notify.update({
                    type: 'danger',
                    message: '<strong>' + VALIDATION_FAILED + '</strong>&nbsp;' + POST_NOT_UPDATED +
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
            },
            success: function (result) {
                if (result === "UserEmailAlreadyThere") {
                    alert(USER_EMAIL_ALREADY_IN_DATABASE_MESSAGE);
                } else {
                    notify.update({
                        type: 'success',
                        message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + SUCCESS + '</strong> ' + YourPostHasBeenUpdated,
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
                return false;
            },
            processData: false
        });
        return false;
    });
});