<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Metro.aspx.cs" Inherits="MobiTme.Member.Metro" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">





    <title></title>
    <link href="Styles/StyleSheet1.css" rel="stylesheet" />

    <link href="../Styles/Custom_Global.css" rel="stylesheet" />
    <script src="../Scripts/jquery-2.0.3.min.js"></script>
    <link href="../Scripts/jquery.mobile/jquery.mobile-1.3.2.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery.blockUI.min.js"></script>
    <script src="Styles/iscroll.js"></script>
    <script src="Styles/overthrow.js"></script>
    <script src="Styles/menu.js"></script>
    <script src="../Scripts/utilities.js"></script>
    <style type="text/css">
        body {
            background-color: black;
            color: white;
        }

        /* main content */
        .main-content {
            padding: 20px;
        }



        .page-title-content {
        }

            .page-title-content > .page-title {
                font-size: 25px;
                font-weight: bold;
                padding: 20px 20px 0px 20px;
                text-transform: uppercase;
            }


        /* side menu content */
        .main-sidemenu-content {
            background-color: #818285;
            min-width: 250px;
            position: absolute;
            top: 0;
            bottom: 0;
            right: 0;
            padding: 10px;
        }

        .menu-content {
            text-align: center;
        }

        /* Site Selector */
        .option-wrapper {
            position: absolute;
            width: 233px;
            right: 22px;
            top: 68px;
            z-index: 1;
        }

            .option-wrapper .selected {
                display: block;
                margin: 25px 0px;
                padding: 8px 10px 9px 10px;
                background-image: url("../Styles/Images/controls/dropdown.png");
                background-repeat: no-repeat;
                font-size: 13px;
                color: #fff;
                text-decoration: none;
            }

                .option-wrapper .selected:hover,
                .option-wrapper .selected.open {
                    text-decoration: none;
                    background-image: url("../Styles/Images/controls/dropdown_hover.png");
                }

                .option-wrapper .selected:hover {
                    cursor: pointer;
                }


        ul.site_selector {
            background-color: #111;
            border: 1px solid #333;
            display: none;
            position: absolute;
            margin: 0px;
            margin-top: -8px;
            padding: 0px;
            width: 230px;
            list-style: none;
            z-index: 1;
        }

            ul.site_selector li {
                position: relative;
                float: none;
                display: block;
                margin: 0;
                padding: 6px 8px;
                color: #fff;
                border-bottom: 1px solid #333;
                cursor: pointer;
            }

                ul.site_selector li .desc {
                    font-size: 11px;
                    color: #acacac;
                    position: absolute;
                    top: 6px;
                    right: 5px;
                    padding: 1px 8px 1px 8px;
                    border-radius: 2px;
                    background: #444;
                    transition: all ease-in-out .15s;
                    -webkit-transition: all ease-in-out .15s;
                    -moz-transition: all ease-in-out .15s;
                    -webkit-border-radius: 2px;
                    -moz-border-radius: 2px;
                    border-radius: 2px;
                    opacity: 0.8;
                    filter: alpha(opacity=80);
                }
    </style>
    <script type="text/javascript">

        var WSAccess;
        var UserName = "4";
        var ApplicationPassword = "1";
        var appInterface = new AppInterface('Interface');

        var PendingAction = "";
        var PerviousPage = "";
        var FormScollerID = "";
        var FormScrollerForm = "";
        var appInterface = new AppInterface('Interface');
        var StartScrollPos;
        var StartScrollPosIndex = 0;


        var SiteMetro = null;

        $(document).ready(function () {
            //fixMapLayout();
            appInterface.UpdateMetroMenuLayout();
            handleSiteSelector();
            $('#btnGsePage').click(function (e) {
                parent.backToMap();

            });


            ListUserSites();
            //fixMapLayout();
            //$('#btnGsePage').click(function (e) {
            //    parent.slideTo('Map', true);

            //});

            //$('#frame1, #frame2, #frame3, #frame4, #frame5').load(function () { closeIframeLoader(); });


        });


        function handleSiteSelector() {
            $('.option-wrapper .selected').click(function () {
                if ($(this).hasClass("open")) {
                    $(this).removeClass("open");
                    $('ul.site_selector').hide();
                } else {
                    $(this).addClass("open");
                    $('ul.site_selector').show();
                }
            });


            $(document).on('click', 'ul.site_selector > li', function () {



                $('#selected-site').attr("data-siteid", $(this).attr("data-siteid"));
                $('#selected-site').html($(this).attr("data-sitename"));
                $(this).removeClass("open");
                $('ul.site_selector').hide();



            });
        }

        function openpage(number, pageUrl) {
            if (($("#selected-site").attr('data-siteid') == "") && (number != "0")) {
                alert("Please select site");
                return;
            }
            parent.metroCommand(number, $("#selected-site").attr('data-siteid'));
        }

        //function fixMapLayout() {
        //    var width = $(document).width();
        //    var height = $(document).height();
        //    $('.contentIframe').css('width', width);
        //    $('.contentIframe').css('height', height);
        //}


        function contentChange(iframeIndex) {
            parent.contentChange(iframeIndex);


        }




        //function fixMapLayout() {
        //    var width = $(document).width();
        //    var height = $(document).height();

        //    var parentSize = parent.getPageSize();



        //    $('.contentIframe').css('width', parentSize.width);
        //    $('.contentIframe').css('height', parentSize.height);
        //}


        function contentChange(iframeIndex) {

            document.getElementById("frame1").style.display = "none";
            document.getElementById("frame2").style.display = "none";
            document.getElementById("frame3").style.display = "none";
            document.getElementById("frame4").style.display = "none";
            document.getElementById("frame5").style.display = "none";

            var frameToDisplay = document.getElementById("frame" + iframeIndex);
            // if iframe is already loaded
            if ($("#frame" + iframeIndex).attr('src') == '') {
                showIframeLoader();

                switch (iframeIndex) {
                    case 1: frameToDisplay.src = "iFrames/MainMenu.aspx";
                        break;
                    case 2: frameToDisplay.src = "iFrames/2.aspx";
                        break;
                    case 3: frameToDisplay.src = "iFrames/3.aspx";
                        break;
                    case 4: frameToDisplay.src = "iFrames/4.aspx";
                        break;
                    case 5: frameToDisplay.src = "iFrames/5.aspx";
                        break;
                }
            }
            frameToDisplay.style.display = "";

        }



        function ListUserSites() {
            $("#selected-site").attr('data-siteid', '');
            $("#selected-site").html('Select a Site.');
            $("#site_selector").html('');

            $.ajax({
                type: "Post",
                async: true,
                url: window.location.pathname + "/ListUserSites",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: "{}",
                success: function (response) {

                    var jsonData = JSON.parse(response.d);
                    if (jsonData != null) {
                        var innerHtml = '';

                        var queryString = getFromQueryString("SITEID");


                        for (var r = 0; r < jsonData.length; r++) {
                            var row = jsonData[r];

                            // Add the placemark to Earth.
                            //innerHtml += "<option value='" + row.SiteID + "'>" + row.Site + "</option>";

                            if (queryString != undefined) {
                                if (parseInt(queryString) == row.SiteID) {
                                    $('#selected-site').attr("data-siteid", row.SiteID);
                                    $('#selected-site').html(row.Site);
                                }


                            } else {
  
                                if (r == 0) {
                                    $('#selected-site').attr("data-siteid", row.SiteID);
                                    $('#selected-site').html(row.Site);
                                }
                            }

                            innerHtml += "<li data-siteid='" + row.SiteID + "' data-clientid='" + row.ClientID + "' data-sitename='" + row.Site + "'> ";
                            innerHtml += row.Site;
                            innerHtml += "<span class='desc'>" + row.Client + "</span>";

                            innerHtml += "</li>";


                        }
                        $("#site_selector").html(innerHtml);
                        //$("#siteList").html(innerHtml);



                    }
                },
                error: function (error) {
                    // report error back to user
                    alert(error.responseText);
                }
            });
        }





        // Might remove this section
        function showIframeLoader() {

            $.blockUI({ message: $('#loadMessage') });
        }

        function closeIframeLoader() {
            $.unblockUI();
        }



    </script>
