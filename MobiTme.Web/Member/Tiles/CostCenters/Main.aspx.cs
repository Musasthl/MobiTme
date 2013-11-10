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

namespace MobiTme.Member.Tiles.CostCenters
{
    public partial class Main : System.Web.UI.Page
    {

        public class CostCenter
        {

            public string SiteID { get; set; }
            public string CostCentreID { get; set; }
            public string CostCentre { get; set; }
            public string PayrollCode { get; set; }
            public string AccountsCode { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string Insert(CostCenter costCenter)
        {
            bool cmdStatus = false;
            var wsCostCenters = new MobiTime.WebServices.CostCentres();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            cmdStatus = wsCostCenters.Insert(
             ApplicationPassword,
               int.Parse(costCenter.SiteID),
             costCenter.CostCentre,
             costCenter.PayrollCode,
             costCenter.AccountsCode,
             UserGuid);

            return cmdStatus.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string Select(string SiteID, string KeyID)
        { 
    
            var wsCostCenters = new MobiTime.WebServices.CostCentres();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            List<MobiTime.ReturnData.ReturnCostCentreData> dataResult = wsCostCenters.Select(ApplicationPassword, int.Parse(SiteID), int.Parse(KeyID));
            return JsonConvert.SerializeObject(dataResult);
        }

        [System.Web.Services.WebMethod]
        public static string Delete(string SiteID, string KeyID)
        {
            var wsCostCenters = new MobiTime.WebServices.CostCentres();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            return wsCostCenters.Delete(ApplicationPassword, int.Parse(SiteID), int.Parse(KeyID), UserGuid).ToString();
        }

        [System.Web.Services.WebMethod]
        public static string Update(CostCenter costCenter)
        {
            bool cmdStatus = false;
            var wsCostCenters = new MobiTime.WebServices.CostCentres();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            cmdStatus = wsCostCenters.Update(
                  ApplicationPassword,
                  int.Parse(costCenter.SiteID),
                 int.Parse(costCenter.CostCentreID),
                 costCenter.CostCentre,
                 costCenter.PayrollCode,
                 costCenter.AccountsCode,
                  UserGuid);


            return cmdStatus.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string ListCostCentres(string siteID)
        {
            StringBuilder sbResult = new StringBuilder();

            var ListCostCentres = new List<MobiTime.ReturnData.ReturnCostCentreData>();

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();
            var wsCostCenters = new MobiTime.WebServices.CostCentres();

            ListCostCentres = wsCostCenters.ListCostCentres(ApplicationPassword, int.Parse(siteID));

            foreach (ReturnCostCentreData costCenters in ListCostCentres)
            {
                sbResult.Append("<tr class='row-select'>");
                sbResult.Append("<td class='key hidden'>" + costCenters.CostCentreID + "</td>");
                sbResult.Append("<td>" + costCenters.CostCentre + "</td>");
                sbResult.Append("<td>" + costCenters.PayrollCode + "</td>");
                sbResult.Append("<td>" + costCenters.AccountsCode + "</td>");
                sbResult.Append("</tr>");
            }
            return sbResult.ToString();
        }
    }
}