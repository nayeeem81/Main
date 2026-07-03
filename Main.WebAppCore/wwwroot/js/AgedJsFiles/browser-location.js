

//Browser and device variables//
var BROWSER_NAME = "";
//Ex: Mobile, Tab, Laptop
var DEVICE_TYPE = "";
//Ex: 800PX
var BROWSER_WIDTH = "";
//Ex: 350PX
var BROWSER_HEIGHT = "";
//Device orientation: Portraite or Landscape
var DEVICE_ORIENTATION = "";

//Session variables//
var SESSION_DATE = "";
//Session starts when the home page is loaded for the first time.
var SESION_START_TIME = "";
//Session ends when the tab in browser is closed or the page is closed.
var SESSION_END_TIME = "";
//FOLLOWING ALL FUNCTIONS ARE IN USE//

function setDataValue(value) {
    if (value == "" || value == null || value == undefined)
        return "";
    return value + "";
}


//We are using this function to get browser Country and other details.
function ipLookUp(withLonLat, lon, lat) {
    $.ajax('https://ip-api.com/json')
        .then(
            function success(response) {
                if (lon == null || lon == undefined || lon == "") {
                    lon = response.lon;
                }
                if (lat == null || lat == undefined || lat == "") {
                    lat = response.lat;
                }
                var objBrowserLog = {};
                objBrowserLog["Lon"] = setDataValue(lon);
                objBrowserLog["Lat"] = setDataValue(lat);
                objBrowserLog["Width"] = setDataValue(setDataValue($("body")[0].clientWidth));
                objBrowserLog["Height"] = setDataValue(setDataValue($("body")[0].clientHeight));
                objBrowserLog["Zip"] = setDataValue(response.zip);
                objBrowserLog["Country"] = setDataValue(response.country);
                objBrowserLog["CountryCode"] = setDataValue(response.countryCode);
                objBrowserLog["Region"] = setDataValue(response.regionName);
                objBrowserLog["City"] = setDataValue(response.city);
                if (!withLonLat) {
                    HasCalledBrowserFunction = true;
                }
                $.ajax({
                    url: browserlogurl,
                    type: 'POST',
                    data: objBrowserLog,
                    cache: false,
                    success: function (result) {
                        
                    },
                    done: function (result) {
                    },
                    error: function () {
                    }
                });
            },
        function fail(data, status) {
            var objBrowserLog = {};
            objBrowserLog["Lon"] = setDataValue("NAEF");
            objBrowserLog["Lat"] = setDataValue("NAEF");
            objBrowserLog["Width"] = setDataValue(setDataValue($("body")[0].clientWidth));
            objBrowserLog["Height"] = setDataValue(setDataValue($("body")[0].clientHeight));
            objBrowserLog["Zip"] = setDataValue("NAEF");
            objBrowserLog["Country"] = setDataValue("NAEF");
            objBrowserLog["CountryCode"] = setDataValue("NAEF");
            objBrowserLog["Region"] = setDataValue("NAEF");
            objBrowserLog["City"] = setDataValue("NAEF");
            $.ajax({
                url: browserlogurl,
                type: 'POST',
                data: objBrowserLog,
                cache: false,
                success: function (result) {

                },
                done: function (result) {
                },
                error: function () {
                }
            });
                //console.log('Function: iplookup. Request failed.  Returned status of ',
                //    status);
                HasCalledBrowserFunction = false;
            }
        );
}
//Try geo location else seek iplookup
function mainLocationFunction() {
    if ("geolocation" in navigator) { 
        navigator.geolocation.getCurrentPosition(
            function success(position) {
                //ipLookUp(false, position.coords.longitude, position.coords.latitude); 
            },
            function error(error_message) {
                var objBrowserLog = {};
                objBrowserLog["Lon"] = setDataValue("NA");
                objBrowserLog["Lat"] = setDataValue("NA");
                objBrowserLog["Width"] = setDataValue(setDataValue($("body")[0].clientWidth));
                objBrowserLog["Height"] = setDataValue(setDataValue($("body")[0].clientHeight));
                objBrowserLog["Zip"] = setDataValue("NA");
                objBrowserLog["Country"] = setDataValue("NA");
                objBrowserLog["CountryCode"] = setDataValue("NA");
                objBrowserLog["Region"] = setDataValue("NA");
                objBrowserLog["City"] = setDataValue("NA");                
                $.ajax({
                    url: browserlogurl,
                    type: 'POST',
                    data: objBrowserLog,
                    cache: false,
                    success: function (result) {

                    },
                    done: function (result) {
                    },
                    error: function () {
                    }
                });
                //console.error('Function: mainFunction. An error has occured while retrieving ' +
                //    'location ', error_message);
                //ipLookUp(true,"",""); 
            }
        );
    } else {
        var objBrowserLog = {};
        objBrowserLog["Lon"] = setDataValue("NAE");
        objBrowserLog["Lat"] = setDataValue("NAE");
        objBrowserLog["Width"] = setDataValue(setDataValue($("body")[0].clientWidth));
        objBrowserLog["Height"] = setDataValue(setDataValue($("body")[0].clientHeight));
        objBrowserLog["Zip"] = setDataValue("NAE");
        objBrowserLog["Country"] = setDataValue("NAE");
        objBrowserLog["CountryCode"] = setDataValue("NAE");
        objBrowserLog["Region"] = setDataValue("NAE");
        objBrowserLog["City"] = setDataValue("NAE");
        $.ajax({
            url: browserlogurl,
            type: 'POST',
            data: objBrowserLog,
            cache: false,
            success: function (result) {

            },
            done: function (result) {
            },
            error: function () {
            }
        });
        //ipLookUp(true,"","");
    }
}

$(function () {
    if (!HasCalledBrowserFunction) {
        mainLocationFunction();
    }
});