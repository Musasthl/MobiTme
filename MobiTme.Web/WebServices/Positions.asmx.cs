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
    public class Positions : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Position detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            int SiteID,
            string Position,
            string PayrollCode,
            string AccountsCode,
            double CtcRate00,
            double CtcRate10,
            double CtcRate13,
            double CtcRate15,
            double CtcRate20,
            double CtcRate23,
            double CtcRate25,
            double CtcRate30,
            double CtcRatePPH,
            string UserGuid)
        {
            int PositionID = 0;
            bool InsertedPosition = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedPosition = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (PositionID) " +
                                        "From " +
                                            "Positions " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And Position = '" + Position + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                                "PositionID " +
                                            "From " +
                                                "Positions " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And Position = '" + Position + "'";
                        PositionID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Positions", "PositionID", PositionID) == true)
                        {
                            com.CommandText = "Update Positions " +
                                                "Set " +
                                                    "Position = '" + Position + "', " +
                                                    "PayrollCode = '" + PayrollCode + "', " +
                                                    "AccountsCode = '" + AccountsCode + "', " +
                                                    "CtcRate00 = " + CtcRate00 + ", " + 
                                                    "CtcRate10 = " + CtcRate10 + ", " +
                                                    "CtcRate13 = " + CtcRate13 + ", " + 
                                                    "CtcRate15 = " + CtcRate15 + ", " + 
                                                    "CtcRate20 = " + CtcRate20 + ", " +
                                                    "CtcRate23 = " + CtcRate23 + ", " + 
                                                    "CtcRate25 = " + CtcRate25 + ", " + 
                                                    "CtcRate30 = " + CtcRate30 + ", " +
                                                    "CtcRatePPH = " + CtcRatePPH + ", " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "SiteID = " + SiteID + " " +
                                                "And Position = '" + Position + "'";
                            com.ExecuteNonQuery();

                            InsertedID = PositionID;
                            InsertedPosition = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedPosition = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into Positions (" +
                                                "SiteID, " +
                                                "Position, " +
                                                "PayrollCode, " +
                                                "AccountsCode, " +
                                                "CtcRate00, " +
                                                "CtcRate10, " + 
                                                "CtcRate13, " +
                                                "CtcRate15, " +
                                                "CtcRate20, " +
                                                "CtcRate23, " +
                                                "CtcRate25, " +
                                                "CtcRate30, " +
                                                "CtcRatePPH, " +
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.PositionID " +
                                            "Select " +
                                                "" + SiteID + ", " +
                                                "'" + Position + "', " +
                                                "'" + PayrollCode + "', " +
                                                "'" + AccountsCode + "', " +
                                                "" + CtcRate00 + ", " +
                                                "" + CtcRate10 + ", " +
                                                "" + CtcRate13 + ", " +
                                                "" + CtcRate15 + ", " +
                                                "" + CtcRate20 + ", " +
                                                "" + CtcRate23 + ", " +
                                                "" + CtcRate25 + ", " +
                                                "" + CtcRate30 + ", " +
                                                "" + CtcRatePPH + ", " + 
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedPosition = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedPosition = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedPosition = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedPosition;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of the Position detail")]
        public List<ReturnData.ReturnPositionData> Select(
            string ApplicationPassword,
            int SiteID,
            int PositionID)
        {
            ReturnData.ReturnPositionData DSReturnPositions = null;

            SqlDataReader PositionList = null;
            List<ReturnData.ReturnPositionData> ReturnedPositions = new List<ReturnData.ReturnPositionData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedPositions = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "PositionID, " +
                                            "SiteID, " +
                                            "Position, " +
                                            "PayrollCode, " +
                                            "AccountsCode, " +
                                            "CtcRate00, " +
                                            "CtcRate10, " + 
                                            "CtcRate13, " +
                                            "CtcRate15, " +
                                            "CtcRate20, " +
                                            "CtcRate23, " +
                                            "CtcRate25, " +
                                            "CtcRate30, " +
                                            "CtcRatePPH " +
                                        "From " +
                                            "Positions " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And PositionID = " + PositionID + " " +
                                            "And DeletedAtUTC is null";
                    PositionList = com.ExecuteReader();

                    while (PositionList.Read())
                    {
                        DSReturnPositions = new ReturnData.ReturnPositionData();
                        DSReturnPositions.PositionID = PositionList["PositionID"].ToString();
                        DSReturnPositions.SiteID = PositionList["SiteID"].ToString();
                        DSReturnPositions.Position = PositionList["Position"].ToString();
                        DSReturnPositions.PayrollCode = PositionList["PayrollCode"].ToString();
                        DSReturnPositions.AccountsCode = PositionList["AccountsCode"].ToString();
                        DSReturnPositions.CtcRate00 = PositionList["CtcRate00"].ToString();
                        DSReturnPositions.CtcRate10 = PositionList["CtcRate10"].ToString();
                        DSReturnPositions.CtcRate13 = PositionList["CtcRate13"].ToString();
                        DSReturnPositions.CtcRate15 = PositionList["CtcRate15"].ToString();
                        DSReturnPositions.CtcRate20 = PositionList["CtcRate20"].ToString();
                        DSReturnPositions.CtcRate23 = PositionList["CtcRate23"].ToString();
                        DSReturnPositions.CtcRate25 = PositionList["CtcRate25"].ToString();
                        DSReturnPositions.CtcRate30 = PositionList["CtcRate30"].ToString();
                        DSReturnPositions.CtcRatePPH = PositionList["CtcRatePPH"].ToString();

                        ReturnedPositions.Add(DSReturnPositions);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedPositions = null;
            }
            catch (Exception Ex)
            {
                ReturnedPositions = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedPositions;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a Position Detail List from MobiTime")]
        public List<ReturnData.ReturnPositionData> ListPositions(
            string ApplicationPassword,
            int SiteID)
        {
            ReturnData.ReturnPositionData DSReturnPositions = null;

            SqlDataReader PositionList = null;
            List<ReturnData.ReturnPositionData> ReturnedPositions = new List<ReturnData.ReturnPositionData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedPositions = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "PositionID, " +
                                            "Position, " +
                                            "PayrollCode, " +
                                            "AccountsCode " +
                                        "From " +
                                            "Positions " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And DeletedAtUTC is null " +
                                        "Order By " +
                                            "Position";
                    PositionList = com.ExecuteReader();

                    while (PositionList.Read())
                    {
                        DSReturnPositions = new ReturnData.ReturnPositionData();
                        DSReturnPositions.PositionID = PositionList["PositionID"].ToString();
                        //DSReturnPositions.SiteID = PositionList["SiteID"].ToString();
                        DSReturnPositions.Position = PositionList["Position"].ToString();
                        DSReturnPositions.PayrollCode = PositionList["PayrollCode"].ToString();
                        DSReturnPositions.AccountsCode = PositionList["AccountsCode"].ToString();

                        ReturnedPositions.Add(DSReturnPositions);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedPositions = null;
            }
            catch (Exception Ex)
            {
                ReturnedPositions = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedPositions;
        }




        [WebMethod(Description = "Mobitime Web Service - Update Position detail in MobiTime")]
        public bool Update(
            string ApplicationPassword,
            int SiteID,
            int PositionID,
            string Position,
            string PayrollCode,
            string AccountsCode,
            double CtcRate00,
            double CtcRate10,
            double CtcRate13,
            double CtcRate15,
            double CtcRate20,
            double CtcRate23,
            double CtcRate25,
            double CtcRate30,
            double CtcRatePPH,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Positions", "PositionID", PositionID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Positions " +
                                            "Set " +
                                                "Position = '" + Position + "', " +
                                                "PayrollCode = '" + PayrollCode + "', " +
                                                "AccountsCode = '" + AccountsCode + "', " +
                                                "CtcRate00 = " + CtcRate00 + ", " +
                                                "CtcRate10 = " + CtcRate10 + ", " +
                                                "CtcRate13 = " + CtcRate13 + ", " +
                                                "CtcRate15 = " + CtcRate15 + ", " +
                                                "CtcRate20 = " + CtcRate20 + ", " +
                                                "CtcRate23 = " + CtcRate23 + ", " +
                                                "CtcRate25 = " + CtcRate25 + ", " +
                                                "CtcRate30 = " + CtcRate30 + ", " +
                                                "CtcRatePPH = " + CtcRatePPH + ", " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And PositionID = " + PositionID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Delete a Position from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int SiteID,
            int PositionID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Positions", "PositionID", PositionID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Positions " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And PositionID = " + PositionID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Recover a Position in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int SiteID,
            int PositionID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Positions", "PositionID", PositionID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Positions " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And PositionID = " + PositionID + "";
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
