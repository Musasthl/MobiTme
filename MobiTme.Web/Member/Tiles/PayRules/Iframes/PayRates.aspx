<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayRates.aspx.cs" Inherits="UITemplate.Member.Tiles.PayRules.Iframes.PayRates" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../../Scripts/metro_ui_css/css/modern.css" rel="stylesheet" />
    <link href="../../../../Scripts/metro_ui_css/css/modern-responsive.css" rel="stylesheet" />
    <link href="../../../../Styles/style_global.css" rel="stylesheet" />
    <script src="../../../../Scripts/jquery-2.0.3.min.js"></script>

    <script src="../../../../Scripts/jquery-ui-1.10.3.min.js"></script>
    <script src="../../../../Scripts/jquery.blockUI.js"></script>
    <script src="../../../../Scripts/jquery.layout-latest.js"></script>
    <script src="../../../../Scripts/metro_ui_css/javascript/accordion.js"></script>
    <script src="../../../../Scripts/metro_ui_css/javascript/buttonset.js"></script>
    <script src="../../../../Scripts/metro_ui_css/javascript/calendar.js"></script>
    <script src="../../../../Scripts/metro_ui_css/javascript/carousel.js"></script>

    <script src="../../../../Scripts/metro_ui_css/javascript/dialog.js"></script>
    <script src="../../../../Scripts/metro_ui_css/javascript/dropdown.js"></script>
    <script src="../../../../Scripts/metro_ui_css/javascript/input-control.js"></script>
    <script src="../../../../Scripts/metro_ui_css/javascript/pagecontrol.js"></script>
    <script src="../../../../Scripts/metro_ui_css/javascript/pagelist.js"></script>
    <script src="../../../../Scripts/metro_ui_css/javascript/rating.js"></script>
    <script src="../../../../Scripts/metro_ui_css/javascript/slider.js"></script>
    <script src="../../../temp/overthrow.js"></script>
    <style type="text/css">
        .pay-row.active > td {
            background-color: silver;
        }

        #txtPayRateNormalStart, #txtPayRateNormalEnd, #txtPayRatePPHStart, #txtPayRatePPHEnd {
            width: 80px;
        }

        .col1 {
            width: 120px;
        }
    </style>
    <script>

        var ARRAY_PAY_RATES = new Array("1.00000", "1.3333", "1.5000", "2.00000", "2.3333", "2.5000", "3.0000");

        $(document).ready(function () {
            fixFrameSize();
            parent.closeLoader();
            wireEvents();

        });

        function fixFrameSize() {
            var pageSize = parent.getPageSize();
            var totalFrames = $(".mainIframe").length;
            $(".mainIframe").css("width", "100%");
            $(".mainIframe").css("height", pageSize.height - 105);

            $('.page-region-content').css('height', pageSize.height - 105);
            $('.page-region-content').css('overflow', 'auto');
            // $('.col2 , .col3').css('width', (parseInt($('.page-region-content').css('width')) - 130) + "px");

        }

        var selectedRow = null;
        function wireEvents() {


            $(document).on('click', '.pay-pph', function (parameters) {
                $('.pay-row').removeClass('active');
                selectedRow = $(this).parent();
                selectedRow.addClass('active');
                showPPHTimeSetup();
            });

            $(document).on('click', '.pay-normal', function (parameters) {
                $('.pay-row').removeClass('active');
                selectedRow = $(this).parent();
                selectedRow.addClass('active');
                showNormalTimeSetup();
            });

        }


        function showNormalTimeSetup(parameters) {

            var htmlContent = '';
            var selectedTime = selectedRow.children(0).html();
            htmlContent += "Pay at Rate ";
            htmlContent += "<select id='dlgDpPayRateNormal'>";
            $.each(ARRAY_PAY_RATES, function (index, value) {
                htmlContent += "<option value='" + value + "'>" + value + "</option>";
            });


            htmlContent += "</select>";
            htmlContent += " from ";
            //htmlContent += '<label class="input-control switch">';
            htmlContent += '<input id="txtPayRateNormalStart" type="text" value="' + selectedTime + '">';

            //htmlContent += '</label>';
            htmlContent += " to ";
            // htmlContent += '<label class="input-control switch">';
            htmlContent += '<input id="txtPayRateNormalEnd" type="text">';

            // htmlContent += '</label>';

            $.Dialog({
                'title': 'Normal Working Day Pay Rate',
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
                            var end = $("#txtPayRateNormalEnd").val();
                            var start = $("#txtPayRateNormalStart").val();
                            var selectedRate = $("#dlgDpPayRateNormal").val();

                            var endRow = $("#tbPayRates > tr > td:contains('" + end + "')");
                            var startRow = $("#tbPayRates > tr > td:contains('" + start + "')");

                            if (startRow.length > 0) {

                                var parentEnd = endRow.parent();
                                var parentStart = startRow.parent();
                                var parentEndID = parseInt(parentEnd.attr("id").replace("pay-row", ""));
                                var parentStartID = parseInt(parentStart.attr("id").replace("pay-row", ""));

                                for (var r = parentStartID; r <= parentEndID; r++) {
                                    $("#pay-row" + r + " > td:eq(1)").html("Pay hours at Rate " + selectedRate);
                                }
                            }

                        }
                    },
                    'Cancel': {
                        'action': function () { }
                    }
                }
            });
        }


        function showPPHTimeSetup(parameters) {

            var htmlContent = '';
            var selectedTime = selectedRow.children(0).html();
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
        


        function toDate(dStr, format) {
            var now = new Date();
            if (format == "h:m") {
                now.setHours(dStr.substr(0, dStr.indexOf(":")));
                now.setMinutes(dStr.substr(dStr.indexOf(":") + 1));
                now.setSeconds(0);
                return now;
            } else
                return "Invalid Format";
        }

    </script>
</head>
<body class="metrouicss iframe-page bg-color-white">
    <form id="form1" runat="server">
        <div class="page">
            <div class="page-region">
                <div class="page-region-content">

                    <h2>Pay Rates</h2>
                    <hr />
                    <%--                    <table class="striped" style="position: absolute;">
                        <thead>
                            <tr>
                                <td class="col1" style="width: 174px"></td>
                                <td class="col2"  >Normal Working Day</td>
                                <td class="col3"  >Paid Public Holiday</td>
                            </tr>
                        </thead>

                    </table>--%>
                    <table class="striped" style="margin-top: 30px;">
                        <thead>
                            <tr>
                                <td class="col1" style="width: 120px"></td>
                                <td class="col2">Normal Working Day</td>
                                <td class="col3">Paid Public Holiday</td>
                            </tr>
                        </thead>
                        <tbody id="tbPayRates" runat="server"></tbody>
                    </table>

                </div>

            </div>
        </div>
    </form>
</body>
</html>
