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
    public class Supervisors : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Supervisor detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            int SiteID,
            string Supervisor,
            string PayrollCode,
            string AccountsCode,
            string UserGuid)
        {
            int SupervisorID = 0;
            bool InsertedSupervisor = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedSupervisor = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (SupervisorID) " +
                                        "From " +
                                            "Supervisors " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And Supervisor = '" + Supervisor + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                                "SupervisorID " +
                                            "From " +
                                                "Supervisors " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And Supervisor = '" + Supervisor + "'";
                        SupervisorID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Supervisors", "SupervisorID", SupervisorID) == true)
                        {
                            com.CommandText = "Update Supervisors " +
                                                "Set " +
                                                    "Supervisor = '" + Supervisor + "', " +
                                                    "PayrollCode = '" + PayrollCode + "', " +
                                                    "AccountsCode = '" + AccountsCode + "', " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "SiteID = " + SiteID + " " +
                                                "And Supervisor = '" + Supervisor + "'";
                            com.ExecuteNonQuery();

                            InsertedID = SupervisorID;
                            InsertedSupervisor = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedSupervisor = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into Supervisors (" +
                                                "SiteID, " +
                                                "Supervisor, " +
                                                "PayrollCode, " +
                                                "AccountsCode, " +
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.SupervisorID " +
                                            "Select " +
                                                "" + SiteID + ", " +
                                                "'" + Supervisor + "', " +
                                                "'" + PayrollCode + "', " +
                                                "'" + AccountsCode + "', " +
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedSupervisor = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedSupervisor = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedSupervisor = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedSupervisor;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of the Supervisor detail")]
        public List<ReturnData.ReturnSupervisorData> Select(
            string ApplicationPassword,
            int SiteID,
            int SupervisorID)
        {
            ReturnData.ReturnSupervisorData DSReturnSupervisors = null;

            SqlDataReader SupervisorList = null;
            List<ReturnData.ReturnSupervisorData> ReturnedSupervisors = new List<ReturnData.ReturnSupervisorData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedSupervisors = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "SupervisorID, " +
                                            "SiteID, " +
                                            "Supervisor, " +
                                            "PayrollCode, " +
                                            "AccountsCode " +
                                        "From " +
                                            "Supervisors " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And SupervisorID = " + SupervisorID + " " +
                                            "And DeletedAtUTC is null";
                    SupervisorList = com.ExecuteReader();

                    while (SupervisorList.Read())
                    {
                        DSReturnSupervisors = new ReturnData.ReturnSupervisorData();
                        DSReturnSupervisors.SupervisorID = SupervisorList["SupervisorID"].ToString();
                        DSReturnSupervisors.SiteID = SupervisorList["SiteID"].ToString();
                        DSReturnSupervisors.Supervisor = SupervisorList["Supervisor"].ToString();
                        DSReturnSupervisors.PayrollCode = SupervisorList["PayrollCode"].ToString();
                        DSReturnSupervisors.AccountsCode = SupervisorList["AccountsCode"].ToString();

                        ReturnedSupervisors.Add(DSReturnSupervisors);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedSupervisors = null;
            }
            catch (Exception Ex)
            {
                ReturnedSupervisors = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedSupervisors;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a Supervisor Detail List from MobiTime")]
        public List<ReturnData.ReturnSupervisorData> ListSupervisors(
            string ApplicationPassword,
            int SiteID)
        {
            ReturnData.ReturnSupervisorData DSReturnSupervisors = null;

            SqlDataReader SupervisorList = null;
            List<ReturnData.ReturnSupervisorData> ReturnedSupervisors = new List<ReturnData.ReturnSupervisorData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedSupervisors = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "SupervisorID, " +
                                            "Supervisor, " +
                                            "PayrollCode, " +
                                            "AccountsCode " +
                                        "From " +
                                            "Supervisors " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And DeletedAtUTC is null " +
                                        "Order By " +
                                            "Supervisor";
                    SupervisorList = com.ExecuteReader();

                    while (SupervisorList.Read())
                    {
                        DSReturnSupervisors = new ReturnData.ReturnSupervisorData();
                        DSReturnSupervisors.SupervisorID = SupervisorList["SupervisorID"].ToString();
                        //DSReturnSupervisors.SiteID = SupervisorList["SiteID"].ToString();
                        DSReturnSupervisors.Supervisor = SupervisorList["Supervisor"].ToString();
                        DSReturnSupervisors.PayrollCode = SupervisorList["PayrollCode"].ToString();
                        DSReturnSupervisors.AccountsCode = SupervisorList["AccountsCode"].ToString();

                        ReturnedSupervisors.Add(DSReturnSupervisors);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedSupervisors = null;
            }
            catch (Exception Ex)
            {
                ReturnedSupervisors = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedSupervisors;
        }




        [WebMethod(Description = "Mobitime Web Service - Update Supervisor detail in MobiTime")]
        public bool Update(
            string ApplicationPassword,
            int SiteID,
            int SupervisorID,
            string Supervisor,
            string PayrollCode,
            string AccountsCode,
            string UserGuid)
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Supervisors", "SupervisorID", SupervisorID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Supervisors " +
                                            "Set " +
                                                "Supervisor = '" + Supervisor + "', " +
                                                "PayrollCode = '" + PayrollCode + "', " +
                                                "AccountsCode = '" + AccountsCode + "', " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And SupervisorID = " + SupervisorID + "";
                        com.ExecuteNonQuery();
                        Successful = true;
                    }
                    else
                    {
                        Successful = false;
                    }
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




        [WebMethod(Description = "Mobitime Web Service - Delete a Supervisor from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int SiteID,
            int SupervisorID,
            string UserGuid)
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Supervisors", "SupervisorID", SupervisorID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Supervisors " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And SupervisorID = " + SupervisorID + "";
                        com.ExecuteNonQuery();
                        Successful = true;
                    }
                    else
                    {
                        Successful = false;
                    }
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




        [WebMethod(Description = "Mobitime Web Service - Recover a Supervisor in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int SiteID,
            int SupervisorID,
            string UserGuid)
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Supervisors", "SupervisorID", SupervisorID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Supervisors " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And SupervisorID = " + SupervisorID + "";
                        com.ExecuteNonQuery();
                        Successful = true;
                    }
                    else
                    {
                        Successful = false;
                    }
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

