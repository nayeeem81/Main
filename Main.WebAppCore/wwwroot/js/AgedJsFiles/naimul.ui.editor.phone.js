$(function () {
    $(document).on("keypress", "#Phone", function (e) {
        var key = e.which;
        var number = $("#Phone").val();
        var len = number.length;

        if (len > 25)
            return false;

        if (key >= 48 && key <= 57) {
            return true;
        }
        return false;
    });

});