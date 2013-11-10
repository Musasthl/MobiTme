using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace MobiTme.Web.Member.Tiles.ShiftPatterns
{
    public partial class Main : System.Web.UI.Page
    {

        public class ShiftPattern
        {
            public string ClientID { get; set; }
            public string SiteID { get; set; }
            public string ShiftPatternID { get; set; }
            public string ShiftPatternName { get; set; }
            public string ShiftPatternStarts { get; set; }
            public string OvertimeCycleStarts { get; set; }
            public string OvertimeCycleDays { get; set; }
        }


        [System.Web.Services.WebMethod]
        public static string Insert(ShiftPattern shiftPattern)
        {
            shiftPattern.ClientID = "0";
            bool cmdStatus = false;
            var wsShiftPattern = new MobiTime.WebServices.ShiftPatterns();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            cmdStatus = wsShiftPattern.Insert(
                  ApplicationPassword,
            int.Parse(shiftPattern.ClientID),
            int.Parse(shiftPattern.SiteID),
            UserGuid,
            shiftPattern.ShiftPatternName,
            DateTime.Parse(shiftPattern.ShiftPatternStarts),
            DateTime.Parse(shiftPattern.OvertimeCycleStarts),
            int.Parse(shiftPattern.OvertimeCycleDays));

            return cmdStatus.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string Update(ShiftPattern shiftPattern)
        {
            bool cmdStatus = false;
            shiftPattern.ClientID = "0";
            var wsShiftPatterns = new MobiTime.WebServices.ShiftPatterns();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            cmdStatus = wsShiftPatterns.Update(
                  ApplicationPassword,
            int.Parse(shiftPattern.ClientID),
            int.Parse(shiftPattern.SiteID),
            UserGuid,
                  int.Parse(shiftPattern.ShiftPatternID),
            shiftPattern.ShiftPatternName,
            DateTime.Parse(shiftPattern.ShiftPatternStarts),
            DateTime.Parse(shiftPattern.OvertimeCycleStarts),
            int.Parse(shiftPattern.OvertimeCycleDays));




            return cmdStatus.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string Select(string SiteID, string KeyID)
        {




            var wsShiftPatterns = new MobiTime.WebServices.ShiftPatterns();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            List<MobiTime.ReturnData.ReturnShiftPatternData> dataResult = wsShiftPatterns.Select(ApplicationPassword, int.Parse(SiteID), UserGuid, int.Parse(KeyID));
            return JsonConvert.SerializeObject(dataResult);
        }

        [System.Web.Services.WebMethod]
        public static string ListShiftPatterns(string siteID)
        {
            StringBuilder sbResult = new StringBuilder();

            var listData = new List<MobiTime.ReturnData.ReturnShiftPatternData>();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();
            var wsShiftPatterns = new MobiTime.WebServices.ShiftPatterns();

            listData = wsShiftPatterns.List(ApplicationPassword, int.Parse(siteID), UserGuid);

            foreach (MobiTime.ReturnData.ReturnShiftPatternData dataItem in listData)
            {
                sbResult.Append("<tr class='row-select'>");
                sbResult.Append("<td class='key hidden'>" + dataItem.ShiftPatternID + "</td>");
                sbResult.Append("<td>" + dataItem.ShiftPattern + "</td>");
                sbResult.Append("<td>" + dataItem.ShiftPatternStarts + "</td>");
                sbResult.Append("<td>" + dataItem.OvertimeCycleStarts + "</td>");
                sbResult.Append("<td>" + dataItem.OvertimeCycleDays + "</td>");
                sbResult.Append("</tr>");
            }


            return sbResult.ToString();
        }
    }
}