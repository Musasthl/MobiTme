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
    public class ShiftPatterns : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Shift Pattern detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            string ShiftPattern, 
            DateTime ShiftPatternStarts, 
            DateTime OvertimeCycleStarts, 
            int OvertimeCycleDays)
        {
            int ShiftPatternID = 0;
            bool InsertedShiftPattern = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedShiftPattern = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (ShiftPatternID) " +
                                        "From " +
                                            "ShiftPatterns " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And ShiftPattern = '" + ShiftPattern + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                                "ShiftPatternID " +
                                            "From " +
                                                "ShiftPatterns " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                            "And ShiftPattern = '" + ShiftPattern + "'";
                        ShiftPatternID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "ShiftPatterns", "ShiftPatternID", ShiftPatternID) == true)
                        {
                            com.CommandText = "Update ShiftPatterns " +
                                                "Set " +
                                                    "ShiftPattern = '" + ShiftPattern + "', " +
                                                    "ShiftPatternStarts = '" + ShiftPatternStarts + "', " +
                                                    "OvertimeCycleStarts = '" + OvertimeCycleStarts + "', " +
                                                    "OvertimeCycleDays = '" + OvertimeCycleDays + "', " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "SiteID = " + SiteID + " " +
                                                    "And ShiftPatternID = '" + ShiftPatternID + "'";
                            com.ExecuteNonQuery();

                            InsertedID = ShiftPatternID;
                            InsertedShiftPattern = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedShiftPattern = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into ShiftPatterns (" +
                                                "SiteID, " + 
                                                "ShiftPattern, " + 
                                                "ShiftPatternStarts, " + 
                                                "OvertimeCycleStarts, " + 
                                                "OvertimeCycleDays, " + 
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.ShiftPatternID " +
                                            "Select " +
                                                "'" + SiteID + "', " + 
                                                "'" + ShiftPattern + "', " + 
                                                "'" + ShiftPatternStarts + "', " + 
                                                "'" + OvertimeCycleStarts + "', " + 
                                                "'" + OvertimeCycleDays + "', " + 
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedShiftPattern = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedShiftPattern = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedShiftPattern = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedShiftPattern;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a stored Shift Pattern record from MobiTime")]
        public List<ReturnData.ReturnShiftPatternData> Select(
            string ApplicationPassword,
            //int ClientID,
            int SiteID,
            string UserGuid,
            int ShiftPatternID)
        {
            ReturnData.ReturnShiftPatternData DSReturnShiftPatterns = null;

            SqlDataReader ShiftPatternList = null;
            List<ReturnData.ReturnShiftPatternData> ReturnedShiftPatterns = new List<ReturnData.ReturnShiftPatternData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedShiftPatterns = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "ShiftPatternID, " + 
                                            "SiteID, " + 
                                            "ShiftPattern, " + 
                                            "ShiftPatternStarts, " + 
                                            "OvertimeCycleStarts, " + 
                                            "OvertimeCycleDays " + 
                                        "From " +
                                            "ShiftPatterns " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And ShiftPatternID = " + ShiftPatternID + " " +
                                            "And DeletedAtUTC is null";
                    ShiftPatternList = com.ExecuteReader();

                    while (ShiftPatternList.Read())
                    {
                        DSReturnShiftPatterns = new ReturnData.ReturnShiftPatternData();

                        DSReturnShiftPatterns.ShiftPatternID = ShiftPatternList["ShiftPatternID"].ToString(); 
                        DSReturnShiftPatterns.SiteID = ShiftPatternList["SiteID"].ToString(); 
                        DSReturnShiftPatterns.ShiftPattern = ShiftPatternList["ShiftPattern"].ToString(); 
                        DSReturnShiftPatterns.ShiftPatternStarts = ShiftPatternList["ShiftPatternStarts"].ToString(); 
                        DSReturnShiftPatterns.OvertimeCycleStarts = ShiftPatternList["OvertimeCycleStarts"].ToString(); 
                        DSReturnShiftPatterns.OvertimeCycleDays = ShiftPatternList["OvertimeCycleDays"].ToString(); 

                        ReturnedShiftPatterns.Add(DSReturnShiftPatterns);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedShiftPatterns = null;
            }
            catch (Exception Ex)
            {
                ReturnedShiftPatterns = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedShiftPatterns;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of Shift Pattern records from MobiTime")]
        public List<ReturnData.ReturnShiftPatternData> List(
            string ApplicationPassword,
            //int ClientID,
            int SiteID,
            string UserGuid)
        {
            ReturnData.ReturnShiftPatternData DSReturnShiftPatterns = null;

            SqlDataReader ShiftPatternList = null;
            List<ReturnData.ReturnShiftPatternData> ReturnedShiftPatterns = new List<ReturnData.ReturnShiftPatternData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedShiftPatterns = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "ShiftPatternID, " +
                                            "SiteID, " +
                                            "ShiftPattern, " +
                                            "ShiftPatternStarts, " +
                                            "OvertimeCycleStarts, " +
                                            "OvertimeCycleDays " + 
                                        "From " +
                                            "ShiftPatterns " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And DeletedAtUTC is null " +
                                        "Order By " +
                                            "ShiftPattern";
                    ShiftPatternList = com.ExecuteReader();

                    while (ShiftPatternList.Read())
                    {
                        DSReturnShiftPatterns = new ReturnData.ReturnShiftPatternData();

                        DSReturnShiftPatterns.ShiftPatternID = ShiftPatternList["ShiftPatternID"].ToString();
                        DSReturnShiftPatterns.SiteID = ShiftPatternList["SiteID"].ToString();
                        DSReturnShiftPatterns.ShiftPattern = ShiftPatternList["ShiftPattern"].ToString();
                        DSReturnShiftPatterns.ShiftPatternStarts = ShiftPatternList["ShiftPatternStarts"].ToString();
                        DSReturnShiftPatterns.OvertimeCycleStarts = ShiftPatternList["OvertimeCycleStarts"].ToString();
                        DSReturnShiftPatterns.OvertimeCycleDays = ShiftPatternList["OvertimeCycleDays"].ToString(); 

                        ReturnedShiftPatterns.Add(DSReturnShiftPatterns);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedShiftPatterns = null;
            }
            catch (Exception Ex)
            {
                ReturnedShiftPatterns = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedShiftPatterns;
        }




        [WebMethod(Description = "Mobitime Web Service - Update a Shift Pattern record in MobiTime")]
        public bool Update(
            string ApplicationPassword, 
            int ClientID, 
            int SiteID, 
            string UserGuid, 
            int ShiftPatternID, 
            string ShiftPattern, 
            DateTime ShiftPatternStarts, 
            DateTime OvertimeCycleStarts, 
            int OvertimeCycleDays)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "ShiftPatterns", "ShiftPatternID", ShiftPatternID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update ShiftPatterns " +
                                            "Set " +
                                                "ShiftPattern = '" + ShiftPattern + "', " +
                                                "ShiftPatternStarts = '" + ShiftPatternStarts + "', " +
                                                "OvertimeCycleStarts = '" + OvertimeCycleStarts + "', " +
                                                "OvertimeCycleDays = '" + OvertimeCycleDays + "', " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And ShiftPatternID = " + ShiftPatternID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Delete a Shift Pattern record from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int ShiftPatternID)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "ShiftPatterns", "ShiftPatternID", ShiftPatternID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update ShiftPatterns " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And ShiftPatternID = " + ShiftPatternID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Recover a Shift Pattern record in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int ShiftPatternID)
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "ShiftPatterns", "ShiftPatternID", ShiftPatternID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update ShiftPatterns " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And ShiftPatternID = " + ShiftPatternID + "";
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
