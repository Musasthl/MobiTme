<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="MobiTme.Web.Member.Tiles.PayRules.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Pay Rule Masterfile</title>
    <link href="../../../Scripts/jquery-ui.css" rel="stylesheet" />




    <link href="../../../Scripts/jquery.timepicker/jquery-ui-timepicker-addon.css" rel="stylesheet" />
    <link href="../../Styles/forms.css" rel="stylesheet" />
    <script src="../../../Scripts/jquery-2.0.3.min.js"></script>
    <script src="../../../Scripts/jquery-ui-1.10.3.js"></script>
    <script src="../../../Scripts/jquery.blockUI.js"></script>


    <script src="../../../Scripts/utilities.js"></script>

    <script src="../../../Scripts/jquery.timepicker/jquery-ui-sliderAccess.js"></script>
    <script src="../../../Scripts/jquery.timepicker/jquery-ui-timepicker-addon.js"></script>
    <link href="../../../Scripts/fontawesome/css/font-awesome.min.css" rel="stylesheet" />



    <script type="text/javascript">
        var LEFT_LAYOUT_SIZE = 271;
        var LEFT_LAYOUT_TITLE_SIZE = 100;
        var SITE_ID = 1;
        var layout_left_width;
        var layout_center_width;
        var isMenu_opened = true;
        var selectedRow = null;
        ;
        var ARRAY_PAY_RATES = new Array("1.00000", "1.3333", "1.5000", "2.00000", "2.3333", "2.5000", "3.0000");

        $(document).ready(function () {
            SITE_ID = getFromQueryString("SITEID");
            InitilizeLayout();
            ListDataTable();
            showDivDetail(1);

            $('#tbShiftStart').datepicker();
            $('#tbShiftEnd').datepicker();


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

            //$('html').keyup(function (e) { if (e.keyCode == 8) alert('backspace trapped') })




            $(document).on('click', '.pay-pph', function (parameters) {
                $('.pay-row').removeClass('active');
                selectedRow = $(this).parent();
                selectedRow.addClass('active');


                var selectedTime = selectedRow.attr("data-time");

                $("#dlgPayRate").attr("date-ispph", "true");
                $("#dlgPayRate").attr("data-ispayrate", "true");
                $("#dlgPayRate > .title").html("Public HoliDay Pay Rate");
                $("#tbPayStartTime").val(selectedTime);

                $.blockUI({ message: $("#dlgPayRate"), css: { border: 'none' } });

            });

            $(document).on('click', '.pay-normal', function (parameters) {
                $('.pay-row').removeClass('active');
                selectedRow = $(this).parent();
                selectedRow.addClass('active');


                var selectedTime = selectedRow.attr("data-time");

                $("#dlgPayRate").attr("date-ispph", "false");
                $("#dlgPayRate").attr("data-ispayrate", "true");
                $("#dlgPayRate > .title").html("Normal Working Day Pay Rate");
                $("#tbPayStartTime").val(selectedTime);

                $.blockUI({ message: $("#dlgPayRate"), css: { border: 'none' } });
            });


            // Shift allawonce 

            $(document).on('click', '.shift-pph', function (parameters) {
                $('.shift-row').removeClass('active');
                selectedRow = $(this).parent();
                selectedRow.addClass('active');


                var selectedTime = selectedRow.attr("data-time");

                $("#dlgPayRate").attr("date-ispph", "true");
                $("#dlgPayRate").attr("data-ispayrate", "false");
                $("#dlgPayRate > .title").html("Public HoliDay Pay Rate");
                $("#tbPayStartTime").val(selectedTime);


                $.blockUI({ message: $("#dlgPayRate"), css: { border: 'none' } });
            });

            $(document).on('click', '.shift-normal', function (parameters) {
                $('.shift-row').removeClass('active');
                selectedRow = $(this).parent();
                selectedRow.addClass('active');

                var selectedTime = selectedRow.attr("data-time");

                $("#dlgPayRate").attr("date-ispph", "false");
                $("#dlgPayRate").attr("data-ispayrate", "false");
                $("#dlgPayRate > .title").html("Normal Working Day Pay Rate");
                $("#tbPayStartTime").val(selectedTime);


                $.blockUI({ message: $("#dlgPayRate"), css: { border: 'none' } });
            });



            $("#dlgPayRateOk").click(function () {
                var isPPH = $("#dlgPayRate").attr("date-ispph");
                var isPayRate = $("#dlgPayRate").attr("data-ispayrate");

                var time = $("#tbPayStartTime").val();
                var payRate = $("#cbPayRate").val();

                if (isPayRate == "true") {
                    var updateRow = $("#payRateData >tr.pay-row[data-time='" + time + "']");
                    if (isPPH == "true") {
                        $(updateRow.children()[3]).html(payRate);
                    } else {
                        $(updateRow.children()[2]).html(payRate);
                    }
                } else {
                    var updateRow = $("#shiftAllawonceData >tr.shift-row[data-time='" + time + "']");
                    if (isPPH == "true") {
                        $(updateRow.children()[3]).html(payRate);
                    } else {
                        $(updateRow.children()[2]).html(payRate);
                    }
                }
                $.unblockUI();
            });

            $("#dlgPayRatCancel").click(function () {
                $.unblockUI();
            });


        });





        function showPPHTimeSetup(parameters) {

            var htmlContent = '';
            var selectedTime = selectedRow.attr("data-time");
            htmlContent += "Pay at Rate ";
            htmlContent += "<select id='dlgDpPayRatePPH'>";
            $.each(ARRAY_PAY_RATES, function (index, value) {
                htmlContent += "<option value='" + value + "'>" + value + "</option>";
            });


            htmlContent += "</select>";
            htmlContent += " from ";
            //htmlContent += '<label class="input-control switch">';
            htmlContent += '<input id="txtPayRatePPHStart" type="text" value="' + selectedTime + '">';

            //htmlContent += '</label>';
            htmlContent += " to ";
            // htmlContent += '<label class="input-control switch">';
            htmlContent += '<input id="txtPayRatePPHEnd" type="text">';

            // htmlContent += '</label>';

            $.Dialog({
                'title': 'Paid Public Holiday Pay Rate',
                'content': htmlContent,
                'draggable': true,
                'overlay': true,
                'closeButton': true,
                'buttonsAlign': 'right',
                'keepOpened': true,
                'position': {
                    'zone': 'center'
                },
                'buttons': {
                    'OK': {
                        'action': function () {
                            var end = $("#txtPayRatePPHEnd").val();
                            var start = $("#txtPayRatePPHStart").val();
                            var selectedRate = $("#dlgDpPayRatePPH").val();

                            var endRow = $("#tbPayRates > tr > td:contains('" + end + "')");
                            var startRow = $("#tbPayRates > tr > td:contains('" + start + "')");

                            if (startRow.length > 0) {

                                var parentEnd = endRow.parent();
                                var parentStart = startRow.parent();
                                var parentEndID = parseInt(parentEnd.attr("id").replace("pay-row", ""));
                                var parentStartID = parseInt(parentStart.attr("id").replace("pay-row", ""));

                                for (var r = parentStartID; r <= parentEndID; r++) {
                                    $("#pay-row" + r + " > td:eq(2)").html("Pay hours at Rate " + selectedRate);
                                }
                            }



                            var a = "10:15"
                            var b = toDate(a, "h:m")
                            alert(b);

                        }
                    },
                    'Cancel': {
                        'action': function () { }
                    }
                }
            });
        }


        function ListDataTable(parameters) {
            $.ajax({
                type: "Post",
                url: window.location.pathname + "/ListPayRules",
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

                //if (index == 5) {

                //    listPayBracketing();

                //}else 

                //    if (index == 6) {
                //    listPayRates();
                //}
            }

            $('.divDetail').css('display', 'none');
            $('#divDetail' + index).css('display', '');

            // disable all and only enable selected one 
            $(".divDetail :input").attr("disabled", true);
            $("#divDetail" + index + " :input").attr("disabled", false);
        }

        function listPayBracketing(parameters) {
            $("#payBracketData").html('');
            $.ajax({
                async: false,
                type: "Post",
                url: window.location.pathname + "/ListBrackets",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: "{ 'siteID': '" + SITE_ID + "', 'payRuleID': '" + $('#HiddenFieldKey').val() + "'}",
                success: function (response) {
                    var data = response.d;

                    if (data != null) {
                        $("#payBracketData").html(data);
                    }
                },
                error: function (error) {
                    // report error back to user
                    alert(error.responseText);
                }
            });
        }

        function listPayRates(parameters) {
            //$("#payRateData").html('');
            //$.ajax({
            //    async: false,
            //    type: "Post",
            //    url: window.location.pathname + "/ListPayRate",
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json", // can be used for plaintext or for JSON
            //    data: "{ 'siteID': '" + SITE_ID + "', 'payRuleID': '" + $('#HiddenFieldKey').val() + "'}",
            //    success: function (response) {
            //        var data = response.d;

            //        if (data != null) {
            //            $("#payRateData").html(data);
            //        }
            //    },
            //    error: function (error) {
            //        // report error back to user
            //        alert(error.responseText);
            //    }
            //});
        }

        function showAllDivDetail() {
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


                        $("#tbPayRule").val(row.PayRule);
                        $("#tbShiftStart").val(row.ShiftStart);
                        $("#tbShiftEnd").val(row.ShiftEnd);
                        $("#tbDescription").val(row.Description);
                        $("#tbShiftDayAdjustment").val(row.ShiftDayAdjustment);
                        $("#tbRoundingType_In").val(row.RoundingType_In);
                        $("#tbRoundingUnit_In").val(row.RoundingUnit_In);
                        $("#tbRoundingBase_In").val(row.RoundingBase_In);
                        $("#tbRoundingType_Out").val(row.RoundingType_Out);
                        $("#tbRoundingUnit_Out").val(row.RoundingUnit_Out);
                        $("#tbRoundingBase_Out").val(row.RoundingBase_Out);




                    }
                },
                error: function (error) {
                    // report error back to user
                    alert(error.responseText);
                }
            });

            listPayRates();
            listPayBracketing();
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
            var payRule = new Object();
            payRule.PayRuleID = $('#HiddenFieldKey').val();
            payRule.SiteID = SITE_ID;
            payRule.PayRuleName = $("#tbPayRule").val();
            payRule.ShiftStart = $("#tbShiftStart").val();
            payRule.ShiftEnd = $("#tbShiftEnd").val();
            payRule.Description = $("#tbDescription").val();
            payRule.ShiftDayAdjustment = $("#tbShiftDayAdjustment").val();
            payRule.RoundingType_In = $("#tbRoundingType_In").val();
            payRule.RoundingUnit_In = $("#tbRoundingUnit_In").val();
            payRule.RoundingBase_In = $("#tbRoundingBase_In").val();
            payRule.RoundingType_Out = $("#tbRoundingType_Out").val();
            payRule.RoundingUnit_Out = $("#tbRoundingUnit_Out").val();
            payRule.RoundingBase_Out = $("#tbRoundingBase_Out").val();

            $.ajax({
                type: "Post",
                url: window.location.pathname + "/Insert",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: JSON.stringify({ payRule: payRule }),
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
            var payRule = new Object();
            payRule.PayRuleID = $('#HiddenFieldKey').val();
            payRule.SiteID = SITE_ID;
            payRule.PayRuleName = $("#tbPayRule").val();
            payRule.ShiftStart = $("#tbShiftStart").val();
            payRule.ShiftEnd = $("#tbShiftEnd").val();
            payRule.Description = $("#tbDescription").val();
            payRule.ShiftDayAdjustment = $("#tbShiftDayAdjustment").val();
            payRule.RoundingType_In = $("#tbRoundingType_In").val();
            payRule.RoundingUnit_In = $("#tbRoundingUnit_In").val();
            payRule.RoundingBase_In = $("#tbRoundingBase_In").val();
            payRule.RoundingType_Out = $("#tbRoundingType_Out").val();
            payRule.RoundingUnit_Out = $("#tbRoundingUnit_Out").val();
            payRule.RoundingBase_Out = $("#tbRoundingBase_Out").val();

            $.ajax({
                type: "Post",
                url: window.location.pathname + "/Update",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: JSON.stringify({ payRule: payRule }),
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
            <div class="pageTitle">Pay Rules Masterfile</div>
            <div id="detailContent">
                <div id="divDetail1" class="divDetail">
                    <div>
                        <h1>Available Cost Centre Listing</h1>
                    </div>

                    <table class="striped" style="margin-top: 30px;">
                        <thead>
                            <tr>
                                <td class="hidden">PayRuleID</td>
                                <td>Pay Rule</td>
                                <td>Description</td>
                                <td>Shift Start</td>

                                <td>Shift End</td>




                            </tr>
                        </thead>
                        <tbody id="tblData" runat="server"></tbody>
                    </table>
                </div>
                <div id="divDetail2" class="divDetail">
                    <div>
                        <h1>Description Detail</h1>
                    </div>
                    <div style="padding-left: 10px;">
                        <div style="width: 120px; float: left; padding-top: 5px">Pay Rule</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbPayRule" style="width: 300px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Shift Starts at</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbShiftStart" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Shift Ends at</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbShiftEnd" style="width: 100px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Pay Rule Description</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbDescription" style="width: 300px" type="text">
                        </div>
                        <br>
                        <br>
                    </div>
                </div>
                <div id="divDetail3" class="divDetail">
                    <div>
                        <h1>Shift Date Adjustment Detail</h1>
                    </div>
                    <div style="padding-left: 10px;">
                        <div style="width: 120px; float: left; padding-top: 5px">Adjustment (Hours)</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input id="tbShiftDayAdjustment" style="width: 50px" type="text">
                        </div>
                        <br>
                        <br>
                    </div>
                </div>
                <div id="divDetail4" class="divDetail">
                    <div>
                        <h1>Clocking Rounding Detail</h1>
                    </div>
                    <div style="padding-left: 10px;">
                        <div style="width: 120px; float: left; padding-top: 5px">In Clocking - Type</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <select id="tbRoundingType_In" style="width: 150px">
                                <option>Up</option>
                                <option>Down</option>
                                <option>Nearest</option>
                            </select>
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">In Clocking - Unit</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <select id="tbRoundingUnit_In" style="width: 150px">
                                <option>Second</option>
                                <option>Minute</option>
                                <option>Hour</option>
                            </select>
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">In Clocking - Base</div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input id="tbRoundingBase_In" style="width: 50px" type="text">
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Out Clocking - Type</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <select id="tbRoundingType_Out" style="width: 150px">
                                <option>Up</option>
                                <option>Down</option>
                                <option>Nearest</option>
                            </select>
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Out Clocking - Unit</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <select id="tbRoundingUnit_Out" style="width: 150px">
                                <option>Second</option>
                                <option>Minute</option>
                                <option>Hour</option>
                            </select>
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Out Clocking - Base</div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input id="tbRoundingBase_Out" style="width: 50px" type="text">
                        </div>
                        <br>
                        <br>
                    </div>
                </div>
                <div id="divDetail5" class="divDetail">
                    <div>
                        <h1>Clocking Bracketing Detial</h1>
                    </div>
                    <div style="padding-left: 10px;">
                        <div>
                            <asp:HiddenField ID="HiddenFieldPayBracketID" runat="server" />
                            <table class="striped" style="margin-top: 30px;">
                                <thead>
                                    <tr>
                                        <td class="hidden">PayBracketID</td>
                                        <td>Clocking Direction</td>
                                        <td>Bracket From</td>
                                        <td>Bracket To</td>

                                        <td>Pay Clocking</td>

                                    </tr>
                                </thead>
                                <tbody id="payBracketData" runat="server"></tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div id="divDetail6" class="divDetail">
                    <div>
                        <h1>Pay Rate Detail</h1>
                    </div>
                    <div style="padding-left: 10px;">
                        <asp:HiddenField ID="HiddenFieldPayRateID" runat="server" />
                        <table class="striped" style="margin-top: 30px;">
                            <thead>
                                <tr>
                                    <td class="hidden">PayRateID</td>
                                    <td style="width: 120px"></td>
                                    <td>Normal Working Day</td>
                                    <td>Paid Public Holiday</td>


                                </tr>
                            </thead>
                            <tbody id="payRateData" runat="server"></tbody>
                        </table>
                    </div>
                </div>
                <div id="divDetail7" class="divDetail">
                    <div>
                        <h1>Shift Allowance Detail</h1>
                    </div>
                    <div style="padding-left: 10px;">

                        <asp:HiddenField ID="HiddenFieldShiftAllowanceID" runat="server" />
                        <table class="striped" style="margin-top: 30px;">
                            <thead>
                                <tr>
                                    <td class="hidden">ShiftAllowanceID</td>
                                    <td style="width: 120px"></td>
                                    <td>Normal Working Day</td>
                                    <td>Paid Public Holiday</td>


                                </tr>
                            </thead>
                            <tbody id="shiftAllawonceData" runat="server"></tbody>
                        </table>
                    </div>
                </div>
                <div id="divDetail8" class="divDetail">
                    <div>
                        <h1>Automated Deduction Detail</h1>
                    </div>
                    <div style="padding-left: 10px;">
                        <div>{ADD DEDUCTION DIV HERE}</div>
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
                        <span class="title">Description Detail</span>
                        <span class="arrow "></span>
                    </a>

                </li>

                <li class="liDetail" onclick="showDivDetail(3)">

                    <a href="javascript:;">
                        <i class="fa fa-edit fa-2x"></i>
                        <span class="title">Shift Date Adjustment Detail</span>
                        <span class="arrow "></span>
                    </a>

                </li>

                <li class="liDetail" onclick="showDivDetail(4)">

                    <a href="javascript:;">
                        <i class="fa fa-edit fa-2x"></i>
                        <span class="title">Clocking Rounding Detail</span>
                        <span class="arrow "></span>
                    </a>

                </li>


                <li class="liDetail" onclick="showDivDetail(5)">

                    <a href="javascript:;">
                        <i class="fa fa-edit fa-2x"></i>
                        <span class="title">Clocking Bracketing Detail</span>
                        <span class="arrow "></span>
                    </a>

                </li>


                <li class="liDetail" onclick="showDivDetail(6)">

                    <a href="javascript:;">
                        <i class="fa fa-edit fa-2x"></i>
                        <span class="title">Pay Rate Detail</span>
                        <span class="arrow "></span>
                    </a>

                </li>

                <li class="liDetail" onclick="showDivDetail(7)">

                    <a href="javascript:;">
                        <i class="fa fa-edit fa-2x"></i>
                        <span class="title">Shift Allowance Detail</span>
                        <span class="arrow "></span>
                    </a>

                </li>

                <li class="liDetail" onclick="showDivDetail(2)">

                    <a href="javascript:;">
                        <i class="fa fa-edit fa-2x"></i>
                        <span class="title">Automated Deduction Detail</span>
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

    <div id="dlgPayRate" date-ispph="false" data-ispayrate="true">
        <div class="title" style="color: #222222; font-weight: bold; background: #0095DA; padding: 5px;">
            Normal Working Day Pay Rate
        </div>

        <div class="content-wrapper">
            Pay rate at
            <select id="cbPayRate">
                <option value="1.00000">1.00000</option>
                <option value="1.3333">1.3333</option>
                <option value="1.5000">1.5000</option>
                <option value="2.00000">2.00000</option>
                <option value="2.3333">2.3333</option>
                <option value="2.5000">2.5000</option>
                <option value="3.0000">3.0000</option>
            </select>
            from
            <input id="tbPayStartTime" type="text" placeholder="time" />
            <hr />
            <button id="dlgPayRateOk">OK</button>
            <button id="dlgPayRatCancel">Cancel</button>
        </div>
    </div>
</body>
</html>
