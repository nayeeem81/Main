function resetSubCategory(category) {
    $("select[name='SubCategoryID'").val("");
    if (category == "" || category == null || category == undefined) {
        $("select[name='SubCategoryID'").empty();
        return false;
    }
    return true;
}

function populateSubCategory(items) {
    $("select[name='SubCategoryID'").empty();
    $.each(items, function (i, item) {
        if (item.Text != null) {
            $("select[name='SubCategoryID'").append(
                "<option value='" + item.value + "'>" + item.text + "</option>"
            );
        } else {
            $("select[name='SubCategoryID'").append(
                "<option value='" + item.value + "'>" + item.text + "</option>"
            );
        }
    });
}

$(document).on("change", "select[name='CategoryID']", function () {
    var category = $(this).val();    
    if (resetSubCategory(category)) {
        $.ajax({
            url: getsubcategoryurlsearch + "?id=" + $.trim(category),
            type: 'GET',
            dataType: 'json',
            error: function (xhr) {
            },
            success: function (items) { 
                populateSubCategory(items);
            },
            cache: true
        });
    }
});
