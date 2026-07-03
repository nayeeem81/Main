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
            url = editpostbyidurl;
        else
            url = viewpostbyidurl;
        return $.ajax({
            url: url,
            type: 'POST',
            data: requestData,
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
        var $dialogview = $(this).closest("div.panel-body").find(".dialog-view").eq(0);
        var $dialogedit = $(this).closest("div.panel-body").find(".dialog-edit").eq(0);
        var postid = $(this).attr("data-id");
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

    $(document).on("click", "a.button-ok", function (e) {
        e.preventDefault();
        var notify = $.notify('<strong>' + DELETING + '</strong>...', {
            type: 'info'
        });
        var postid = $(this).closest('.popover').find(".popover-postid").eq(0).attr("data-postid");
        var requestData = { "PostID": postid };
        var url = "";
        url = deletepostbyidurl;
        $.ajax({
            url: url,
            type: 'POST',
            data: requestData,
            cache: false
        }).done(function (result) {
            if (result) {
                notify.update({
                    type: 'success',
                    message: '<strong>' + DELETE_SUCCESS + '</strong> ' + DELETED_TEXT,
                    allow_dismiss: true,
                    placement: {
                        from: "top",
                        align: "right"
                    },
                    delay: 1000,
                    template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                        '<span data-notify="icon"></span>' +
                        '<span data-notify="title">{1}</span>' +
                        '<span data-notify="message">{2}</span>' +
                        '</div>'
                });
                setTimeout(function () { window.location = manageposturl; }, 1000);
            }
        });
    });

    $(document).on("click", "a.button-no", function (e) {
        e.preventDefault();
        $(this).closest('.popover').popover("hide");
    });

    $(document).on("click", "a.delete-post", function (e) {
        e.preventDefault();
        var postid = $(this).attr("data-id");
        var id = $(this).attr("id");
        if (postid !== -1) {
            $(this).popover({
                animation: true,
                popout: false,
                singleton: true,
                container: 'body',
                title: DELETE_CNFIRMATION,
                trigger: 'click',
                placement: 'left',
                selector: this,
                html: true,
                content: ARE_YOU_SURE_TO_DLETE + '<span class="popover-postid" data-postid="' + postid + '"></span>',
                template: '<div class="popover" role="tooltip" id="popover-' + postid + '">' +
                    '<div class="arrow"></div>' +
                    '<h3 class="popover-title"></h3>' +
                    '<div class="popover-content"></div>' +
                    '<div class="row">' +
                    '<div class="col-md-6 col-md-offset-3">' +
                    '<div class="btn-group text-center">' +
                    '<a class="btn btn-small btn-primary button-ok" id="btnOk-' + postid + '">' +
                    '<i class="icon-ok-sign icon-white"></i>' +
                    ' Yes</a>&nbsp;' +
                    '<a class="btn btn-small btn-default button-no" id="btnNo-' + postid + '">' +
                    '<i class="icon-remove-sign"></i>' +
                    ' No</a>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +

                    '</div>'
            });
            $(this).popover('show');
        } else {
            notify.update({
                type: 'danger',
                message: '<strong>' + Error + '</strong>&nbsp;' + YOUR_POST_IS_NOT_DELETED_BECAUSE_OF,
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

    function isFormRequiredFieldsValid(that) {
        var $parentObject = $(that).closest(".modify-post-root").eq(0);
        var $form1 = $parentObject.find("form.form1").eq(0);
        var $titleObject = $("#Title", $form1);
        var $categoryObject = $("#CategoryID", $form1);
        var $subCategoryObject = $("#SubCategoryID", $form1);
        var $priceObject = $("#Price", $form1);

        var isValid = true;
        if (!validateMaxLengthTextData($titleObject, 80, "Title")) {
            isValid = false;
        }
        if (!validateRequiredTextData($titleObject, "Title")) {
            isValid = false;
        }
        //if (!validateRequiredTextData($categoryObject, "Category")) {
        //    isValid = false;
        //}
        //if (!validateRequiredTextData($subCategoryObject, "Sub Category")) {
        //    isValid = false;
        //}
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

    $(document).on("change", ".modify-post-root", function (e) {
        e.preventDefault();
        var root = this;
        var that = $(root).find("input.update-post").eq(0);
        updateValidation(that);
    });

    function collectFormData(that) {
        var $parentObject = $(that).closest(".modify-post-root").eq(0);
        var $form1 = $parentObject.find("form.form1").eq(0);
        //form1
        var postId = $form1.find("#PostID").val();
        var posterContact = $form1.find("#PosterContactNumber").val();
        var posterName = $form1.find("#PosterName").val();
        var title = $form1.find("#Title").val();
        var price = $form1.find("#Price").val();
        //form2
        var description = $form1.find("#Description").val();
        var searchTag = $form1.find("#SearchTag").val();
        var state = $form1.find("#StateID").val();
        var area = $form1.find("#AreaDescription").val();
        var website = $form1.find("#WebsiteUrl").val();
        //form1
        var forSell = $form1.find("input[id='IsForSell']:checked").val();
        var privateSeller = $form1.find("input[id='IsPrivateSeller']:checked").val();
        var urgentDeal = $form1.find("input[id='IsUrgent']:checked").val();
        var newItem = $form1.find("input[id='IsBrandNew']:checked").val();

        var requestData = {
            "PostID": $.trim(postId),
            "Title": $.trim(title),
            "PosterContactNumber": $.trim(posterContact),
            "PosterName": $.trim(posterName),
            "Description": $.trim(description),
            "Price": $.trim(price),
            "StateID": $.trim(state),
            "AreaDescription": $.trim(area),
            "IsBrandNew": newItem === "True",
            "IsUsed": newItem !== "True",
            "IsUrgent": urgentDeal === "True",
            "IsPrivateSeller": privateSeller === "True",
            "IsCompanySeller": privateSeller !== "True",
            "IsForSell": forSell === "True",
            "IsForRent": forSell !== "True",
            "SearchTag": $.trim(searchTag),
            "WebsiteUrl": $.trim(website)
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
                url: updateposturl, //Server script to process data
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

    function resetSubCategory(category, that) {
        var $subcatSelect = $(that).closest(".modify-post-root").find(".subcategory-select").find("#SubCategoryID").eq(0);
        $subcatSelect.val("");
        if (category == "" || category == null || category == undefined) {
            $subcatSelect.empty();
            return false;
        }
        return true;
    }

    function populateSubCategory(items, that) {
        var $subcatSelect = $(that).closest(".modify-post-root").find(".subcategory-select").find("#SubCategoryID").eq(0);
        $subcatSelect.empty();
        $.each(items, function (i, item) {
            if (item.Text != null) {
                $subcatSelect.append($('<option>', {
                    value: item.Value,
                    text: item.Text
                }));
            } else {
                $subcatSelect.append($('<option>', {
                    value: "",
                    text: ""
                }));
            }
        });
    }

    $(".category-select").change(function (e) {
        e.preventDefault();
        var that = this;
        var category = $(this).find("#CategoryID").eq(0).val();

        if (resetSubCategory(category, that)) {
            var requestData = { "categoryValueID": $.trim(category) };
            $.ajax({
                url: getsubcategoriesurl, //Server script to process data
                type: 'POST',
                data: JSON.stringify(requestData),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                error: function (xhr) {
                },
                success: function (items) {
                    populateSubCategory(items, that);
                },
                processData: false,
                cache: false
            });
        }
    });
});