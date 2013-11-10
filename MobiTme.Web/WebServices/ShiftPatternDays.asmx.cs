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
    public class ShiftPatternDays : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Shift Pattern Day detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword, 
            int ClientID, 
            int SiteID, 
            string UserGuid, 
            int ShiftPatternID, 
            int Day, 
            bool Working, 
            int PayRuleID)
        {
            int ShiftPatternDayID = 0;
            bool InsertedShiftPatternDay = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedShiftPatternDay = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (ShiftPatternDayID) " +
                                        "From " +
                                            "ShiftPatternDays " +
                                        "Where " +
                                            "ShiftPatternID = " + ShiftPatternID + " " +
                                            "And Day = '" + Day + "' " +
                                            "And Working = '" + Working + "' " +
                                            "And PayRuleID = '" + PayRuleID + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                                "ShiftPatternDayID " +
                                            "From " +
                                                "ShiftPatternDays " +
                                            "Where " +
                                                "ShiftPatternID = " + ShiftPatternID + " " +
                                                "And Day = '" + Day + "' " +
                                                "And Working = '" + Working + "' " +
                                                "And PayRuleID = '" + PayRuleID + "'";
                        ShiftPatternDayID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "ShiftPatternDays", "ShiftPatternDayID", ShiftPatternDayID) == true)
                        {
                            com.CommandText = "Update ShiftPatternDays " +
                                                "Set " +
                                                    "ShiftPatternID = '" + ShiftPatternID + "', " +
                                                    "Day = '" + Day + "', " +
                                                    "Working = '" + Working + "', " +
                                                    "PayRuleID = '" + PayRuleID + "', " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "ShiftPatternID = " + ShiftPatternID + " " +
                                                    "And Day = '" + Day + "' " +
                                                    "And Working = '" + Working + "' " +
                                                    "And PayRuleID = '" + PayRuleID + "'";
                            com.ExecuteNonQuery();

                            InsertedID = ShiftPatternDayID;
                            InsertedShiftPatternDay = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedShiftPatternDay = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into ShiftPatternDays (" +
                                                "ShiftPatternID, " + 
                                                "Day, " + 
                                                "Working, " + 
                                                "PayRuleID, " + 
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.ShiftPatternDayID " +
                                            "Select " +
                                                "'" + ShiftPatternID + "', " + 
                                                "'" + Day + "', " + 
                                                "'" + Working + "', " + 
                                                "'" + PayRuleID + "', " + 
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedShiftPatternDay = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedShiftPatternDay = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedShiftPatternDay = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedShiftPatternDay;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a stored Shift Pattern Day record from MobiTime")]
        public List<ReturnData.ReturnShiftPatternDayData> Select(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int ShiftPatternID,
            int ShiftPatternDayID)
        {
            ReturnData.ReturnShiftPatternDayData DSReturnShiftPatternDays = null;

            SqlDataReader ShiftPatternDayList = null;
            List<ReturnData.ReturnShiftPatternDayData> ReturnedShiftPatternDays = new List<ReturnData.ReturnShiftPatternDayData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedShiftPatternDays = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "ShiftPatternDayID, " + 
                                            "ShiftPatternID, " + 
                                            "Day, " + 
                                            "Working, " + 
                                            "PayRuleID " +     
                                        "From " +
                                            "ShiftPatternDays " +
                                        "Where " +
                                            "ShiftPatternID = " + ShiftPatternID + " " +
                                            "And ShiftPatternDayID = " + ShiftPatternDayID + " " +
                                            "And DeletedAtUTC is null";
                    ShiftPatternDayList = com.ExecuteReader();

                    while (ShiftPatternDayList.Read())
                    {
                        DSReturnShiftPatternDays = new ReturnData.ReturnShiftPatternDayData();

                        DSReturnShiftPatternDays.ShiftPatternDayID = ShiftPatternDayList["ShiftPatternDayID"].ToString(); 
                        DSReturnShiftPatternDays.ShiftPatternID = ShiftPatternDayList["ShiftPatternID"].ToString(); 
                        DSReturnShiftPatternDays.Day = ShiftPatternDayList["Day"].ToString(); 
                        DSReturnShiftPatternDays.Working = ShiftPatternDayList["Working"].ToString(); 
                        DSReturnShiftPatternDays.PayRuleID = ShiftPatternDayList["PayRuleID"].ToString(); 

                        ReturnedShiftPatternDays.Add(DSReturnShiftPatternDays);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedShiftPatternDays = null;
            }
            catch (Exception Ex)
            {
                ReturnedShiftPatternDays = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedShiftPatternDays;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of Shift Pattern Day records from MobiTime")]
        public List<ReturnData.ReturnShiftPatternDayData> List(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int ShiftPatternID)
        {
            ReturnData.ReturnShiftPatternDayData DSReturnShiftPatternDays = null;

            SqlDataReader ShiftPatternDayList = null;
            List<ReturnData.ReturnShiftPatternDayData> ReturnedShiftPatternDays = new List<ReturnData.ReturnShiftPatternDayData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedShiftPatternDays = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "ShiftPatternDayID, " +
                                            "ShiftPatternID, " +
                                            "Day, " +
                                            "Working, " +
                                            "PayRuleID " + 
                                        "From " +
                                            "ShiftPatternDays " +
                                        "Where " +
                                            "ShiftPatternID = " + ShiftPatternID + " " +
                                            "And DeletedAtUTC is null " +
                                        "Order By " +
                                            "Day";
                    ShiftPatternDayList = com.ExecuteReader();

                    while (ShiftPatternDayList.Read())
                    {
                        DSReturnShiftPatternDays = new ReturnData.ReturnShiftPatternDayData();

                        DSReturnShiftPatternDays.ShiftPatternDayID = ShiftPatternDayList["ShiftPatternDayID"].ToString();
                        DSReturnShiftPatternDays.ShiftPatternID = ShiftPatternDayList["ShiftPatternID"].ToString();
                        DSReturnShiftPatternDays.Day = ShiftPatternDayList["Day"].ToString();
                        DSReturnShiftPatternDays.Working = ShiftPatternDayList["Working"].ToString();
                        DSReturnShiftPatternDays.PayRuleID = ShiftPatternDayList["PayRuleID"].ToString(); 

                        ReturnedShiftPatternDays.Add(DSReturnShiftPatternDays);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedShiftPatternDays = null;
            }
            catch (Exception Ex)
            {
                ReturnedShiftPatternDays = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedShiftPatternDays;
        }




        [WebMethod(Description = "Mobitime Web Service - Update a Shift Pattern Day record in MobiTime")]
        public bool Update(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int ShiftPatternID,
            int ShiftPatternDayID, 
            int Day, 
            bool Working, 
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "ShiftPatternDays", "ShiftPatternDayID", ShiftPatternDayID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update ShiftPatternDays " +
                                            "Set " +
                                                "ShiftPatternID = '" + ShiftPatternID + "', " +
                                                "Day = '" + Day + "', " +
                                                "Working = '" + Working + "', " +
                                                "PayRuleID = '" + PayRuleID + "', " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "ShiftPatternID = " + ShiftPatternID + " " +
                                                "And ShiftPatternDayID = " + ShiftPatternDayID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Delete a Shift Pattern Day record from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int ShiftPatternID,
            int ShiftPatternDayID)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "ShiftPatternDays", "ShiftPatternDayID", ShiftPatternDayID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update ShiftPatternDays " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "ShiftPatternID = " + ShiftPatternID + " " +
                                                "And ShiftPatternDayID = " + ShiftPatternDayID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Recover a Shift Pattern Day record in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int ShiftPatternID,
            int ShiftPatternDayID)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "ShiftPatternDays", "ShiftPatternDayID", ShiftPatternDayID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update ShiftPatternDays " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "ShiftPatternID = " + ShiftPatternID + " " +
                                                "And ShiftPatternDayID = " + ShiftPatternDayID + "";
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
