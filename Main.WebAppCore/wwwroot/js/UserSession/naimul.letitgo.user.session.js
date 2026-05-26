
var sessionModel = {};
var listMousePositions = [];
var activeUrl = "";
var elementId = "";
var elementClass = "";
var targetUrl = "";
var elementTagName = "";
var mouseIndex = 0;

//$(document).mousemove(function (event) {
//    //if (mouseIndex == 0) {
//    //    listMousePositions[listMousePositions.length] = event.pageX + "," + event.pageY;        
//    //}
//    //mouseIndex++;
//    //if (mouseIndex == 15)
//    //    mouseIndex = 0;
//});

//$(document).on('click', 'li > a, a, input, div > a', function () {
//    sessionModel = {};
//    activeUrl = window.location.pathname;
//    var width = $("body")[0].clientWidth == null || $("body")[0].clientWidth == undefined ? "" : $("body")[0].clientWidth + "";
//    var height = $("body")[0].clientHeight == null || $("body")[0].clientHeight == undefined ? "" : $("body")[0].clientHeight + "";
//    targetUrl = "";
//    elementTagName = "";
//    elementId = "";
//    elementClass = "";
//    var id = $(this).attr("id");
//    var cls = $(this).attr("class");

//    if ($(this).is("a")) {
//        targetUrl = $(this).attr('href');
//        elementTagName = "a";
//    }
//    if ($(this).is("input")) {
//        targetUrl = "";
//        elementTagName = "input";
//    }
//    if ($(this).is("li")) {
//        targetUrl = "";
//        elementTagName = "li";
//    }
//    if (id != undefined || id != null) {
//        elementId = id;
//    }
//    if (cls != undefined || cls != null) {
//        elementClass = cls;
//    }
//    sessionModel["ListMousePosition"] = listMousePositions;
//    sessionModel["ActiveUrl"] = activeUrl;
//    sessionModel["ElementId"] = elementId;
//    sessionModel["ElementClass"] = elementClass;
//    sessionModel["TargetUrl"] = targetUrl;
//    sessionModel["ElementTagName"] = elementTagName;
//    sessionModel["BrowserWidth"] = width;
//    sessionModel["BrowserHeight"] = height;

//    $.ajax({
//        url: userSessionUrl,
//        type: 'POST',
//        data: sessionModel,
//        cache: false,
//        success: function (result) {
//            listMousePositions = [];
//            mouseIndex = 0;
//        },
//        done: function (result) {
//            listMousePositions = [];
//            mouseIndex = 0;
//        },
//        error: function () {
//            listMousePositions = [];
//            mouseIndex = 0;
//        }
//    });

//});

//$(function () {
//    sessionModel = {};
//    var width = $("body")[0].clientWidth == null || $("body")[0].clientWidth == undefined ? "" : $("body")[0].clientWidth + "";
//    var height = $("body")[0].clientHeight == null || $("body")[0].clientHeight == undefined ? "" : $("body")[0].clientHeight + "";
//    sessionModel["ListMousePosition"] = listMousePositions;
//    sessionModel["ActiveUrl"] = PAGE_NAME;
//    sessionModel["ElementId"] = "";
//    sessionModel["ElementClass"] = "";
//    sessionModel["TargetUrl"] = "";
//    sessionModel["ElementTagName"] = "";
//    sessionModel["BrowserWidth"] = width;
//    sessionModel["BrowserHeight"] = height;

//    $.ajax({
//        url: userSessionUrl,
//        type: 'POST',
//        data: sessionModel,
//        cache: false,
//        success: function (result) {
//            listMousePositions = [];
//            mouseIndex = 0;
//        },
//        done: function (result) {
//            listMousePositions = [];
//            mouseIndex = 0;
//        },
//        error: function () {
//            listMousePositions = [];
//            mouseIndex = 0;
//        }
//    });
//});