$(document).ready(function () {
    fixFrameSize();
 
    parent.closeLoader();

    $('#btnBackToMap').click(function (e) {
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



 
}


 