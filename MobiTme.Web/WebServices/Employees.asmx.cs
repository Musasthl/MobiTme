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
    public class Employees : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Employee detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            int SiteID,
            string Surname,
            string FirstNames,
            string Title,
            string Country_OfBirth,
            string IdentityNumber,
            string IdentityNumberType,
            string Telephone,
            string Facsimile,
            string Cellular,
            string Email,
            string Physical01,
            string Physical02,
            string Physical03,
            string Physical04,
            string Country_Physical,
            string PhysicalCode,
            string Postal01,
            string Postal02,
            string Postal03,
            string Postal04,
            string Country_Postal,
            string PostalCode,
            string Residential01,
            string Residential02,
            string Residential03,
            string Residential04,
            string Country_Residential,
            string ResidentialCode,
            string NOKSurname,
            string NOKFirstNames,
            string NOKTelephone,
            string NOKCellular,
            string NOKPhysical01,
            string NOKPhysical02,
            string NOKPhysical03,
            string NOKPhysical04,
            string Country_NOKPhysical,
            string NOKPhysicalCode,
            string EmployeeNumber,
            string ClockingNumber,
            DateTime EngagementDate,
            int CostCentreID,
            int DepartmentID,
            int SupervisorID,
            int PositionID,
            int ShiftPatternID,
            DateTime TerminationDate,
            string UserGuid)

        {
            int EmployeeID = 0;
            bool InsertedEmployee = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedEmployee = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (EmployeeID) " +
                                        "From " +
                                            "Employees " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And IdentityNumber = '" + IdentityNumber + "' " +
                                            "And IdentityNumberType = '" + IdentityNumberType + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                                "EmployeeID " +
                                            "From " +
                                                "Employees " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And IdentityNumber = '" + IdentityNumber + "' " +
                                                "And IdentityNumberType = '" + IdentityNumberType + "'";
                        EmployeeID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Employees", "EmployeeID", EmployeeID) == true)
                        {
                            com.CommandText = "Update Employees " +
                                                "Set " +
                                                    "Surname = '" + Surname + "', " +
                                                    "FirstNames = '" + FirstNames + "', " +
                                                    "Title = '" + Title + "', " +
                                                    "Country_OfBirth = " + Country_OfBirth + ", " +
                                                    "IdentityNumber = '" + IdentityNumber + "', " +
                                                    "IdentityNumberType = '" + IdentityNumberType + "', " +
                                                    "Telephone = '" + Telephone + "', " +
                                                    "Facsimile = '" + Facsimile + "', " +
                                                    "Cellular = '" + Cellular + "', " +
                                                    "Email = '" + Email + "', " +
                                                    "Physical01 = '" + Physical01 + "', " +
                                                    "Physical02 = '" + Physical02 + "', " +
                                                    "Physical03 = '" + Physical03 + "', " +
                                                    "Physical04 = '" + Physical04 + "', " +
                                                    "Country_Physical = '" + Country_Physical + "', " +
                                                    "PhysicalCode = '" + PhysicalCode + "', " +
                                                    "Postal01 = '" + Postal01 + "', " +
                                                    "Postal02 = '" + Postal02 + "', " +
                                                    "Postal03 = '" + Postal03 + "', " +
                                                    "Postal04 = '" + Postal04 + "', " +
                                                    "Country_Postal = '" + Country_Postal + "', " +
                                                    "PostalCode = '" + PostalCode + "', " +
                                                    "Residential01 = '" + Residential01 + "', " +
                                                    "Residential02 = '" + Residential02 + "', " +
                                                    "Residential03 = '" + Residential03 + "', " +
                                                    "Residential04 = '" + Residential04 + "', " +
                                                    "Country_Residential = '" + Country_Residential + "', " +
                                                    "ResidentialCode = '" + ResidentialCode + "', " +
                                                    "NOKSurname = '" + NOKSurname + "', " +
                                                    "NOKFirstNames = '" + NOKFirstNames + "', " +
                                                    "NOKTelephone = '" + NOKTelephone + "', " +
                                                    "NOKCellular = '" + NOKCellular + "', " +
                                                    "NOKPhysical01 = '" + NOKPhysical01 + "', " +
                                                    "NOKPhysical02 = '" + NOKPhysical02 + "', " +
                                                    "NOKPhysical03 = '" + NOKPhysical03 + "', " +
                                                    "NOKPhysical04 = '" + NOKPhysical04 + "', " +
                                                    "Country_NOKPhysical = '" + Country_NOKPhysical + "', " +
                                                    "NOKPhysicalCode = '" + NOKPhysicalCode + "', " +
                                                    "EmployeeNumber = '" + EmployeeNumber + "', " +
                                                    "ClockingNumber = '" + ClockingNumber + "', " +
                                                    "EngagementDate = '" + EngagementDate + "', " +
                                                    "CostCentreID = '" + CostCentreID + "', " +
                                                    "DepartmentID = '" + DepartmentID + "', " +
                                                    "SupervisorID = '" + SupervisorID + "', " +
                                                    "PositionID = '" + PositionID + "', " +
                                                    "ShiftPatternID = '" + ShiftPatternID + "', " +
                                                    "TerminationDate = '" + TerminationDate + "', " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "SiteID = " + SiteID + " " +
                                                    "And EmployeeID = '" + EmployeeID +"'";
                            com.ExecuteNonQuery();

                            InsertedID = EmployeeID;
                            InsertedEmployee = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedEmployee = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into Employees (" +
                                                "SiteID, " +
                                                "Surname, " +
                                                "FirstNames, " +
                                                "Title, " +
                                                "Country_OfBirth, " +
                                                "IdentityNumber, " +
                                                "IdentityNumberType, " +
                                                "Telephone, " +
                                                "Facsimile, " +
                                                "Cellular, " +
                                                "Email, " +
                                                "Physical01, " +
                                                "Physical02, " +
                                                "Physical03, " +
                                                "Physical04, " +
                                                "Country_Physical, " +
                                                "PhysicalCode, " +
                                                "Postal01, " +
                                                "Postal02, " +
                                                "Postal03, " +
                                                "Postal04, " +
                                                "Country_Postal, " +
                                                "PostalCode, " +
                                                "Residential01, " +
                                                "Residential02, " +
                                                "Residential03, " +
                                                "Residential04, " +
                                                "Country_Residential, " +
                                                "ResidentialCode, " +
                                                "NOKSurname, " +
                                                "NOKFirstNames, " +
                                                "NOKTelephone, " +
                                                "NOKCellular, " +
                                                "NOKPhysical01, " +
                                                "NOKPhysical02, " +
                                                "NOKPhysical03, " +
                                                "NOKPhysical04, " +
                                                "Country_NOKPhysical, " +
                                                "NOKPhysicalCode, " +
                                                "EmployeeNumber, " +
                                                "ClockingNumber, " +
                                                "EngagementDate, " +
                                                "CostCentreID, " +
                                                "DepartmentID, " +
                                                "SupervisorID, " +
                                                "PositionID, " +
                                                "ShiftPatternID, " +
                                                "TerminationDate, " +
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.EmployeeID " +
                                            "Select " +
                                                "" + SiteID + ", " + 
                                                "'" + Surname + "', " + 
                                                "'" + FirstNames + "', " + 
                                                "'" + Title + "', " + 
                                                "'" + Country_OfBirth + "', " + 
                                                "'" + IdentityNumber + "', " + 
                                                "'" + IdentityNumberType + "', " + 
                                                "'" + Telephone + "', " + 
                                                "'" + Facsimile + "', " + 
                                                "'" + Cellular + "', " + 
                                                "'" + Email + "', " + 
                                                "'" + Physical01 + "', " + 
                                                "'" + Physical02 + "', " + 
                                                "'" + Physical03 + "', " + 
                                                "'" + Physical04 + "', " + 
                                                "'" + Country_Physical + "', " + 
                                                "'" + PhysicalCode + "', " + 
                                                "'" + Postal01 + "', " + 
                                                "'" + Postal02 + "', " + 
                                                "'" + Postal03 + "', " + 
                                                "'" + Postal04 + "', " + 
                                                "'" + Country_Postal + "', " + 
                                                "'" + PostalCode + "', " + 
                                                "'" + Residential01 + "', " + 
                                                "'" + Residential02 + "', " + 
                                                "'" + Residential03 + "', " + 
                                                "'" + Residential04 + "', " + 
                                                "'" + Country_Residential + "', " + 
                                                "'" + ResidentialCode + "', " + 
                                                "'" + NOKSurname + "', " + 
                                                "'" + NOKFirstNames + "', " + 
                                                "'" + NOKTelephone + "', " + 
                                                "'" + NOKCellular + "', " + 
                                                "'" + NOKPhysical01 + "', " + 
                                                "'" + NOKPhysical02 + "', " + 
                                                "'" + NOKPhysical03 + "', " + 
                                                "'" + NOKPhysical04 + "', " + 
                                                "'" + Country_NOKPhysical + "', " + 
                                                "'" + NOKPhysicalCode + "', " + 
                                                "'" + EmployeeNumber + "', " + 
                                                "'" + ClockingNumber + "', " + 
                                                "'" + EngagementDate + "', " + 
                                                "" + CostCentreID + ", " + 
                                                "" + DepartmentID + ", " + 
                                                "" + SupervisorID + ", " + 
                                                "" + PositionID + ", " + 
                                                "" + ShiftPatternID + ", " + 
                                                "'" + TerminationDate + "', " + 
                                                "'" + DateTime.UtcNow + "', " + 
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedEmployee = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedEmployee = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedEmployee = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedEmployee;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of the Employee detail")]
        public List<ReturnData.ReturnEmployeeData> Select(
            string ApplicationPassword,
            int SiteID,
            int EmployeeID)
        {
            ReturnData.ReturnEmployeeData DSReturnEmployees = null;

            SqlDataReader EmployeeList = null;
            List<ReturnData.ReturnEmployeeData> ReturnedEmployees = new List<ReturnData.ReturnEmployeeData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedEmployees = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "emp.EmployeeID As EmployeeID, " +
                                            "emp.SiteID As SiteID, " +
                                            "emp.Surname As Surname, " +
                                            "emp.FirstNames As FirstNames, " +
                                            "emp.Title As Title, " +
                                            "emp.Country_OfBirth As Country_OfBirth, " +
                                            "emp.IdentityNumber As IdentityNumber, " +
                                            "emp.IdentityNumberType As IdentityNumberType, " +
                                            "emp.Telephone As Telephone, " +
                                            "emp.Facsimile As Facsimile, " +
                                            "emp.Cellular As Cellular, " +
                                            "emp.Email As Email, " +
                                            "emp.Physical01 As Physical01, " +
                                            "emp.Physical02 As Physical02, " +
                                            "emp.Physical03 As Physical03, " +
                                            "emp.Physical04 As Physical04, " +
                                            "emp.Country_Physical As Country_Physical, " +
                                            "emp.PhysicalCode As PhysicalCode, " +
                                            "emp.Postal01 As Postal01, " +
                                            "emp.Postal02 As Postal02, " +
                                            "emp.Postal03 As Postal03, " +
                                            "emp.Postal04 As Postal04, " +
                                            "emp.Country_Postal As Country_Postal, " +
                                            "emp.PostalCode As PostalCode, " +
                                            "emp.Residential01 As Residential01, " +
                                            "emp.Residential02 As Residential02, " +
                                            "emp.Residential03 As Residential03, " +
                                            "emp.Residential04 As Residential04, " +
                                            "emp.Country_Residential As Country_Residential, " +
                                            "emp.ResidentialCode As ResidentialCode, " +
                                            "emp.NOKSurname As NOKSurname, " +
                                            "emp.NOKFirstNames As NOKFirstNames, " +
                                            "emp.NOKTelephone As NOKTelephone, " +
                                            "emp.NOKCellular As NOKCellular, " +
                                            "emp.NOKPhysical01 As NOKPhysical01, " +
                                            "emp.NOKPhysical02 As NOKPhysical02, " +
                                            "emp.NOKPhysical03 As NOKPhysical03, " +
                                            "emp.NOKPhysical04 As NOKPhysical04, " +
                                            "emp.Country_NOKPhysical As Country_NOKPhysical, " +
                                            "emp.NOKPhysicalCode As NOKPhysicalCode, " +
                                            "emp.EmployeeNumber As EmployeeNumber, " +
                                            "emp.ClockingNumber As ClockingNumber, " +
                                            "emp.EngagementDate As EngagementDate, " +
                                            "emp.CostCentreID As CostCentre, " +
                                            "emp.DepartmentID As Department, " +
                                            "emp.SupervisorID As Supervisor, " +
                                            "emp.PositionID As Position, " +
                                            "emp.ShiftPatternID As ShiftPattern, " +
                                            "emp.TerminationDate " +
                                        "From " +
                                            "Employees emp " +
                                                "Left Outer Join CostCentres cst on emp.CostCentreID = cst.CostCentreID " + 
                                                    "And emp.SiteID = cst.SiteID " + 
                                                "Left Outer Join Departments dep on emp.DepartmentID = dep.DepartmentID " + 
                                                    "And emp.SiteID = dep.SiteID " + 
                                                "Left Outer Join Supervisors sup on emp.SupervisorID = sup.SupervisorID " + 
                                                    "And emp.SiteID = sup.SiteID " + 
                                                "Left Outer Join Positions pos on emp.PositionID = pos.PositionID " + 
                                                    "And emp.SiteID = pos.SiteID " + 
                                                "Left Outer Join ShiftPatterns spt on emp.ShiftPatternID = spt.ShiftPatternID " + 
                                                    "And emp.SiteID = spt.SiteID " +
                                        "Where " +
                                            "emp.SiteID = " + SiteID + " " +
                                            "And emp.EmployeeID = " + EmployeeID + " " +
                                            "And emp.DeletedAtUTC is null";
                    EmployeeList = com.ExecuteReader();

                    while (EmployeeList.Read())
                    {
                        DSReturnEmployees = new ReturnData.ReturnEmployeeData();

                        DSReturnEmployees.SiteID = EmployeeList["SiteID"].ToString();
                        DSReturnEmployees.Surname = EmployeeList["Surname"].ToString();
                        DSReturnEmployees.FirstNames = EmployeeList["FirstNames"].ToString();
                        DSReturnEmployees.Title = EmployeeList["Title"].ToString();
                        DSReturnEmployees.Country_OfBirth = EmployeeList["Country_OfBirth"].ToString();
                        DSReturnEmployees.IdentityNumber = EmployeeList["IdentityNumber"].ToString();
                        DSReturnEmployees.IdentityNumberType = EmployeeList["IdentityNumberType"].ToString();
                        DSReturnEmployees.Telephone = EmployeeList["Telephone"].ToString();
                        DSReturnEmployees.Facsimile = EmployeeList["Facsimile"].ToString();
                        DSReturnEmployees.Cellular = EmployeeList["Cellular"].ToString();
                        DSReturnEmployees.Email = EmployeeList["Email"].ToString();
                        DSReturnEmployees.Physical01 = EmployeeList["Physical01"].ToString();
                        DSReturnEmployees.Physical02 = EmployeeList["Physical02"].ToString();
                        DSReturnEmployees.Physical03 = EmployeeList["Physical03"].ToString();
                        DSReturnEmployees.Physical04 = EmployeeList["Physical04"].ToString();
                        DSReturnEmployees.Country_Physical = EmployeeList["Country_Physical"].ToString();
                        DSReturnEmployees.PhysicalCode = EmployeeList["PhysicalCode"].ToString();
                        DSReturnEmployees.Postal01 = EmployeeList["Postal01"].ToString();
                        DSReturnEmployees.Postal02 = EmployeeList["Postal02"].ToString();
                        DSReturnEmployees.Postal03 = EmployeeList["Postal03"].ToString();
                        DSReturnEmployees.Postal04 = EmployeeList["Postal04"].ToString();
                        DSReturnEmployees.Country_Postal = EmployeeList["Country_Postal"].ToString();
                        DSReturnEmployees.PostalCode = EmployeeList["PostalCode"].ToString();
                        DSReturnEmployees.Residential01 = EmployeeList["Residential01"].ToString();
                        DSReturnEmployees.Residential02 = EmployeeList["Residential02"].ToString();
                        DSReturnEmployees.Residential03 = EmployeeList["Residential03"].ToString();
                        DSReturnEmployees.Residential04 = EmployeeList["Residential04"].ToString();
                        DSReturnEmployees.Country_Residential = EmployeeList["Country_Residential"].ToString();
                        DSReturnEmployees.ResidentialCode = EmployeeList["ResidentialCode"].ToString();
                        DSReturnEmployees.NOKSurname = EmployeeList["NOKSurname"].ToString();
                        DSReturnEmployees.NOKFirstNames = EmployeeList["NOKFirstNames"].ToString();
                        DSReturnEmployees.NOKTelephone = EmployeeList["NOKTelephone"].ToString();
                        DSReturnEmployees.NOKCellular = EmployeeList["NOKCellular"].ToString();
                        DSReturnEmployees.NOKPhysical01 = EmployeeList["NOKPhysical01"].ToString();
                        DSReturnEmployees.NOKPhysical02 = EmployeeList["NOKPhysical02"].ToString();
                        DSReturnEmployees.NOKPhysical03 = EmployeeList["NOKPhysical03"].ToString();
                        DSReturnEmployees.NOKPhysical04 = EmployeeList["NOKPhysical04"].ToString();
                        DSReturnEmployees.Country_NOKPhysical = EmployeeList["Country_NOKPhysical"].ToString();
                        DSReturnEmployees.NOKPhysicalCode = EmployeeList["NOKPhysicalCode"].ToString();
                        DSReturnEmployees.EmployeeNumber = EmployeeList["EmployeeNumber"].ToString();
                        DSReturnEmployees.ClockingNumber = EmployeeList["ClockingNumber"].ToString();
                        DSReturnEmployees.EngagementDate = EmployeeList["EngagementDate"].ToString();
                        DSReturnEmployees.CostCentre = EmployeeList["CostCentre"].ToString();
                        DSReturnEmployees.Department = EmployeeList["Department"].ToString();
                        DSReturnEmployees.Supervisor = EmployeeList["Supervisor"].ToString();
                        DSReturnEmployees.Position = EmployeeList["Position"].ToString();
                        DSReturnEmployees.ShiftPattern = EmployeeList["ShiftPattern"].ToString();
                        DSReturnEmployees.TerminationDate = EmployeeList["TerminationDate"].ToString();

                        ReturnedEmployees.Add(DSReturnEmployees);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedEmployees = null;
            }
            catch (Exception Ex)
            {
                ReturnedEmployees = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedEmployees;
        }




        [WebMethod(Description = "Mobitime Web Service - Select an Employee Detail List from MobiTime")]
        public List<ReturnData.ReturnEmployeeData> ListEmployees(
            string ApplicationPassword,
            int SiteID)
        {
            ReturnData.ReturnEmployeeData DSReturnEmployees = null;

            SqlDataReader EmployeeList = null;
            List<ReturnData.ReturnEmployeeData> ReturnedEmployees = new List<ReturnData.ReturnEmployeeData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedEmployees = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "EmployeeID, " + 
                                            "Surname, " +
                                            "FirstNames, " +
                                            "Title, " +
                                            "Country_OfBirth, " +
                                            "IdentityNumber, " +
                                            "IdentityNumberType, " +
                                            "Telephone, " +
                                            "Facsimile, " +
                                            "Cellular, " +
                                            "Email, " +
                                            "EngagementDate, " + 
                                            "TerminationDate " + 
                                        "From " +
                                            "Employees " +
                                        "Where " +
                                            "SiteID = " + SiteID + " " +
                                            "And DeletedAtUTC is null " +
                                        "Order By " +
                                            "Surname, FirstNames";
                    EmployeeList = com.ExecuteReader();

                    while (EmployeeList.Read())
                    {
                        DSReturnEmployees = new ReturnData.ReturnEmployeeData();

                        DSReturnEmployees.EmployeeID = EmployeeList["EmployeeID"].ToString();
                        DSReturnEmployees.Surname = EmployeeList["Surname"].ToString();
                        DSReturnEmployees.FirstNames = EmployeeList["FirstNames"].ToString();
                        DSReturnEmployees.Title = EmployeeList["Title"].ToString();
                        DSReturnEmployees.Country_OfBirth = EmployeeList["Country_OfBirth"].ToString();
                        DSReturnEmployees.IdentityNumber = EmployeeList["IdentityNumber"].ToString();
                        DSReturnEmployees.IdentityNumberType = EmployeeList["IdentityNumberType"].ToString();
                        DSReturnEmployees.Telephone = EmployeeList["Telephone"].ToString();
                        DSReturnEmployees.Facsimile = EmployeeList["Facsimile"].ToString();
                        DSReturnEmployees.Cellular = EmployeeList["Cellular"].ToString();
                        DSReturnEmployees.Email = EmployeeList["Email"].ToString();
                        DSReturnEmployees.EngagementDate = EmployeeList["EngagementDate"].ToString();
                        DSReturnEmployees.TerminationDate = EmployeeList["TerminationDate"].ToString();

                        ReturnedEmployees.Add(DSReturnEmployees);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedEmployees = null;
            }
            catch (Exception Ex)
            {
                ReturnedEmployees = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedEmployees;
        }




        [WebMethod(Description = "Mobitime Web Service - Update Employee detail in MobiTime")]
        public bool Update(
            string ApplicationPassword,
            int EmployeeID,
            int SiteID,
            string Surname,
            string FirstNames,
            string Title,
            string Country_OfBirth,
            string IdentityNumber,
            string IdentityNumberType,
            string Telephone,
            string Facsimile,
            string Cellular,
            string Email,
            string Physical01,
            string Physical02,
            string Physical03,
            string Physical04,
            string Country_Physical,
            string PhysicalCode,
            string Postal01,
            string Postal02,
            string Postal03,
            string Postal04,
            string Country_Postal,
            string PostalCode,
            string Residential01,
            string Residential02,
            string Residential03,
            string Residential04,
            string Country_Residential,
            string ResidentialCode,
            string NOKSurname,
            string NOKFirstNames,
            string NOKTelephone,
            string NOKCellular,
            string NOKPhysical01,
            string NOKPhysical02,
            string NOKPhysical03,
            string NOKPhysical04,
            string Country_NOKPhysical,
            string NOKPhysicalCode,
            string EmployeeNumber,
            string ClockingNumber,
            DateTime EngagementDate,
            int CostCentreID,
            int DepartmentID,
            int SupervisorID,
            int PositionID,
            int ShiftPatternID,
            DateTime TerminationDate,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Employees", "EmployeeID", EmployeeID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Employees " +
                                            "Set " +
                                                    "Surname = '" + Surname + "', " +
                                                    "FirstNames = '" + FirstNames + "', " +
                                                    "Title = '" + Title + "', " +
                                                    "Country_OfBirth = " + Country_OfBirth + ", " +
                                                    "IdentityNumber = '" + IdentityNumber + "', " +
                                                    "IdentityNumberType = '" + IdentityNumberType + "', " +
                                                    "Telephone = '" + Telephone + "', " +
                                                    "Facsimile = '" + Facsimile + "', " +
                                                    "Cellular = '" + Cellular + "', " +
                                                    "Email = '" + Email + "', " +
                                                    "Physical01 = '" + Physical01 + "', " +
                                                    "Physical02 = '" + Physical02 + "', " +
                                                    "Physical03 = '" + Physical03 + "', " +
                                                    "Physical04 = '" + Physical04 + "', " +
                                                    "Country_Physical = '" + Country_Physical + "', " +
                                                    "PhysicalCode = '" + PhysicalCode + "', " +
                                                    "Postal01 = '" + Postal01 + "', " +
                                                    "Postal02 = '" + Postal02 + "', " +
                                                    "Postal03 = '" + Postal03 + "', " +
                                                    "Postal04 = '" + Postal04 + "', " +
                                                    "Country_Postal = '" + Country_Postal + "', " +
                                                    "PostalCode = '" + PostalCode + "', " +
                                                    "Residential01 = '" + Residential01 + "', " +
                                                    "Residential02 = '" + Residential02 + "', " +
                                                    "Residential03 = '" + Residential03 + "', " +
                                                    "Residential04 = '" + Residential04 + "', " +
                                                    "Country_Residential = '" + Country_Residential + "', " +
                                                    "ResidentialCode = '" + ResidentialCode + "', " +
                                                    "NOKSurname = '" + NOKSurname + "', " +
                                                    "NOKFirstNames = '" + NOKFirstNames + "', " +
                                                    "NOKTelephone = '" + NOKTelephone + "', " +
                                                    "NOKCellular = '" + NOKCellular + "', " +
                                                    "NOKPhysical01 = '" + NOKPhysical01 + "', " +
                                                    "NOKPhysical02 = '" + NOKPhysical02 + "', " +
                                                    "NOKPhysical03 = '" + NOKPhysical03 + "', " +
                                                    "NOKPhysical04 = '" + NOKPhysical04 + "', " +
                                                    "Country_NOKPhysical = '" + Country_NOKPhysical + "', " +
                                                    "NOKPhysicalCode = '" + NOKPhysicalCode + "', " +
                                                    "EmployeeNumber = '" + EmployeeNumber + "', " +
                                                    "ClockingNumber = '" + ClockingNumber + "', " +
                                                    "EngagementDate = '" + EngagementDate + "', " +
                                                    "CostCentreID = '" + CostCentreID + "', " +
                                                    "DepartmentID = '" + DepartmentID + "', " +
                                                    "SupervisorID = '" + SupervisorID + "', " +
                                                    "PositionID = '" + PositionID + "', " +
                                                    "ShiftPatternID = '" + ShiftPatternID + "', " +
                                                    "TerminationDate = '" + TerminationDate + "', " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "SiteID = " + SiteID + " " +
                                                    "And EmployeeID = '" + EmployeeID + "'";
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




        [WebMethod(Description = "Mobitime Web Service - Delete an Employee from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int SiteID,
            int EmployeeID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Employees", "EmployeeID", EmployeeID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Employees " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And EmployeeID = " + EmployeeID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Recover an Employee in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int SiteID,
            int EmployeeID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Employees", "EmployeeID", EmployeeID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Employees " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "SiteID = " + SiteID + " " +
                                                "And EmployeeID = " + EmployeeID + "";
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

