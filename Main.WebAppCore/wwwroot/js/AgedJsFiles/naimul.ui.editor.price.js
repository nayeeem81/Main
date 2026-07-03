$(function () {
    $(document).on("keypress", "#Price", function (e) {
        var key = e.which;
        var number = $("#Price").val();
        var len = number.length;

        if (len > 8)
            return false;

        if (key >= 48 && key <= 57) {
            return true;
        }
        return false;
    });
});