function FirstSupportedPropertyName(prefixedPropertyNames) {
    var tempDiv = document.createElement("div");
    for (var i = 0; i < prefixedPropertyNames.length; ++i) {

        if (typeof tempDiv.style[prefixedPropertyNames[i]] != 'undefined')

            return prefixedPropertyNames[i];
    }
    return null;
}

function FirstSupportedPropertyNameForButton(prefixedPropertyNames) {
    var tempDiv = $("<button></button>");
    for (var i = 0; i < prefixedPropertyNames.length; ++i) {

        if (typeof tempDiv.style[prefixedPropertyNames[i]] != 'undefined')

            return prefixedPropertyNames[i];
    }
    return null;
}

function FirstSupportedFunctionName(property, prefixedFunctionNames, argString) {
    var tempDiv = document.createElement("div");
    for (var i = 0; i < prefixedFunctionNames.length; ++i) {
        tempDiv.style[property] = prefixedFunctionNames[i] + argString;
        if (tempDiv.style[property] != "")
            return prefixedFunctionNames[i];
    }
    return null;
}

var textsizeadjust = FirstSupportedPropertyName(["text-size-adjust", "-webkit-text-size-adjust", "-ms-text-size-adjust", "-moz-text-size-adjust", "-o-text-size-adjust"]);
//var appearance = FirstSupportedPropertyNameForButton(["appearance", "-webkit-appearance", "-ms-appearance", "-moz-appearance", "-o-appearance"]);
var transition = FirstSupportedPropertyName(["transition", "-webkit-transition", "-ms-transition", "-moz-transition", "-o-transition"]);
var transform = FirstSupportedPropertyName(["transform", "-webkit-transform", "-ms-transform", "-moz-transform", "-o-transform"]);

//Updating vendr prefix based on following link scan: Microsoft Edge
//https://dev.modern.ie/tools/staticscan/?url=http%3A%2F%2Fwww.cooleasy.seleasy.com

function UpdateVendorPrefix() {
   // debugger;
    //$("html").css("text-size-adjust", "100%");

    //$("button").css(appearance, "box");
    //$("button.close").css(appearance, "box");
    //$("input[type=search]").css(appearance, "textfield");

    //$(".img-thumbnail").css(transition, "border .2s ease-in-out");
    //$(".form-control").css(transition, "border .2s ease-in-out");
    //$(".fade").css(transition, "border .2s ease-in-out");
    //$(".thumbnail").css(transition, "border .2s ease-in-out");
    //$(".progress-bar").css(transition, "border .2s ease-in-out");
    //$(".modal.fade .modal-dialog").css(transition, "border .2s ease-in-out");
    //$(".carousel-inner .item").css(transition, "border .2s ease-in-out");   
}
