$(function () {
    var notify;
    $(document).on("change", "input[name='Agreement']", function () {
        if ($(this).val() == true) {
            $("#Agreement").val(true);
        } else if ($(this).val() == false) {
            $("#Agreement").val(false);
        }
    });

    function LoadImageContentTradeLicense() {
        $.ajax({
            type: "GET",
            url: tradeLicensePartialLoadURL,
            cache: false,
            async: true,
            success: function (result) {
                $("#imageTradeLicenseDiv").empty().append(result);
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
            },
            error: function (e) {
            }
        });
    }

    function LoadImageContentNIDFile() {
        $.ajax({
            type: "GET",
            url: ownerNIDPartialLoadURL,
            cache: false,
            async: true,
            success: function (result) {
                $("#imageOwnerNIDDiv").empty().append(result);
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
            },
            error: function (e) {
            }
        });
    }

    $("#UploadTradeLicenseFile").ajaxUpload({
        url: tradeLicenseUploadURL,
        name: "file",
       // data: true,
        onSubmit: function () {
        },
        onComplete: function (result) {
            if (result != 'false') {
                LoadImageContentTradeLicense();
            }
            else {
                notify = $.notify('<strong>&nbsp;' + 'File upload failed! Please select another smaller size file.' + '</strong>', {
                    type: 'danger'
                });
            }
        }
    });

    $("#UploadOwnerNIDFile").ajaxUpload({
        url: ownerNIDUploadURL,
        name: "file",
        //data: true,
        onSubmit: function () {
        },
        onComplete: function (result) {
            if (result != 'false') {
                LoadImageContentNIDFile();
            }
            else {
                notify = $.notify('<strong>&nbsp;' + 'File upload failed! Please select another smaller size file.' + '</strong>', {
                    type: 'danger'
                });
            }
        }
    });

    function RemoveTradeLicenseFile() {
        $.ajax({
            type: "GET",
            url: removeTradeLicenseFileURL,
            cache: false,
            async: true,
            success: function (result) {
                $("#imageTradeLicenseDiv").empty().append(result);
                notify = $.notify({
                    type: 'success',
                    message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + SUCCESS + '</strong> ' + 'File removed.',
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
            },
            error: function (e) {
            }
        });
    }

    function RemoveOwnerNIDFile() {
        $.ajax({
            type: "GET",
            url: removeOwnerNIDFileURL,
            cache: false,
            async: true,
            success: function (result) {
                $("#imageOwnerNIDDiv").empty().append(result);
                notify = $.notify({
                    type: 'success',
                    message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;' + SUCCESS + '</strong> ' + 'File removed.',
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
            },
            error: function (e) {
            }
        });
    }

    $(document).on("click", "#removeOwnerNIDFile", function () {
        RemoveOwnerNIDFile();
    });

    $(document).on("click", "#removeTradeLicenseFile", function () {
        RemoveTradeLicenseFile();
    });
});