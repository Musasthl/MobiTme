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
    public class PayBrackets : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Pay Bracket detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword, 
            int ClientID, 
            int SiteID, 
            string UserGuid, 
            int PayRuleID, 
            string ClockingDirection,
            DateTime BracketFrom, 
            DateTime BracketTo, 
            DateTime PayClocking)
        {
            int PayBracketID = 0;
            bool InsertedPayBracket = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedPayBracket = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (PayBracketID) " +
                                        "From " +
                                            "PayBrackets " +
                                        "Where " +
                                            "PayRuleID = " + PayRuleID + " " +
                                            "And BracketFrom = '" + BracketFrom + "' " + 
                                            "And BracketTo = '" + BracketTo + "' " + 
                                            "And ClockingDirection = '" + ClockingDirection + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                                "PayBracketID " +
                                            "From " +
                                                "PayBrackets " +
                                            "Where " +
                                                "PayRuleID = " + PayRuleID + " " +
                                                "And BracketFrom = '" + BracketFrom + "' " +
                                                "And BracketTo = '" + BracketTo + "' " +
                                                "And ClockingDirection = '" + ClockingDirection + "'";
                        PayBracketID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayBrackets", "PayBracketID", PayBracketID) == true)
                        {
                            com.CommandText = "Update PayBrackets " +
                                                "Set " +
                                                    "ClockingDirection = '" + ClockingDirection + "', " +
                                                    "BracketFrom = '" + BracketFrom + "', " +
                                                    "BracketTo = '" + BracketTo + "', " +
                                                    "PayClocking = '" + PayClocking + "', " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "PayRuleID = " + PayRuleID + " " +
                                                    "And PayBracketID = '" + PayBracketID + "'";
                            com.ExecuteNonQuery();

                            InsertedID = PayBracketID;
                            InsertedPayBracket = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedPayBracket = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into PayBrackets (" +
                                                "PayRuleID, " + 
                                                "ClockingDirection, " + 
                                                "BracketFrom, " + 
                                                "BracketTo, " + 
                                                "PayClocking, " + 
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.PayBracketID " +
                                            "Select " +
                                                "'" + PayRuleID + "', " + 
                                                "'" + ClockingDirection + "', " + 
                                                "'" + BracketFrom + "', " + 
                                                "'" + BracketTo + "', " + 
                                                "'" + PayClocking + "', " + 
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedPayBracket = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedPayBracket = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedPayBracket = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedPayBracket;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a stored Pay Bracket record from MobiTime")]
        public List<ReturnData.ReturnPayBracketData> Select(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int PayRuleID,
            int PayBracketID)
        {
            ReturnData.ReturnPayBracketData DSReturnPayBrackets = null;

            SqlDataReader PayBracketList = null;
            List<ReturnData.ReturnPayBracketData> ReturnedPayBrackets = new List<ReturnData.ReturnPayBracketData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedPayBrackets = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "PayBracketID, " + 
                                            "PayRuleID, " + 
                                            "ClockingDirection, " + 
                                            "BracketFrom, " + 
                                            "BracketTo, " + 
                                            "PayClocking " + 
                                        "From " +
                                            "PayBrackets " +
                                        "Where " +
                                            "PayRuleID = " + PayRuleID + " " +
                                            "And PayBracketID = " + PayBracketID + " " +
                                            "And DeletedAtUTC is null";
                    PayBracketList = com.ExecuteReader();

                    while (PayBracketList.Read())
                    {
                        DSReturnPayBrackets = new ReturnData.ReturnPayBracketData();

                        DSReturnPayBrackets.PayBracketID = PayBracketList["PayBracketID"].ToString(); 
                        DSReturnPayBrackets.PayRuleID = PayBracketList["PayRuleID"].ToString();
                        DSReturnPayBrackets.ClockingDirection = PayBracketList["ClockingDirection"].ToString();
                        DSReturnPayBrackets.BracketFrom = PayBracketList["BracketFrom"].ToString(); 
                        DSReturnPayBrackets.BracketTo = PayBracketList["BracketTo"].ToString(); 
                        DSReturnPayBrackets.PayClocking = PayBracketList["PayClocking"].ToString(); 

                        ReturnedPayBrackets.Add(DSReturnPayBrackets);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedPayBrackets = null;
            }
            catch (Exception Ex)
            {
                ReturnedPayBrackets = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedPayBrackets;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of Pay Bracket records from MobiTime")]
        public List<ReturnData.ReturnPayBracketData> List(
            string ApplicationPassword,
            //int ClientID,
            int SiteID,
            string UserGuid,
            int PayRuleID)
        {
            ReturnData.ReturnPayBracketData DSReturnPayBrackets = null;

            SqlDataReader PayBracketList = null;
            List<ReturnData.ReturnPayBracketData> ReturnedPayBrackets = new List<ReturnData.ReturnPayBracketData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedPayBrackets = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "PayBracketID, " +
                                            "PayRuleID, " +
                                            "ClockingDirection, " +
                                            "BracketFrom, " +
                                            "BracketTo, " +
                                            "PayClocking " +
                                        "From " +
                                            "PayBrackets " +
                                        "Where " +
                                            "PayRuleID = " + PayRuleID + " " +
                                            "And DeletedAtUTC is null " + 
                                        "Order By " + 
                                            "ClockingDirection, " + 
                                            "BracketFrom, " + 
                                            "BracketTo";
                    PayBracketList = com.ExecuteReader();

                    while (PayBracketList.Read())
                    {
                        DSReturnPayBrackets = new ReturnData.ReturnPayBracketData();

                        DSReturnPayBrackets.PayBracketID = PayBracketList["PayBracketID"].ToString();
                        DSReturnPayBrackets.PayRuleID = PayBracketList["PayRuleID"].ToString();
                        DSReturnPayBrackets.ClockingDirection = PayBracketList["ClockingDirection"].ToString();
                        DSReturnPayBrackets.BracketFrom = PayBracketList["BracketFrom"].ToString();
                        DSReturnPayBrackets.BracketTo = PayBracketList["BracketTo"].ToString();
                        DSReturnPayBrackets.PayClocking = PayBracketList["PayClocking"].ToString();

                        ReturnedPayBrackets.Add(DSReturnPayBrackets);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedPayBrackets = null;
            }
            catch (Exception Ex)
            {
                ReturnedPayBrackets = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedPayBrackets;
        }




        [WebMethod(Description = "Mobitime Web Service - Update a Pay Bracket record in MobiTime")]
        public bool Update(
            string ApplicationPassword, 
            int ClientID, 
            int SiteID, 
            string UserGuid, 
            int PayBracketID, 
            int PayRuleID, 
            string ClockingDirection,
            DateTime BracketFrom, 
            DateTime BracketTo, 
            DateTime PayClocking)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayBrackets", "PayBracketID", PayBracketID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update PayBrackets " +
                                            "Set " +
                                                "ClockingDirection = '" + ClockingDirection + "', " +
                                                "BracketFrom = '" + BracketFrom + "', " +
                                                "BracketTo = '" + BracketTo + "', " +
                                                "PayClocking = '" + PayClocking + "', " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "PayRuleID = " + PayRuleID + " " +
                                                "And PayBracketID = " + PayBracketID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Delete a Pay Bracket record from MobiTime")]
        public bool Delete(
            string ApplicationPassword, 
            int ClientID, 
            int SiteID, 
            string UserGuid, 
            int PayRuleID,
            int PayBracketID)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayBrackets", "PayBracketID", PayBracketID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update PayBrackets " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "PayRuleID = " + PayRuleID + " " +
                                                "And PayBracketID = " + PayBracketID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Recover a Pay Bracket record in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid, 
            int PayRuleID,
            int PayBracketID)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayBrackets", "PayBracketID", PayBracketID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update PayBrackets " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "PayRuleID = " + PayRuleID + " " +
                                                "And PayBracketID = " + PayBracketID + "";
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
