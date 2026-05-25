var $Header = $("#layoutHeaderContainer", document);
var $lnkMyHomeObject = $("#ancorMyHome", $Header);
var CURRENT_LANGUAGE = 'EN';

function refreshUserSession() {
    var loginUseriD = '';
    if (getLoginAuthTicketCookie() != null) {
        loginUseriD = getLoginUserIDCookie();
    }
    if (loginUseriD == undefined || loginUseriD == null) {
        loginUseriD = "";
    }
    var requestData = { "UserId": loginUseriD };

    $.ajax({
        url: userSessionUpdateUrl,
        type: 'POST',
        data: JSON.stringify(requestData),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        error: function (xhr) {
        },
        success: function (categorytext) {           
        },
        processData: false,
        cache: false,
        async: true
    });
}

function verifyUserAccount() {
    SendVerifyEmail();  
}

function SendVerifyEmail() {
    $.ajax({
        url: urlVerifyEmailAccount,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        error: function (xhr) {
        },
        success: function (result) {  
            if (result == false) {
                notify = $.notify('<strong>&nbsp;' + 'Please verify your email address. We have sent an email.' + '</strong>', {
                    type: 'info'
                });
            }
        },
        processData: false,
        cache: false,
        async: true
    });
}

function setDataUserID(user) {
    $lnkMyHomeObject.attr("data-userid", user.UserID);
}

function removeDataUserID() {
    $lnkMyHomeObject.attr("data-userid", "");
}

function checkForNewUserMessages() {
    $.ajax({
        url: anynewmailurl,
        type: 'POST',
        cache: true,
        success: function (result) {
            if (result == true) {
                $.notify({
                    type: 'success',
                    message: '<strong><i class="fa fa-thumbs-up"></i>&nbsp;New Msg!</strong> Please check your message box.',
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
        }
    });
}

function loadPaymentOption(response) {
    var data = { "UserID": response.UserID };
    $.ajax({
        url: loginUrl,
        type: 'POST',
        data: data,
        cache: true
    }).done(function (user) {
        if (user !== -1) {
            window.location = response.Url;
        }
    });
}

function changeLanguage(language) {
    $.ajax({
        url: changeLanguageUrl + language,
        type: 'GET',
        success: function (result) {
            window.location = window.location.href;
        }
    });
}

$(function () {
    function getCurrentLanguage() {
        if ($.cookie('Language') == "en") {
            $("#imgNigeriaFlag").hide();
            $("#spnNigeriaFlag").hide();
            $("#imgBanglaFlag").hide();
            $("#imgEnglishFlag").show();
            $("#spnBanglaFlag").hide();
            $("#spnEnglishFlag").show();
        }
        else if ($.cookie('Language') == "bn") {
            $("#imgNigeriaFlag").hide();
            $("#spnNigeriaFlag").hide();
            $("#imgEnglishFlag").hide();
            $("#imgBanglaFlag").show();
            $("#spnEnglishFlag").hide();
            $("#spnBanglaFlag").show();            
        }
        else if ($.cookie('Language') == "ng") {
            $("#imgEnglishFlag").hide();
            $("#imgBanglaFlag").hide();
            $("#spnEnglishFlag").hide();
            $("#spnBanglaFlag").hide();
            $("#imgNigeriaFlag").show();
            $("#spnNigeriaFlag").show();
        }
    }

    //refreshUserSession();
    getCurrentLanguage();

    $(document).on("click", "#ancorBanglaLanguage", function () {
        changeLanguage("bn");
    });

    $(document).on("click", "#ancorEnglishLanguage", function () {
        changeLanguage("en");
    });

    $(document).on("click", "#ancorNigeriaLanguage", function () {
        changeLanguage("ng");
    });

    $("#btnMainMenu").click(function () {
        $(".side-menu, .market-list-dropdown").toggleClass("popup");
        $(".subcategory").removeClass("popup");
    });

    $(".side-menu .category").on("click", function (e) {
        e.stopPropagation();
        e.preventDefault();
        var category = $(this).attr('data-category');
        $("[data-subcategory]").removeClass("popup");
        $("li.category").removeClass("menu-item-hover");
        $("[data-subcategory=" + category + "]").addClass("popup");
        $(this).closest("li.category").addClass("menu-item-hover");
    });

    $(document).click(function (ev) {
        if ($(ev.target).is(".subcategory") || $(ev.target).is(".side-menu") || $(ev.target).is(".main-menu") ||
            $(ev.target).parents(".subcategory").length !== 0 || $(ev.target).parents(".side-menu").length !== 0 || $(ev.target).parents(".main-menu").length !== 0) {
            return;
        }
        $(".side-menu, .market-list-dropdown").removeClass("popup");
        $(".subcategory").removeClass("popup");
    });

    

        

    $(document).on("click", "span.market-add-to-cart", function () {
        var postID = $(this).data("id");
        $.ajax({
            url: addToCartURL + "?postID=" + postID,
            type: 'GET',
            cache: false,
            async: true
        }).done(function (result) {
            $("#requesCartViewBackground").addClass("active");
            $("#requestCartViewFormContent").addClass("active");
        });
    });

    $(document).on("click", "#continueShopping", function () {
        $("#requesCartViewBackground").removeClass("active");
        $("#requestCartViewFormContent").removeClass("active");
    });
});



