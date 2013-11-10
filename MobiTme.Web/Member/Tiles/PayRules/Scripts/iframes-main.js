$(document).ready(function () {
    fixFrameSize();
    parent.closeLoader();
 
});

function fixFrameSize() {
    var pageSize = parent.getPageSize();
    var totalFrames = $(".mainIframe").length;
    $(".mainIframe").css("width", "100%");
    $(".mainIframe").css("height", pageSize.height - 105);

}
