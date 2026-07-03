
function setUserCookei(userid, name, email) {
    // cookie expires in 10 days  
    $.cookie('userid', userid, { path: '/', expires: 30 });
    $.cookie('username', name, { path: '/', expires: 30 });
    $.cookie('useremail', email, { path: '/', expires: 30 });
}

function getUserCookei() {
    var user = null;
    if (isCookeiAlive()) {
        user = {};
        user.UserID = getCookeiUserId();
        user.ClientName = getCookeiUserName();
        return user;
    }
    return null;
}

function getCookeiUserId() {
    return $.cookie('userid') != undefined ? $.cookie('userid') : null;;
}

function getCookeiUserName() {
    return $.cookie('username')!= undefined ? $.cookie('username') : null;
}

function getCookeiUserEmail() {
    return $.cookie('useremail') != undefined ? $.cookie('useremail') : null;
}

function getLoginUserIDCookie() {
    return $.cookie('LoginUserID') != undefined && $.cookie('LoginUserID') != null ? $.cookie('LoginUserID') : null;
}

function getLoginAuthTicketCookie() {
    return $.cookie('.ASPXAUTH') != undefined && $.cookie('.ASPXAUTH') != null ? $.cookie('.ASPXAUTH') : null;
}


function removeUserCookei() {
    if (isCookeiAlive()) {
        $.cookie('userid', null);
        $.cookie('username', null);
        $.cookie('useremail', null);
    }
}

function isCookeiAlive() {
    if (getCookeiUserId() !== null && getCookeiUserName() !== null)
        return true;
    return false;
}