</head>
<body class="">
    <form id="form2" runat="server">
        <!-- before 

        <iframe id="frame1" class="contentIframe" src="iFrames/MainMenu.aspx" frameborder="0" scrolling="no"></iframe>
        <iframe id="frame2" style="display: none;" class="contentIframe" src="" frameborder="0" scrolling="no"></iframe>
        <iframe id="frame3" style="display: none;" class="contentIframe" src="" frameborder="0" scrolling="no"></iframe>
        <iframe id="frame4" style="display: none;" class="contentIframe" src="" frameborder="0" scrolling="no"></iframe>
        <iframe id="frame5" style="display: none;" class="contentIframe" src="" frameborder="0" scrolling="no"></iframe>

        -->
        <!-- HOME -->
        <div data-role="page" id="Home" data-theme="c" style="padding: 20px 10px 10px 10px;">
            <div data-role="content" id="MetroMenu">
                <!-- Share -->
                <div class="page-title-content" style="margin-bottom: 30px;">
                    <span class="page-title" style="margin-bottom: 40px;">MetroMenu</span>

                    <div class="option-wrapper">
                        <a id="selected-site" data-site="" href="javascript:;" class="selected open">Select a site</a>
                        <ul id="site_selector" class="site_selector" style="">
                        </ul>
                    </div>

                </div>
                <div data-role="popup" data-position-to="window" id="popupBasicShare">
                    <div id="buttons">
                    </div>
                </div>
                <!-- End Share -->
                <div id="ScrollMetroMenu" style="height: 100%; width: 100%; position: relative;">
                    <div id="MenuContant" style="padding-top: 15px;">
                    </div>
                </div>
            </div>
        </div>
        <!-- /HOME -->



        <div id="loadMessage" style="display: none;">
            <h1>Loading page.</h1>
        </div>

    </form>
</body>
</html>
