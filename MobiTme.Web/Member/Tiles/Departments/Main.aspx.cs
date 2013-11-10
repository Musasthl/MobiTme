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

namespace MobiTme.Member.Tiles.Departments
{
    public partial class Main : System.Web.UI.Page
    {

        public class Department
        {

            public string SiteID { get; set; }
            public string DepartmentID { get; set; }
            public string DepartmentName { get; set; }
            public string PayrollCode { get; set; }
            public string AccountsCode { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string Insert(Department department)
        {
            bool cmdStatus = false;
            var wsDepartments = new MobiTime.WebServices.Departments();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();
           
            cmdStatus = wsDepartments.Insert(
             ApplicationPassword,
               int.Parse(department.SiteID),
             department.DepartmentName,
             department.PayrollCode,
             department.AccountsCode,
             UserGuid);

            return cmdStatus.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string Select(string SiteID, string KeyID)
        { 
    
            var wsDepartment = new MobiTime.WebServices.Departments();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            List<MobiTime.ReturnData.ReturnDepartmentData> dataResult = wsDepartment.Select(ApplicationPassword, int.Parse(SiteID), int.Parse(KeyID));
            return JsonConvert.SerializeObject(dataResult);
        }

        [System.Web.Services.WebMethod]
        public static string Delete(string SiteID, string KeyID)
        {
            var wsDepartment = new MobiTime.WebServices.Departments();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            return wsDepartment.Delete(ApplicationPassword, int.Parse(SiteID), int.Parse(KeyID), UserGuid).ToString();
        }

        [System.Web.Services.WebMethod]
        public static string Update(Department department)
        {
            bool cmdStatus = false;
            var wsCostCenters = new MobiTime.WebServices.Departments();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            cmdStatus = wsCostCenters.Update(
                  ApplicationPassword,
                  int.Parse(department.SiteID),
                 int.Parse(department.DepartmentID),
                 department.DepartmentName,
                 department.PayrollCode,
                 department.AccountsCode,
                  UserGuid);


            return cmdStatus.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string ListDepartments(string siteID)
        {
            StringBuilder sbResult = new StringBuilder();

            var ListDepartments = new List<MobiTime.ReturnData.ReturnDepartmentData>();

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();
            var wsDepartments = new MobiTime.WebServices.Departments();

            ListDepartments = wsDepartments.ListDepartments(ApplicationPassword, int.Parse(siteID));

            foreach (ReturnDepartmentData departments in ListDepartments)
            {
                sbResult.Append("<tr class='row-select'>");
                sbResult.Append("<td class='key hidden'>" + departments.DepartmentID + "</td>");
                sbResult.Append("<td>" + departments.Department + "</td>");
                sbResult.Append("<td>" + departments.PayrollCode + "</td>");
                sbResult.Append("<td>" + departments.AccountsCode + "</td>");
                sbResult.Append("</tr>");
            }
            return sbResult.ToString();
        }
    }
}