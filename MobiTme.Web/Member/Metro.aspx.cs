using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MS.Internal.Xml.XPath;
using MobiTime.WebServices;
using Newtonsoft.Json;

namespace MobiTme.Member
{
    public partial class Metro : System.Web.UI.Page
    {


        public class UserSites
        {
            public string ClientID { get; set; }
            public string Client { get; set; }
            public string SiteID { get; set; }
            public string SiteName { get; set; }
            public string GGPS01 { get; set; }
            public string GGPS02 { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string ListUserSites()
        {
            UserLogin wsUserLogin = new UserLogin();
            List<MobiTime.ReturnData.ReturnUserSiteData> listUserSites = null;
            listUserSites = wsUserLogin.ListUserSites(MobiTime.Library.Authentication.Manager.GetWebServiceKey(), MobiTime.Library.Authentication.Manager.GetCurrentUser());

 
            // listUserSites
            return JsonConvert.SerializeObject(listUserSites);
        }
    }
}