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
    public class Departments : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Department detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            int SiteID,
            string Department,
            string PayrollCode,
            string AccountsCode,
            string UserGuid)
        {
            int DepartmentID = 0;
            bool InsertedDepartment = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedDepartment = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (DepartmentID) " +
                                        "From " +
                                            "Departments " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And Department = '" + Department + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                                "DepartmentID " +
                                            "From " +
                                                "Departments " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And Department = '" + Department + "'";
                        DepartmentID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Departments", "DepartmentID", DepartmentID) == true)
                        {
                            com.CommandText = "Update Departments " +
                                                "Set " +
                                                    "Department = '" + Department + "', " +
                                                    "PayrollCode = '" + PayrollCode + "', " +
                                                    "AccountsCode = '" + AccountsCode + "', " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "SiteID = " + SiteID + " " +
                                                    "And DepartmentID = '" + DepartmentID + "'";
                            com.ExecuteNonQuery();

                            InsertedID = DepartmentID;
                            InsertedDepartment = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedDepartment = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into Departments (" +
                                                "SiteID, " +
                                                "Department, " +
                                                "PayrollCode, " +
                                                "AccountsCode, " +
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.DepartmentID " +
                                            "Select " +
                                                "" + SiteID + ", " +
                                                "'" + Department + "', " +
                                                "'" + PayrollCode + "', " +
                                                "'" + AccountsCode + "', " +
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedDepartment = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedDepartment = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedDepartment = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedDepartment;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of the Department detail")]
        public List<ReturnData.ReturnDepartmentData> Select(
            string ApplicationPassword,
            int SiteID,
            int DepartmentID)
        {
            ReturnData.ReturnDepartmentData DSReturnDepartments = null;

            SqlDataReader DepartmentList = null;
            List<ReturnData.ReturnDepartmentData> ReturnedDepartments = new List<ReturnData.ReturnDepartmentData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedDepartments = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "DepartmentID, " +
                                            "SiteID, " +
                                            "Department, " +
                                            "PayrollCode, " +
                                            "AccountsCode " +
                                        "From " +
                                            "Departments " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And DepartmentID = " + DepartmentID + " " +
                                            "And DeletedAtUTC is null";
                    DepartmentList = com.ExecuteReader();

                    while (DepartmentList.Read())
                    {
                        DSReturnDepartments = new ReturnData.ReturnDepartmentData();
                        DSReturnDepartments.DepartmentID = DepartmentList["DepartmentID"].ToString();
                        DSReturnDepartments.SiteID = DepartmentList["SiteID"].ToString();
                        DSReturnDepartments.Department = DepartmentList["Department"].ToString();
                        DSReturnDepartments.PayrollCode = DepartmentList["PayrollCode"].ToString();
                        DSReturnDepartments.AccountsCode = DepartmentList["AccountsCode"].ToString();

                        ReturnedDepartments.Add(DSReturnDepartments);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedDepartments = null;
            }
            catch (Exception Ex)
            {
                ReturnedDepartments = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedDepartments;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a Department Detail List from MobiTime")]
        public List<ReturnData.ReturnDepartmentData> ListDepartments(
            string ApplicationPassword,
            int SiteID)
        {
            ReturnData.ReturnDepartmentData DSReturnDepartments = null;

            SqlDataReader DepartmentList = null;
            List<ReturnData.ReturnDepartmentData> ReturnedDepartments = new List<ReturnData.ReturnDepartmentData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedDepartments = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "DepartmentID, " +
                                            "Department, " +
                                            "PayrollCode, " +
                                            "AccountsCode " +
                                        "From " +
                                            "Departments " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And DeletedAtUTC is null " +
                                        "Order By " +
                                            "Department";
                    DepartmentList = com.ExecuteReader();

                    while (DepartmentList.Read())
                    {
                        DSReturnDepartments = new ReturnData.ReturnDepartmentData();
                        DSReturnDepartments.DepartmentID = DepartmentList["DepartmentID"].ToString();
                        //DSReturnDepartments.SiteID = DepartmentList["SiteID"].ToString();
                        DSReturnDepartments.Department = DepartmentList["Department"].ToString();
                        DSReturnDepartments.PayrollCode = DepartmentList["PayrollCode"].ToString();
                        DSReturnDepartments.AccountsCode = DepartmentList["AccountsCode"].ToString();

                        ReturnedDepartments.Add(DSReturnDepartments);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedDepartments = null;
            }
            catch (Exception Ex)
            {
                ReturnedDepartments = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedDepartments;
        }




        [WebMethod(Description = "Mobitime Web Service - Update Department detail in MobiTime")]
        public bool Update(
            string ApplicationPassword,
            int SiteID,
            int DepartmentID,
            string Department,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Departments", "DepartmentID", DepartmentID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Departments " +
                                            "Set " +
                                                "Department = '" + Department + "', " +
                                                "PayrollCode = '" + PayrollCode + "', " +
                                                "AccountsCode = '" + AccountsCode + "', " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And DepartmentID = " + DepartmentID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Delete a Department from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int SiteID,
            int DepartmentID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Departments", "DepartmentID", DepartmentID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Departments " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And DepartmentID = " + DepartmentID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Recover a Department in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int SiteID,
            int DepartmentID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Departments", "DepartmentID", DepartmentID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Departments " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And DepartmentID = " + DepartmentID + "";
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
