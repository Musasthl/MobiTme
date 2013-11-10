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
    public class ResetDatabase : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod]
        public bool Reset(
            string ResetPassword)
        {
            bool successful = false;
            try
            {
                if (ResetPassword == "Clint is King 7701205161082 !@#$%^&*()")
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Delete From AuditTrails"; 
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (AuditTrails, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From Clients";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (Clients, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From Clockings";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (Clockings, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From CostCentres";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (CostCentres, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From Departments";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (Departments, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From Emails";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (Emails, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From Employees";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (Employees, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From PayAdjustments";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (PayAdjustments, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From PayBrackets";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (PayBrackets, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From PayPeriods";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (PayPeriods, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From PayRoundings";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (PayRoundings, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From PayRules";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (PayRules, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From PaySchedules";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (PaySchedules, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From PayShiftDateAdjustments";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (PayShiftDateAdjustments, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From Positions";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (Positions, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From ShiftPatternDays";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (ShiftPatternDays, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From ShiftPatterns";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (ShiftPatterns, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From Sites";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (Sites, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From Supervisors";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (Supervisors, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From Terminals";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (Terminals, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From TimesheetCalculations";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (TimesheetCalculations, Reseed, 0)";
                    com.ExecuteNonQuery();

                    com.CommandText = "Delete From Timesheets";
                    com.ExecuteNonQuery();
                    com.CommandText = "DBCC CheckIdent (Timesheets, Reseed, 0)";
                    com.ExecuteNonQuery();

                    successful = true;

                }
                else
                {
                    successful = false;
                }
            }
            catch (SqlException Ex)
            {
                successful = false;
            }
            catch (Exception Ex)
            {
                successful = false;
            }
            finally
            {
                con.Close();
            }
            return successful;
        }
    }
}
