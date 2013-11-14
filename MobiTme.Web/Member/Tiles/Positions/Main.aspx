<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="MobiTme.Member.Tiles.Positions.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            ListDataTable();
            showDivDetail(1);
 

            $('#tbCtcRate10').number(true, 2);
            $('#tbCtcRate13').number(true, 2);
            $('#tbCtcRate15').number(true, 2);
            $('#tbCtcRate20').number(true, 2);
            $('#tbCtcRate23').number(true, 2);
            $('#tbCtcRate25').number(true, 2);
            $('#tbCtcRate30').number(true, 2);

            $('#tbCtcSA01').number(true, 2);
            $('#tbCtcSA02').number(true, 2);
            $('#tbCtcSA03').number(true, 2);

            $('#tbCtcHoursPPH').number(true, 2);
            $('#tbCtcPaidPPH').number(true, 2);

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
        });

        function ListDataTable(parameters) {
            $.ajax({
                type: "Post",
                url: window.location.pathname + "/ListPositions",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: "{ 'siteID': '" + SITE_ID + "'}",
                success: function (response) {
                    var data = response.d;

                    if (data != null) {
                        $("#tblData").html(data);
                    }
                },
                error: function (error) {
                    // report error back to user
                    alert(error.responseText);
                }
            });
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
                $("#liEdit").css("display", "none");
                $("#liSave").css("display", "none");
                $("#liDelete").css("display", "none");
                $(".liDetail").css("display", "none");
                $("#liList").css("display", "none");
                $("#liNew").css("display", "");

                $("#liView").css("display", "");


            } else {
                $("#liList").css("display", "");
                $("#liEdit").css("display", "");
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

        function showAllDivDetail() {
            if ($('#HiddenFieldKey').val() == "") {
                alert("Please select the record you want to view.");
                return;
            }
            // show all and disable all
            $(".liDetail").css("display", "");
            $("#liEdit").css("display", "");
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

                        $("#tbPosition").val(row.Position);
                        $("#tbPayrollCode").val(row.PayrollCode);
                        $("#tbAccountsCode").val(row.AccountsCode);
                        //$("#tbCtcRate10").val(row.CtcRate00 );
                        //position.CtcRate00 = $("#tbCtcRate00").val();
                        $("#tbCtcRate10").val(row.CtcRate10);
                        $("#tbCtcRate13").val(row.CtcRate13);
                        $("#tbCtcRate15").val(row.CtcRate15);
                        $("#tbCtcRate20").val(row.CtcRate20);
                        $("#tbCtcRate23").val(row.CtcRate23);
                        $("#tbCtcRate25").val(row.CtcRate25);
                        $("#tbCtcRate30").val(row.CtcRate30);
                        $("#tbCtcPaidPPH").val(row.CtcRatePPH);



                    }
                },
                error: function (error) {
                    // report error back to user
                    alert(error.responseText);
                }
            });
        }


        function deleteData() {
            var r = confirm("Are you sure that you want to delete this record?");
            if (r == true) {

                var KeyID = $('#HiddenFieldKey').val();
                $.ajax({
                    type: "Post",
                    async: false,
                    url: window.location.pathname + "/Select",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json", // can be used for plaintext or for JSON
                    data: "{ 'SiteID': '" + SITE_ID + "', 'KeyID': '" + KeyID + "'}",
                    success: function (response) {
                        var data = response.d;
                        if (data.toLowerCase() == 'true') {
                            ListDataTable();
                            alert('Record deleted successful.');
                        } else {
                            alert('Failed to delete record.');
                        }
                    },
                    error: function (error) {
                        // report error back to user
                        alert(error.responseText);
                    }
                });
            }
        }

        function saveData() {
            var key = $('#HiddenFieldKey').val();
            if (key != '') {
                update();
            } else {
                insert();
            }
        }

        function insert() {
            var position = new Object();
            position.PositionID = $('#HiddenFieldKey').val();
            position.SiteID = SITE_ID;
            position.PositionName = $("#tbPosition").val();
            position.PayrollCode = $("#tbPayrollCode").val();
            position.AccountsCode = $("#tbAccountsCode").val();
            position.CtcRate00 = $("#tbCtcRate10").val();
            //position.CtcRate00 = $("#tbCtcRate00").val();
            position.CtcRate10 = $("#tbCtcRate10").val();
            position.CtcRate13 = $("#tbCtcRate13").val();
            position.CtcRate15 = $("#tbCtcRate15").val();
            position.CtcRate20 = $("#tbCtcRate20").val();
            position.CtcRate23 = $("#tbCtcRate23").val();
            position.CtcRate25 = $("#tbCtcRate25").val();
            position.CtcRate30 = $("#tbCtcRate30").val();
            position.CtcRatePPH = $("#tbCtcPaidPPH").val();

            $.ajax({
                type: "Post",
                url: window.location.pathname + "/Insert",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: JSON.stringify({ position: position }),
                success: function (response) {
                    var data = response.d;

                    if (data.toLowerCase() == 'true') {
                        ListDataTable();
                        alert('Saved successful.');

                    } else {
                        alert('Failed to insert record.');
                    }
                },
                error: function (error) {
                    // report error back to user
                    alert(error.responseText);
                }
            });
        }

        function update() {
            var position = new Object();
            position.PositionID = $('#HiddenFieldKey').val();
            position.SiteID = SITE_ID;
            position.PositionName = $("#tbPosition").val();
            position.PayrollCode = $("#tbPayrollCode").val();
            position.AccountsCode = $("#tbAccountsCode").val();
            position.CtcRate00 = $("#tbCtcRate10").val();
            //position.CtcRate00 = $("#tbCtcRate00").val();
            position.CtcRate10 = $("#tbCtcRate10").val();
            position.CtcRate13 = $("#tbCtcRate13").val();
            position.CtcRate15 = $("#tbCtcRate15").val();
            position.CtcRate20 = $("#tbCtcRate20").val();
            position.CtcRate23 = $("#tbCtcRate23").val();
            position.CtcRate25 = $("#tbCtcRate25").val();
            position.CtcRate30 = $("#tbCtcRate30").val();
            position.CtcRatePPH = $("#tbCtcPaidPPH").val();

            $.ajax({
                type: "Post",
                url: window.location.pathname + "/Update",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: JSON.stringify({ position: position }),
                success: function (response) {
                    var data = response.d;

                    if (data.toLowerCase() == 'true') {
                        ListDataTable();
                        alert('Updated successful.');
                    } else {
                        alert('Failed to update record.');
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
            <div class="pageTitle">Position Masterfile</div>
            <div id="detailContent">
                <div id="divDetail1" class="divDetail">
                    <div>
                        <h1>Position Listing</h1>
                    </div>

                    <table class="striped" style="margin-top: 30px;">
                        <thead>
                            <tr>

                                <td class="hidden">Position ID</td>
                      
                                <td>Position</td>
                                <td>Payroll Code</td>
                                <td>Accounts Code</td>
                            </tr>
                        </thead>



                        <tbody id="tblData" runat="server"></tbody>
                    </table>
                </div>
                <div id="divDetail2" class="divDetail">
                    <div>
                        <h1>Position Detail</h1>
                    </div>
                    <div style="padding-left: 10px;">
                        <div style="width: 120px; float: left; padding-top: 5px">Position</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbPosition" style="width: 300px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Payroll Code</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbPayrollCode" style="width: 150px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Accounts Code</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbAccountsCode" style="width: 150px" type="text">
                        </div>
                        <br>
                        <br>
                    </div>
                </div>
                <div id="divDetail3" class="divDetail">
                    <div>
                        <h1>Cost to Company</h1>
                    </div>
                    <div style="padding-left: 10px;">
                        <div style="width: 120px; float: left; padding-top: 5px">Normal Time</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbCtcRate10" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Overtime 1.3333</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbCtcRate13" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Overtime  1.5000</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbCtcRate15" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Overtime  2.0000</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbCtcRate20" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Overtime  2.3333</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbCtcRate23" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Overtime  2.5000</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbCtcRate25" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Overtime  3.0000</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbCtcRate30" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Shift Allowance 01</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbCtcSA01" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Shift Allowance 02</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbCtcSA02" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Shift Allowance 03</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbCtcSA03" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">PPH - Hours Worked</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbCtcHoursPPH" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">PPH - Entitlement</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbCtcPaidPPH" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>
                    </div>
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


                <li id="liNew" onclick="insertNew()">
                    <a href="javascript:;">
                        <i class="fa fa-plus-circle fa-2x"></i>
                        <span class="title">Insert New</span>
                        <span class="arrow "></span>
                    </a>
                </li>
                <li id="liView" onclick="showAllDivDetail()">

                    <a href="javascript:;">
                        <i class="fa  fa-list-alt fa-2x"></i>
                        <span class="title">View Details</span>
                        <span class="arrow "></span>
                    </a>
                </li>


                <li class="liDetail" onclick="showDivDetail(2)">

                    <a href="javascript:;">
                        <i class="fa fa-edit fa-2x"></i>
                        <span class="title">Edit Position Detail</span>
                        <span class="arrow "></span>
                    </a>

                </li>
                
                                <li class="liDetail" onclick="showDivDetail(3)">

                    <a href="javascript:;">
                        <i class="fa fa-edit fa-2x"></i>
                        <span class="title">Edit Cost to Company Detail</span>
                        <span class="arrow "></span>
                    </a>

                </li>

                <li id="liSave" onclick="saveData()">

                    <a href="javascript:;">
                        <i class="fa fa-save fa-2x"></i>
                        <span class="title">Save</span>
                        <span class="arrow "></span>
                    </a>
                </li>

                <li id="liDelete">


                    <a href="javascript:;">
                        <i class="fa fa-eraser fa-2x"></i>
                        <span class="title">Delete</span>
                        <span class="arrow "></span>
                    </a>
                </li>

                <li id="liList" onclick="showDivDetail(1)">


                    <a href="javascript:;">
                        <i class="fa fa-th-list fa-2x"></i>
                        <span class="title">Return</span>
                        <span class="arrow "></span>
                    </a>
                </li>






            </ul>
        </div>

     
    </form>
</body>
</html>
