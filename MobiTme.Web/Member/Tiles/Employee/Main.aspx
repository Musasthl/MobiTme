<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="MobiTme.Web.Member.Tiles.Employee.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Masterfile</title>
    <link href="../../../Scripts/jquery-ui.css" rel="stylesheet" />
    <link href="../../../Scripts/jquery.timepicker/jquery-ui-timepicker-addon.css" rel="stylesheet" />
    <link href="../../Styles/forms.css" rel="stylesheet" />
    <script src="../../../Scripts/jquery-2.0.3.min.js"></script>
    <script src="../../../Scripts/utilities.js"></script>
    <script src="../../../Scripts/jquery-ui-1.10.3.js"></script>
    <script src="../../../Scripts/jquery.timepicker/jquery-ui-sliderAccess.js"></script>
    <script src="../../../Scripts/jquery.timepicker/jquery-ui-timepicker-addon.js"></script>
    <link href="../../../Scripts/fontawesome/css/font-awesome.min.css" rel="stylesheet" />
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


            $('#tbEngagementDate').datepicker();

            $('#tbTerminationDate').datepicker();


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
                url: window.location.pathname + "/ListEmployees",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: "{ 'siteID': '" + SITE_ID + "'}",
                success: function (response) {
                    var data = response.d;

                    if (data != null) {
                        $("#tblDataEmployeeList").html(data);
                    }
                },
                error: function (error) {
                    // report error back to user
                    alert(error.responseText);
                }
            });
        }


        function editData() {
            $.ajax({
                type: "Post",
                url: window.location.pathname + "/ListEmployees",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: "{ 'siteID': '" + SITE_ID + "'}",
                success: function (response) {
                    var data = response.d;

                    if (data != null) {
                        $("#tblDataEmployeeList").html(data);
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
                        $("#tbCostCentre").val(row.jsonData);


                        /// row.SiteID  SITE_ID;
                        $("#tbSurname").val(row.Surname);
                        $("#tbFirstNames").val(row.FirstNames);
                        $("#tbTitle").val(row.Title);
                        $("#tbCountry_OfBirth").val(row.Country_OfBirth);
                        $("#tbIdentityNumber").val(row.IdentityNumber);
                        $("#tbIdentityNumberType").val(row.IdentityNumberType);
                        $("#tbTelephone").val(row.Telephone);
                        $("#tbFacsimile").val(row.Facsimile);
                        $("#tbCellular").val(row.Cellular);
                        $("#tbEmail").val(row.Email);
                        $("#tbPhysical01").val(row.Physical01);
                        $("#tbPhysical02").val(row.Physical02);
                        $("#tbPhysical03").val(row.Physical03);
                        $("#tbPhysical04").val(row.Physical04);
                        $("#tbCountry_Physical").val(row.Country_Physical);
                        $("#tbPhysicalCode").val(row.PhysicalCode);
                        $("#tbPostal01").val(row.Postal01);
                        $("#tbPostal02").val(row.Postal02);
                        $("#tbPostal03").val(row.Postal03);
                        $("#tbPostal04").val(row.Postal04);
                        $("#tbCountry_Postal").val(row.Country_Postal);
                        $("#tbPostalCode").val(row.PostalCode);
                        $("#tbResidential01").val(row.Residential01);
                        $("#tbResidential02").val(row.Residential02);
                        $("#tbResidential03").val(row.Residential03);
                        $("#tbResidential04").val(row.Residential04);
                        $("#tbCountry_Residential").val(row.Country_Residential);
                        $("#tbResidentialCode").val(row.ResidentialCode);
                        $("#tbNOKSurname").val(row.NOKSurname);
                        $("#tbNOKFirstNames").val(row.NOKFirstNames);
                        $("#tbNOKTelephone").val(row.NOKTelephone);
                        $("#tbNOKCellular").val(row.NOKCellular);
                        $("#tbNOKPhysical01").val(row.NOKPhysical01);
                        $("#tbNOKPhysical02").val(row.NOKPhysical02);
                        $("#tbNOKPhysical03").val(row.NOKPhysical03);
                        $("#tbNOKPhysical04").val(row.NOKPhysical04);
                        $("#tbCountry_NOKPhysical").val(row.Country_NOKPhysical);
                        $("#tbNOKPhysicalCode").val(row.NOKPhysicalCode);
                        $("#tbEmployeeNumber").val(row.EmployeeNumber);
                        $("#tbClockingNumber").val(row.ClockingNumber);
                        $("#tbEngagementDate").val(row.EngagementDate);
                        $("#tbCostCentre").val(row.CostCentre);
                        $("#tbDepartment").val(row.Department);
                        $("#tbSupervisor").val(row.Supervisor);
                        $("#tbPosition").val(row.Position);
                        $("#tbShiftPattern").val(row.ShiftPattern);
                        $("#tbTerminationDate").val(row.TerminationDate);

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
            var employee = new Object();
            employee.employeeID = $('#HiddenFieldKey').val();
            employee.SiteID = SITE_ID;
            employee.Surname = $("#tbSurname").val();
            employee.FirstNames = $("#tbFirstNames").val();
            employee.Title = $("#tbTitle").val();
            employee.Country_OfBirth = $("#tbCountry_OfBirth").val();
            employee.IdentityNumber = $("#tbIdentityNumber").val();
            employee.IdentityNumberType = $("#tbIdentityNumberType").val();
            employee.Telephone = $("#tbTelephone").val();
            employee.Facsimile = $("#tbFacsimile").val();
            employee.Cellular = $("#tbCellular").val();
            employee.Email = $("#tbEmail").val();
            employee.Physical01 = $("#tbPhysical01").val();
            employee.Physical02 = $("#tbPhysical02").val();
            employee.Physical03 = $("#tbPhysical03").val();
            employee.Physical04 = $("#tbPhysical04").val();
            employee.Country_Physical = $("#tbCountry_Physical").val();
            employee.PhysicalCode = $("#tbPhysicalCode").val();
            employee.Postal01 = $("#tbPostal01").val();
            employee.Postal02 = $("#tbPostal02").val();
            employee.Postal03 = $("#tbPostal03").val();
            employee.Postal04 = $("#tbPostal04").val();
            employee.Country_Postal = $("#tbCountry_Postal").val();
            employee.PostalCode = $("#tbPostalCode").val();
            employee.Residential01 = $("#tbResidential01").val();
            employee.Residential02 = $("#tbResidential02").val();
            employee.Residential03 = $("#tbResidential03").val();
            employee.Residential04 = $("#tbResidential04").val();
            employee.Country_Residential = $("#tbCountry_Residential").val();
            employee.ResidentialCode = $("#tbResidentialCode").val();
            employee.NOKSurname = $("#tbNOKSurname").val();
            employee.NOKFirstNames = $("#tbNOKFirstNames").val();
            employee.NOKTelephone = $("#tbNOKTelephone").val();
            employee.NOKCellular = $("#tbNOKCellular").val();
            employee.NOKPhysical01 = $("#tbNOKPhysical01").val();
            employee.NOKPhysical02 = $("#tbNOKPhysical02").val();
            employee.NOKPhysical03 = $("#tbNOKPhysical03").val();
            employee.NOKPhysical04 = $("#tbNOKPhysical04").val();
            employee.Country_NOKPhysical = $("#tbCountry_NOKPhysical").val();
            employee.NOKPhysicalCode = $("#tbNOKPhysicalCode").val();
            employee.EmployeeNumber = $("#tbEmployeeNumber").val();
            employee.ClockingNumber = $("#tbClockingNumber").val();
            employee.EngagementDate = $("#tbEngagementDate").val();
            employee.CostCentreID = $("#tbCostCentre").val();
            employee.DepartmentID = $("#tbDepartment").val();
            employee.SupervisorID = $("#tbSupervisor").val();
            employee.PositionID = $("#tbPosition").val();
            employee.ShiftPatternID = $("#tbShiftPattern").val();
            employee.TerminationDate = $("#tbTerminationDate").val();

            $.ajax({
                type: "Post",
                url: window.location.pathname + "/Insert",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: JSON.stringify({ employee: employee }),
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
            var employee = new Object();
            employee.SiteID = SITE_ID;
            employee.EmployeeID = $("#HiddenFieldKey").val();
            employee.Surname = $("#tbSurname").val();
            employee.FirstNames = $("#tbFirstNames").val();
            employee.Title = $("#tbTitle").val();
            employee.Country_OfBirth = $("#tbCountry_OfBirth").val();
            employee.IdentityNumber = $("#tbIdentityNumber").val();
            employee.IdentityNumberType = $("#tbIdentityNumberType").val();
            employee.Telephone = $("#tbTelephone").val();
            employee.Facsimile = $("#tbFacsimile").val();
            employee.Cellular = $("#tbCellular").val();
            employee.Email = $("#tbEmail").val();
            employee.Physical01 = $("#tbPhysical01").val();
            employee.Physical02 = $("#tbPhysical02").val();
            employee.Physical03 = $("#tbPhysical03").val();
            employee.Physical04 = $("#tbPhysical04").val();
            employee.Country_Physical = $("#tbCountry_Physical").val();
            employee.PhysicalCode = $("#tbPhysicalCode").val();
            employee.Postal01 = $("#tbPostal01").val();
            employee.Postal02 = $("#tbPostal02").val();
            employee.Postal03 = $("#tbPostal03").val();
            employee.Postal04 = $("#tbPostal04").val();
            employee.Country_Postal = $("#tbCountry_Postal").val();
            employee.PostalCode = $("#tbPostalCode").val();
            employee.Residential01 = $("#tbResidential01").val();
            employee.Residential02 = $("#tbResidential02").val();
            employee.Residential03 = $("#tbResidential03").val();
            employee.Residential04 = $("#tbResidential04").val();
            employee.Country_Residential = $("#tbCountry_Residential").val();
            employee.ResidentialCode = $("#tbResidentialCode").val();
            employee.NOKSurname = $("#tbNOKSurname").val();
            employee.NOKFirstNames = $("#tbNOKFirstNames").val();
            employee.NOKTelephone = $("#tbNOKTelephone").val();
            employee.NOKCellular = $("#tbNOKCellular").val();
            employee.NOKPhysical01 = $("#tbNOKPhysical01").val();
            employee.NOKPhysical02 = $("#tbNOKPhysical02").val();
            employee.NOKPhysical03 = $("#tbNOKPhysical03").val();
            employee.NOKPhysical04 = $("#tbNOKPhysical04").val();
            employee.Country_NOKPhysical = $("#tbCountry_NOKPhysical").val();
            employee.NOKPhysicalCode = $("#tbNOKPhysicalCode").val();
            employee.EmployeeNumber = $("#tbEmployeeNumber").val();
            employee.ClockingNumber = $("#tbClockingNumber").val();
            employee.EngagementDate = $("#tbEngagementDate").val();
            employee.CostCentreID = $("#tbCostCentre").val();
            employee.DepartmentID = $("#tbDepartment").val();
            employee.SupervisorID = $("#tbSupervisor").val();
            employee.PositionID = $("#tbPosition").val();
            employee.ShiftPatternID = $("#tbShiftPattern").val();
            employee.TerminationDate = $("#tbTerminationDate").val();

            $.ajax({
                type: "Post",
                url: window.location.pathname + "/Update",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // can be used for plaintext or for JSON
                data: JSON.stringify({ employee: employee }),
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
            <div class="pageTitle">Employee</div>
            <div id="detailContent">
                <div id="divDetail1" class="divDetail">
                    <div>
                        <h1>Employee List</h1>
                    </div>

                    <table class="striped" style="margin-top: 30px;">
                        <thead>
                            <tr>

                                <td class="hidden">EmployeeID</td>
                                <td>Surname</td>
                                <td>First Names</td>
                                <td>Title</td>
                                <td>Country Of Birth</td>
                                <td>Identity Number </td>
                                <td>Identity Number Type</td>
                                <td>Telephone</td>
                                <td>Facsimile</td>
                                <td>Cellular</td>
                                <td>Email</td>
                                <td>Engagement Date</td>
                                <td>Termination Date</td>


                            </tr>
                        </thead>
                        <tbody id="tblDataEmployeeList" runat="server"></tbody>
                    </table>

                </div>
                <div id="divDetail2" class="divDetail">
                    <div>
                        <h1>Personal Detail</h1>
                    </div>
                    <div style="padding-left: 10px;">
                        <div style="width: 120px; float: left; padding-top: 5px">Surname</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" runat="server" id="tbSurname" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">First Names</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbFirstNames" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Title</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbTitle" style="width: 50px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Country of Birth</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">

                            <select id="tbCountry_OfBirth" style="width: 300px">
                                <option value="4">Afghanistan</option>
                                <option value="248">Åland Islands</option>
                                <option value="8">Albania</option>
                                <option value="12">Algeria</option>
                                <option value="16">American Samoa</option>
                                <option value="20">Andorra</option>
                                <option value="24">Angola</option>
                                <option value="660">Anguilla</option>
                                <option value="10">Antarctica</option>
                                <option value="28">Antigua and Barbuda</option>
                                <option value="32">Argentina</option>
                                <option value="51">Armenia</option>
                                <option value="533">Aruba</option>
                                <option value="36">Australia</option>
                                <option value="40">Austria</option>
                                <option value="31">Azerbaijan</option>
                                <option value="44">Bahamas</option>
                                <option value="48">Bahrain</option>
                                <option value="50">Bangladesh</option>
                                <option value="52">Barbados</option>
                                <option value="112">Belarus</option>
                                <option value="56">Belgium</option>
                                <option value="84">Belize</option>
                                <option value="204">Benin</option>
                                <option value="60">Bermuda</option>
                                <option value="64">Bhutan</option>
                                <option value="68">Bolivia, Plurinational State of</option>
                                <option value="535">Bonaire, Sint Eustatius and Saba</option>
                                <option value="70">Bosnia and Herzegovina</option>
                                <option value="72">Botswana</option>
                                <option value="74">Bouvet Island</option>
                                <option value="76">Brazil</option>
                                <option value="86">British Indian Ocean Territory</option>
                                <option value="96">Brunei Darussalam</option>
                                <option value="100">Bulgaria</option>
                                <option value="854">Burkina Faso</option>
                                <option value="108">Burundi</option>
                                <option value="116">Cambodia</option>
                                <option value="120">Cameroon</option>
                                <option value="124">Canada</option>
                                <option value="132">Cape Verde</option>
                                <option value="136">Cayman Islands</option>
                                <option value="140">Central African Republic</option>
                                <option value="148">Chad</option>
                                <option value="152">Chile</option>
                                <option value="156">China</option>
                                <option value="162">Christmas Island</option>
                                <option value="166">Cocos (Keeling) Islands</option>
                                <option value="170">Colombia</option>
                                <option value="174">Comoros</option>
                                <option value="178">Congo</option>
                                <option value="180">Congo, the Democratic Republic of the</option>
                                <option value="184">Cook Islands</option>
                                <option value="188">Costa Rica</option>
                                <option value="384">Côte d'Ivoire</option>
                                <option value="191">Croatia</option>
                                <option value="192">Cuba</option>
                                <option value="531">Curaçao</option>
                                <option value="196">Cyprus</option>
                                <option value="203">Czech Republic</option>
                                <option value="208">Denmark</option>
                                <option value="262">Djibouti</option>
                                <option value="212">Dominica</option>
                                <option value="214">Dominican Republic</option>
                                <option value="218">Ecuador</option>
                                <option value="818">Egypt</option>
                                <option value="222">El Salvador</option>
                                <option value="226">Equatorial Guinea</option>
                                <option value="232">Eritrea</option>
                                <option value="233">Estonia</option>
                                <option value="231">Ethiopia</option>
                                <option value="238">Falkland Islands (Malvinas)</option>
                                <option value="234">Faroe Islands</option>
                                <option value="242">Fiji</option>
                                <option value="246">Finland</option>
                                <option value="250">France</option>
                                <option value="254">French Guiana</option>
                                <option value="258">French Polynesia</option>
                                <option value="260">French Southern Territories</option>
                                <option value="266">Gabon</option>
                                <option value="270">Gambia</option>
                                <option value="268">Georgia</option>
                                <option value="276">Germany</option>
                                <option value="288">Ghana</option>
                                <option value="292">Gibraltar</option>
                                <option value="300">Greece</option>
                                <option value="304">Greenland</option>
                                <option value="308">Grenada</option>
                                <option value="312">Guadeloupe</option>
                                <option value="316">Guam</option>
                                <option value="320">Guatemala</option>
                                <option value="831">Guernsey</option>
                                <option value="324">Guinea</option>
                                <option value="624">Guinea-Bissau</option>
                                <option value="328">Guyana</option>
                                <option value="332">Haiti</option>
                                <option value="334">Heard Island and McDonald Islands</option>
                                <option value="336">Holy See (Vatican City State)</option>
                                <option value="340">Honduras</option>
                                <option value="344">Hong Kong</option>
                                <option value="348">Hungary</option>
                                <option value="352">Iceland</option>
                                <option value="356">India</option>
                                <option value="360">Indonesia</option>
                                <option value="364">Iran, Islamic Republic of</option>
                                <option value="368">Iraq</option>
                                <option value="372">Ireland</option>
                                <option value="833">Isle of Man</option>
                                <option value="376">Israel</option>
                                <option value="380">Italy</option>
                                <option value="388">Jamaica</option>
                                <option value="392">Japan</option>
                                <option value="832">Jersey</option>
                                <option value="400">Jordan</option>
                                <option value="398">Kazakhstan</option>
                                <option value="404">Kenya</option>
                                <option value="296">Kiribati</option>
                                <option value="408">Korea, Democratic People's Republic of</option>
                                <option value="410">Korea, Republic of</option>
                                <option value="414">Kuwait</option>
                                <option value="417">Kyrgyzstan</option>
                                <option value="418">Lao People's Democratic Republic</option>
                                <option value="428">Latvia</option>
                                <option value="422">Lebanon</option>
                                <option value="426">Lesotho</option>
                                <option value="430">Liberia</option>
                                <option value="434">Libya</option>
                                <option value="438">Liechtenstein</option>
                                <option value="440">Lithuania</option>
                                <option value="442">Luxembourg</option>
                                <option value="446">Macao</option>
                                <option value="807">Macedonia, the former Yugoslav Republic of</option>
                                <option value="450">Madagascar</option>
                                <option value="454">Malawi</option>
                                <option value="458">Malaysia</option>
                                <option value="462">Maldives</option>
                                <option value="466">Mali</option>
                                <option value="470">Malta</option>
                                <option value="584">Marshall Islands</option>
                                <option value="474">Martinique</option>
                                <option value="478">Mauritania</option>
                                <option value="480">Mauritius</option>
                                <option value="175">Mayotte</option>
                                <option value="484">Mexico</option>
                                <option value="583">Micronesia, Federated States of</option>
                                <option value="498">Moldova, Republic of</option>
                                <option value="492">Monaco</option>
                                <option value="496">Mongolia</option>
                                <option value="499">Montenegro</option>
                                <option value="500">Montserrat</option>
                                <option value="504">Morocco</option>
                                <option value="508">Mozambique</option>
                                <option value="104">Myanmar</option>
                                <option value="516">Namibia</option>
                                <option value="520">Nauru</option>
                                <option value="524">Nepal</option>
                                <option value="528">Netherlands</option>
                                <option value="540">New Caledonia</option>
                                <option value="554">New Zealand</option>
                                <option value="558">Nicaragua</option>
                                <option value="562">Niger</option>
                                <option value="566">Nigeria</option>
                                <option value="570">Niue</option>
                                <option value="574">Norfolk Island</option>
                                <option value="580">Northern Mariana Islands</option>
                                <option value="578">Norway</option>
                                <option value="512">Oman</option>
                                <option value="586">Pakistan</option>
                                <option value="585">Palau</option>
                                <option value="275">Palestinian Territory, Occupied</option>
                                <option value="591">Panama</option>
                                <option value="598">Papua New Guinea</option>
                                <option value="600">Paraguay</option>
                                <option value="604">Peru</option>
                                <option value="608">Philippines</option>
                                <option value="612">Pitcairn</option>
                                <option value="616">Poland</option>
                                <option value="620">Portugal</option>
                                <option value="630">Puerto Rico</option>
                                <option value="634">Qatar</option>
                                <option value="638">Réunion</option>
                                <option value="642">Romania</option>
                                <option value="643">Russian Federation</option>
                                <option value="646">Rwanda</option>
                                <option value="652">Saint Barthélemy</option>
                                <option value="654">Saint Helena, Ascension and Tristan da Cunha</option>
                                <option value="659">Saint Kitts and Nevis</option>
                                <option value="662">Saint Lucia</option>
                                <option value="663">Saint Martin (French part)</option>
                                <option value="666">Saint Pierre and Miquelon</option>
                                <option value="670">Saint Vincent and the Grenadines</option>
                                <option value="882">Samoa</option>
                                <option value="674">San Marino</option>
                                <option value="678">Sao Tome and Principe</option>
                                <option value="682">Saudi Arabia</option>
                                <option value="686">Senegal</option>
                                <option value="688">Serbia</option>
                                <option value="690">Seychelles</option>
                                <option value="694">Sierra Leone</option>
                                <option value="702">Singapore</option>
                                <option value="534">Sint Maarten (Dutch part)</option>
                                <option value="703">Slovakia</option>
                                <option value="705">Slovenia</option>
                                <option value="90">Solomon Islands</option>
                                <option value="706">Somalia</option>
                                <option value="710">South Africa</option>
                                <option value="239">South Georgia and the South Sandwich Islands</option>
                                <option value="728">South Sudan</option>
                                <option value="724">Spain</option>
                                <option value="144">Sri Lanka</option>
                                <option value="729">Sudan</option>
                                <option value="740">Suriname</option>
                                <option value="744">Svalbard and Jan Mayen</option>
                                <option value="748">Swaziland</option>
                                <option value="752">Sweden</option>
                                <option value="756">Switzerland</option>
                                <option value="760">Syrian Arab Republic</option>
                                <option value="158">Taiwan, Province of China</option>
                                <option value="762">Tajikistan</option>
                                <option value="834">Tanzania, United Republic of</option>
                                <option value="764">Thailand</option>
                                <option value="626">Timor-Leste</option>
                                <option value="768">Togo</option>
                                <option value="772">Tokelau</option>
                                <option value="776">Tonga</option>
                                <option value="780">Trinidad and Tobago</option>
                                <option value="788">Tunisia</option>
                                <option value="792">Turkey</option>
                                <option value="795">Turkmenistan</option>
                                <option value="796">Turks and Caicos Islands</option>
                                <option value="798">Tuvalu</option>
                                <option value="800">Uganda</option>
                                <option value="804">Ukraine</option>
                                <option value="784">United Arab Emirates</option>
                                <option value="826">United Kingdom</option>
                                <option value="840">United States</option>
                                <option value="581">United States Minor Outlying Islands</option>
                                <option value="858">Uruguay</option>
                                <option value="860">Uzbekistan</option>
                                <option value="548">Vanuatu</option>
                                <option value="862">Venezuela, Bolivarian Republic of</option>
                                <option value="704">Viet Nam</option>
                                <option value="92">Virgin Islands, British</option>
                                <option value="850">Virgin Islands, U.S.</option>
                                <option value="876">Wallis and Futuna</option>
                                <option value="732">Western Sahara</option>
                                <option value="887">Yemen</option>
                                <option value="894">Zambia</option>
                                <option value="716">Zimbabwe</option>
                            </select>
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Identity Number</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbIdentityNumber" style="width: 150px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Identity Document</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbIdentityNumberType" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>
                    </div>
                </div>
                <div id="divDetail3" class="divDetail">
                    <div>
                        <h1>Contract Detail</h1>
                    </div>
                    <div style="padding-left: 10px;">
                        <div style="width: 120px; float: left; padding-top: 5px">Telephone</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbTelephone" style="width: 150px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Facsimile</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbFacsimile" style="width: 150px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Cellular</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbCellular" style="width: 150px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Email</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbEmail" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>
                    </div>
                </div>
                <div id="divDetail4" class="divDetail">
                    <div>
                        <h1>Address Detail</h1>
                    </div>
                    <div style="padding-left: 10px;">
                        <div style="width: 120px; float: left; padding-top: 5px">Physical Address</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbPhysical01" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px"></div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbPhysical02" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px"></div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbPhysical03" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>


                        <div style="width: 120px; float: left; padding-top: 5px"></div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbPhysical04" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Country</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbCountry_Physical" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Code</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbPhysicalCode" style="width: 50px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Postal Address</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbPostal01" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px"></div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbPostal02" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px"></div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbPostal03" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>


                        <div style="width: 120px; float: left; padding-top: 5px"></div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbPostal04" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Country</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbCountry_Postal" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Code</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbPostalCode" style="width: 50px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Residential Address</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbResidential01" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px"></div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbResidential02" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px"></div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbResidential03" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>


                        <div style="width: 120px; float: left; padding-top: 5px"></div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbResidential04" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Country</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbCountry_Residential" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Code</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbResidentialCode" style="width: 50px" type="text" />
                        </div>
                        <br>
                        <br>
                    </div>
                </div>
                <div id="divDetail5" class="divDetail">
                    <div>
                        <h1>Next of Kin Detail</h1>
                    </div>
                    <div style="padding-left: 10px;">
                        <div style="width: 120px; float: left; padding-top: 5px">Surname</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbNOKSurname" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">First Names</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbNOKFirstNames" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Telephone</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbNOKTelephone" style="width: 150px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Cellular</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbNOKCellular" style="width: 150px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Physical Address</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbNOKPhysical01" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px"></div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbNOKPhysical02" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px"></div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbNOKPhysical03" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px"></div>
                        <div style="width: 20px; float: left; padding-top: 5px"></div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbNOKPhysical04" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Country</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbCountry_NOKPhysical02" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Code</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbNOKPhysicalCode" style="width: 50px" type="text" />
                        </div>
                        <br>
                        <br>
                    </div>
                </div>
                <div id="divDetail6" class="divDetail">
                    <div>
                        <h1>Employment Detail</h1>
                    </div>
                    <div style="padding-left: 10px;">
                        <div style="width: 120px; float: left; padding-top: 5px">Employee Number</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbEmployeeNumber" style="width: 100px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Clocking Number</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbClockingNumber" style="width: 300px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Engagement Date</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbEngagementDate" style="width: 150px" type="text" />
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Cost Centre</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <select runat="server" id="tbCostCentre" style="width: 300px"></select>
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Department</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <select runat="server" id="tbDepartment" style="width: 300px">
                            </select>
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Supervisor</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <select runat="server" id="tbSupervisor" style="width: 300px">
                            </select>
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Position</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <select runat="server" id="tbPosition" style="width: 300px">
                            </select>
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Shift Pattern</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <select runat="server" id="tbShiftPattern" style="width: 300px">
                            </select>
                        </div>
                        <br>
                        <br>

                        <div style="width: 120px; float: left; padding-top: 5px">Termination Date</div>
                        <div style="width: 20px; float: left; padding-top: 5px">:</div>
                        <div style="width: 300px; float: left;">
                            <input runat="server" id="tbTerminationDate" style="width: 150px" type="text" />
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
                        <span class="title">Personal Detail</span>
                        <span class="arrow "></span>
                    </a>

                </li>


                <li class="liDetail" onclick="showDivDetail(3)">

                    <a href="javascript:;">
                        <i class="fa fa-edit fa-2x"></i>
                        <span class="title">Contact Detail</span>
                        <span class="arrow "></span>
                    </a>

                </li>


                <li class="liDetail" onclick="showDivDetail(4)">

                    <a href="javascript:;">
                        <i class="fa fa-edit fa-2x"></i>
                        <span class="title">Address Detail</span>
                        <span class="arrow "></span>
                    </a>

                </li>

                <li class="liDetail" onclick="showDivDetail(5)">

                    <a href="javascript:;">
                        <i class="fa fa-edit fa-2x"></i>
                        <span class="title">Next of Kin Detail</span>
                        <span class="arrow "></span>
                    </a>

                </li>


                <li class="liDetail" onclick="showDivDetail(6)">

                    <a href="javascript:;">
                        <i class="fa fa-edit fa-2x"></i>
                        <span class="title">Employment Detail</span>
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
