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
    public class Clockings : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());



        [WebMethod(Description = "Mobitime Web Service - Insert new Correction Clocking detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            DateTime ClockingDate,
            int EmployeeID)
        {
            bool Accepted = false;
            int InsertedClocking = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    Accepted = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Insert Into Clockings (" +
                                            "ClockingDate, " +
                                            "ClockingType, " +
                                            "SiteID, " + 
                                            "EmployeeID, " + 
                                            "DateAcceptedUTC, " +
                                            "CapturedAtUTC, " +
                                            "CapturedBy)" +
                                        "Output " + 
                                            "Inserted.ClockingID " + 
                                        "Select " +
                                            "'" + ClockingDate + "', " +
                                            "'Correction', " +
                                            "" + SiteID + ", " + 
                                            "" + EmployeeID + ", " + 
                                            "'" + DateTime.UtcNow + "', " +
                                            "'" + DateTime.UtcNow + "', " +
                                            "'" + UserGuid + "'";
                    InsertedClocking = (int)com.ExecuteScalar();

                    com.CommandText = "Update Clockings " +
                                        "Set " +
                                            "CostCentreID = e.CostCentreID, " +
                                            "DepartmentID = e.DepartmentID, " +
                                            "SupervisorID = e.SupervisorID, " +
                                            "PositionID = e.PositionID, " +
                                            "ShiftPatternID = e.ShiftPatternID " +
                                        "From " +
                                            "Clockings c " +
                                            "Inner Join Employees e on c.SiteID = e.SiteID " +
                                            "And c.EmployeeID = e.EmployeeID " +
                                        "Where " +
                                            "ClockingID = " + InsertedClocking + "";
                    Accepted = true;
                }
            }
            catch (SqlException Ex)
            {
                Accepted = false;
            }
            catch (Exception Ex)
            {
                Accepted = false;
            }
            finally
            {
                con.Close();
            }

            return Accepted;
        }




        [WebMethod(Description = "Mobitime Web Service - Delete a Clocking from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            int ClockingID,
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Clockings", "ClockingID", ClockingID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Clockings " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "ClockingID = " + ClockingID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Update a Clocking's Grouping Detail from MobiTime")]
        public bool UpdateGrouping(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string UserGuid,
            int Type,
            int Grouping,
            int GroupingID,
            int ClockingID)
        {
            // GROUPING IDENTIFICATION                TYPE IDENTIFICATION               
            // ~~~~~~~~~~~~~~~~~~~~~~~~               ~~~~~~~~~~~~~~~~~~~~~~~~~~
            // 1 = Change Cost Centre                 1 = This Clocking Record
            // 2 = Change Department                  2 = This Shift Date
            // 3 = Change Supervisor                  3 = From this Line
            // 4 = Change Position
            // 5 = Change Shift Pattern
            
            bool Successful = false;
            int EmployeeID = 0;
            DateTime ClockingDate = DateTime.UtcNow;

            try
            {
                com.Connection = con;
                con.Open();

                if (Type == 1)
                {
                    if (Grouping == 1)
                    {
                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Clockings", "ClockingID", ClockingID) == true)
                        {
                            com.CommandText = "Update Clockings " +
                                                    "Set " + 
                                                        "CostCentreID = " + GroupingID + ", " +
                                                        "UpdatedAtUTC = '" + DateTime.UtcNow + "', " + 
                                                        "UpdatedBy = '" + UserGuid + "' " + 
                                                "Where " +
                                                    "ClockingID = " + ClockingID + "";
                            com.ExecuteNonQuery();

                            Successful = true;
                        }
                        else
                        {
                            Successful = false;
                        }
                    }

                    if (Grouping == 2)
                    {
                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Clockings", "ClockingID", ClockingID) == true)
                        {
                            com.CommandText = "Update Clockings " +
                                                    "Set " + 
                                                        "DepartmentID = " + GroupingID + ", " +
                                                        "UpdatedAtUTC = '" + DateTime.UtcNow + "', " + 
                                                        "UpdatedBy = '" + UserGuid + "' " + 
                                                "Where " +
                                                    "ClockingID = " + ClockingID + "";
                            com.ExecuteNonQuery();

                            Successful = true;
                        }
                        else
                        {
                            Successful = false;
                        }
                    }

                    if (Grouping == 3)
                    {
                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Clockings", "ClockingID", ClockingID) == true)
                        {
                            com.CommandText = "Update Clockings " +
                                                    "Set " + 
                                                        "SupervisorID = " + GroupingID + ", " +
                                                        "UpdatedAtUTC = '" + DateTime.UtcNow + "', " + 
                                                        "UpdatedBy = '" + UserGuid + "' " + 
                                                "Where " +
                                                    "ClockingID = " + ClockingID + "";
                            com.ExecuteNonQuery();

                            Successful = true;
                        }
                        else
                        {
                            Successful = false;
                        }

                    }

                    if (Grouping == 4)
                    {
                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Clockings", "ClockingID", ClockingID) == true)
                        {
                            com.CommandText = "Update Clockings " +
                                                    "Set " + 
                                                        "PositionID = " + GroupingID + ", " +
                                                        "UpdatedAtUTC = '" + DateTime.UtcNow + "', " + 
                                                        "UpdatedBy = '" + UserGuid + "' " + 
                                                "Where " +
                                                    "ClockingID = " + ClockingID + "";
                            com.ExecuteNonQuery();

                            Successful = true;
                        }
                        else
                        {
                            Successful = false;
                        }
                    }

                    if (Grouping == 5)
                    {
                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Clockings", "ClockingID", ClockingID) == true)
                        {
                            com.CommandText = "Update Clockings " +
                                                    "Set " + 
                                                        "ShiftPatternID = " + GroupingID + ", " +
                                                        "UpdatedAtUTC = '" + DateTime.UtcNow + "', " + 
                                                        "UpdatedBy = '" + UserGuid + "' " + 
                                                "Where " +
                                                    "ClockingID = " + ClockingID + "";
                            com.ExecuteNonQuery();

                            Successful = true;
                        }
                        else
                        {
                            Successful = false;
                        }
                    }
                }

                if (Type == 2)
                {
                    if (Grouping == 1)
                    {
                        Successful = false;
                    }

                    if (Grouping == 2)
                    {
                        Successful = false;
                    }

                    if (Grouping == 3)
                    {
                        Successful = false;
                    }

                    if (Grouping == 4)
                    {
                        Successful = false;
                    }

                    if (Grouping == 5)
                    {
                        Successful = false;
                    }
                }

                if (Type == 3)
                {
                    com.CommandText = "Select " +
                                            "EmployeeID " +
                                        "From " +
                                            "Clockings " +
                                        "Where " +
                                            "ClockingID = " + ClockingID + "";
                    EmployeeID = (int)com.ExecuteScalar();

                    com.CommandText = "Select " +
                                            "ClockingDate " +
                                        "From " +
                                            "Clockings " +
                                        "Where " +
                                            "ClockingID = " + ClockingID + "";
                    ClockingDate = (DateTime)com.ExecuteScalar();

                    if (Grouping == 1)
                    {
                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Employee", "EmployeeID", EmployeeID) == true)
                        {
                            com.CommandText = "Update Employees " +
                                                "Set " +
                                                    "CostCentreID = " + GroupingID + ", " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                "Where " +
                                                    "EmployeeID = " + EmployeeID + " " +
                                                    "And SiteID = " + SiteID + "";
                            com.ExecuteNonQuery();
                            Successful = true;
                        }
                        else
                        {
                            Successful = false;
                        }

                        if (Successful == true)
                        {
                            if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Clockings", "ClockingID", ClockingID) == true)
                            {
                                com.CommandText = "Update Clockings " +
                                                        "Set CostCentreID = " + GroupingID + " " +
                                                    "Where " +
                                                        "EmployeeID = " + EmployeeID + " " +
                                                        "And ClockingDate >= '" + ClockingDate + "' " +
                                                        "And DeletedAtUTC is null " +
                                                    "Order By ClockingDate";
                                com.ExecuteNonQuery();

                                Successful = true;
                            }
                            else
                            {
                                Successful = false;
                            }
                        }
                    }

                    if (Grouping == 2)
                    {
                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Employee", "EmployeeID", EmployeeID) == true)
                        {
                            com.CommandText = "Update Employees " +
                                                "Set " +
                                                    "DepartmentID = " + GroupingID + ", " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                "Where " +
                                                    "EmployeeID = " + EmployeeID + " " +
                                                    "And SiteID = " + SiteID + "";
                            com.ExecuteNonQuery();
                            Successful = true;
                        }
                        else
                        {
                            Successful = false;
                        }

                        if (Successful == true)
                        {
                            if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Clockings", "ClockingID", ClockingID) == true)
                            {
                                com.CommandText = "Update Clockings " +
                                                        "Set DepartmentID = " + GroupingID + " " +
                                                    "Where " +
                                                        "EmployeeID = " + EmployeeID + " " +
                                                        "And ClockingDate >= '" + ClockingDate + "' " +
                                                        "And DeletedAtUTC is null " +
                                                    "Order By ClockingDate";
                                com.ExecuteNonQuery();

                                Successful = true;
                            }
                            else
                            {
                                Successful = false;
                            }
                        }
                    }

                    if (Grouping == 3)
                    {
                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Employee", "EmployeeID", EmployeeID) == true)
                        {
                            com.CommandText = "Update Employees " +
                                                "Set " +
                                                    "SupervisorID = " + GroupingID + ", " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                "Where " +
                                                    "EmployeeID = " + EmployeeID + " " +
                                                    "And SiteID = " + SiteID + "";
                            com.ExecuteNonQuery();
                            Successful = true;
                        }
                        else
                        {
                            Successful = false;
                        }

                        if (Successful == true)
                        {
                            if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Clockings", "ClockingID", ClockingID) == true)
                            {
                                com.CommandText = "Update Clockings " +
                                                        "Set SupervisorID = " + GroupingID + " " +
                                                    "Where " +
                                                        "EmployeeID = " + EmployeeID + " " +
                                                        "And ClockingDate >= '" + ClockingDate + "' " +
                                                        "And DeletedAtUTC is null " +
                                                    "Order By ClockingDate";
                                com.ExecuteNonQuery();

                                Successful = true;
                            }
                            else
                            {
                                Successful = false;
                            }
                        }
                    }

                    if (Grouping == 4)
                    {
                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Employee", "EmployeeID", EmployeeID) == true)
                        {
                            com.CommandText = "Update Employees " +
                                                "Set " +
                                                    "PositionID = " + GroupingID + ", " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                "Where " +
                                                    "EmployeeID = " + EmployeeID + " " +
                                                    "And SiteID = " + SiteID + "";
                            com.ExecuteNonQuery();
                            Successful = true;
                        }
                        else
                        {
                            Successful = false;
                        }

                        if (Successful == true)
                        {
                            if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Clockings", "ClockingID", ClockingID) == true)
                            {
                                com.CommandText = "Update Clockings " +
                                                        "Set PositionID = " + GroupingID + " " +
                                                    "Where " +
                                                        "EmployeeID = " + EmployeeID + " " +
                                                        "And ClockingDate >= '" + ClockingDate + "' " +
                                                        "And DeletedAtUTC is null " +
                                                    "Order By ClockingDate";
                                com.ExecuteNonQuery();

                                Successful = true;
                            }
                            else
                            {
                                Successful = false;
                            }
                        }
                    }

                    if (Grouping == 5)
                    {
                        if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Employee", "EmployeeID", EmployeeID) == true)
                        {
                            com.CommandText = "Update Employees " +
                                                "Set " +
                                                    "ShiftPatternID = " + GroupingID + ", " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                "Where " +
                                                    "EmployeeID = " + EmployeeID + " " +
                                                    "And SiteID = " + SiteID + "";
                            com.ExecuteNonQuery();
                            Successful = true;
                        }
                        else
                        {
                            Successful = false;
                        }

                        if (Successful == true)
                        {
                            if (Functions.AuditTrails.BackupRecord(ClientID, SiteID, UserGuid, "Clockings", "ClockingID", ClockingID) == true)
                            {
                                com.CommandText = "Update Clockings " +
                                                        "Set ShiftPatternID = " + GroupingID + " " +
                                                    "Where " +
                                                        "EmployeeID = " + EmployeeID + " " +
                                                        "And ClockingDate >= '" + ClockingDate + "' " +
                                                        "And DeletedAtUTC is null " +
                                                    "Order By ClockingDate";
                                com.ExecuteNonQuery();

                                Successful = true;
                            }
                            else
                            {
                                Successful = false;
                            }
                        }
                    }
                }

                Successful = true;

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
