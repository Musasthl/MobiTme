$(document).ready(function () {
    fixFrameSize();
    init();
    parent.closeLoader();
  
    $('#btnMainMenu').click(function (e) {
        parent.slideForward();
    });

    $('#btnLogout').click(function (e) {
        // call logout page method
        var confirmLogout = confirm("Are you sure you want to log out?");
        if (confirmLogout == true) {
            parent.logout();
        }
    });
});


function getPageSize() {
    var width = $(document).width();
    var height = $(document).height();
    return { width: width, height: height };
}

function fixFrameSize() {
    //var pageSize = getPageSize();
    //var totalFrames = $(".mainIframe").length;
    //$(".mainIframe").css("width", pageSize.width);
    //$(".mainIframe").css("height", pageSize.height);
 
    

    var parentSize = parent.getPageSize();

    $('#map3d').css('width', parentSize.width);
    $('#map3d').css('height', parentSize.height - 200);
}

 

var ge;
google.load("earth", "1");
google.load("earth", "1");

var ge = null;

function init() {
    google.earth.createInstance("map3d", initCallback, failureCallback);
}

function initCallback(object) {
    ge = object;
    ge.getWindow().setVisibility(true);
}

function failureCallback(object) {
}