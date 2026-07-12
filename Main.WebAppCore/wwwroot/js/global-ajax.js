$.ajaxPrefilter(function (options, originalOptions, jqXHR) {

    // KEEP THIS: Forces the browser to send your authentication and anti-forgery cookies

    options.xhrFields = options.xhrFields || {};

    options.xhrFields.withCredentials = true;

    options.headers = options.headers || {};

    // KEEP THIS: Delivers the mandatory "second piece" of the security puzzle for mutations

    const requestType = options.type ? options.type.toUpperCase() : "GET";

    if (requestType === "POST" || requestType === "PUT" || requestType === "DELETE")
    {
        const antiForgeryTokenValue = $('input[name="__RequestVerificationToken"]').val();

        if (antiForgeryTokenValue) {
            options.headers["RequestVerificationToken"] = antiForgeryTokenValue;
        }
    }
});

async function secureFetch(url, options = {}) {

    options.headers = options.headers || {};

    // EQUIVALENT TO withCredentials: true (Forces cookies to send)
    options.credentials = 'include';

    // EQUIVALENT TO jQuery Prefilter (Appends Anti-Forgery Header)

    const method = options.method ? options.method.toUpperCase() : "GET";

    if (method === "POST" || method === "PUT" || method === "DELETE") {
        const antiForgeryTokenValue = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

        if (antiForgeryTokenValue) {
            options.headers["RequestVerificationToken"] = antiForgeryTokenValue;
        }
    }
}

$(document).on("submit", "form", function () {

    const currentForm = $(this);

    // Check if the form is already missing the token field
    if (currentForm.find('input[name="__RequestVerificationToken"]').length === 0) {

        // Grab the single global token from the top of the body
        const globalTokenValue = $('body > input[name="__RequestVerificationToken"]').val();

        if (globalTokenValue) {

            // Append it cleanly so the browser submits it natively
            currentForm.append(
                $('<input>', { type: 'hidden', name: '__RequestVerificationToken', value: globalTokenValue })
            );

        }
    }
});