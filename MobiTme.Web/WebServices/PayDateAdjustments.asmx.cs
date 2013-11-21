using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Data.SqlClient;

namespace MobiTime.WebServices
{
    [WebService(Namespace = "http://www.mobitime.co.za/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PayDateAdjustments : System.Web.Services.WebService
    {

        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Pay Date Adjustment detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int PayRuleID,
            DateTime BracketFrom,
            DateTime BracketTo,
            int Adjustment)
        {
            int PayDateAdjustmentID = 0;
            bool InsertedPayDateAdjustment = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedPayDateAdjustment = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (PayDateAdjustmentID) " +
                                        "From " +
                                            "PayDateAdjustments " +
                                        "Where " +
                                            "PayRuleID = " + PayRuleID + " " +
                                            "And BracketFrom = '" + BracketFrom + "' " +
                                            "And BracketTo = '" + BracketTo + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                                "PayDateAdjustmentID " +
                                            "From " +
                                                "PayDateAdjustments " +
                                            "Where " +
                                                "PayRuleID = " + PayRuleID + " " +
                                                "And BracketFrom = '" + BracketFrom + "' " +
                                                "And BracketTo = '" + BracketTo + "'";
                        PayDateAdjustmentID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayDateAdjustments", "PayDateAdjustmentID", PayDateAdjustmentID) == true)
                        {
                            com.CommandText = "Update PayDateAdjustments " +
                                                "Set " +
                                                    "BracketFrom = '" + BracketFrom + "', " +
                                                    "BracketTo = '" + BracketTo + "', " +
                                                    "Adjustment = '" + Adjustment + "', " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "PayRuleID = " + PayRuleID + " " +
                                                    "And PayDateAdjustmentID = '" + PayDateAdjustmentID + "'";
                            com.ExecuteNonQuery();

                            InsertedID = PayDateAdjustmentID;
                            InsertedPayDateAdjustment = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedPayDateAdjustment = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into PayDateAdjustments (" +
                                                "PayRuleID, " +
                                                "BracketFrom, " +
                                                "BracketTo, " +
                                                "Adjustment, " +
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.PayDateAdjustmentID " +
                                            "Select " +
                                                "'" + PayRuleID + "', " +
                                                "'" + BracketFrom + "', " +
                                                "'" + BracketTo + "', " +
                                                "'" + Adjustment + "', " +
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedPayDateAdjustment = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedPayDateAdjustment = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedPayDateAdjustment = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedPayDateAdjustment;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a stored Pay Date Adjustment record from MobiTime")]
        public List<ReturnData.ReturnPayDateAdjustmentData> Select(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int PayRuleID,
            int PayDateAdjustmentID)
        {
            ReturnData.ReturnPayDateAdjustmentData DSReturnPayDateAdjustments = null;

            SqlDataReader PayDateAdjustmentList = null;
            List<ReturnData.ReturnPayDateAdjustmentData> ReturnedPayDateAdjustments = new List<ReturnData.ReturnPayDateAdjustmentData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedPayDateAdjustments = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "PayDateAdjustmentID, " +
                                            "PayRuleID, " +
                                            "BracketFrom, " +
                                            "BracketTo, " +
                                            "Adjustment " +
                                        "From " +
                                            "PayDateAdjustments " +
                                        "Where " +
                                            "PayRuleID = " + PayRuleID + " " +
                                            "And PayDateAdjustmentID = " + PayDateAdjustmentID + " " +
                                            "And DeletedAtUTC is null";
                    PayDateAdjustmentList = com.ExecuteReader();

                    while (PayDateAdjustmentList.Read())
                    {
                        DSReturnPayDateAdjustments = new ReturnData.ReturnPayDateAdjustmentData();

                        DSReturnPayDateAdjustments.PayDateAdjustmentID = PayDateAdjustmentList["PayDateAdjustmentID"].ToString();
                        DSReturnPayDateAdjustments.PayRuleID = PayDateAdjustmentList["PayRuleID"].ToString();
                        DSReturnPayDateAdjustments.BracketFrom = PayDateAdjustmentList["BracketFrom"].ToString();
                        DSReturnPayDateAdjustments.BracketTo = PayDateAdjustmentList["BracketTo"].ToString();
                        DSReturnPayDateAdjustments.Adjustment = PayDateAdjustmentList["Adjustment"].ToString();

                        ReturnedPayDateAdjustments.Add(DSReturnPayDateAdjustments);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedPayDateAdjustments = null;
            }
            catch (Exception Ex)
            {
                ReturnedPayDateAdjustments = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedPayDateAdjustments;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of Pay Date Adjustment records from MobiTime")]
        public List<ReturnData.ReturnPayDateAdjustmentData> List(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int PayRuleID)
        {
            ReturnData.ReturnPayDateAdjustmentData DSReturnPayDateAdjustments = null;

            SqlDataReader PayDateAdjustmentList = null;
            List<ReturnData.ReturnPayDateAdjustmentData> ReturnedPayDateAdjustments = new List<ReturnData.ReturnPayDateAdjustmentData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedPayDateAdjustments = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "PayDateAdjustmentID, " +
                                            "PayRuleID, " +
                                            "BracketFrom, " +
                                            "BracketTo, " +
                                            "Adjustment " +
                                        "From " +
                                            "PayDateAdjustments " +
                                        "Where " +
                                            "PayRuleID = " + PayRuleID + " " +
                                            "And DeletedAtUTC is null " +
                                        "Order By " +
                                            "BracketFrom, " +
                                            "BracketTo";
                    PayDateAdjustmentList = com.ExecuteReader();

                    while (PayDateAdjustmentList.Read())
                    {
                        DSReturnPayDateAdjustments = new ReturnData.ReturnPayDateAdjustmentData();

                        DSReturnPayDateAdjustments.PayDateAdjustmentID = PayDateAdjustmentList["PayDateAdjustmentID"].ToString();
                        DSReturnPayDateAdjustments.PayRuleID = PayDateAdjustmentList["PayRuleID"].ToString();
                        DSReturnPayDateAdjustments.BracketFrom = PayDateAdjustmentList["BracketFrom"].ToString();
                        DSReturnPayDateAdjustments.BracketTo = PayDateAdjustmentList["BracketTo"].ToString();
                        DSReturnPayDateAdjustments.Adjustment = PayDateAdjustmentList["Adjustment"].ToString();

                        ReturnedPayDateAdjustments.Add(DSReturnPayDateAdjustments);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedPayDateAdjustments = null;
            }
            catch (Exception Ex)
            {
                ReturnedPayDateAdjustments = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedPayDateAdjustments;
        }




        [WebMethod(Description = "Mobitime Web Service - Update a Pay Date Adjustment record in MobiTime")]
        public bool Update(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int PayDateAdjustmentID,
            int PayRuleID,
            DateTime BracketFrom,
            DateTime BracketTo,
            int Adjustment)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayDateAdjustments", "PayDateAdjustmentID", PayDateAdjustmentID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update PayDateAdjustments " +
                                            "Set " +
                                                "BracketFrom = '" + BracketFrom + "', " +
                                                "BracketTo = '" + BracketTo + "', " +
                                                "Adjustment = '" + Adjustment + "', " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "PayRuleID = " + PayRuleID + " " +
                                                "And PayDateAdjustmentID = " + PayDateAdjustmentID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Delete a Pay Date Adjustment record from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int PayRuleID,
            int PayDateAdjustmentID)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayDateAdjustments", "PayDateAdjustmentID", PayDateAdjustmentID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update PayDateAdjustments " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "PayRuleID = " + PayRuleID + " " +
                                                "And PayDateAdjustmentID = " + PayDateAdjustmentID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Recover a Pay Date Adjustment record in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int PayRuleID,
            int PayDateAdjustmentID)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayDateAdjustments", "PayDateAdjustmentID", PayDateAdjustmentID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update PayDateAdjustments " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "PayRuleID = " + PayRuleID + " " +
                                                "And PayDateAdjustmentID = " + PayDateAdjustmentID + "";
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
