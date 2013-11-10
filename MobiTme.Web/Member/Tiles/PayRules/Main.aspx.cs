using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using UITemplate.Member.Tiles.PayRules.Iframes;

namespace MobiTme.Web.Member.Tiles.PayRules
{
    public partial class Main : System.Web.UI.Page
    {


        public class PayRule
        {
            public string SiteID { get; set; }
            public string ClientID { get; set; }
            public string PayRuleID { get; set; }
            public string PayRuleName { get; set; }
            public string ShiftStart { get; set; }
            public string ShiftEnd { get; set; }
            public string Description { get; set; }
            public string ShiftDayAdjustment { get; set; }
            public string RoundingType_In { get; set; }
            public string RoundingUnit_In { get; set; }
            public string RoundingBase_In { get; set; }
            public string RoundingType_Out { get; set; }
            public string RoundingUnit_Out { get; set; }
            public string RoundingBase_Out { get; set; }
        }


        DateTime dateTime = DateTime.Parse("1/1/0001 08:00:00 AM");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulatePayRates();
                PopulateShiftAllawonce();

            }
        }


        private void PopulatePayRates()
        {
            StringBuilder sbHtml = new StringBuilder();


            for (int i = 0; i < 96; i++)
            {
                sbHtml.Append("<tr id='pay-row" + i + "' class='pay-row'  data-time='" + dateTime.ToShortTimeString() + "' data-row='" + i + "'>");
                sbHtml.Append("<td class=\"hidden\"> </td>");
                sbHtml.Append("<td style=\"width: 120px;\">" + dateTime.ToShortTimeString() + "</td>");
                sbHtml.Append("<td class='pay-normal'></td>");

                sbHtml.Append("<td class='pay-pph'></td>");
                sbHtml.Append("</tr>");
                dateTime = dateTime.AddMinutes(15);
            }

            payRateData.InnerHtml = sbHtml.ToString();

        }


        private void PopulateShiftAllawonce()
        {
            StringBuilder sbHtml = new StringBuilder();

            for (int i = 0; i < 96; i++)
            {
                sbHtml.Append("<tr id='shift-row" + i + "' class='shift-row' data-time='" + dateTime.ToShortTimeString() + "' data-row='" + i + "'>");
                sbHtml.Append("<td class=\"hidden\"> </td>");
                sbHtml.Append("<td style=\"width: 120px;\">" + dateTime.ToShortTimeString() + "</td>");
                sbHtml.Append("<td class='shift-normal' ></td>");

                sbHtml.Append("<td class='shift-pph'></td>");
                sbHtml.Append("</tr>");
                dateTime = dateTime.AddMinutes(15);
            }

            shiftAllawonceData.InnerHtml = sbHtml.ToString();

        }

        [System.Web.Services.WebMethod]
        public static string Select(string SiteID, string KeyID)
        {

            var wsPayRules = new MobiTime.WebServices.PayRules();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();
            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();

            List<MobiTime.ReturnData.ReturnPayRuleData> dataResult = wsPayRules.Select(ApplicationPassword, int.Parse(SiteID), UserGuid, int.Parse(KeyID));
            return JsonConvert.SerializeObject(dataResult);
        }

        [System.Web.Services.WebMethod]
        public static string ListPayRules(string siteID)
        {
            StringBuilder sbResult = new StringBuilder();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();

            var listData = new List<MobiTime.ReturnData.ReturnPayRuleData>();

            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();
            var wsPayRules = new MobiTime.WebServices.PayRules();

            listData = wsPayRules.List(ApplicationPassword, int.Parse(siteID), UserGuid);

            foreach (MobiTime.ReturnData.ReturnPayRuleData itemData in listData)
            {
                sbResult.Append("<tr class='row-select'>");
                sbResult.Append("<td class='key hidden'>" + itemData.PayRuleID + "</td>");
                sbResult.Append("<td>" + itemData.PayRule + "</td>");
                sbResult.Append("<td>" + itemData.Description + "</td>");
                sbResult.Append("<td>" + itemData.ShiftStart + "</td>");
                sbResult.Append("<td>" + itemData.ShiftEnd + "</td>");

                sbResult.Append("</tr>");
            }



            return sbResult.ToString();
        }


        [System.Web.Services.WebMethod]
        public static string Update(PayRule payRule)
        {
            payRule.ClientID = "0";
            bool cmdStatus = false;
            var wsPayRules = new MobiTime.WebServices.PayRules();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();
            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();


            cmdStatus = wsPayRules.Update(

                       ApplicationPassword,
             int.Parse(payRule.ClientID),
             int.Parse(payRule.SiteID),
             UserGuid,
             int.Parse(payRule.PayRuleID),
             payRule.PayRuleName,
          DateTime.Parse(payRule.ShiftStart),
              DateTime.Parse(payRule.ShiftEnd),
             payRule.Description,
           double.Parse(payRule.ShiftDayAdjustment),
             payRule.RoundingType_In,
             payRule.RoundingUnit_In,
             int.Parse(payRule.RoundingBase_In),
             payRule.RoundingType_Out,
             payRule.RoundingUnit_Out,
             int.Parse(payRule.RoundingBase_Out));


            return cmdStatus.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string Insert(PayRule payRule)
        {
            bool cmdStatus = false;
            payRule.ClientID = "0";
            var wsPayRules = new MobiTime.WebServices.PayRules();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();
            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();

            cmdStatus = wsPayRules.Insert(
         ApplicationPassword,
             int.Parse(payRule.ClientID),
             int.Parse(payRule.SiteID),
             UserGuid,
             payRule.PayRuleName,
          DateTime.Parse(payRule.ShiftStart),
              DateTime.Parse(payRule.ShiftEnd),
             payRule.Description,
           double.Parse(payRule.ShiftDayAdjustment),
             payRule.RoundingType_In,
             payRule.RoundingUnit_In,
             int.Parse(payRule.RoundingBase_In),
             payRule.RoundingType_Out,
             payRule.RoundingUnit_Out,
             int.Parse(payRule.RoundingBase_Out));

            return cmdStatus.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string ListPayRate(string siteID, string payRuleID)
        {
            StringBuilder sbResult = new StringBuilder();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();

            var listData = new List<MobiTime.ReturnData.ReturnPayRateData>();

            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();


            listData = new MobiTime.WebServices.PayRates().List(ApplicationPassword, int.Parse(siteID), UserGuid, int.Parse(payRuleID));

            foreach (MobiTime.ReturnData.ReturnPayRateData itemData in listData)
            {
                sbResult.Append("<tr class='row-select'>");
                sbResult.Append("<td class='key hidden'>" + itemData.PayRateID + "</td>");

                sbResult.Append("<td>" + itemData.PayType + "</td>");
                sbResult.Append("<td>" + itemData.PayRateFrom + "</td>");
                sbResult.Append("<td>" + itemData.PayRateTo + "</td>");



                sbResult.Append("</tr>");
            }



            return sbResult.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string ListBrackets(string siteID, string payRuleID)
        {
            StringBuilder sbResult = new StringBuilder();
            string UserGuid = MobiTime.Library.Authentication.Manager.GetCurrentUser();

            var listData = new List<MobiTime.ReturnData.ReturnPayBracketData>();

            string ApplicationPassword = MobiTime.Library.Authentication.Manager.GetWebServiceKey();


            listData = new MobiTime.WebServices.PayBrackets().List(ApplicationPassword, int.Parse(siteID), UserGuid, int.Parse(payRuleID));

            foreach (MobiTime.ReturnData.ReturnPayBracketData itemData in listData)
            {
                sbResult.Append("<tr class='row-select'>");
                sbResult.Append("<td class='key hidden'>" + itemData.PayBracketID + "</td>");

                sbResult.Append("<td>" + itemData.ClockingDirection + "</td>");
                sbResult.Append("<td>" + itemData.BracketFrom + "</td>");
                sbResult.Append("<td>" + itemData.BracketTo + "</td>");
                sbResult.Append("<td>" + itemData.PayClocking + "</td>");


                sbResult.Append("</tr>");
            }





            return sbResult.ToString();
        }

    }
}