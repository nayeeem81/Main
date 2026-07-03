var parentObjectSelector = "#fabiaNewProviderRoot";

function IsFormRequiredFieldsValid() {
    var isValid = true;
    if (!validateMaxLengthTextData($(parentObjectSelector).find("#ProviderName"), 80, "Provider Name")) {
        isValid = false;
    }
    if (!validateRequiredTextData($(parentObjectSelector).find("#ProviderName"), "Provider Name")) {
        isValid = false;
    }
    if (!validateRequiredTextData($(parentObjectSelector).find("#ProviderPhone"), "Provider Phone")) {
        isValid = false;
    }   
    if (!validateRequiredTextData($(parentObjectSelector).find("#ServiceDescription"), "Service Description")) {
        isValid = false;
    }  
    if (!validateRequiredTextData($(parentObjectSelector).find("#Email"), "Email")) {
        isValid = false;
    }
    if (!validateRequiredTextData($(parentObjectSelector).find("#Password"), "Password")) {
        isValid = false;
    }
    if (!$("#hSignin").is(":visible")) {
        if (!validateRequiredTextData($(parentObjectSelector).find("#RePassword"), "Re-Password")) {
            isValid = false;
        }
        if (!IsTermsChecked()) {
            $("#divTerms").append(getRequiredCheckboxErrorMessage("Terms"));
            isValid = false;
        }
    }
    return isValid;
}

function IsTermsChecked() {
    if ($('#chkTerms').is(':checked'))
        return true;
    else
        return false;
}

function LoadImageContentProfile() {
    $.ajax({
        type: "GET",
        url: imgLoadProfileUrl,
        cache: false,
        async: true,
        success: function (result) {
            $("#imgDivProfile").html(result);
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

function LoadFileControls() {
    $("#UploadProfile").ajaxUpload({
        url: imgUploadProfileUrl,
        name: "file",
        data: true,
        onSubmit: function () {
        },
        onComplete: function (result) {
            if (result != 'false') {
                LoadImageContentProfile();
                $("#infoErrorUploadProfile").hide();
            }
            else {
                $("#infoErrorUploadProfile").show();
                notify = $.notify('<strong>&nbsp;' + 'File upload failed!' + '</strong>', {
                    type: 'danger'
                });
            }
        }
    });
}

function UpdateTabControlDisplay(currentPosition, nextPosition) {    
    $(".content" + currentPosition).hide();
    $(".content" + nextPosition).show();
    $("#btnPrev").data("current", nextPosition);
    $("#btnPrev").data("curcontent", "content" + nextPosition);
    $("#btnNext").data("current", nextPosition);
    $("#btnNext").data("curcontent", "content" + nextPosition);
}

$(function () {
    LoadFileControls();

    function ShowNotification(message) {
        var notify = $.notify({
            type: 'danger',
            message: '<strong>Error: </strong> ' + message,
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

    function GetRequestData() {
        var fabiaServiceID = $(parentObjectSelector).find("#FabiaServiceID").val();
        var providerID = $(parentObjectSelector).find("#ProviderID").val();
        var personName = $(parentObjectSelector).find("#ProviderName").val();
        var primaryPhone = $(parentObjectSelector).find("#ProviderPhone").val();
        var serviceCharge = $(parentObjectSelector).find("#ServiceCharge").val();
        var website = $(parentObjectSelector).find("#Website").val();              
        var serviceDescription = $(parentObjectSelector).find("#ServiceDescription").val();
        var email = $(parentObjectSelector).find("#Email").val();
        var password = $(parentObjectSelector).find("#Password").val();
        var rePassword = $(parentObjectSelector).find("#RePassword").val();
        var serviceTitle = $(parentObjectSelector).find("#ServiceTitle").val();
        var stateID = $(parentObjectSelector).find("#StateID").val();

        var requestData = {
            "ProviderID": providerID,
            "FabiaServiceID": fabiaServiceID,
            "ProviderName": $.trim(personName),
            "ProviderPhone": $.trim(primaryPhone),
            "Website": $.trim(website),            
            "ServiceDescription": serviceDescription,
            "ServiceCharge": serviceCharge,
            "Email": email,
            "Password": password,
            "RePassword": rePassword,
            "ServiceTitle": serviceTitle,
            "StateID": stateID
        };
        return requestData;
    }

    $(document).on("click", "#btnSubmitProvider", function () {
        if (!IsFormRequiredFieldsValid()) {
            ShowNotification("Validation Failed: Please go back and fill all required fields!");
        }
        $.ajax({
            url: saveProviderUrl,
            type: 'POST',
            data: JSON.stringify(GetRequestData()),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            error: function (xhr) {
            },
            success: function (url) {
                window.location = url;
            },
            processData: false
        });
    });
   
    $(document).on("click", "#btnPrev", function () {
        var that = this;
        var currentPosition = $(that).data("current");
        var boundaryNumber = $(that).data("lastpos");
        if (currentPosition > boundaryNumber) {
            var nextPosition = currentPosition - 1;
            UpdateTabControlDisplay(currentPosition, nextPosition);
        }
        $("#btnSubmitProvider").hide();
        $("#btnNext").show();
    });

    $(document).on("click", "#btnNext", function () {
        var that = this;
        var currentPosition = $(that).data("current");
        var boundaryNumber = $(that).data("lastpos");
        if (currentPosition < boundaryNumber) {
            var nextPosition = currentPosition + 1;
            UpdateTabControlDisplay(currentPosition, nextPosition);
            if (nextPosition == boundaryNumber) {
                $("#btnSubmitProvider").show();
                $("#btnNext").hide();
            } else {
                $("#btnSubmitProvider").hide();
                $("#btnNext").show();
            }
        }
    });

    $(document).on("change", "#Email", function () {
        var $passobject = $("#rePasswordDiv");
        var email = $(this).val();
        var requestData = {
            "Email": $.trim(email)
        };
        $.ajax({
            url: checkemailexistsnewpost, 
            type: 'POST',
            data: JSON.stringify(requestData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                if (result == "EmailInvalid") {
                    ShowNotification("Please enter a valuid email address!");
                }
                if (result == "EmailNotFound") {
                    $("#hSignup").show();
                    $("#divTerms").show();
                    $("#hSignin").hide();
                    $passobject.show();
                    return false;
                }
                if (result == "EmailFound") {
                    $("#hSignup").hide();
                    $("#divTerms").hide();
                    $("#hSignin").show();
                    $passobject.hide();
                    return false;
                }
            }
        });
    });
});