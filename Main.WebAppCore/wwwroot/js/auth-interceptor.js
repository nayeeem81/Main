// Global flag to prevent multiple overlapping refresh requests

let isRefreshing = false;
let failedQueue = [];

const processQueue = (error, success = false) => {
    failedQueue.forEach(prom => {
        if (success) {
            prom.resolve();
        } else {
            prom.reject(error);
        }
    });
    failedQueue = [];
};

// Intercept all global jQuery AJAX completions

$.ajaxSetup({
    statusCode: {
        401: function (xhr, textStatus, errorThrown) {

            // Keep track of the original AJAX settings that just failed

            const originalSettings = this;

            // If we are already in the middle of refreshing, queue this request

            if (isRefreshing) {
                return new Promise((resolve, reject) => {

                    failedQueue.push({ resolve, reject });

                }).then(() => {

                    return $.ajax(originalSettings);

                }).catch((err) => {

                    return Promise.reject(err);

                });
            }

            isRefreshing = true;

            // Make a hidden POST request to your Account Controller refresh endpoint
            // The browser automatically attaches the .App.RefreshToken.{tenantId} cookie!

            return $.ajax({
                url: '/refresh/refresh',
                type: 'POST',

                // Include anti-forgery token header if your refresh endpoint requires it

                headers: {
                    "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
                }

            }).then(function (response) {
                isRefreshing = false;
                processQueue(null, true);

                // Retry the original AJAX call that failed now that cookies are updated

                return $.ajax(originalSettings);

            }).fail(function (refreshXhr) {
                isRefreshing = false;

                processQueue(refreshXhr, false);

                // If the refresh token is also expired or revoked, kick the user out

                console.warn("Refresh token expired or revoked. Redirecting to login.");

                window.location.href = '/account/login?returnUrl=' + encodeURIComponent(window.location.pathname);
            });
        }
    }
});

const { fetch: originalFetch } = window;

window.fetch = async (...args) => {
    let [resource, config] = args;
    let response = await originalFetch(resource, config);

    // If the short-lived access cookie expired, intercept the 401

    if (response.status === 401) {

        if (isRefreshing) {

            return new Promise((resolve, reject) => {

                failedQueue.push({ resolve, reject });

            }).then(() => originalFetch(resource, config))

                .catch(err => Promise.reject(err));
        }

        isRefreshing = true;

        try {

            // Run background token rotation

            const refreshResponse = await originalFetch('/refresh/refresh', {
                method: 'POST',
                headers: {
                    "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]')?.value || ""
                }
            });

            if (refreshResponse.ok) {
                isRefreshing = false;
                processQueue(null, true);

                // Retry original request with the fresh cookie set

                return originalFetch(resource, config);
            }
        } catch (err) {
            // Network or server failure handling
        }

        // Failure: Clear state and boot user out

        isRefreshing = false;

        processQueue(new Error("Refresh failed"), false);

        window.location.href = '/account/login?returnUrl=' + encodeURIComponent(window.location.pathname);
    }

    return response;
};