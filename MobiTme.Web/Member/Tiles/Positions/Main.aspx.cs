using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobiTime.ReturnData;
using Newtonsoft.Json;

namespace MobiTme.Member.Tiles.Positions
{
    public partial class Main : System.Web.UI.Page
    {


        public class Position
        {

            public string SiteID { get; set; }
            public string PositionID { get; set; }
            public string PositionName { get; set; }
            public string PayrollCode { get; set; }
            public string AccountsCode { get; set; }
            public string CtcRate00 { get; set; }
            public string CtcRate10 { get; set; }
            public string CtcRate13 { get; set; }
            public string CtcRate15 { get; set; }
            public string CtcRate20 { get; set; }
            public string CtcRate23 { get; set; }
            public string CtcRate25 { get; set; }
            public string CtcRate30 { get; set; }
            public string CtcRatePPH { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string Insert(Position position)
        {
            bool cmdStatus = false;
            var wsPositions = new MobiTime.WebServices.Positions();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            cmdStatus = wsPositions.Insert(
                                        ApplicationPassword,
                                        int.Parse(position.SiteID),
                                        position.PositionName,
                                        position.PayrollCode,
                                        position.AccountsCode,
                                        double.Parse(position.CtcRate00),
                                        double.Parse(position.CtcRate10),
                                        double.Parse(position.CtcRate13),
                                        double.Parse(position.CtcRate15),
                                        double.Parse(position.CtcRate20),
                                        double.Parse(position.CtcRate23),
                                        double.Parse(position.CtcRate25),
                                        double.Parse(position.CtcRate30),
                                        double.Parse(position.CtcRatePPH),
                                        UserGuid);

            return cmdStatus.ToString(); ;

        }
        [System.Web.Services.WebMethod]
        public static string Update(Position position)
        {
            bool cmdStatus = false;
            var wsPositions = new MobiTime.WebServices.Positions();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            cmdStatus = wsPositions.Update(
                               ApplicationPassword,
                                    int.Parse(position.SiteID),
                                    int.Parse(position.PositionID),
                                    position.PositionName,
                                    position.PayrollCode,
                                    position.AccountsCode,
                                    double.Parse(position.CtcRate00),
                                    double.Parse(position.CtcRate10),
                                    double.Parse(position.CtcRate13),
                                    double.Parse(position.CtcRate15),
                                    double.Parse(position.CtcRate20),
                                    double.Parse(position.CtcRate23),
                                    double.Parse(position.CtcRate25),
                                    double.Parse(position.CtcRate30),
                                    double.Parse(position.CtcRatePPH),
                                    UserGuid);


            return cmdStatus.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string ListPositions(string siteID)
        {
            StringBuilder sbResult = new StringBuilder();
            var wsPosition = new MobiTime.WebServices.Positions();

            var ListPosition = new List<MobiTime.ReturnData.ReturnPositionData>();

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();


            ListPosition = wsPosition.ListPositions(ApplicationPassword, int.Parse(siteID));


            foreach (ReturnPositionData positionData in ListPosition)
            {
                sbResult.Append("<tr class='row-select'>");
                sbResult.Append("<td class='key hidden'>" + positionData.PositionID + "</td>");
                //sbResult.Append("<td>" + positionData.SiteID + "</td>");
                sbResult.Append("<td>" + positionData.Position + "</td>");
                sbResult.Append("<td>" + positionData.PayrollCode + "</td>");
                sbResult.Append("<td>" + positionData.AccountsCode + "</td>");
                sbResult.Append("</tr>");
            }
            return sbResult.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string Select(string SiteID, string KeyID)
        {

            var wsPositions = new MobiTime.WebServices.Positions();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            List<ReturnPositionData> dataResult = wsPositions.Select(ApplicationPassword, int.Parse(SiteID), int.Parse(KeyID));
            return JsonConvert.SerializeObject(dataResult);
        }

        [System.Web.Services.WebMethod]
        public static string Delete(string SiteID, string KeyID)
        {

            var wsPositions = new MobiTime.WebServices.Positions();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            return wsPositions.Delete(ApplicationPassword, int.Parse(SiteID), int.Parse(KeyID), UserGuid).ToString();

        }
    }
}