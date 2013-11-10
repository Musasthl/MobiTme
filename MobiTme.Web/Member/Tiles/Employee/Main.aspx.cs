using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobiTime.ReturnData;
using MobiTime.WebServices;
using Newtonsoft.Json;

namespace MobiTme.Web.Member.Tiles.Employee
{
    public partial class Main : System.Web.UI.Page
    {
        public class Employee
        {
            public string EmployeeID { get; set; }
            public string SiteID { get; set; }
            public string Surname { get; set; }
            public string FirstNames { get; set; }
            public string Title { get; set; }
            public string Country_OfBirth { get; set; }
            public string IdentityNumber { get; set; }
            public string IdentityNumberType { get; set; }
            public string Telephone { get; set; }
            public string Facsimile { get; set; }
            public string Cellular { get; set; }
            public string Email { get; set; }
            public string Physical01 { get; set; }
            public string Physical02 { get; set; }
            public string Physical03 { get; set; }
            public string Physical04 { get; set; }
            public string Country_Physical { get; set; }
            public string PhysicalCode { get; set; }
            public string Postal01 { get; set; }
            public string Postal02 { get; set; }
            public string Postal03 { get; set; }
            public string Postal04 { get; set; }
            public string Country_Postal { get; set; }
            public string PostalCode { get; set; }
            public string Residential01 { get; set; }
            public string Residential02 { get; set; }
            public string Residential03 { get; set; }
            public string Residential04 { get; set; }
            public string Country_Residential { get; set; }
            public string ResidentialCode { get; set; }
            public string NOKSurname { get; set; }
            public string NOKFirstNames { get; set; }
            public string NOKTelephone { get; set; }
            public string NOKCellular { get; set; }
            public string NOKPhysical01 { get; set; }
            public string NOKPhysical02 { get; set; }
            public string NOKPhysical03 { get; set; }
            public string NOKPhysical04 { get; set; }
            public string Country_NOKPhysical { get; set; }
            public string NOKPhysicalCode { get; set; }
            public string EmployeeNumber { get; set; }
            public string ClockingNumber { get; set; }
            public string EngagementDate { get; set; }
            public string CostCentreID { get; set; }
            public string DepartmentID { get; set; }
            public string SupervisorID { get; set; }
            public string PositionID { get; set; }
            public string ShiftPatternID { get; set; }
            public string TerminationDate { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (Page.Request.QueryString["SITEID"] != null)
                {
                    int siteID = int.Parse(Request.QueryString["SITEID"].ToString());
                    string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();
                    string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();
                    var listDepartments = new Departments().ListDepartments(ApplicationPassword, siteID);
                    var listPoistions = new Positions().ListPositions(ApplicationPassword, siteID);
                    var listCostCentres = new CostCentres().ListCostCentres(ApplicationPassword, siteID);
                    var listSupervisors = new Supervisors().ListSupervisors(ApplicationPassword, siteID);
                    var listShiftPatterns = new MobiTime.WebServices.ShiftPatterns().List(ApplicationPassword, siteID, UserGuid);


                    tbCostCentre.Items.Add(new ListItem("Select cost centre", ""));
                    foreach (var costCenter in listCostCentres)
                    {
                        tbCostCentre.Items.Add(new ListItem(costCenter.CostCentre, costCenter.CostCentreID)); 
                    }
 
 
                    tbSupervisor.Items.Add(new ListItem("Select supervisor", ""));
                    foreach (var supervisor in listSupervisors)
                    {
                        tbSupervisor.Items.Add(new ListItem(supervisor.Supervisor, supervisor.SupervisorID));
                    }


                    tbPosition.Items.Add(new ListItem("Select position", ""));
                    foreach (var position in listPoistions)
                    {
                        tbPosition.Items.Add(new ListItem(position.Position, position.PositionID));

                    }

                    tbDepartment.Items.Add(new ListItem("Select department", ""));
                    foreach (var department in listDepartments)
                    {
                        tbDepartment.Items.Add(new ListItem(department.Department, department.DepartmentID)); 
                    }


                    tbShiftPattern.Items.Add(new ListItem("Select shift pattern", ""));
                    foreach (var shiftPattern in listShiftPatterns)
                    {
                        tbShiftPattern.Items.Add(new ListItem(shiftPattern.ShiftPattern, shiftPattern.ShiftPatternID));
                    }
  
                }
            }


        }

