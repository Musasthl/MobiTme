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

namespace MobiTme.Member.Tiles.Supervisors
{
    public partial class Main : System.Web.UI.Page
    {

        public class Supervisor
        {

            public string SiteID { get; set; }
            public string SupervisorID { get; set; }
            public string SupervisorName { get; set; }
            public string PayrollCode { get; set; }
            public string AccountsCode { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string Insert(Supervisor supervisor)
        {
            bool cmdStatus = false;
            var wsSupervisors = new MobiTime.WebServices.Supervisors();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();
            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();
           
            cmdStatus = wsSupervisors.Insert(
             ApplicationPassword,
               int.Parse(supervisor.SiteID),
             supervisor.SupervisorName,
             supervisor.PayrollCode,
             supervisor.AccountsCode,
             UserGuid);

            return cmdStatus.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string Select(string SiteID, string KeyID)
        { 
    
            var wsSupervisor = new MobiTime.WebServices.Supervisors();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();
            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();

            List<MobiTime.ReturnData.ReturnSupervisorData> dataResult = wsSupervisor.Select(ApplicationPassword, int.Parse(SiteID), int.Parse(KeyID));
            return JsonConvert.SerializeObject(dataResult);
        }

        [System.Web.Services.WebMethod]
        public static string Delete(string SiteID, string KeyID)
        {
            var wsSupervisor = new MobiTime.WebServices.Supervisors();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();
            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();

            return wsSupervisor.Delete(ApplicationPassword, int.Parse(SiteID), int.Parse(KeyID), UserGuid).ToString();
        }

        [System.Web.Services.WebMethod]
        public static string Update(Supervisor supervisor)
        {
            bool cmdStatus = false;
            var wsCostCenters = new MobiTime.WebServices.Supervisors();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();
            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();

            cmdStatus = wsCostCenters.Update(
                  ApplicationPassword,
                  int.Parse(supervisor.SiteID),
                 int.Parse(supervisor.SupervisorID),
                 supervisor.SupervisorName,
                 supervisor.PayrollCode,
                 supervisor.AccountsCode,
                  UserGuid);


            return cmdStatus.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string ListSupervisors(string siteID)
        {
            StringBuilder sbResult = new StringBuilder();

            var ListSupervisors = new List<MobiTime.ReturnData.ReturnSupervisorData>();

            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();
            var wsSupervisor = new MobiTime.WebServices.Supervisors();

            ListSupervisors = wsSupervisor.ListSupervisors(ApplicationPassword, int.Parse(siteID));

            foreach (ReturnSupervisorData Supervisors in ListSupervisors)
            {
                sbResult.Append("<tr class='row-select'>");
                sbResult.Append("<td class='key hidden'>" + Supervisors.SupervisorID + "</td>");
                sbResult.Append("<td>" + Supervisors.Supervisor + "</td>");
                sbResult.Append("<td>" + Supervisors.PayrollCode + "</td>");
                sbResult.Append("<td>" + Supervisors.AccountsCode + "</td>");
                sbResult.Append("</tr>");
            }
            return sbResult.ToString();
        }
    }
}