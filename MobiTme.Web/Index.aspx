<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MobiTme.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MobiTime</title>
    <link href="Scripts/jquery.mobile/jquery.mobile-1.3.2.min.css" rel="stylesheet" />
    <link href="Styles/Custom_Global.css" rel="stylesheet" />
    <script src="Scripts/jquery-2.0.3.min.js"></script>
    <script src="Scripts/jquery.mobile/jquery.mobile-1.3.2.min.js"></script>
    <script src="Scripts/jquery.blockUI.min.js"></script>
    <style type="text/css">
        .ui-content {
            padding: 0;
        }
    </style>
    <script type="text/javascript">
        var PAGE_ONE = 'zero';

        $(document).ready(function (e) {
            fixMapLayout();


            attachSwipFucntionality();

            attachIframeEvents();
            //  renderPage('wucLogin');
        });

        /*
        function renderPage(pageID) {
            var targetPage = $('#' + pageID);
            if (targetPage.length > 0) {
                $.mobile.changePage(targetPage, { transition: 'slide' });
            } else {
                $.ajax({
                    type: "GET",
      
                    url: "RenderPage.ashx",
                    //data: JSON.stringify([{ pageID: pageID }]),
                    data: { 'pageID': pageID  },
                    success: function (response) {
                        $('#htmlContainer').append(response);
                    },
                    error: function () {
                        $('#htmlContainer').append('error');
                    }
                });
            }
        }
        */
        function fixMapLayout() {
            var width = $(document).width();
            var height = $(document).height();
            $('.contentIframe').css('width', width - 21);
            $('.contentIframe').css('height', height);
        }





        function hideIframeLoader() {
            $.unblockUI();
        }

        function showIframeLoader() {
            $.blockUI({ message: '<h1><img src="busy.gif" /> Just a moment...</h1>' });
        }



        function attachSwipFucntionality() {
            $(document).delegate('.ui-page', "swipeleft", function () {
                var $nextPage = $(this).next('[data-role="page"]');
                // swipe using id of next page if exists
                if ($nextPage.length > 0) {
                    $.mobile.changePage($nextPage, { transition: 'slide' });
                }
            }).delegate('.ui-page', "swiperight", function () {
                var $prevPage = $(this).prev('[data-role="page"]');
                if ($prevPage.attr('id') == PAGE_ONE) {
                    var confirmLogout = confirm("Are you sure you want to log out?");
                    if (confirmLogout == false) {
                        return false;
                    }
                }

                // swipe using id of next page if exists
                if ($prevPage.length > 0) {
                    $.mobile.changePage($prevPage, { transition: 'slide', reverse: true });
                }
            });

        }


        function attachIframeEvents(parameters) {
            // iframe content is loaded
            $('#IframePage2').load(function () {
                hideIframeLoader();
                var $nextPage = $('#Map');
                $.mobile.changePage($nextPage, { transition: 'slide' });
            });

            $('#IframePage3').load(function () {
                hideIframeLoader();
                var $nextPage = $('#Menu');
                $.mobile.changePage($nextPage, { transition: 'slide' });
            });


            $("#IframePage4").load(function () {
                hideIframeLoader();
                var $nextPage = $('#Content');
                $.mobile.changePage($nextPage, { transition: 'slide' });
            });

        }

        function slideTo(page, reverse, siteid) {
            var $nextPage = $('#' + page);
            if ($nextPage.length > 0) {

                if (reverse == false) {
                    var frameToDisplay = document.getElementById("Iframe" + page);


                    switch (page) {
                        case "Login":
                            var frameToDisplay = document.getElementById("IframePage1");
                            frameToDisplay.src = "LogOut.aspx";
                            break;
                        case "Map":
                            var frameToDisplay = document.getElementById("IframePage2");
                            frameToDisplay.src = "Member/GSEMap.aspx";
                            break;
                        case "Menu":

                            var frameToDisplay = document.getElementById("IframePage3");
                            if (siteid != undefined)

                                frameToDisplay.src = "Member/Metro.aspx?SITEID=" + siteid;
                            else

                                frameToDisplay.src = "Member/Metro.aspx";
                            break;
                    }
                } else {
                    if ($nextPage.length > 0) {
                        $.mobile.changePage($nextPage, { transition: 'slide', reverse: true });
                    }
                }
            }
        }





        function getPageSize() {
            var width = $(document).width();
            var height = $(document).height();
            return { width: width, height: height };
        }

        function metroCommand(actionID, querystring) {
            //Member/MetroMenu.aspx
            if (actionID == "0")
                backToMap();
            else
                if (actionID == "1") {
                    var confirmLogout = confirm("Are you sure you want to log out?");
                    if (confirmLogout == true) {
                        slideTo('Login', true);
                    }
                }
                else {


                    //document.getElementById("frame1").style.display = "none";
                    //document.getElementById("frame2").style.display = "none";
                    //document.getElementById("frame3").style.display = "none";
                    //document.getElementById("frame4").style.display = "none";

                    showLoader();


                    //document.getElementById("IframePage1").style.display = "none";
                    //document.getElementById("IframePage2").style.display = "none";
                    //document.getElementById("IframePage3").style.display = "none";
                    //document.getElementById("IframePage4").style.display = "none";


                    var frameToDisplay = document.getElementById("IframePage4");

                    frameToDisplay.src = actionID + "?SITEID=" + querystring;
                    frameToDisplay.style.display = "";
                    slideTo('Content', false);

                }
        }

        function backToMap() {
            slideTo('Map', true);
        }



        function contentChange(iframeIndex) {

            document.getElementById("frame1").style.display = "none";
            document.getElementById("frame2").style.display = "none";
            document.getElementById("frame3").style.display = "none";
            document.getElementById("frame4").style.display = "none";

            showIframeLoader();

            var frameToDisplay = document.getElementById("frame" + iframeIndex);
            switch (iframeIndex) {
                case 1: frameToDisplay.src = "Member/MetroMenu.aspx";
                    break;
                case 2: frameToDisplay.src = "mainIframe/2.html";
                    break;
                case 3: frameToDisplay.src = "mainIframe/3.html";
                    break;
                case 4: frameToDisplay.src = "mainIframe/4.html";
                    break;
                case 5: frameToDisplay.src = "http://forum.jquery.com/";
                    break;
            }

            frameToDisplay.style.display = "";

        }



        function showLoader() {
            $.blockUI({ message: $("#loader"), css: { border: 'none' } });
        }

        function closeLoader() {
            $.unblockUI();
        }


    </script>

</head>
<body>
    <form id="form1" runat="server">

        <div data-role="page" id="Login">

            <div data-role="content">
                <!-- Login -->

                <iframe id="IframePage1" class="contentIframe" src="Login.aspx" frameborder="0" scrolling="no"></iframe>
                <!-- /Login -->
            </div>

        </div>

        <div data-role="page" id="Map">
            <!-- GSE Map -->

            <iframe id="IframePage2" class="contentIframe" src="" frameborder="0" scrolling="no"></iframe>
            <!-- /GSE Map -->
        </div>
        <div data-role="page" id="Menu">
            <div data-role="content">
                <!-- Metro -->

                <iframe id="IframePage3" class="contentIframe" src="" frameborder="0" scrolling="no"></iframe>
                <!-- /Metro -->
            </div>
        </div>
    </form>

    <div data-role="page" id="Content">
        <div data-role="content">
            <!-- 1 -->

            <iframe id="IframePage4" class="contentIframe" src="" frameborder="0" scrolling="no"></iframe>
            <!-- /Metro -->
        </div>
    </div>


    <div id="loader" style="display: none;">
        <h1>Loading page.</h1>
    </div>
</body>
</html>
