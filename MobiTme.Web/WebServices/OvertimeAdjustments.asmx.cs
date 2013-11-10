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
    public class OvertimeAdjustments : System.Web.Services.WebService
    {

        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Overtime Adjustment detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword, 
            int ClientID, 
            int SiteID, 
            string UserGuid, 
            int ShiftPatternID, 
            double RequiredHours, 
            decimal PayTypeID_TransferFrom, 
            decimal PayTypeID_TransferTo)
        {
            int OvertimeAdjustmentID = 0;
            bool InsertedOvertimeAdjustment = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedOvertimeAdjustment = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (OvertimeAdjustmentID) " +
                                        "From " +
                                            "OvertimeAdjustments " +
                                        "Where " +
                                            "ShiftPatternID = " + ShiftPatternID + " " +
                                            "And PayTypeID_TransferFrom = " + PayTypeID_TransferFrom + " " +
                                            "And PayTypeID_TransferTo = " + PayTypeID_TransferTo + "";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                                "OvertimeAdjustmentID " +
                                            "From " +
                                                "OvertimeAdjustments " +
                                            "Where " +
                                                "ShiftPatternID = " + ShiftPatternID + " " +
                                                "And PayTypeID_TransferFrom = " + PayTypeID_TransferFrom + " " +
                                                "And PayTypeID_TransferTo = " + PayTypeID_TransferTo + "";
                        OvertimeAdjustmentID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "OvertimeAdjustments", "OvertimeAdjustmentID", OvertimeAdjustmentID) == true)
                        {
                            com.CommandText = "Update OvertimeAdjustments " +
                                                "Set " +
                                                    "ShiftPatternID = " + ShiftPatternID + ", " +
                                                    "RequiredHours = " + RequiredHours + ", " +
                                                    "PayTypeID_TransferFrom = " + PayTypeID_TransferFrom + ", " +
                                                    "PayTypeID_TransferTo = " + PayTypeID_TransferTo + ", " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "ShiftPatternID = " + ShiftPatternID + " " +
                                                    "And OvertimeAdjustmentID = " + OvertimeAdjustmentID + "";
                            com.ExecuteNonQuery();

                            InsertedID = OvertimeAdjustmentID;
                            InsertedOvertimeAdjustment = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedOvertimeAdjustment = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into OvertimeAdjustments (" +
                                                "ShiftPatternID, " + 
                                                "RequiredHours, " + 
                                                "PayTypeID_TransferFrom, " + 
                                                "PayTypeID_TransferTo, " + 
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.OvertimeAdjustmentID " +
                                            "Select " +
                                                "" + ShiftPatternID + ", " + 
                                                "" + RequiredHours + ", " + 
                                                "" + PayTypeID_TransferFrom + ", " + 
                                                "" + PayTypeID_TransferTo + ", " + 
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedOvertimeAdjustment = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedOvertimeAdjustment = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedOvertimeAdjustment = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedOvertimeAdjustment;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of the Overtime Adjustment detail")]
        public List<ReturnData.ReturnOvertimeAdjustmentData> Select(
            string ApplicationPassword,
            int ShiftPatternID,
            int OvertimeAdjustmentID)
        {
            ReturnData.ReturnOvertimeAdjustmentData DSReturnOvertimeAdjustments = null;

            SqlDataReader OvertimeAdjustmentList = null;
            List<ReturnData.ReturnOvertimeAdjustmentData> ReturnedOvertimeAdjustments = new List<ReturnData.ReturnOvertimeAdjustmentData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedOvertimeAdjustments = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "OvertimeAdjustmentID, " + 
                                            "ShiftPatternID, " + 
                                            "RequiredHours, " + 
                                            "PayTypeID_TransferFrom, " + 
                                            "PayTypeID_TransferTo " + 
                                        "From " +
                                            "OvertimeAdjustments " +
                                        "Where " +
                                            "ShiftPatternID = " + ShiftPatternID + " " +
                                            "And OvertimeAdjustmentID = " + OvertimeAdjustmentID + " " +
                                            "And DeletedAtUTC is null";
                    OvertimeAdjustmentList = com.ExecuteReader();

                    while (OvertimeAdjustmentList.Read())
                    {
                        DSReturnOvertimeAdjustments = new ReturnData.ReturnOvertimeAdjustmentData();

                        DSReturnOvertimeAdjustments.OvertimeAdjustmentID = OvertimeAdjustmentList["OvertimeAdjustmentID"].ToString(); 
                        DSReturnOvertimeAdjustments.ShiftPatternID = OvertimeAdjustmentList["ShiftPatternID"].ToString(); 
                        DSReturnOvertimeAdjustments.RequiredHours = OvertimeAdjustmentList["RequiredHours"].ToString(); 
                        DSReturnOvertimeAdjustments.PayTypeID_TransferFrom = OvertimeAdjustmentList["PayTypeID_TransferFrom"].ToString(); 
                        DSReturnOvertimeAdjustments.PayTypeID_TransferTo = OvertimeAdjustmentList["PayTypeID_TransferTo"].ToString(); 

                        ReturnedOvertimeAdjustments.Add(DSReturnOvertimeAdjustments);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedOvertimeAdjustments = null;
            }
            catch (Exception Ex)
            {
                ReturnedOvertimeAdjustments = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedOvertimeAdjustments;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a Overtime Adjustment Detail List from MobiTime")]
        public List<ReturnData.ReturnOvertimeAdjustmentData> ListOvertimeAdjustments(
            string ApplicationPassword,
            int ShiftPatternID)
        {
            ReturnData.ReturnOvertimeAdjustmentData DSReturnOvertimeAdjustments = null;

            SqlDataReader OvertimeAdjustmentList = null;
            List<ReturnData.ReturnOvertimeAdjustmentData> ReturnedOvertimeAdjustments = new List<ReturnData.ReturnOvertimeAdjustmentData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedOvertimeAdjustments = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "OvertimeAdjustmentID, " +
                                            "ShiftPatternID, " +
                                            "RequiredHours, " +
                                            "PayTypeID_TransferFrom, " +
                                            "PayTypeID_TransferTo " +
                                        "From " +
                                            "OvertimeAdjustments " +
                                        "Where " +
                                            "ShiftPatternID = " + ShiftPatternID + " " +
                                            "And DeletedAtUTC is null " +
                                        "Order By " +
                                            "PayTypeID_TransferFrom, PayTypeID_TransferTo";
                    OvertimeAdjustmentList = com.ExecuteReader();

                    while (OvertimeAdjustmentList.Read())
                    {
                        DSReturnOvertimeAdjustments = new ReturnData.ReturnOvertimeAdjustmentData();

                        DSReturnOvertimeAdjustments.OvertimeAdjustmentID = OvertimeAdjustmentList["OvertimeAdjustmentID"].ToString();
                        DSReturnOvertimeAdjustments.ShiftPatternID = OvertimeAdjustmentList["ShiftPatternID"].ToString();
                        DSReturnOvertimeAdjustments.RequiredHours = OvertimeAdjustmentList["RequiredHours"].ToString();
                        DSReturnOvertimeAdjustments.PayTypeID_TransferFrom = OvertimeAdjustmentList["PayTypeID_TransferFrom"].ToString();
                        DSReturnOvertimeAdjustments.PayTypeID_TransferTo = OvertimeAdjustmentList["PayTypeID_TransferTo"].ToString();

                        ReturnedOvertimeAdjustments.Add(DSReturnOvertimeAdjustments);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedOvertimeAdjustments = null;
            }
            catch (Exception Ex)
            {
                ReturnedOvertimeAdjustments = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedOvertimeAdjustments;
        }




        [WebMethod(Description = "Mobitime Web Service - Update Overtime Adjustment detail in MobiTime")]
        public bool Update(
            string ApplicationPassword, 
            int ClientID, 
            int SiteID, 
            string UserGuid, 
            int OvertimeAdjustmentID, 
            int ShiftPatternID, 
            double RequiredHours, 
            decimal PayTypeID_TransferFrom, 
            decimal PayTypeID_TransferTo)
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "OvertimeAdjustments", "OvertimeAdjustmentID", OvertimeAdjustmentID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update OvertimeAdjustments " +
                                            "Set " +
                                                "ShiftPatternID = '" + ShiftPatternID + "', " +
                                                "RequiredHours = '" + RequiredHours + "', " +
                                                "PayTypeID_TransferFrom = '" + PayTypeID_TransferFrom + "', " +
                                                "PayTypeID_TransferTo = '" + PayTypeID_TransferTo + "', " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "OvertimeAdjustmentID = " + OvertimeAdjustmentID + " " +
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




        [WebMethod(Description = "Mobitime Web Service - Delete a OvertimeAdjustment from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int SiteID,
            int ShiftPatternID,
            int OvertimeAdjustmentID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "OvertimeAdjustments", "OvertimeAdjustmentID", OvertimeAdjustmentID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update OvertimeAdjustments " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "ShiftPatternID = " + ShiftPatternID + " " +
                                                "And OvertimeAdjustmentID = " + OvertimeAdjustmentID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Recover a OvertimeAdjustment in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            int ShiftPatternID,
            int OvertimeAdjustmentID,
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "OvertimeAdjustments", "OvertimeAdjustmentID", OvertimeAdjustmentID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update OvertimeAdjustments " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "ShiftPatternID = " + ShiftPatternID + " " +
                                                "And OvertimeAdjustmentID = " + OvertimeAdjustmentID + "";
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
