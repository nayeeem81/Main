var notify;
function FailedMessage() {
    notify = $.notify('<strong>&nbsp;' + 'File upload failed!' + '</strong>', {
        type: 'danger'
    });
}

function NotifySuccessMessage() {
    notify = $.notify({
        type: 'success',
        message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + SUCCESS + '</strong> ' + IMAGE_LOAD_SUCCESSFUL,
        allow_dismiss: true,
        placement: {
            from: "top",
            align: "right"
        },
        delay: { show: 80, hide: 50 },
        template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
            '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
            '<span data-notify="icon"></span>' +
            '<span data-notify="title">{1}</span>' +
            '<span data-notify="message">{2}</span>' +
            '</div>'
    });
}

function LoadCarousel() {
    $.ajax({
        type: "GET",
        url: loadCarouselURL,
        cache: false,
        async: true,
        success: function (result) {
            $("#divCarousel").append(result);
            NotifySuccessMessage();
        },
        error: function (e) {
            FailedMessage();
        }
    });
}

function LoadThumbnail() {
    $.ajax({
        type: "GET",
        url: loadThumbnailURL,
        cache: false,
        async: true,
        success: function (result) {
            $("#divThumbnail").append(result);
            NotifySuccessMessage();
        },
        error: function (e) {
            FailedMessage();
        }
    });
}

function LoadSquare() {
    $.ajax({
        type: "GET",
        url: loadSquareURL,
        cache: false,
        async: true,
        success: function (result) {
            $("#divSquare").append(result);
            NotifySuccessMessage();
        },
        error: function (e) {
            FailedMessage();
        }
    });
}

function LoadRectangle() {
    $.ajax({
        type: "GET",
        url: loadRectangleURL,
        cache: false,
        async: true,
        success: function (result) {
            $("#divRectangle").append(result);
            NotifySuccessMessage();
        },
        error: function (e) {
            FailedMessage();
        }
    });
}

function UploadCarousel() {
    $(".UploadCarousel").ajaxUpload({
        url: uploadCarouselURL,
        name: "file",
       //data: true,
        onSubmit: function () {
        },
        onComplete: function (result) {
            if (result != 'false') {
                LoadCarousel();
            }
            else {
                FailedMessage();
            }
        }
    });
}

function UploadThumbnail() {
    $(".UploadThumbnail").ajaxUpload({
        url: uploadThumbnailURL,
        name: "file",
       // data: true,
        onSubmit: function () {
        },
        onComplete: function (result) {
            if (result != 'false') {
                LoadThumbnail();
            }
            else {
                FailedMessage();
            }
        }
    });
}

function UploadSquare() {
    $(".UploadSquare").ajaxUpload({
        url: uploadSquareURL,
        name: "file",
       // data: true,
        onSubmit: function () {
        },
        onComplete: function (result) {
            if (result != 'false') {
                LoadSquare();
            }
            else {
                FailedMessage();
            }
        }
    });
}

function UploadRectangle() {
    $(".UploadRectangle").ajaxUpload({
        url: uploadRectangleURL,
        name: "file",
     //   data: true,
        onSubmit: function () {
        },
        onComplete: function (result) {
            if (result != 'false') {
                LoadRectangle();
            }
            else {
                FailedMessage();
            }
        }
    });
} 

$(function () {
    $('[data-manage-post="collapse"]').collapse(
        {
            parent: "#accordion",
            toggle: true
        });

    function getProductDetails(postid, isedit) {
        var requestData = { "PostID": postid };
        var url = "";
        if (isedit)
            url = editPostURL;
        else
            url = viewPostURL;
        return $.ajax({
            url: url,
            type: 'POST',
            data: requestData,
            cache: false
        });
    }   

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
            getProductDetails(postid, false).done(function (result) {
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
            getProductDetails(postid, true).done(function (result) {
                $dialogview.empty().hide();
                $dialogedit.empty().append(result).show();                
                UploadCarousel();
                UploadThumbnail();
                UploadSquare();
                UploadRectangle();                   
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
        url = deletePostURL;
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
                setTimeout(function () { window.location = managePostURL; }, 1000);
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
        var $parentObject = $(that).closest(".modify-product-root").eq(0);
        var $titleObject = $("#Title", $parentObject);       
        var $priceObject = $("#Price", $parentObject);
        var isValid = true;      
        //if (!validateRequiredTextData($titleObject, "Title")) {
        //    isValid = false;
        //}
        //if (!validateRequiredTextData($priceObject, "Price")) {
        //    isValid = false;
        //}
        return isValid;
    }

    function updateManageProductValidation(that) {
        var $parentObject = $(that).closest(".modify-product-root").eq(0);
        resetValidation($parentObject);
        isFormRequiredFieldsValid(that);
    }

    function isManageProductValidForm(that) {
        var $parentObject = $(that).closest(".modify-product-root").eq(0);
        resetValidation($parentObject);
        if (isFormRequiredFieldsValid(that)) {
            return true;
        }
        return false;
    }

    $(document).on("change", ".modify-product-root", function (e) {
        e.preventDefault();
        var root = this;
        var that = $(root).find("input.update-post").eq(0);
        updateManageProductValidation(that);
    });

    function collectManageProductFormData(that) {
        var $parentObject = $(that).closest(".modify-product-root").eq(0);
        var postId = $parentObject.find("#PostID").val();
        var title = $parentObject.find("#Title").val();   
        var categoryID = $parentObject.find("#CategoryID").val();        
        var subCategoryID = $parentObject.find("#SubCategoryID").val();        
        var description = $parentObject.find("#Description").val();
        var searchTag = $parentObject.find("#SearchTag").val();
        var state = $parentObject.find("#StateID").val();
        var area = $parentObject.find("#AreaDescription").val();
        var newItem = $parentObject.find("input[id='IsBrandNew1']:checked").val();
        var price = $parentObject.find("#Price").val();
        var discountedPrice = $parentObject.find("#DiscountedPrice").val();
        var paymentGatewayCommision = $parentObject.find("#PaymentGatewayCommision").val();
        var deshiHutCommissionAmount = $parentObject.find("#DeshiHutCommissionAmount").val();
        var portalProductPrice = $parentObject.find("#PortalProductPrice").val();
        var shopShareAmount = $parentObject.find("#ShopShareAmount").val();        
        
        var requestData = {
            "PostID": $.trim(postId),
            "Title": $.trim(title),
            "SubCategoryID": subCategoryID,
            "CategoryID": categoryID,
            "StateID": $.trim(state),
            "AreaDescription": $.trim(area),
            "Description": $.trim(description),
            "SearchTag": $.trim(searchTag),
            "Price": $.trim(price),                       
            "DiscountedPrice": discountedPrice,
            "PortalProductPrice": portalProductPrice,
            "DeshiHutCommissionAmount": deshiHutCommissionAmount,
            "PaymentGatewayCommision": paymentGatewayCommision,
            "ShopShareAmount": $.trim(shopShareAmount)            
        };
        return requestData;
    }

    $(document).on("click", "input.btn-submit-post-modify-mode", function (e) {
        e.preventDefault();
        var that = this;
        var notify = $.notify('<strong><i class="fa fa-spinner"></i>&nbsp;' + UPDATEING + '</strong>...', {
            type: 'info'
        });
        if (isManageProductValidForm(that)) {
            var requestData = collectManageProductFormData(that);
            $.ajax({
                url: updatePostURL, 
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
                url: getSubCategoryURL, //Server script to process data
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