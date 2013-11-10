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
    public class PayRules : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Pay Rule detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            string PayRule,
            DateTime ShiftStart,
            DateTime ShiftEnd,
            string Description,
            double ShiftDayAdjustment,
            string RoundingType_In,
            string RoundingUnit_In,
            int RoundingBase_In,
            string RoundingType_Out,
            string RoundingUnit_Out,
            int RoundingBase_Out)
        {
            int PayRuleID = 0;
            bool InsertedPayRule = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedPayRule = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (PayRuleID) " +
                                        "From " +
                                            "PayRules " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And PayRule = '" + PayRule + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                                "PayRuleID " +
                                            "From " +
                                                "PayRules " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And PayRule = '" + PayRule + "'";
                        PayRuleID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayRules", "PayRuleID", PayRuleID) == true)
                        {
                            com.CommandText = "Update PayRules " +
                                                "Set " +
                                                    "PayRule = '" + PayRule + "', " +
                                                    "ShiftStart = '" + ShiftStart + "', " +
                                                    "ShiftEnd = '" + ShiftEnd + "', " +
                                                    "Description = '" + Description + "', " +
                                                    "ShiftDayAdjustment = '" + ShiftDayAdjustment + "', " +
                                                    "RoundingType_In = '" + RoundingType_In + "', " +
                                                    "RoundingUnit_In = '" + RoundingUnit_In + "', " +
                                                    "RoundingBase_In = '" + RoundingBase_In + "', " +
                                                    "RoundingType_Out = '" + RoundingType_Out + "', " +
                                                    "RoundingUnit_Out = '" + RoundingUnit_Out + "', " +
                                                    "RoundingBase_Out = '" + RoundingBase_Out + "', " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "SiteID = " + SiteID + " " +
                                                    "And PayRuleID = '" + PayRuleID + "'";
                            com.ExecuteNonQuery();

                            InsertedID = PayRuleID;
                            InsertedPayRule = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedPayRule = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into PayRules (" +
                                                "SiteID, " + 
                                                "PayRule, " + 
                                                "ShiftStart, " + 
                                                "ShiftEnd, " + 
                                                "Description, " + 
                                                "ShiftDayAdjustment, " + 
                                                "RoundingType_In, " + 
                                                "RoundingUnit_In, " + 
                                                "RoundingBase_In, " + 
                                                "RoundingType_Out, " + 
                                                "RoundingUnit_Out, " + 
                                                "RoundingBase_Out, " + 
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.PayRuleID " +
                                            "Select " +
                                                "" + SiteID + ", " + 
                                                "'" + PayRule + "', " + 
                                                "'" + ShiftStart + "', " + 
                                                "'" + ShiftEnd + "', " + 
                                                "'" + Description + "', " + 
                                                "" + ShiftDayAdjustment + ", " + 
                                                "'" + RoundingType_In + "', " + 
                                                "'" + RoundingUnit_In + "', " + 
                                                "" + RoundingBase_In + ", " + 
                                                "'" + RoundingType_Out + "', " + 
                                                "'" + RoundingUnit_Out + "', " + 
                                                "" + RoundingBase_Out + ", " + 
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedPayRule = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedPayRule = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedPayRule = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedPayRule;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a stored Pay Rule record from MobiTime")]
        public List<ReturnData.ReturnPayRuleData> Select(
            string ApplicationPassword,
            //int ClientID,
            int SiteID,
            string UserGuid,
            int PayRuleID)
        {
            ReturnData.ReturnPayRuleData DSReturnPayRules = null;

            SqlDataReader PayRuleList = null;
            List<ReturnData.ReturnPayRuleData> ReturnedPayRules = new List<ReturnData.ReturnPayRuleData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedPayRules = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "PayRuleID, " + 
                                            "SiteID, " + 
                                            "PayRule, " + 
                                            "ShiftStart, " + 
                                            "ShiftEnd, " + 
                                            "Description, " + 
                                            "ShiftDayAdjustment, " + 
                                            "RoundingType_In, " + 
                                            "RoundingUnit_In, " + 
                                            "RoundingBase_In, " + 
                                            "RoundingType_Out, " + 
                                            "RoundingUnit_Out, " + 
                                            "RoundingBase_Out " + 
                                        "From " +
                                            "PayRules " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And PayRuleID = " + PayRuleID + " " +
                                            "And DeletedAtUTC is null";
                    PayRuleList = com.ExecuteReader();

                    while (PayRuleList.Read())
                    {
                        DSReturnPayRules = new ReturnData.ReturnPayRuleData();

                        DSReturnPayRules.PayRuleID = PayRuleList["PayRuleID"].ToString(); 
                        DSReturnPayRules.SiteID = PayRuleList["SiteID"].ToString(); 
                        DSReturnPayRules.PayRule = PayRuleList["PayRule"].ToString(); 
                        DSReturnPayRules.ShiftStart = PayRuleList["ShiftStart"].ToString(); 
                        DSReturnPayRules.ShiftEnd = PayRuleList["ShiftEnd"].ToString(); 
                        DSReturnPayRules.Description = PayRuleList["Description"].ToString(); 
                        DSReturnPayRules.ShiftDayAdjustment = PayRuleList["ShiftDayAdjustment"].ToString(); 
                        DSReturnPayRules.RoundingType_In = PayRuleList["RoundingType_In"].ToString(); 
                        DSReturnPayRules.RoundingUnit_In = PayRuleList["RoundingUnit_In"].ToString(); 
                        DSReturnPayRules.RoundingBase_In = PayRuleList["RoundingBase_In"].ToString(); 
                        DSReturnPayRules.RoundingType_Out = PayRuleList["RoundingType_Out"].ToString(); 
                        DSReturnPayRules.RoundingUnit_Out = PayRuleList["RoundingUnit_Out"].ToString(); 
                        DSReturnPayRules.RoundingBase_Out = PayRuleList["RoundingBase_Out"].ToString(); 

                        ReturnedPayRules.Add(DSReturnPayRules);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedPayRules = null;
            }
            catch (Exception Ex)
            {
                ReturnedPayRules = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedPayRules;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of Pay Rule records from MobiTime")]
        public List<ReturnData.ReturnPayRuleData> List(
            string ApplicationPassword,
            //int ClientID,
            int SiteID,
            string UserGuid)
        {
            ReturnData.ReturnPayRuleData DSReturnPayRules = null;

            SqlDataReader PayRuleList = null;
            List<ReturnData.ReturnPayRuleData> ReturnedPayRules = new List<ReturnData.ReturnPayRuleData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedPayRules = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "PayRuleID, " +
                                            "SiteID, " +
                                            "PayRule, " +
                                            "ShiftStart, " +
                                            "ShiftEnd, " +
                                            "Description " +
                                        "From " +
                                            "PayRules " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And DeletedAtUTC is null " +
                                        "Order By " +
                                            "PayRule";
                    PayRuleList = com.ExecuteReader();

                    while (PayRuleList.Read())
                    {
                        DSReturnPayRules = new ReturnData.ReturnPayRuleData();

                        DSReturnPayRules.PayRuleID = PayRuleList["PayRuleID"].ToString();
                        DSReturnPayRules.SiteID = PayRuleList["SiteID"].ToString();
                        DSReturnPayRules.PayRule = PayRuleList["PayRule"].ToString();
                        DSReturnPayRules.ShiftStart = PayRuleList["ShiftStart"].ToString();
                        DSReturnPayRules.ShiftEnd = PayRuleList["ShiftEnd"].ToString();
                        DSReturnPayRules.Description = PayRuleList["Description"].ToString();

                        ReturnedPayRules.Add(DSReturnPayRules);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedPayRules = null;
            }
            catch (Exception Ex)
            {
                ReturnedPayRules = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedPayRules;
        }




        [WebMethod(Description = "Mobitime Web Service - Update a Pay Rule record in MobiTime")]
        public bool Update(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int PayRuleID,
            string PayRule, 
            DateTime ShiftStart, 
            DateTime ShiftEnd, 
            string Description, 
            double ShiftDayAdjustment, 
            string RoundingType_In, 
            string RoundingUnit_In, 
            int RoundingBase_In, 
            string RoundingType_Out, 
            string RoundingUnit_Out, 
            int RoundingBase_Out)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayRules", "PayRuleID", PayRuleID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update PayRules " +
                                            "Set " +
                                                "PayRule = '" + PayRule + "', " +
                                                "ShiftStart = '" + ShiftStart + "', " +
                                                "ShiftEnd = '" + ShiftEnd + "', " +
                                                "Description = '" + Description + "', " +
                                                "ShiftDayAdjustment = '" + ShiftDayAdjustment + "', " +
                                                "RoundingType_In = '" + RoundingType_In + "', " +
                                                "RoundingUnit_In = '" + RoundingUnit_In + "', " +
                                                "RoundingBase_In = '" + RoundingBase_In + "', " +
                                                "RoundingType_Out = '" + RoundingType_Out + "', " +
                                                "RoundingUnit_Out = '" + RoundingUnit_Out + "', " +
                                                "RoundingBase_Out = '" + RoundingBase_Out + "', " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And PayRuleID = " + PayRuleID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Delete a Pay Rule record from MobiTime")]
        public bool Delete(
            string ApplicationPassword, 
            int ClientID, 
            int SiteID, 
            string UserGuid, 
            int PayRuleID)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayRules", "PayRuleID", PayRuleID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update PayRules " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And PayRuleID = " + PayRuleID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Recover a Pay Rule record in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid, 
            int PayRuleID)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "PayRules", "PayRuleID", PayRuleID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update PayRules " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And PayRuleID = " + PayRuleID + "";
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

