<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="MobiTme.Web.Member.Tiles.TimeCard.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../Scripts/jquery-ui.css" rel="stylesheet" />
    <link href="../../../Scripts/jquery.timepicker/jquery-ui-timepicker-addon.css" rel="stylesheet" />
    <link href="../../Styles/forms.css" rel="stylesheet" />
    <link href="../../../Scripts/fontawesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../../../Scripts/jquery-2.0.3.min.js"></script>
    <script src="../../../Scripts/jquery.blockUI.min.js"></script>

    <script src="../../../Scripts/utilities.js"></script>
    <script src="../../../Scripts/jquery-ui-1.10.3.js"></script>
    <script src="../../../Scripts/jquery.timepicker/jquery-ui-sliderAccess.js"></script>
    <script src="../../../Scripts/jquery.timepicker/jquery-ui-timepicker-addon.js"></script>
    <script type="text/javascript">
        var LEFT_LAYOUT_SIZE = 271;
        var LEFT_LAYOUT_TITLE_SIZE = 40;
        var LEFT_LAYOUT_FOOTER_SIZE = 50;
        var SITE_ID = 1;
        var layout_left_width;
        var layout_center_width;
        var isMenu_opened = true;
        $(document).ready(function () {
            SITE_ID = getFromQueryString("SITEID");
            InitilizeLayout();
            ListDataTable();

            layout_left_width = $('#layout_left')[0].offsetWidth;
            layout_center_width = $('#layout_center')[0].offsetWidth;

            $('#dlgClockingNewClockingDate').datetimepicker();
            

            $(document).on('click', '.emp-selector', function () {
                $(".emp-selector").removeClass("selected");
                $(this).addClass("selected");
            });


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
                //$('.row-select').removeClass('selected');
                $(this).addClass('selected');
                var keyElem = $(this).find('.key');
                if (keyElem.length == 0)
                    alert('Key not found.');
                else
                    $('#HiddenFieldKey').val(keyElem.html());
            });

            $(document).on('click', '.employee-li', function () {
                $('.employee-li').removeClass('selected');
                $(this).addClass('selected');

            });
            
            $(document).on('change', '.shiftCycleDropdown', function () {
                var val = $(this).val();
                var parent = $(this).parent().parent();
                var keyID = parent.attr('key');
                var lineID = parent.find('.TypeDropdown').val();
                var siteID = SITE_ID;
                var empID = $("#HiddenFieldKey").val();
                $.ajax({
                    type: "Post",
                    async: false,
                    url: window.location.pathname + "/UpdateShiftCycle",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json", // can be used for plaintext or for JSON
                    data: "{ 'siteID': '" + siteID + "', 'lineID': '" + lineID + "', 'empID': '" + empID + "', 'keyID': '" + keyID + "', 'val': '" + val + "'}",
                    success: function (response) {
                        var data = response.d;

                        if (data != null) {
                            if (data.toLowerCase() == 'true') {
                                SelectTimeCard(empID);
                            }
                        }
                    },
                    error: function (error) {
                        // report error back to user
                        closeLoader();
                        alert(error.responseText);
                    }
                });

            });
            


            $(document).on('change', '.costCentreDropdown', function () {
                var val = $(this).val();
                var parent = $(this).parent().parent();
                var keyID = parent.attr('key');
                var lineID = parent.find('.TypeDropdown').val();
                var siteID = SITE_ID;
                var empID = $("#HiddenFieldKey").val();
                $.ajax({
                    type: "Post",
                    async: false,
                    url: window.location.pathname + "/UpdateTimeCardCostCentre",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json", // can be used for plaintext or for JSON
                    data: "{ 'siteID': '" + siteID + "', 'lineID': '" + lineID + "', 'empID': '" + empID + "', 'keyID': '" + keyID + "', 'val': '" + val + "'}",
                    success: function (response) {
                        var data = response.d;

                        if (data != null) {
                            if (data.toLowerCase() == 'true') {
                                SelectTimeCard(empID);
                            }
                        }
                    },
                    error: function (error) {
                        // report error back to user
                        closeLoader();
                        alert(error.responseText);
                    }
                });

            });
            


            $(document).on('change', '.departmentDropdown', function () {
                var val = $(this).val();
                var parent = $(this).parent().parent();
                var keyID = parent.attr('key');
                var lineID = parent.find('.TypeDropdown').val();
                var siteID = SITE_ID;
                var empID = $("#HiddenFieldKey").val();
                $.ajax({
                    type: "Post",
                    async: false,
                    url: window.location.pathname + "/UpdateTimeDepartment",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json", // can be used for plaintext or for JSON
                    data: "{ 'siteID': '" + siteID + "', 'lineID': '" + lineID + "', 'empID': '" + empID + "', 'keyID': '" + keyID + "', 'val': '" + val + "'}",
                    success: function (response) {
                        var data = response.d;

                        if (data != null) {
                            if (data.toLowerCase() == 'true') {
                                SelectTimeCard(empID);
                            }
                        }
                    },
                    error: function (error) {
                        // report error back to user
                        closeLoader();
                        alert(error.responseText);
                    }
                });

            });

            $(document).on('change', '.supervisorDropdown', function () {
                var val = $(this).val();
                var parent = $(this).parent().parent();
                var keyID = parent.attr('key');
                var lineID = parent.find('.TypeDropdown').val();
                var siteID = SITE_ID;
                var empID = $("#HiddenFieldKey").val();
                $.ajax({
                    type: "Post",
                    async: false,
                    url: window.location.pathname + "/UpdateTimeSuperVisor",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json", // can be used for plaintext or for JSON
                    data: "{ 'siteID': '" + siteID + "', 'lineID': '" + lineID + "', 'empID': '" + empID + "', 'keyID': '" + keyID + "', 'val': '" + val + "'}",
                    success: function (response) {
                        var data = response.d;

                        if (data != null) {
                            if (data.toLowerCase() == 'true') {
                                SelectTimeCard(empID);
                            }
                        }
                    },
                    error: function (error) {
                        // report error back to user
                        closeLoader();
                        alert(error.responseText);
                    }
                });

            });

            $(document).on('change', '.positionDropdown', function () {
                var val = $(this).val();
                var parent = $(this).parent().parent();
                var keyID = parent.attr('key');
                var lineID = parent.find('.TypeDropdown').val();
                var siteID = SITE_ID;
                var empID = $("#HiddenFieldKey").val();
                $.ajax({
                    type: "Post",
                    async: false,
                    url: window.location.pathname + "/UpdateTimeCardPosition",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json", // can be used for plaintext or for JSON
                    data: "{ 'siteID': '" + siteID + "', 'lineID': '" + lineID + "', 'empID': '" + empID + "', 'keyID': '" + keyID + "', 'val': '" + val + "'}",
                    success: function (response) {
                        var data = response.d;

                        if (data != null) {
                            if (data.toLowerCase() == 'true') {
                                SelectTimeCard(empID);
                            }
                        }
                    },
                    error: function (error) {
                        // report error back to user
                        closeLoader();
                        alert(error.responseText);
                    }
                });


            });

            $(document).on('change', '.shiftCycleDropdown', function () {
                var val = $(this).val();
                var key = $(this).parent().parent().attr('key');

            });

            // siteid, inclocing id, department, line
        });

        function SelectTimeCard(EmployeeID) {
            showLoader();

            $.ajax({
                type: "Post",
                async: false,
                url: window.location.pathname + "/SelectTimeCard",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: "{ 'SiteID': '" + SITE_ID + "', 'EmployeeID': '" + EmployeeID + "'}",
                success: function (response) {
                    var data = response.d;

                    if (data != null) {
                        $("#tblData").html(data);
                    }
                    $("#HiddenFieldKey").val(EmployeeID);
                    closeLoader();
                },
                error: function (error) {
                    // report error back to user
                    closeLoader();
                    alert(error.responseText);
                }
            });

        }



        function InitilizeLayout() {
            var pageSize = parent.getPageSize();

            $('#layout_center').css('width', pageSize.width - LEFT_LAYOUT_SIZE);

            $('#layout_left').css('height', pageSize.height);
            $('#layout_center').css('height', pageSize.height);
            $('#detailContent').css('height', pageSize.height - LEFT_LAYOUT_TITLE_SIZE - LEFT_LAYOUT_FOOTER_SIZE);

        }


        function getPageSize() {
            var width = $(document).width();
            var height = $(document).height();
            return { width: width, height: height };
        }



        function ListDataTable(parameters) {
            $.ajax({
                type: "Post",
                url: window.location.pathname + "/EmployeeList",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: "{ 'siteID': '" + SITE_ID + "'}",
                success: function (response) {
                    var data = response.d;

                    if (data != null) {
                        $("#sideMenu").append(data);
                    }
                },
                error: function (error) {
                    // report error back to user
                    alert(error.responseText);
                }
            });
        }



        function ShowInClocking(clockingid, clocking) {
            $("#dlgClockingDescription").html("in");
            $("#dlgClockingClockingID").val(clockingid);
            $("#dlgClockingOriginalClocking").val(clocking);
            $.blockUI({ message: $("#dlgClocking"), css: { border: 'none', width: '300px' } });
        }

        function ShowOutClocking(clockingid, clocking) {
            $("#dlgClockingDescription").html("out");
            $("#dlgClockingClockingID").val(clockingid);
            $("#dlgClockingOriginalClocking").val(clocking);
            $.blockUI({ message: $("#dlgClocking"), css: { border: 'none', width: '300px' } });
        }


        function showLoader() {
            $.blockUI({ message: $("#loader"), css: { border: 'none' } });
        }

        function closeLoader() {
            $.unblockUI();
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
            <div class="pageTitle">Time Card</div>
            <div id="detailContent">
                <div id="divDetail1" class="divDetail">
                    <div>
                        <h1>Available Supervisor Listing</h1>
                    </div>

                    <table class="striped" style="margin-top: 30px;">
                        <thead>
                            <tr>
                                <td>ShiftDate</td>
                                <td>In Clocking</td>
                                <td>Out Clocking</td>
                                <td>Type</td>
                                <td>Shift Cycle</td>
                                <td>Cost Center</td>
                                <td>Department</td>
                                <td>Supervisor</td>
                                <td>Position</td>
                                <td>Shift Pattern</td>
                                <td>Hours 10</td>
                                <td>Hours 13</td>
                                <td>Hours 15</td>
                                <td>Hours 20</td>
                                <td>Hours 23</td>
                                <td>Hours 25</td>
                                <td>Hours 30</td>
                                <td>SA 01</td>
                                <td>SA 02</td>
                                <td>SA 03</td>
                                <td>Hours PPH</td>
                                <td>Annual</td>
                                <td>Sick</td>
                                <td>Coida</td>
                                <td>Family</td>
                                <td>Study</td>
                                <td>Awol</td>
                            </tr>
                        </thead>
                        <tbody id="tblData" runat="server"></tbody>
                    </table>
                </div>
            </div>
            <div class="divDetail-footer">
                <!-- 
<button value="Leave / Awol / Sick"></button> -->
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





            </ul>
        </div>


    </form>
    <div id="loader" style="display: none;">
        <h1>Loading page.</h1>
    </div>

    <div id="dlgClocking" style="display: none;  ">
        <div id="title" style="background: #0095DA !important;
padding: 5px;
font-weight: bold;">Clocking</div>
        <div id="content">
            <table>
                <tr>
                    <td>Clocking ID</td>
                    <td>
                        <input id="dlgClockingClockingID" type="text" value="" disabled="disabled" /></td>
                </tr>
                <tr>
                    <td>Original Clocking</td>
                    <td>
                        <input id="dlgClockingOriginalClocking" type="text" value="" disabled="disabled"  /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">Add correct <span id="dlgClockingDescription" style="font-weight: bold;"></span> clocking.
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input id="dlgClockingNewClockingDate" type="text" value="" />
                  
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                               <input id="Button4" type="button" value="Update" />
                        <input id="Button1" type="button" value="Delete" />
                        <input id="Button2" type="button" value="Insert New" />
                        <input id="Button3" type="button" value="Cancel" onclick="closeLoader()" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>
