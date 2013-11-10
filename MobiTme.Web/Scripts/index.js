/// <reference path="../Member/Map.aspx" />
/// <reference path="../Member/GSEngine.aspx" />
var pageChangeEffect = "slide";
// most effect types need no options passed by default
var pageChangeOptions = { percent: 100 };

var isFirstLoad = true;
$(document).ready(function () {
    fixFrameSize();

    $('#btnLogin').click(function (e) {
        login();
    });
    
    $('#frame1, #frame2, #frame3, #frame4, #frame5').load(function() {

        if (isFirstLoad == true)
            return;
        closeLoader();
        
        var activeIframe = $(".mainIframe.active");
        activeIframe.animate({ width: "hide" }, 500);
        $(this).animate({ width: "show" }, 500, logoutCallback);


        $(".mainIframe").removeClass("active");
        $(this).addClass("active");
        
    });

});


function login() {
    var username = $('#txtUsername').val();
    var password = $('#txtPassword').val();

    parent.slideTo('Page2', false);

    return false;
    $.ajax({
        type: "Post",
        url: window.location.pathname + "/LoginUser",
        contentType: "application/json; charset=utf-8",
        dataType: "json", // can be used for plaintext or for JSON
        data: { 'username': username, 'password': password },
        success: function (response) {
            var data = response.d;
            if (data != null) {
                if (data == "0") {
                    // wrong username or password
                } else {
                    // login successfull call parent to swap to gse 
                    parent.slideTo('Page2', false);
                }
            }
        },
        error: function (error) {
            // report error back to user
            alert(error.responseText);
        }
    });
}


function logout() {
    var targetIframe;
    var activeIframe = $(".mainIframe.active");
    var activeIframeID = activeIframe.attr("id");
    var activeIframeIndex = activeIframe.attr("data-index");
    var targetIframePageurl = "";
    var totalFrames = $(".mainIframe").length;

    if (parseInt(activeIframeIndex) != totalFrames) {

        //showLoader();
        targetIframe = $("#frame1");
        targetIframePageurl = targetIframe.attr("data-pageurl") + "?logout=true";

        // loading page
        targetIframe.attr("src", targetIframePageurl);

        for (var i = 1; i < totalFrames; i++) {
            var iframeID = "frame" + i;

            if ((iframeID != targetIframe.attr('id')) && (iframeID != activeIframeID)) {
                $("#" + iframeID).css('display', 'none');
            }
        }




        activeIframe.animate({ width: "hide" }, 500);
        targetIframe.animate({ width: "show" }, 500, logoutCallback);


        $(".mainIframe").removeClass("active");
        targetIframe.addClass("active");


    } else {
        log('end of slide');
    }
}

function logoutCallback(parameters) {

}

function getPageSize() {
    var width = $(document).width();
    var height = $(document).height();
    return { width: width, height: height };
}

function fixFrameSize() {
    var pageSize = getPageSize();
    var totalFrames = $(".mainIframe").length;
    $(".mainIframe").css("width", pageSize.width);
    $(".mainIframe").css("height", pageSize.height);
    // $("#mainContent").css("width", totalFrames * pageSize.width);
}

function slideBack() {
    isFirstLoad = false;
    var targetIframe;
    var activeIframe = $(".mainIframe.active");
    var activeIframeID = activeIframe.attr("id");
    var activeIframeIndex = activeIframe.attr("data-index");
    var totalFrames = $(".mainIframe").length;

    if (parseInt(activeIframeIndex) != 1) {

        targetIframe = $("#frame" + (parseInt(activeIframeIndex) - 1));
        switch (activeIframeID) {
            case "frame1":

                targetIframe.load("http://www.google.com");
                break;
            case "frame2":

                targetIframe.load("http://www.google.com");
                break;
            case "frame3":

                targetIframe.load("http://www.google.com");
                break;
            case "frame4":
                break;

        }


        for (var i = totalFrames; i > 0; i--) {
            var iframeID = "frame" + i;

            if ((iframeID != targetIframe.attr('id')) && (iframeID != activeIframeID)) {
                $("#" + iframeID).hide();
            }
        }

        activeIframe.animate({ width: "hide" }, 500);
        targetIframe.animate({ width: "show" }, 500, slideCallback);


        $(".mainIframe").removeClass("active");
        targetIframe.addClass("active");
    } else {
        log('end of slide');
    }
}

function slideForward() {
    isFirstLoad = false;
    var targetIframe;
    var activeIframe = $(".mainIframe.active");
    var activeIframeID = activeIframe.attr("id");
    var activeIframeIndex = activeIframe.attr("data-index");
    var targetIframePageurl = "";
    var totalFrames = $(".mainIframe").length;

    if (parseInt(activeIframeIndex) != totalFrames) {

        showLoader();
        targetIframe = $("#frame" + (parseInt(activeIframeIndex) + 1));
        targetIframePageurl = targetIframe.attr("data-pageurl");

        // loading page
        targetIframe.attr("src", targetIframePageurl);

        for (var i = 1; i < totalFrames; i++) {
            var iframeID = "frame" + i;

            if ((iframeID != targetIframe.attr('id')) && (iframeID != activeIframeID)) {
                $("#" + iframeID).css('display', 'none');
            }
        }




        //activeIframe.animate({ width: "hide" }, 500);
        //targetIframe.animate({ width: "show" }, 500, slideCallback);


        //$(".mainIframe").removeClass("active");
        //targetIframe.addClass("active");


    } else {
        log('end of slide');
    }



}

//callback function to bring a hidden box back
function slideCallback() {
    //setTimeout(function () {
    //      $("#frame1").hide();
    //}, 1000);
};


function showLoader() {
    $.blockUI({ message: $("#loader") });
}

function closeLoader() {
    $.unblockUI();
}

