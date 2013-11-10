using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace MobiTime.WebServices
{
    [WebService(Namespace = "http://www.mobitime.co.za/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Timesheets : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "MobiTime Web Service - Select a Client Detail List from MobiTime")]
        public List<ReturnData.ReturnTimesheetData> ListEmployeeTimesheet(
            string ApplicationPassword,
            int SiteID,
            int EmployeeID)
        {
            ReturnData.ReturnTimesheetData DSReturnEmployeeTimesheet = null;

            SqlDataReader TimesheetList = null;
            List<ReturnData.ReturnTimesheetData> ReturnedEmployeeTimesheet = new List<ReturnData.ReturnTimesheetData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedEmployeeTimesheet = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "ClockingIn_ClockingID, " + 
                                            "ClockingIn, " + 
                                            "ClockingOut_ClockingID, " + 
                                            "ClockingOut, " + 
                                            "'2013-01-01 00:00:00' As ShiftDate, " +
                                            "'2013-01-01 00:00:00' As ProcessClockingIn, " +
                                            "'2013-01-01 00:00:00' As ProcessClockingOut, " + 
                                            "CostCentre, " + 
                                            "Department, " + 
                                            "Supervisor, " + 
                                            "Position, " + 
                                            "ShiftPattern, " +
                                            "0.00 As Hours10, " +
                                            "0.00 As Hours13, " +
                                            "0.00 As Hours15, " +
                                            "0.00 As Hours20, " +
                                            "0.00 As Hours23, " +
                                            "0.00 As Hours25, " +
                                            "0.00 As Hours30, " +
                                            "0.00 As SA01, " +
                                            "0.00 As SA02, " +
                                            "0.00 As SA03, " + 
                                            "0.00 As HoursPPH, " + 
                                            "0.00 As Annual, " + 
                                            "0.00 As Sick, " +
                                            "0.00 As Coida, " +
                                            "0.00 As Family, " +
                                            "0.00 As Study, " +
                                            "0.00 As Awol " + 
                                        "From " +
                                            "Timesheets " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " + 
                                            "And EmployeeID = " + EmployeeID + " " +
                                            "And PayPeriodID is null " + 
                                        "Order By " +
                                            "ClockingIn";
                    TimesheetList = com.ExecuteReader();

                    while (TimesheetList.Read())
                    {
                        DSReturnEmployeeTimesheet = new ReturnData.ReturnTimesheetData();

                        DSReturnEmployeeTimesheet.ClockingIn_ClockingID = TimesheetList["ClockingIn_ClockingID"].ToString(); 
                        DSReturnEmployeeTimesheet.ClockingIn = TimesheetList["ClockingIn"].ToString(); 
                        DSReturnEmployeeTimesheet.ClockingOut_ClockingID = TimesheetList["ClockingOut_ClockingID"].ToString(); 
                        DSReturnEmployeeTimesheet.ClockingOut = TimesheetList["ClockingOut"].ToString(); 
                        DSReturnEmployeeTimesheet.ShiftDate = TimesheetList["ShiftDate"].ToString(); 
                        DSReturnEmployeeTimesheet.ProcessClockingIn = TimesheetList["ProcessClockingIn"].ToString(); 
                        DSReturnEmployeeTimesheet.ProcessClockingOut = TimesheetList["ProcessClockingOut"].ToString(); 
                        DSReturnEmployeeTimesheet.CostCentre = TimesheetList["CostCentre"].ToString(); 
                        DSReturnEmployeeTimesheet.Department = TimesheetList["Department"].ToString(); 
                        DSReturnEmployeeTimesheet.Supervisor = TimesheetList["Supervisor"].ToString(); 
                        DSReturnEmployeeTimesheet.Position = TimesheetList["Position"].ToString(); 
                        DSReturnEmployeeTimesheet.ShiftPattern = TimesheetList["ShiftPattern"].ToString(); 
                        DSReturnEmployeeTimesheet.Hours10 = TimesheetList["Hours10"].ToString(); 
                        DSReturnEmployeeTimesheet.Hours13 = TimesheetList["Hours13"].ToString(); 
                        DSReturnEmployeeTimesheet.Hours15 = TimesheetList["Hours15"].ToString(); 
                        DSReturnEmployeeTimesheet.Hours20 = TimesheetList["Hours20"].ToString(); 
                        DSReturnEmployeeTimesheet.Hours23 = TimesheetList["Hours23"].ToString(); 
                        DSReturnEmployeeTimesheet.Hours25 = TimesheetList["Hours25"].ToString(); 
                        DSReturnEmployeeTimesheet.Hours30 = TimesheetList["Hours30"].ToString(); 
                        DSReturnEmployeeTimesheet.SA01 = TimesheetList["SA01"].ToString(); 
                        DSReturnEmployeeTimesheet.SA02 = TimesheetList["SA02"].ToString(); 
                        DSReturnEmployeeTimesheet.SA03 = TimesheetList["SA03"].ToString(); 
                        DSReturnEmployeeTimesheet.HoursPPH = TimesheetList["HoursPPH"].ToString(); 
                        DSReturnEmployeeTimesheet.Annual = TimesheetList["Annual"].ToString(); 
                        DSReturnEmployeeTimesheet.Sick = TimesheetList["Sick"].ToString(); 
                        DSReturnEmployeeTimesheet.Coida = TimesheetList["Coida"].ToString(); 
                        DSReturnEmployeeTimesheet.Family = TimesheetList["Family"].ToString(); 
                        DSReturnEmployeeTimesheet.Study = TimesheetList["Study"].ToString(); 
                        DSReturnEmployeeTimesheet.Awol = TimesheetList["Awol"].ToString(); 

                        ReturnedEmployeeTimesheet.Add(DSReturnEmployeeTimesheet);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedEmployeeTimesheet = null;
            }
            catch (Exception Ex)
            {
                ReturnedEmployeeTimesheet = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedEmployeeTimesheet;
        }
    }
}
