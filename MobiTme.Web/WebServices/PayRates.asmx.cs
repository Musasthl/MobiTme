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
    public class PayRates : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Pay Rate detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword, 
            int ClientID, 
            int SiteID, 
            string UserGuid, 
            int PayRuleID, 
            string PayType, 
            DateTime PayRateFrom, 
            DateTime PayRateTo)
        {
            int PayRateID = 0;
            bool InsertedPayRate = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedPayRate = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (PayRateID) " +
                                        "From " +
                                            "PayRates " +
                                        "Where " +
                                            "PayRuleID = " + PayRuleID + " " +
                                            "And PayRateFrom = '" + PayRateFrom + "' " +
                                            "And PayRateTo = '" + PayRateTo + "' " +
                                            "And PayType = '" + PayType + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                                "PayRateID " +
                                            "From " +
                                                "PayRates " +
                                            "Where " +
                                                "PayRuleID = " + PayRuleID + " " +
                                                "And PayRateFrom = '" + PayRateFrom + "' " +
                                                "And PayRateTo = '" + PayRateTo + "' " +
                                                "And PayType = '" + PayType + "'";
                        PayRateID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayRates", "PayRateID", PayRateID) == true)
                        {
                            com.CommandText = "Update PayRates " +
                                                "Set " +
                                                    "PayRuleID = '" + PayRuleID + "', " +
                                                    "PayType = '" + PayType + "', " +
                                                    "PayRateFrom = '" + PayRateFrom + "', " +
                                                    "PayRateTo = '" + PayRateTo + "', " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "PayRuleID = " + PayRuleID + " " +
                                                    "And PayRateID = '" + PayRateID + "'";
                            com.ExecuteNonQuery();

                            InsertedID = PayRateID;
                            InsertedPayRate = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedPayRate = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into PayRates (" +
                                                "PayRuleID, " + 
                                                "PayType, " + 
                                                "PayRateFrom, " + 
                                                "PayRateTo, " + 
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.PayRateID " +
                                            "Select " +
                                                "'" + PayRuleID + "', " + 
                                                "'" + PayType + "', " + 
                                                "'" + PayRateFrom + "', " + 
                                                "'" + PayRateTo + "', " + 
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedPayRate = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedPayRate = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedPayRate = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedPayRate;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a stored Pay Rate record from MobiTime")]
        public List<ReturnData.ReturnPayRateData> Select(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int PayRuleID,
            int PayRateID)
        {
            ReturnData.ReturnPayRateData DSReturnPayRates = null;

            SqlDataReader PayRateList = null;
            List<ReturnData.ReturnPayRateData> ReturnedPayRates = new List<ReturnData.ReturnPayRateData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedPayRates = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "PayRateID, " + 
                                            "PayRuleID, " + 
                                            "PayType, " + 
                                            "PayRateFrom, " + 
                                            "PayRateTo " + 
                                        "From " +
                                            "PayRates " +
                                        "Where " +
                                            "PayRuleID = " + PayRuleID + " " +
                                            "And PayRateID = " + PayRateID + " " +
                                            "And DeletedAtUTC is null";
                    PayRateList = com.ExecuteReader();

                    while (PayRateList.Read())
                    {
                        DSReturnPayRates = new ReturnData.ReturnPayRateData();

                        DSReturnPayRates.PayRateID = PayRateList["PayRateID"].ToString();
                        DSReturnPayRates.PayRuleID = PayRateList["PayRuleID"].ToString();
                        DSReturnPayRates.PayType = PayRateList["PayType"].ToString(); 
                        DSReturnPayRates.PayRateFrom = PayRateList["PayRateFrom"].ToString(); 
                        DSReturnPayRates.PayRateTo = PayRateList["PayRateTo"].ToString(); 

                        ReturnedPayRates.Add(DSReturnPayRates);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedPayRates = null;
            }
            catch (Exception Ex)
            {
                ReturnedPayRates = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedPayRates;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of Pay Rate records from MobiTime")]
        public List<ReturnData.ReturnPayRateData> List(
            string ApplicationPassword,
            //int ClientID,
            int SiteID,
            string UserGuid,
            int PayRuleID)
        {
            ReturnData.ReturnPayRateData DSReturnPayRates = null;

            SqlDataReader PayRateList = null;
            List<ReturnData.ReturnPayRateData> ReturnedPayRates = new List<ReturnData.ReturnPayRateData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedPayRates = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "PayRateID, " +
                                            "PayRuleID, " +
                                            "PayType, " +
                                            "PayRateFrom, " +
                                            "PayRateTo " +
                                        "From " +
                                            "PayRates " +
                                        "Where " +
                                            "PayRuleID = " + PayRuleID + " " +
                                            "And DeletedAtUTC is null " + 
                                        "Order By " +
                                            "PayType, " +
                                            "PayRateFrom, " +
                                            "PayRateTo";
                    PayRateList = com.ExecuteReader();

                    while (PayRateList.Read())
                    {
                        DSReturnPayRates = new ReturnData.ReturnPayRateData();

                        DSReturnPayRates.PayRateID = PayRateList["PayRateID"].ToString();
                        DSReturnPayRates.PayRuleID = PayRateList["PayRuleID"].ToString();
                        DSReturnPayRates.PayType = PayRateList["PayType"].ToString();
                        DSReturnPayRates.PayRateFrom = PayRateList["PayRateFrom"].ToString();
                        DSReturnPayRates.PayRateTo = PayRateList["PayRateTo"].ToString(); 

                        ReturnedPayRates.Add(DSReturnPayRates);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedPayRates = null;
            }
            catch (Exception Ex)
            {
                ReturnedPayRates = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedPayRates;
        }




        [WebMethod(Description = "Mobitime Web Service - Update a Pay Rate record in MobiTime")]
        public bool Update(
            string ApplicationPassword, 
            int ClientID, 
            int SiteID, 
            string UserGuid, 
            int PayRateID, 
            int PayRuleID, 
            string PayType, 
            DateTime PayRateFrom, 
            DateTime PayRateTo)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayRates", "PayRateID", PayRateID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update PayRates " +
                                            "Set " +
                                                "PayRuleID = '" + PayRuleID + "', " +
                                                "PayType = '" + PayType + "', " +
                                                "PayRateFrom = '" + PayRateFrom + "', " +
                                                "PayRateTo = '" + PayRateTo + "', " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "PayRuleID = " + PayRuleID + " " +
                                                "And PayRateID = " + PayRateID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Delete a Pay Rate record from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int PayRuleID,
            int PayRateID)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayRates", "PayRateID", PayRateID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update PayRates " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "PayRuleID = " + PayRuleID + " " +
                                                "And PayRateID = " + PayRateID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Recover a Pay Rate record in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int PayRuleID,
            int PayRateID)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayRates", "PayRateID", PayRateID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update PayRates " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "PayRuleID = " + PayRuleID + " " +
                                                "And PayRateID = " + PayRateID + "";
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
