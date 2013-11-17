using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiTme.Web.Member.Tiles.LiveView
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [System.Web.Services.WebMethod]
        public static string ListLiveView(string siteID, string NumberOfClockings)
        {
            StringBuilder sbResult = new StringBuilder();

            var listData = new List<MobiTime.ReturnData.ReturnInboundClockingData>();

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();
            var wsService = new MobiTime.WebServices.InboundClocking();

            listData = wsService.ViewCurrentClockings(ApplicationPassword, int.Parse(siteID), int.Parse(NumberOfClockings));


 

            foreach (MobiTime.ReturnData.ReturnInboundClockingData dataItem in listData)
            {
                sbResult.Append("<tr class='row-select'>");

                sbResult.Append("<td>" + dataItem.FirstNames + "</td>");
                sbResult.Append("<td>" + dataItem.Surname + "</td>");
                sbResult.Append("<td>" + dataItem.Cellular + "</td>");
                sbResult.Append("<td>" + dataItem.ClockingNumber + "</td>");
                sbResult.Append("<td>" + dataItem.ClockingDate + "</td>");
                sbResult.Append("</tr>");
            }
            return sbResult.ToString();

        }
    }
}