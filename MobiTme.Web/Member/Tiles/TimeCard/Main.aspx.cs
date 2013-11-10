using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobiTime.WebServices;

namespace MobiTme.Web.Member.Tiles.TimeCard
{
    public partial class Main : System.Web.UI.Page
    {
        // Grouping 1 = cost center. 2 = department, 

        protected void Page_Load(object sender, EventArgs e)
        {

        }



        [System.Web.Services.WebMethod]
        public static string UpdateShiftCycle(string siteID, string empID, string lineID, string keyID, string val)
        {
            int ClientID = 1;
            // Type = lineID
            bool bResult = false;
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            var wsService = new MobiTime.WebServices.Clockings();
            bResult = wsService.UpdateGrouping(ApplicationPassword, ClientID, int.Parse(siteID), UserGuid, int.Parse(lineID), 5,
                                      int.Parse(val), int.Parse(keyID));
            //var listCostCentres = new MobiTime.WebServices.OvertimeAdjustments().Update()

            return bResult.ToString();
        }
        [System.Web.Services.WebMethod]
        public static string UpdateTimeSuperVisor(string siteID, string empID, string lineID, string keyID, string val)
        {
            int ClientID = 1;
            // Type = lineID
            bool bResult = false;
           

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            var wsService = new MobiTime.WebServices.Clockings();
            bResult = wsService.UpdateGrouping(ApplicationPassword, ClientID, int.Parse(siteID), UserGuid, int.Parse(lineID), 3,
                                      int.Parse(val), int.Parse(keyID));

            return bResult.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string UpdateTimeDepartment(string siteID, string empID, string lineID, string keyID, string val)
        {
            int ClientID = 1;
            bool bResult = false;

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            var wsService = new MobiTime.WebServices.Clockings();
            bResult = wsService.UpdateGrouping(ApplicationPassword, ClientID, int.Parse(siteID), UserGuid, int.Parse(lineID), 2,
                                      int.Parse(val), int.Parse(keyID));
            return bResult.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string UpdateTimeCardCostCentre(string siteID, string empID, string lineID, string keyID, string val)
        {
            int ClientID = 1;
            bool bResult = false;

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            var wsService = new MobiTime.WebServices.Clockings();
            bResult = wsService.UpdateGrouping(ApplicationPassword, ClientID, int.Parse(siteID), UserGuid, int.Parse(lineID), 1,
                                      int.Parse(val), int.Parse(keyID));

            return bResult.ToString();
        }


        [System.Web.Services.WebMethod]
        public static string UpdateTimeCardPosition(string siteID, string empID, string lineID, string keyID, string val)
        {
            int ClientID = 1;
            bool bResult = false;
           
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            var wsService = new MobiTime.WebServices.Clockings();
            bResult = wsService.UpdateGrouping(ApplicationPassword, ClientID, int.Parse(siteID), UserGuid, int.Parse(lineID), 4,
                                      int.Parse(val), int.Parse(keyID));
            return bResult.ToString();
        }
        [System.Web.Services.WebMethod]
        public static string EmployeeList(string siteID)
        {
            StringBuilder sbInnerHtml = new StringBuilder();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();
            using (var wsService = new Employees())
            {
                foreach (var serviceData in wsService.ListEmployees(ApplicationPassword, int.Parse(siteID)))
                {
                    //sbInnerHtml.Append("<li class='employee-li' onclick='SelectTimeCard(" + serviceData.EmployeeID + ")'>");
                    //sbInnerHtml.Append("<div class='empolyee-num'>");
                    //sbInnerHtml.Append(serviceData.EmployeeNumber);
                    //sbInnerHtml.Append("</div>");
                    //sbInnerHtml.Append("<div class='empolyee-clocking'>");
                    //sbInnerHtml.Append(serviceData.ClockingNumber);
                    //sbInnerHtml.Append("</div>");
                    //sbInnerHtml.Append("<div class='empolyee-surname'>");
                    //sbInnerHtml.Append(serviceData.Surname);
                    //sbInnerHtml.Append("</div>");
                    //sbInnerHtml.Append("<div class='empolyee-names'>");
                    //sbInnerHtml.Append(serviceData.FirstNames);
                    //sbInnerHtml.Append("</div>");
                    //sbInnerHtml.Append("</li>");





                    sbInnerHtml.Append("<li class='emp-selector' onclick='SelectTimeCard(" + serviceData.EmployeeID + ")'>");
                    sbInnerHtml.Append("<a onclick='javascript:;'>");
                    sbInnerHtml.Append("<i class=\"fa fa-user fa-2x\"></i>");
                    sbInnerHtml.Append("<span class=\"title\">");
                    sbInnerHtml.Append(serviceData.EmployeeNumber + " - " + serviceData.ClockingNumber);
                    sbInnerHtml.Append("<br/>");
                    sbInnerHtml.Append(serviceData.FirstNames);
                    sbInnerHtml.Append(" ");
                    sbInnerHtml.Append(serviceData.Surname);
                    sbInnerHtml.Append("</span>");
                    sbInnerHtml.Append("<span class=\"selected\"></span>");
                    sbInnerHtml.Append("</a>");

                    sbInnerHtml.Append("</li>");

                }
            }
            return sbInnerHtml.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string SelectTimeCard(string SiteID, string EmployeeID)
        {
            int siteID = int.Parse(SiteID);
            StringBuilder sbInnerHtml = new StringBuilder();
            StringBuilder sbInnerHtmlShiftCycle = new StringBuilder();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            var listDepartments = new Departments().ListDepartments(ApplicationPassword, siteID);
            var listPositions = new Positions().ListPositions(ApplicationPassword, siteID);
            var listCostCentres = new CostCentres().ListCostCentres(ApplicationPassword, siteID);
            var listSupervisors = new Supervisors().ListSupervisors(ApplicationPassword, siteID);
            var listShiftCycle = new MobiTime.WebServices.ShiftPatterns().List(ApplicationPassword, siteID, UserGuid);
            MobiTime.WebServices.Timesheets wsTimesheets = new Timesheets();
            List<MobiTime.ReturnData.ReturnTimesheetData> TimeCardDataList =
                wsTimesheets.ListEmployeeTimesheet(ApplicationPassword, siteID, int.Parse(EmployeeID));


            Dictionary<string, string> shiftCycle = new Dictionary<string, string>()
            {
	            {"1", "This Clocking Record"},
	            {"2", "This Shift Date"},
	            {"3", "From this line"} 
	        };

            sbInnerHtmlShiftCycle.Append("<select class='TypeDropdown'>");
            foreach (var VARIABLE in shiftCycle)
            {

                sbInnerHtmlShiftCycle.Append("<option value='" + VARIABLE.Key + "'>");
                sbInnerHtmlShiftCycle.Append(VARIABLE.Value);
                sbInnerHtmlShiftCycle.Append("</option>");

            }
            sbInnerHtmlShiftCycle.Append("</select>");
            //string DropdownCostCenters = MobiTme.Web.App_Code.


            // List<TimeCardData> TimeCardDataList = new List<TimeCardData>();
            // TimeCardDataList.Add(new TimeCardData()
            //{
            //    ClockingIn = "2013-11-03 15:45:25.000",
            //    ClockingOut = "2013-11-03 15:45:25.000",
            //    ClockingID_In = "1",
            //    ClockingID_Out = "2",
            //    ProcessedClocking_In = "2013-11-03 15:45:25.000",
            //    ProcessedClocking_Out = "2013-11-03 15:45:25.000",
            //    CostCente = "CostCente",
            //    Department = "Department",
            //    Supervisor = "Supervisor"

            //});

            // TimeCardDataList.Add(new TimeCardData()
            // {
            //     ClockingIn = "2013-11-03 15:45:25.000",
            //     ClockingOut = "2013-11-03 15:45:25.000",
            //     ClockingID_In = "2",
            //     ClockingID_Out = "2",
            //     ProcessedClocking_In = "2013-11-03 15:45:25.000",
            //     ProcessedClocking_Out = "2013-11-03 15:45:25.000",
            //     CostCente = "CostCente",
            //     Department = "Department",
            //     Supervisor = "Supervisor"

            // });


            // TimeCardDataList.Add(new TimeCardData()
            // {
            //     ClockingIn = "2013-11-03 15:45:25.000",
            //     ClockingOut = "2013-11-03 15:45:25.000",
            //     ClockingID_In = "3",
            //     ClockingID_Out = "2",
            //     ProcessedClocking_In = "2013-11-03 15:45:25.000",
            //     ProcessedClocking_Out = "2013-11-03 15:45:25.000",
            //     CostCente = "CostCente",
            //     Department = "Department",
            //     Supervisor = "Supervisor"

            // });


            // TimeCardDataList.Add(new TimeCardData()
            // {
            //     ClockingIn = "2013-11-03 15:45:25.000",
            //     ClockingOut = "2013-11-03 15:45:25.000",
            //     ClockingID_In = "4",
            //     ClockingID_Out = "2",
            //     ProcessedClocking_In = "2013-11-03 15:45:25.000",
            //     ProcessedClocking_Out = "2013-11-03 15:45:25.000",
            //     CostCente = "CostCente",
            //     Department = "Department",
            //     Supervisor = "Supervisor"

            // });



            if (TimeCardDataList != null)
                foreach (var timeCardData in TimeCardDataList)
                {

                    sbInnerHtml.Append("<tr class='row' key='" + timeCardData.ClockingIn_ClockingID + "'>");
                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.ShiftDate);
                    sbInnerHtml.Append("</td>");
                    sbInnerHtml.Append("<td onclick=\"ShowInClocking('" + timeCardData.ClockingIn_ClockingID + "' , '" + timeCardData.ClockingIn + "')\">");
                    sbInnerHtml.Append(timeCardData.ProcessClockingIn);
                    sbInnerHtml.Append("</td>");

                    sbInnerHtml.Append("<td onclick=\"ShowOutClocking('" + timeCardData.ClockingOut_ClockingID + "' , '" + timeCardData.ClockingOut + "')\">");
                    sbInnerHtml.Append(timeCardData.ProcessClockingOut);
                    sbInnerHtml.Append("</td>");
                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(sbInnerHtmlShiftCycle.ToString());
                    sbInnerHtml.Append("</td>");
                    // Shift Cycle 
                    sbInnerHtml.Append("<td>");

                    sbInnerHtml.Append("<select class='shiftCycleDropdown'>");
                    foreach (var VARIABLE in listShiftCycle)
                    {
                        if (VARIABLE.ShiftPattern == timeCardData.ShiftPattern)
                        {
                            sbInnerHtml.Append("<option value='" + VARIABLE.ShiftPatternID + "' selected>");
                        }
                        else
                        {
                            sbInnerHtml.Append("<option value='" + VARIABLE.ShiftPatternID + "'>");
                        }
                        sbInnerHtml.Append(VARIABLE.ShiftPattern);
                        sbInnerHtml.Append("</option>");

                    }
                    sbInnerHtml.Append("</select>");

                    sbInnerHtml.Append("</td>");

                    // Cost center 
                    sbInnerHtml.Append("<td>");

                    sbInnerHtml.Append("<select class='costCentreDropdown'>");
                    foreach (var VARIABLE in listCostCentres)
                    {
                        if (VARIABLE.CostCentre == timeCardData.CostCentre)
                        {
                            sbInnerHtml.Append("<option value='" + VARIABLE.CostCentreID + "' selected>");
                        }
                        else
                        {
                            sbInnerHtml.Append("<option value='" + VARIABLE.CostCentreID + "'>");
                        }
                        sbInnerHtml.Append(VARIABLE.CostCentre);
                        sbInnerHtml.Append("</option>");

                    }
                    sbInnerHtml.Append("</select>");

                    sbInnerHtml.Append("</td>");
                    // department
                    sbInnerHtml.Append("<td>");

                    sbInnerHtml.Append("<select class='departmentDropdown'>");
                    foreach (var VARIABLE in listDepartments)
                    {
                        if (VARIABLE.Department == timeCardData.Department)
                        {
                            sbInnerHtml.Append("<option value='" + VARIABLE.DepartmentID + "' selected>");
                        }
                        else
                        {
                            sbInnerHtml.Append("<option value='" + VARIABLE.DepartmentID + "'>");
                        }
                        sbInnerHtml.Append(VARIABLE.Department);
                        sbInnerHtml.Append("</option>");

                    }
                    sbInnerHtml.Append("</select>");

                    sbInnerHtml.Append("</td>");
                    // Supervisor
                    sbInnerHtml.Append("<td>");

                    sbInnerHtml.Append("<select class='supervisorDropdown'>");
                    foreach (var VARIABLE in listSupervisors)
                    {
                        if (VARIABLE.Supervisor == timeCardData.Supervisor)
                        {
                            sbInnerHtml.Append("<option value='" + VARIABLE.SupervisorID + "' selected>");
                        }
                        else
                        {
                            sbInnerHtml.Append("<option value='" + VARIABLE.SupervisorID + "'>");
                        }
                        sbInnerHtml.Append(VARIABLE.Supervisor);
                        sbInnerHtml.Append("</option>");

                    }
                    sbInnerHtml.Append("</select>");

                    sbInnerHtml.Append("</td>");


                    // Supervisor
                    sbInnerHtml.Append("<td>");

                    sbInnerHtml.Append("<select class='positionDropdown'>");
                    foreach (var VARIABLE in listPositions)
                    {
                        if (VARIABLE.Position == timeCardData.Position)
                        {
                            sbInnerHtml.Append("<option value='" + VARIABLE.PositionID + "' selected>");
                        }
                        else
                        {
                            sbInnerHtml.Append("<option value='" + VARIABLE.PositionID + "'>");
                        }
                        sbInnerHtml.Append(VARIABLE.Position);
                        sbInnerHtml.Append("</option>");

                    }
                    sbInnerHtml.Append("</select>");

                    sbInnerHtml.Append("</td>");



                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.ShiftPattern);
                    sbInnerHtml.Append("</td>");


                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.Hours10);
                    sbInnerHtml.Append("</td>");


                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.Hours13);
                    sbInnerHtml.Append("</td>");


                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.Hours15);
                    sbInnerHtml.Append("</td>");


                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.Hours20);
                    sbInnerHtml.Append("</td>");


                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.Hours23);
                    sbInnerHtml.Append("</td>");


                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.Hours25);
                    sbInnerHtml.Append("</td>");


                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.Hours30);
                    sbInnerHtml.Append("</td>");

                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.SA01);
                    sbInnerHtml.Append("</td>");


                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.SA02);
                    sbInnerHtml.Append("</td>");






                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.SA03);
                    sbInnerHtml.Append("</td>");



                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.HoursPPH);
                    sbInnerHtml.Append("</td>");



                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.Annual);
                    sbInnerHtml.Append("</td>");




                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.Sick);
                    sbInnerHtml.Append("</td>");




                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.Coida);
                    sbInnerHtml.Append("</td>");



                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.Family);
                    sbInnerHtml.Append("</td>");



                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.Study);
                    sbInnerHtml.Append("</td>");


                    sbInnerHtml.Append("<td>");
                    sbInnerHtml.Append(timeCardData.Awol);
                    sbInnerHtml.Append("</td>");





                    sbInnerHtml.Append("</tr>");
                }


            return sbInnerHtml.ToString();
        }


    }
}