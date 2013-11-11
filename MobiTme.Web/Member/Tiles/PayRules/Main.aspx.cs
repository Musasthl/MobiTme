using System;
using System.Collections.Generic;
using System.Globalization;
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
                sbHtml.Append("<tr id='pay-row" + i + "' class='pay-row'  data-time='" + dateTime.ToString("hh:mm tt") + "' data-row='" + i + "'>");
                sbHtml.Append("<td class=\"hidden\"> </td>");
                sbHtml.Append("<td style=\"width: 120px;\">" + dateTime.ToString("hh:mm tt") + "</td>");
                sbHtml.Append("<td class='pay-normal' data-keyid=''></td>");

                sbHtml.Append("<td class='pay-pph' data-keyid=''></td>");
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
                sbHtml.Append("<tr id='shift-row" + i + "' class='shift-row' data-time='" + dateTime.ToString("hh:mm tt") + "' data-row='" + i + "'>");
                sbHtml.Append("<td class=\"hidden\"> </td>");
                sbHtml.Append("<td style=\"width: 120px;\">" + dateTime.ToString("hh:mm tt") + "</td>");
                sbHtml.Append("<td class='shift-normal'  data-keyid=''></td>");

                sbHtml.Append("<td class='shift-pph' data-keyid=''></td>");
                sbHtml.Append("</tr>");
                dateTime = dateTime.AddMinutes(15);
            }

            shiftAllawonceData.InnerHtml = sbHtml.ToString();

        }

        [System.Web.Services.WebMethod]
        public static string Select(string SiteID, string KeyID)
        {

            var wsPayRules = new MobiTime.WebServices.PayRules();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            List<MobiTime.ReturnData.ReturnPayRuleData> dataResult = wsPayRules.Select(ApplicationPassword, int.Parse(SiteID), UserGuid, int.Parse(KeyID));
            return JsonConvert.SerializeObject(dataResult);
        }

        [System.Web.Services.WebMethod]
        public static string ListPayRules(string siteID)
        {
            StringBuilder sbResult = new StringBuilder();
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();

            var listData = new List<MobiTime.ReturnData.ReturnPayRuleData>();

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();
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
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();


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
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();
            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

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
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();

            var listData = new List<MobiTime.ReturnData.ReturnPayRateData>();

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();


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
            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();

            var listData = new List<MobiTime.ReturnData.ReturnPayBracketData>();

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();


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


        public class MyPayRate
        {
            public string PayRateID { get; set; }
            public string SiteID { get; set; }
            public string PayRuleID { get; set; }
            public string PayType { get; set; }
            public string PayRateFrom { get; set; }
            public string PayRateTo { get; set; }
        }



        [System.Web.Services.WebMethod]
        public static string payRateList(string SiteID, string PayRuleID)
        {
            int ClientID = 1;

            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            var wsService = new MobiTime.WebServices.PayRates();

            List<MobiTime.ReturnData.ReturnPayRateData> dataResult = wsService.List(ApplicationPassword, int.Parse(SiteID), UserGuid,
                              int.Parse(PayRuleID));
            return JsonConvert.SerializeObject(dataResult);

        }





        [System.Web.Services.WebMethod]
        public static string paySchedulesList(string SiteID, string PayRuleID)
        {
            int ClientID = 1;

            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            var wsService = new MobiTime.WebServices.PaySchedules();

            List<MobiTime.ReturnData.ReturnPayScheduleData> dataResult = wsService.ListShiftAllowance(ApplicationPassword,
                              int.Parse(PayRuleID));
            return JsonConvert.SerializeObject(dataResult);

        }

        [System.Web.Services.WebMethod]
        public static string insertPayRates(MyPayRate myyPayRate)
        {
            int ClientID = 1;

            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            var wsService = new MobiTime.WebServices.PayRates();
            bool result = wsService.Insert(ApplicationPassword, ClientID, int.Parse(myyPayRate.SiteID), UserGuid,
                              int.Parse(myyPayRate.PayRuleID), myyPayRate.PayType, DateTime.Parse("1900/01/01 " + myyPayRate.PayRateFrom),
                               DateTime.Parse("1900/01/01 " + myyPayRate.PayRateTo));


            return result.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string insertShiftPayRate(MyPayRate myyPayRate)
        {
            int ClientID = 1;

            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            var wsService = new MobiTime.WebServices.PaySchedules();
            bool result = wsService.Insert(ApplicationPassword,
                              int.Parse(myyPayRate.PayRuleID), myyPayRate.PayType, DateTime.Parse("2013/01/01 " + myyPayRate.PayRateFrom),
                               DateTime.Parse("1900/01/01 " + myyPayRate.PayRateTo), UserGuid);


            return result.ToString();
        }


        [System.Web.Services.WebMethod]
        public static string updatePayRates(MyPayRate myyPayRate)
        {
            int ClientID = 1;

            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            var wsService = new MobiTime.WebServices.PayRates();
            bool result = wsService.Update(ApplicationPassword, ClientID, int.Parse(myyPayRate.SiteID), UserGuid, int.Parse(myyPayRate.PayRateID),
                              int.Parse(myyPayRate.PayRuleID), myyPayRate.PayType, DateTime.Parse("2013/11/05 " + myyPayRate.PayRateFrom),
                               DateTime.Parse("2013/11/05 " + myyPayRate.PayRateTo));


            return result.ToString();
        }


        [System.Web.Services.WebMethod]
        public static string deletePayRates(MyPayRate myyPayRate)
        {
            int ClientID = 1;

            string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();

            string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            var wsService = new MobiTime.WebServices.PayRates();
            bool result = wsService.Delete(ApplicationPassword, ClientID, int.Parse(myyPayRate.SiteID), UserGuid,
                              int.Parse(myyPayRate.PayRuleID), int.Parse(myyPayRate.PayRateID));


            return result.ToString();
        }

        public string insertShiftRate(MyPayRate myyPayRate)
        {
            //int ClientID = 1;

            //string UserGuid = MobiTme.Web.Functions.AuthManager.GetCurrentUser();

            //string ApplicationPassword = MobiTme.Web.Functions.AuthManager.GetWebServiceKey();

            //var wsService = new MobiTime.WebServices.ShiftPatternDays();
            //bool result = wsService.Insert(ApplicationPassword, ClientID, int.Parse(myyPayRate.SiteID), UserGuid,
            //                  int.Parse(myyPayRate.PayRuleID), myyPayRate.PayType, DateTime.Parse(myyPayRate.PayRateFrom),
            //                  DateTime.Now);


            return null;
        }

    }
}