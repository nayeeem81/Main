function submitUserLikePost(id, actionType) {
    $.ajax({
        url: postLikeUrl + '?postId=' + id + '&actionType=' + actionType,
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
        }
    });
}

$(document).on("click",
    "[data-like-market]",
    function () {
        var jObject = $(this);
        var curCount = jObject.data("count");
        var postId = jObject.data("id");
        var jLike = $("[data-like-market][data-id=" + postId + "]");
        var jCount = $("[data-like-count][data-id=" + postId + "]");

        if (jObject.hasClass("green-icon-active")) {
            curCount = Math.max(curCount - 1, 0);
            jLike.data("count", curCount);
            jCount.text(curCount);
            //jLike.removeClass("active");
            jLike.removeClass("green-icon-active");
            jLike.addClass("green-icon-inactive");
            submitUserLikePost(postId, "Minus");
        } else {
            curCount = Math.max(curCount + 1, 0);
            jLike.data("count", curCount);
            jCount.text(curCount);
            jLike.removeClass("green-icon-inactive");
            //jLike.addClass("active");
            jLike.addClass("green-icon-active");
            submitUserLikePost(postId, "Plus");
        }
    });