        [System.Web.Services.WebMethod]
        public static string Insert(Employee employee)
        {
            bool cmdStatus = false;
            var wsEmployees = new Employees();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();
            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();

            cmdStatus = wsEmployees.Insert(
             ApplicationPassword,
               int.Parse(employee.SiteID),
             employee.Surname,
             employee.FirstNames,
             employee.Title,
             employee.Country_OfBirth,
             employee.IdentityNumber,
             employee.IdentityNumberType,
             employee.Telephone,
             employee.Facsimile,
             employee.Cellular,
             employee.Email,
             employee.Physical01,
             employee.Physical02,
            employee.Physical03,
             employee.Physical04,
             employee.Country_Physical,
             employee.PhysicalCode,
             employee.Postal01,
             employee.Postal02,
             employee.Postal03,
             employee.Postal04,
             employee.Country_Postal,
             employee.PostalCode,
             employee.Residential01,
             employee.Residential02,
             employee.Residential03,
             employee.Residential04,
             employee.Country_Residential,
             employee.ResidentialCode,
             employee.NOKSurname,
             employee.NOKFirstNames,
             employee.NOKTelephone,
             employee.NOKCellular,
             employee.NOKPhysical01,
             employee.NOKPhysical02,
             employee.NOKPhysical03,
             employee.NOKPhysical04,
             employee.Country_NOKPhysical,
             employee.NOKPhysicalCode,
             employee.EmployeeNumber,
             employee.ClockingNumber,
              DateTime.Parse(employee.EngagementDate),
              int.Parse(employee.CostCentreID),
              int.Parse(employee.DepartmentID),
              int.Parse(employee.SupervisorID),
              int.Parse(employee.PositionID),
              int.Parse(employee.ShiftPatternID),
              DateTime.Parse(employee.TerminationDate),
             UserGuid);

            return cmdStatus.ToString(); ;

        }
        [System.Web.Services.WebMethod]
        public static string Update(Employee employee)
        {
            bool cmdStatus = false;
            var wsEmployees = new Employees();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();
            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();

            cmdStatus = wsEmployees.Update(
              ApplicationPassword,
                 int.Parse(employee.EmployeeID),
                int.Parse(employee.SiteID),
              employee.Surname,
              employee.FirstNames,
              employee.Title,
              employee.Country_OfBirth,
              employee.IdentityNumber,
              employee.IdentityNumberType,
              employee.Telephone,
              employee.Facsimile,
              employee.Cellular,
              employee.Email,
              employee.Physical01,
              employee.Physical02,
             employee.Physical03,
              employee.Physical04,
              employee.Country_Physical,
              employee.PhysicalCode,
              employee.Postal01,
              employee.Postal02,
              employee.Postal03,
              employee.Postal04,
              employee.Country_Postal,
              employee.PostalCode,
              employee.Residential01,
              employee.Residential02,
              employee.Residential03,
              employee.Residential04,
              employee.Country_Residential,
              employee.ResidentialCode,
              employee.NOKSurname,
              employee.NOKFirstNames,
              employee.NOKTelephone,
              employee.NOKCellular,
              employee.NOKPhysical01,
              employee.NOKPhysical02,
              employee.NOKPhysical03,
              employee.NOKPhysical04,
              employee.Country_NOKPhysical,
              employee.NOKPhysicalCode,
              employee.EmployeeNumber,
              employee.ClockingNumber,
               DateTime.Parse(employee.EngagementDate),
               int.Parse(employee.CostCentreID),
               int.Parse(employee.DepartmentID),
               int.Parse(employee.SupervisorID),
               int.Parse(employee.PositionID),
               int.Parse(employee.ShiftPatternID),
               DateTime.Parse(employee.TerminationDate),
              UserGuid);


            return cmdStatus.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string ListEmployees(string siteID)
        {
            StringBuilder sbResult = new StringBuilder();
            var ListEmployees = new List<MobiTime.ReturnData.ReturnEmployeeData>();

            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();
            var wsEmployees = new Employees();

            ListEmployees = wsEmployees.ListEmployees(ApplicationPassword, int.Parse(siteID));

            foreach (ReturnEmployeeData employeeData in ListEmployees)
            {
                sbResult.Append("<tr class='row-select'>");
                sbResult.Append("<td class='key hidden'>" + employeeData.EmployeeID + "</td>");
                sbResult.Append("<td>" + employeeData.Surname + "</td>");
                sbResult.Append("<td>" + employeeData.FirstNames + "</td>");
                sbResult.Append("<td>" + employeeData.Title + "</td>");
                sbResult.Append("<td>" + employeeData.Country_OfBirth + "</td>");
                sbResult.Append("<td>" + employeeData.IdentityNumber + "</td>");
                sbResult.Append("<td>" + employeeData.IdentityNumberType + "</td>");
                sbResult.Append("<td>" + employeeData.Telephone + "</td>");
                sbResult.Append("<td>" + employeeData.Facsimile + "</td>");
                sbResult.Append("<td>" + employeeData.Cellular + "</td>");
                sbResult.Append("<td>" + employeeData.Email + "</td>");
                sbResult.Append("<td>" + employeeData.EngagementDate + "</td>");
                sbResult.Append("<td>" + employeeData.TerminationDate + "</td>");
                sbResult.Append("</tr>");
            }
            return sbResult.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string Select(string SiteID, string KeyID)
        {

            var wsEmployees = new MobiTime.WebServices.Employees();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();
            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();

            List<ReturnEmployeeData> dataResult = wsEmployees.Select(ApplicationPassword, int.Parse(SiteID), int.Parse(KeyID));
            return JsonConvert.SerializeObject(dataResult);
        }

        [System.Web.Services.WebMethod]
        public static string Delete(string SiteID, string KeyID)
        {

            var wsEmployees = new MobiTime.WebServices.Employees();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();
            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();

            return wsEmployees.Delete(ApplicationPassword, int.Parse(SiteID), int.Parse(KeyID), UserGuid).ToString();

        }
    }
}