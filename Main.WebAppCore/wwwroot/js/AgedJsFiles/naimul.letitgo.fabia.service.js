    var notify;
    
    function getPostDetails() {
        return $.ajax({
            url: editpostbyidurl,
            type: 'GET',
            cache: false
        });
    }

    function getUploadImageURL(postid, serial) {
        var url = uploadimageurl + '?postId=' + postid + '&serialNo=' + serial;
        return url;
    }

    function getImageContentLoadURL(postId, serial) {
        var url = imageloaadmodifymodeurl + '?postId=' + postId + '&serialNo=' + serial;
        return url;
    }

    function loadImageContent(postId, serial, dialogedit) {
        $.ajax({
            type: 'GET',
            url: getImageContentLoadURL(postId, serial),
            cache: false,
            success: function (result) {
                $("#imgDiv" + serial, $(dialogedit)).empty().html(result);
            },
            error: function (e) {
                notify = $.notify('<strong>' + Error + '</i>&nbsp;</strong>' + REQUEST_SEND_FAILED, {
                    type: 'danger',
                    allow_dismiss: true,
                    placement: {
                        from: "top",
                        align: "right"
                    },
                    delay: { show: 500, hide: 100 }
                });
            }
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
}

function loadProcessImageContent() {
    $.ajax({
        type: 'GET',

        url: imageLoadProcessUrl,
        cache: false,
        success: (function (result) {
            $("#imgDivProcess").empty().html(result);
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

function loadServiceImageContent() {
    $.ajax({
        type: 'GET',
        url: imageLoadServiceUrl,
        cache: false,
        success: (function (result) {
            $("#imgDivService").empty().html(result);
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

    function isFormRequiredFieldsValid(that) {
        var $parentObject = $(that).closest(".modify-post-root").eq(0);
        var $titleObject = $("#Title", $parentObject);       
        var $priceObject = $("#Price", $parentObject);

        var isValid = true;        
        if (!validateRequiredTextData($titleObject, "Title")) {
            isValid = false;
        }       
        if (!validateRequiredTextData($priceObject, "Price")) {
            isValid = false;
        }
        return isValid;
    }

    function updateValidation(that) {
        var $parentObject = $(that).closest(".modify-post-root").eq(0);
        resetValidation($parentObject);
        isFormRequiredFieldsValid(that);
    }

    function isValidForm(that) {
        var $parentObject = $(that).closest(".modify-post-root").eq(0);
        resetValidation($parentObject);
        if (isFormRequiredFieldsValid(that)) {
            return true;
        }
        return false;
    }    

    function collectFormData(that) {
        var $frm = $(that).closest(".modify-post-root");
        var postId = $frm.find("#PostID").val();
        var posterContact = $frm.find("#PosterContactNumber").val();
        var posterName = $frm.find("#PosterName").val();
        var title = $frm.find("#Title").val();
        var price = $frm.find("#Price").val();
        var description = $frm.find("#Description").val();
        var searchTag = $frm.find("#SearchTag").val();
        var websiteUrl = $frm.find("#WebsiteUrl").val();

        var requestData = {
            "PostID": postId,
            "Title": $.trim(title),
            "PosterContactNumber": $.trim(posterContact),
            "PosterName": $.trim(posterName),
            "Description": $.trim(description),
            "Price": price,          
            "WebsiteUrl": $.trim(websiteUrl),
            "SearchTag": $.trim(searchTag)
        };
        return requestData;
    }

    $(document).on("click", "input.btn-submit-post-modify-mode", function (e) {
        e.preventDefault();
        var that = this;
        var notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + UPDATEING + '</strong>...', {
            type: 'info'
        });

        if (isValidForm(that)) {
            var requestData = collectFormData(that);
            $.ajax({
                url: updateposturl, 
                type: 'POST',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                error: function (xhr) {
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
                        window.location = reloadUrl;
                    }
                    return false;
                },
                processData: false
            });
        } else {
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
            
        }
        return false;
});


function getPostInfo() {
    var $dialogedit = $(".modify-post-root");

    $(".Upload1", $dialogedit).ajaxUpload({
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

    $(".Upload2", $dialogedit).ajaxUpload({
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

    $(".Upload3", $dialogedit).ajaxUpload({
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

    $(".Upload4", $dialogedit).ajaxUpload({
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

    $("#UploadProcess").ajaxUpload({
        url: uploadProcesImageUrl,
        name: "file",
        data: true,
        cache: false,
        onSubmit: function () {
            notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + LOADING + '</strong>...', {
                type: 'info'
            });
        },
        onComplete: function (response) {
            loadProcessImageContent();
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

    $("#UploadService").ajaxUpload({
        url: uploadServiceImageUrl,
        name: "file",
        data: true,
        cache: false,
        onSubmit: function () {
            notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + LOADING + '</strong>...', {
                type: 'info'
            });
        },
        onComplete: function (response) {
            loadServiceImageContent();
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

    $(".browse-service").ajaxUpload({
        url: uploadServiceImageUrl,
        name: "file",
        data: true,
        cache: false,
        onSubmit: function () {
            notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + LOADING + '</strong>...', {
                type: 'info'
            });
        },
        onComplete: function (response) {
            $("#spnUpdateUpload").show();
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

    $(".browse-process").ajaxUpload({
        url: uploadProcessImageUrl,
        name: "file",
        data: true,
        cache: false,
        onSubmit: function () {
            notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + LOADING + '</strong>...', {
                type: 'info'
            });
        },
        onComplete: function (response) {
            $("#spnUpdateUpload").show();
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
}         
