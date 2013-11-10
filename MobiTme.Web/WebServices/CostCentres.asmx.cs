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
    public class CostCentres : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description="Mobitime Web Service - Insert new Cost Centre detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            int SiteID,
            string CostCentre,
            string PayrollCode,
            string AccountsCode,
            string UserGuid)
        {
            int CostCentreID = 0;
            bool InsertedCostCentre = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedCostCentre = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (CostCentreID) " +
                                        "From " +
                                            "CostCentres " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And CostCentre = '" + CostCentre + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " + 
                                                "CostCentreID " + 
                                            "From " + 
                                                "CostCentres " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And CostCentre = '" + CostCentre + "'";
                        CostCentreID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "CostCentres", "CostCentreID", CostCentreID) == true)
                        {
                            com.CommandText = "Update CostCentres " + 
                                                "Set " + 
                                                    "CostCentre = '" + CostCentre + "', " + 
                                                    "PayrollCode = '" + PayrollCode + "', " + 
                                                    "AccountsCode = '" + AccountsCode + "', " + 
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " + 
                                                    "UpdatedBy = '" + UserGuid + "', " + 
                                                    "DeletedAtUTC = null, " + 
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "SiteID = " + SiteID + " " +
                                                "And CostCentre = '" + CostCentre + "'";
                            com.ExecuteNonQuery();

                            InsertedID = CostCentreID;
                            InsertedCostCentre = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedCostCentre = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into CostCentres (" +
                                                "SiteID, " +
                                                "CostCentre, " +
                                                "PayrollCode, " +
                                                "AccountsCode, " +
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.CostCentreID " +
                                            "Select " +
                                                "" + SiteID + ", " +
                                                "'" + CostCentre + "', " +
                                                "'" + PayrollCode + "', " +
                                                "'" + AccountsCode + "', " +
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedCostCentre = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedCostCentre = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedCostCentre = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedCostCentre;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of the Cost Centre detail")]
        public List<ReturnData.ReturnCostCentreData> Select(
            string ApplicationPassword,
            int SiteID,
            int CostCentreID)
        {
            ReturnData.ReturnCostCentreData DSReturnCostCentres = null;

            SqlDataReader CostCentreList = null;
            List<ReturnData.ReturnCostCentreData> ReturnedCostCentres = new List<ReturnData.ReturnCostCentreData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedCostCentres = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "CostCentreID, " +
                                            "SiteID, " + 
                                            "CostCentre, " +
                                            "PayrollCode, " +
                                            "AccountsCode " +
                                        "From " +
                                            "CostCentres " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And CostCentreID = " + CostCentreID + " " +
                                            "And DeletedAtUTC is null";
                    CostCentreList = com.ExecuteReader();

                    while (CostCentreList.Read())
                    {
                        DSReturnCostCentres = new ReturnData.ReturnCostCentreData();
                        DSReturnCostCentres.CostCentreID = CostCentreList["CostCentreID"].ToString();
                        DSReturnCostCentres.SiteID = CostCentreList["SiteID"].ToString();
                        DSReturnCostCentres.CostCentre = CostCentreList["CostCentre"].ToString();
                        DSReturnCostCentres.PayrollCode = CostCentreList["PayrollCode"].ToString();
                        DSReturnCostCentres.AccountsCode = CostCentreList["AccountsCode"].ToString();

                        ReturnedCostCentres.Add(DSReturnCostCentres);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedCostCentres = null;
            }
            catch (Exception Ex)
            {
                ReturnedCostCentres = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedCostCentres;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a Cost Centre Detail List from MobiTime")]
        public List<ReturnData.ReturnCostCentreData> ListCostCentres(
            string ApplicationPassword,
            int SiteID)
        {
            ReturnData.ReturnCostCentreData DSReturnCostCentres = null;

            SqlDataReader CostCentreList = null;
            List<ReturnData.ReturnCostCentreData> ReturnedCostCentres = new List<ReturnData.ReturnCostCentreData>();
            
            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedCostCentres = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "CostCentreID, " +
                                            "CostCentre, " +
                                            "PayrollCode, " +
                                            "AccountsCode " +
                                        "From " +
                                            "CostCentres " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And DeletedAtUTC is null " +
                                        "Order By " +
                                            "CostCentre";
                    CostCentreList = com.ExecuteReader();

                    while (CostCentreList.Read())
                    {
                        DSReturnCostCentres = new ReturnData.ReturnCostCentreData();
                        DSReturnCostCentres.CostCentreID = CostCentreList["CostCentreID"].ToString();
                        //DSReturnCostCentres.SiteID = CostCentreList["SiteID"].ToString();
                        DSReturnCostCentres.CostCentre = CostCentreList["CostCentre"].ToString();
                        DSReturnCostCentres.PayrollCode = CostCentreList["PayrollCode"].ToString();
                        DSReturnCostCentres.AccountsCode = CostCentreList["AccountsCode"].ToString();

                        ReturnedCostCentres.Add(DSReturnCostCentres);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedCostCentres = null;
            }
            catch (Exception Ex)
            {
                ReturnedCostCentres = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedCostCentres;
        }




        [WebMethod(Description = "Mobitime Web Service - Update Cost Centre detail in MobiTime")]
        public bool Update(
            string ApplicationPassword,
            int SiteID,
            int CostCentreID,
            string CostCentre,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "CostCentres", "CostCentreID", CostCentreID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update CostCentres " +
                                            "Set " + 
                                                "CostCentre = '" + CostCentre + "', " +
                                                "PayrollCode = '" + PayrollCode + "', " +
                                                "AccountsCode = '" + AccountsCode + "', " + 
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "' " + 
                                            "Where " + 
                                                "SiteID = " + SiteID + " " +
                                                "And CostCentreID = " + CostCentreID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Delete a Cost Centre from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int SiteID,
            int CostCentreID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "CostCentres", "CostCentreID", CostCentreID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update CostCentres " +
                                            "Set " + 
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " + 
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " + 
                                                "SiteID = " + SiteID + " " +
                                                "And CostCentreID = " + CostCentreID + "";
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



        
        [WebMethod(Description = "Mobitime Web Service - Recover a Cost Centre in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int SiteID,
            int CostCentreID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "CostCentres", "CostCentreID", CostCentreID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update CostCentres " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " + 
                                                "DeletedBy = null " +
                                            "Where " + 
                                                "SiteID = " + SiteID + " " +
                                                "And CostCentreID = " + CostCentreID + "";
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
