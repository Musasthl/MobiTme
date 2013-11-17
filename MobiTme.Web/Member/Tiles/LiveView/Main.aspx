<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="MobiTme.Web.Member.Tiles.LiveView.Main" %>
 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/forms.css" rel="stylesheet" />
    <link href="../../../Scripts/fontawesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../../../Scripts/jquery-2.0.3.min.js"></script>
            <script src="../../../Scripts/jquery.number.min.js"></script>
    <script src="../../../Scripts/utilities.js"></script>
    <script type="text/javascript">
        var LEFT_LAYOUT_SIZE = 275;
        var LEFT_LAYOUT_TITLE_SIZE = 100;
        var SITE_ID = 1;
        var layout_left_width;
        var layout_center_width;
        var isMenu_opened = true;
        $(document).ready(function () {
            SITE_ID = getFromQueryString("SITEID");
            InitilizeLayout();
       
            showDivDetail(1);

            $('#tbQuantity').number(true, 0);
            

            layout_left_width = $('#layout_left')[0].offsetWidth;
            layout_center_width = $('#layout_center')[0].offsetWidth;
            $(".sidebar-toggler").click(
                function () {

                    if (isMenu_opened == false) {
                        $('#layout_center').css('width', layout_center_width);

                        $('#layout_left').css('width', layout_left_width);

                        $("#sideMenu > li .title").css("display", "");
                        isMenu_opened = true;
                    } else {
                        $('#layout_center').css('width', $('#layout_center')[0].offsetWidth + 220);

                        $('#layout_left').css('width', "50");


                        $("#sideMenu > li .title").css("display", "none");
                        isMenu_opened = false;
                    }

                });

            $(document).on('click', '.row-select', function () {
                $('.row-select').removeClass('selected');
                $(this).addClass('selected');
                var keyElem = $(this).find('.key');
                if (keyElem.length == 0)
                    alert('Key not found.');
                else

                    $('#HiddenFieldKey').val(keyElem.html());
            });

            StartTimer();
        });
        
        var getDataInterval;
        var getDataIntervalSeconds = 30;

        function StartTimer() {
            countDown = 0;
            StopTheTimer();

            getDataInterval = setInterval("ListDataTable()", 1000);
    
        }
        function ListDataTable() {

            if (countDown > 0) {
                //consoleLog('Timer: ' + countDown);
                $("#refreshInterval").text(countDown);
                countDown--;
                //isBusyReceivingData(false, null);
                return;
            } else {
                countDown = getDataIntervalSeconds;
                gettingData = true;
            }



            StopTheTimer();
            
            var NumberOfClockings = $("#tbQuantity").val();
            $.ajax({
                type: "Post",
                url: window.location.pathname + "/ListLiveView",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: "{ 'siteID': '" + SITE_ID + "' ,'NumberOfClockings' : '" + NumberOfClockings  + "' }",
                success: function (response) {
                    var data = response.d;

                    if (data != null) {
                        $("#tblData").html(data);
                    }
                    resetTimer();
                },
                error: function (error) {
                    // report error back to user
                    alert(error.responseText);
                    resetTimer();
                }
            });
        }

        function StopTheTimer() {
            /// Stop the timer from running
            if (getDataInterval != null) {
                clearInterval(getDataInterval);
                getDataInterval = null;
            }
        }


        function resetTimer() {
  
                if (getDataInterval == null)
                    getDataInterval = setInterval("ListDataTable()", 1000);
            
        }
        
        function InitilizeLayout() {
            var pageSize = parent.getPageSize();

            $('#layout_center').css('width', pageSize.width - LEFT_LAYOUT_SIZE);

            $('#layout_left').css('height', pageSize.height);
            $('#layout_center').css('height', pageSize.height);
            $('#detailContent').css('height', pageSize.height - LEFT_LAYOUT_TITLE_SIZE);

        }


        function getPageSize() {
            var width = $(document).width();
            var height = $(document).height();
            return { width: width, height: height };
        }



        function insertNew() {
            $('#HiddenFieldKey').val('');
            $('input[type="text"]').val('');
            showDivDetail(2);
        }
        function showDivDetail(index) {
            // hide all and only show selected

            if (index == 1) {
                //$("#liEdit").css("display", "none");
                $("#liSave").css("display", "none");
                $("#liDelete").css("display", "none");
                $(".liDetail").css("display", "none");
                $("#liList").css("display", "none");
                $("#liNew").css("display", "");

                $("#liView").css("display", "");


            } else {
                $("#liList").css("display", "");
                //$("#liEdit").css("display", "");
                $("#liSave").css("display", "");
                $("#liDelete").css("display", "");
                $(".liDetail").css("display", "");
                $("#liNew").css("display", "none");
                $("#liView").css("display", "none");
                $("#liList").css("display", "");

                $(".divDetail :input").css("color", "black");
                $(".divDetail :input").css("border", "2px ");
                $(".divDetail :input").css("background-color", "white");
            }

            $('.divDetail').css('display', 'none');
            $('#divDetail' + index).css('display', '');

            // disable all and only enable selected one 
            $(".divDetail :input").attr("disabled", true);
            $("#divDetail" + index + " :input").attr("disabled", false);
        }

        function showAllDivDetail() {$("#sideMenu > li:eq(1)").nextAll().removeClass("selected");
            if ($('#HiddenFieldKey').val() == "") {
                alert("Please select the record you want to view.");
                return;
            }
            // show all and disable all
            $(".liDetail").css("display", "");
            //$("#liEdit").css("display", "");
            $("#liSave").css("display", "");
            $("#liDelete").css("display", "");
            $("#liList").css("display", "");
            $("#liNew").css("display", "none");
            $("#liView").css("display", "none");
            $("#liList").css("display", "");
            $('#divDetail1').css('display', 'none');
            $(".divDetail:not(#divDetail1)").css('display', '');

            $(".divDetail :input").attr("disabled", true);

            $(".divDetail :input").css("color", "white");
            $(".divDetail :input").css("border", "none");
            $(".divDetail :input").css("background-color", "transparent");
            viewData();
        }


        function viewData() {
            var KeyID = $('#HiddenFieldKey').val();
            $.ajax({
                type: "Post",
                url: window.location.pathname + "/Select",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: "{ 'SiteID': '" + SITE_ID + "', 'KeyID': '" + KeyID + "'}",
                success: function (response) {

                    var jsonData = JSON.parse(response.d);
                    if (jsonData != null) {
                        var row = jsonData[0];
                        $("#tbDepartment").val(row.Department);
                        $("#tbPayrollCode").val(row.PayrollCode);
                        $("#tbAccountsCode").val(row.AccountsCode);

                    }
                },
                error: function (error) {
                    // report error back to user
                    alert(error.responseText);
                }
            });
        }
         

        function back() {
            parent.slideTo('Menu', true);
        };

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="HiddenFieldKey" runat="server" />
        <div id="layout_center">
            <div class="pageTitle">Live View</div>
            <div id="detailContent">
                <div id="divDetail1" class="divDetail">
                    <div>
                        <h1>Live View Listing
                            <span style="font-size: 12px;font-weight: normal;">(refreshing in <span id="refreshInterval"></span> seconds)</span>

                        </h1>
                    </div>
 
                    <table class="striped" style="margin-top: 30px;">
                        <thead>
                            <tr>
                 
                                <td>First Names</td>
                                <td>Surname</td>
                                <td>Cellular</td>
                                        <td>Clocking Number</td>
                                        <td>Clocking Date</td>
                            </tr>
                        </thead>
                        <tbody id="tblData" runat="server"></tbody>
                    </table>
                </div>
 
            </div>
        </div>
        <div id="layout_left">
            <ul id="sideMenu">
                <li style="height: 53px;">
                    <!-- BEGIN SIDEBAR TOGGLER BUTTON -->
                    <div class="sidebar-toggler"></div>
                    <!-- END SIDEBAR TOGGLER BUTTON -->
                </li>
                <li class="start active">

                    <a onclick="javascript:back();">
                        <i class="fa fa-home fa-2x"></i>
                        <span class="title">Main Menu</span>
                        <span class="selected"></span>
                    </a>

                </li>

 
    <li id="liView" style="color: white;padding: 5px;">

                    
                      <span class="title">Quantity to View</span> <input type="text" id="tbQuantity" value="10" style="
    width: 142px;
    text-align: right;
">
                </li>

         






            </ul>
        </div>

    </form>
</body>
</html>
