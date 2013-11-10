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
    public class Terminals : System.Web.Services.WebService
    {

        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Terminal detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            string Terminal,
            string TerminalCatagory,
            string Manufacturer,
            string Model,
            string SerialNumber,
            int SiteID, 
            string UserGuid)
        {
            int TerminalID = 0;
            bool InsertedTerminal = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedTerminal = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (TerminalID) " +
                                        "From " +
                                            "Terminals " +
                                        "Where " +
                                            "Terminal = '" + Terminal + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                                "TerminalID " +
                                            "From " +
                                                "Terminals " +
                                            "Where " +
                                                "Terminal = '" + Terminal + "'";
                        TerminalID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Terminals", "TerminalID", TerminalID) == true)
                        {
                            com.CommandText = "Update Terminals " +
                                                "Set " +
                                                    "Terminal = '" + Terminal + "', " +
                                                    "TerminalCatagory = '" + TerminalCatagory + "', " +
                                                    "Manufacturer = '" + Manufacturer + "', " +
                                                    "Model = '" + Model + "', " +
                                                    "SerialNumber = '" + SerialNumber + "', " +
                                                    "SiteID = '" + SiteID + "', " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "SiteID = " + SiteID + " " +
                                                    "And TerminalID = '" + TerminalID + "'";
                            com.ExecuteNonQuery();

                            InsertedID = TerminalID;
                            InsertedTerminal = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedTerminal = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into Terminals (" +
                                                "Terminal, " + 
                                                "TerminalCatagory, " + 
                                                "Manufacturer, " + 
                                                "Model, " + 
                                                "SerialNumber, " + 
                                                "SiteID, " + 
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.TerminalID " +
                                            "Select " +
                                                "'" + Terminal + "', " + 
                                                "'" + TerminalCatagory + "', " + 
                                                "'" + Manufacturer + "', " + 
                                                "'" + Model + "', " + 
                                                "'" + SerialNumber + "', " + 
                                                "'" + SiteID + "', " + 
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedTerminal = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedTerminal = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedTerminal = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedTerminal;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of the Terminal detail")]
        public List<ReturnData.ReturnTerminalData> Select(
            string ApplicationPassword,
            int SiteID,
            int TerminalID)
        {
            ReturnData.ReturnTerminalData DSReturnTerminals = null;

            SqlDataReader TerminalList = null;
            List<ReturnData.ReturnTerminalData> ReturnedTerminals = new List<ReturnData.ReturnTerminalData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedTerminals = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "TerminalID, " + 
                                            "Terminal, " +
                                            "TerminalCatagory, " +
                                            "Manufacturer, " +
                                            "Model, " +
                                            "SerialNumber, " +
                                            "SiteID " + 
                                        "From " +
                                            "Terminals " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And TerminalID = " + TerminalID + " " +
                                            "And DeletedAtUTC is null";
                    TerminalList = com.ExecuteReader();

                    while (TerminalList.Read())
                    {
                        DSReturnTerminals = new ReturnData.ReturnTerminalData();

                        DSReturnTerminals.TerminalID = TerminalList["TerminalID"].ToString(); 
                        DSReturnTerminals.Terminal = TerminalList["Terminal"].ToString(); 
                        DSReturnTerminals.TerminalCatagory = TerminalList["TerminalCatagory"].ToString(); 
                        DSReturnTerminals.Manufacturer = TerminalList["Manufacturer"].ToString(); 
                        DSReturnTerminals.Model = TerminalList["Model"].ToString(); 
                        DSReturnTerminals.SerialNumber = TerminalList["SerialNumber"].ToString(); 
                        DSReturnTerminals.SiteID = TerminalList["SiteID"].ToString(); 

                        ReturnedTerminals.Add(DSReturnTerminals);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedTerminals = null;
            }
            catch (Exception Ex)
            {
                ReturnedTerminals = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedTerminals;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a Terminal Detail List from MobiTime")]
        public List<ReturnData.ReturnTerminalData> ListTerminals(
            string ApplicationPassword)
        {
            ReturnData.ReturnTerminalData DSReturnTerminals = null;

            SqlDataReader TerminalList = null;
            List<ReturnData.ReturnTerminalData> ReturnedTerminals = new List<ReturnData.ReturnTerminalData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedTerminals = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "TerminalID, " + 
                                            "Terminal, " +
                                            "TerminalCatagory, " +
                                            "Manufacturer, " +
                                            "Model, " +
                                            "SerialNumber, " +
                                            "SiteID " + 
                                        "From " +
                                            "Terminals " +
                                        "Where " +
                                            "DeletedAtUTC is null " +
                                        "Order By " +
                                            "Terminal";
                    TerminalList = com.ExecuteReader();

                    while (TerminalList.Read())
                    {
                        DSReturnTerminals = new ReturnData.ReturnTerminalData();

                        DSReturnTerminals.TerminalID = TerminalList["TerminalID"].ToString();
                        DSReturnTerminals.Terminal = TerminalList["Terminal"].ToString();
                        DSReturnTerminals.TerminalCatagory = TerminalList["TerminalCatagory"].ToString();
                        DSReturnTerminals.Manufacturer = TerminalList["Manufacturer"].ToString();
                        DSReturnTerminals.Model = TerminalList["Model"].ToString();
                        DSReturnTerminals.SerialNumber = TerminalList["SerialNumber"].ToString();
                        DSReturnTerminals.SiteID = TerminalList["SiteID"].ToString(); 

                        ReturnedTerminals.Add(DSReturnTerminals);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedTerminals = null;
            }
            catch (Exception Ex)
            {
                ReturnedTerminals = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedTerminals;
        }




        [WebMethod(Description = "Mobitime Web Service - Update Terminal detail in MobiTime")]
        public bool Update(
            string ApplicationPassword,
            int TerminalID,
            string Terminal,
            string TerminalCatagory,
            string Manufacturer,
            string Model,
            string SerialNumber,
            int SiteID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Terminals", "TerminalID", TerminalID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Terminals " +
                                            "Set " +
                                                "Terminal = '" + Terminal + "', " +
                                                    "TerminalCatagory = '" + TerminalCatagory + "', " +
                                                    "Manufacturer = '" + Manufacturer + "', " +
                                                    "Model = '" + Model + "', " +
                                                    "SerialNumber = '" + SerialNumber + "', " +
                                                    "SiteID = '" + SiteID + "', " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And TerminalID = " + TerminalID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Delete a Terminal from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int SiteID,
            int TerminalID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Terminals", "TerminalID", TerminalID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Terminals " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And TerminalID = " + TerminalID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Recover a Terminal in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int SiteID,
            int TerminalID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Terminals", "TerminalID", TerminalID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Terminals " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And TerminalID = " + TerminalID + "";
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
