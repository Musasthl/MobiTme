using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace UITemplate.Member.Tiles.PayRules.Iframes
{
    public partial class PayRates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulatePayRates();
            }
        }

        private void PopulatePayRates()
        {
            StringBuilder sbHtml = new StringBuilder();

            DateTime dateTime = DateTime.Parse("1/1/0001 08:00:00 AM");

            for (int i = 0; i < 96; i++)
            {
                sbHtml.Append("<tr id='pay-row" + i + "' class='pay-row'>");
                sbHtml.Append("<td style=\"width: 120px;\">" + dateTime.ToShortTimeString() + "</td>");
                sbHtml.Append("<td class='pay-normal'></td>");

                sbHtml.Append("<td class='pay-pph'></td>");
                sbHtml.Append("</tr>");
                dateTime = dateTime.AddMinutes(15);
            }

            tbPayRates.InnerHtml = sbHtml.ToString();

        }
    }
}