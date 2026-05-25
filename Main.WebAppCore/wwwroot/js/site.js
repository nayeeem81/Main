(function ($) {

    $("[data-adv-search=button]").click(function () {
        $("[data-adv-search=modal],[data-adv-search=content]").addClass("active");
        $("body").addClass("no-scroll");
    });

    $("[data-adv-search=modal]").click(function () {
        $("[data-adv-search=modal],[data-adv-search=content]").removeClass("active");
        $("body").removeClass("no-scroll");
    });


    $("[data-request=button]").click(function () {
        $("[data-request=modal],[data-request=content]").addClass("active");
        $("body").addClass("no-scroll");
    });

    $("[data-request=modal]").click(function () {
        $("[data-request=modal],[data-request=content]").removeClass("active");
        $("body").removeClass("no-scroll");
    });
})(jQuery);
