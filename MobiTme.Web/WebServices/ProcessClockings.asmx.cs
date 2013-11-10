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
    public class ProcessClockings : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description="Process Received Clockings")]
        public void ProcessReceivedClockings(
            string ApplicationPassword)
        {
            try
            {
                com.Connection = con;
                con.Open();

                if (Functions.Authentication.Authenticate(ApplicationPassword) == true)
                {

                    com.CommandText = "Update Clockings " +
                                        "Set " +
                                            "SiteID = t.siteID, " +
                                            "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                            "UpdatedBy = 'System' " +
                                        "From " +
                                            "Clockings c " +
                                            "Inner Join Terminals t on c.Terminal = t.Terminal " +
                                        "Where " +
                                            "c.SiteID is Null " +
                                            "And c.DeletedAtUTC is Null";
                    com.ExecuteNonQuery();

                    com.CommandText = "Update Clockings " +
                                        "Set " +
                                            "EmployeeID = e.EmployeeID, " +
                                            "CostCentreID = e.CostCentreID, " +
                                            "DepartmentID = e.DepartmentID, " +
                                            "SupervisorID = e.SupervisorID, " +
                                            "PositionID = e.PositionID, " +
                                            "ShiftPatternID = e.ShiftPatternID, " +
                                            "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                            "UpdatedBy = 'System' " +
                                        "From " +
                                            "Clockings c " +
                                            "Inner Join Employees e on c.SiteID = e.SiteID " +
                                            "And c.ClockingNumber = e.ClockingNumber " +
                                            "And c.ClockingDate >= e.EngagementDate  " +
                                            "And c.ClockingDate <= isnull(e.TerminationDate, GETDATE()) " +
                                            "And c.EmployeeID is null " +
                                            "And e.DeletedAtUTC is null";
                    com.ExecuteNonQuery();
                }
            }
            catch (SqlException Ex)
            {
            }
            catch (Exception Ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
    }
}
