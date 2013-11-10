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
    public class ProcessTimesheet : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Process Received Clockings into Timesheets")]
        public bool ProcessToTimesheet(
            string ApplicationPassword,
            int SiteID)
        {
            bool Successful = false;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    Successful = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Insert Into Timesheets (" + 
	                                        "CidIn, " + 
	                                        "SiteID, " +
                                            "EmployeeID," + 
	                                        "ClockingIn_ClockingID, " + 
	                                        "ClockingIn) " + 
                                        "Select " + 
                                            "* " + 
                                        "From " + 
	                                        "(Select ROW_NUMBER() " + 
		                                        "Over " + 
		                                        "(Order By " + 
			                                        "SiteID, " + 
			                                        "EmployeeID, " + 
			                                        "ClockingDate) As cid, " + 
	                                        "SiteID, " + 
	                                        "EmployeeID, " + 
	                                        "ClockingID, " + 
	                                        "ClockingDate " + 
                                        "From " + 
	                                        "Clockings " + 
                                            "Where Clockings.DeletedAtUTC is null) ic " + 
                                        "Where " + 
	                                        "cid%2 = 1 " + 
	                                        "And ic.SiteID = " + SiteID + " " + 
	                                        "And ic.EmployeeID is not null";
                    com.ExecuteNonQuery();

                    com.CommandText = "Update Timesheets " + 
	                                    "Set " + 
		                                    "CostCentre = cen.CostCentre, " + 
		                                    "Department = dep.Department, " + 
		                                    "Supervisor = sup.Supervisor, " + 
		                                    "Position = pos.Position, " + 
		                                    "ShiftPatternID = pat.ShiftPatternID, " + 
		                                    "ShiftPattern = pat.ShiftPattern " + 
                                        "From " + 
	                                        "Timesheets tim " + 
	                                        "Inner Join Clockings clo on tim.ClockingIn_ClockingID = clo.ClockingID " + 
	                                        "Inner Join CostCentres cen on clo.CostCentreID = cen.CostCentreID and clo.SiteID = cen.SiteID " + 
	                                        "Inner Join Departments dep on clo.DepartmentID = dep.DepartmentID and clo.SiteID = dep.SiteID " + 
	                                        "Inner Join Supervisors sup on clo.SupervisorID = sup.SupervisorID and clo.SiteID = sup.SiteID " + 
	                                        "Inner Join Positions pos on clo.PositionID = pos.PositionID and clo.SiteID = pos.SiteID " + 
	                                        "Inner Join ShiftPatterns pat on clo.ShiftPatternID = pat.ShiftPatternID and clo.SiteID = pat.SiteID " + 
                                        "Where " + 
                                            "tim.SiteID = " + SiteID + " " +
                                            "And tim.PayPeriodID is null";
                    com.ExecuteNonQuery();

                    com.CommandText = "Update Timesheets " +
                                        "Set " +
                                            "CidOut = ic.cid, " +
                                            "ClockingOut_ClockingID = ic.ClockingID, " +
                                            "ClockingOut = ic.ClockingDate " +
                                        "From " +
                                            "Timesheets tim " +
                                            "Inner Join (Select ROW_NUMBER() " +
                                                "Over " +
                                                "(Order By " +
                                                    "SiteID, " +
                                                    "EmployeeID, " +
                                                    "ClockingDate) " +
                                                "As cid, " +
                                            "SiteID, " +
                                            "EmployeeID, " +
                                            "ClockingID, " +
                                            "ClockingDate " +
                                        "From " +
                                            "Clockings) ic on tim.EmployeeID = ic.EmployeeID " +
                                        "Where " +
                                            "cid%2 = 0 " +
                                            "And ic.SiteID = 1 " +
                                            "And ic.ClockingDate >= tim.ClockingIn " +
                                            "And ic.ClockingDate <= ISNULL((Select Top 1 " +
                                                                        "ClockingIn " +
                                                                    "From " +
                                                                        "Timesheets " +
                                                                    "Where " +
                                                                        "EmployeeID = tim.EmployeeID " +
                                                                        "And ClockingIn > ic.ClockingDate),GETDATE())";
                    com.ExecuteNonQuery();

                    Successful = true;
                }
            }
            catch (SqlException Ex)
            {
                Successful = false;
            }
            catch (Exception Ex)
            {
                Successful = false;
            }
            finally
            {
                con.Close();
            }

            return Successful;
        }
    }
}
