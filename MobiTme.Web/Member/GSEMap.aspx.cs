﻿using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobiTime.WebServices;
using Newtonsoft.Json;
//http://blog.shamess.info/2009/09/29/zoom-to-fit-all-markers-on-google-maps-api-v3/
namespace MobiTme.Member
{
    public partial class GSEMap : System.Web.UI.Page
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
            listUserSites = wsUserLogin.ListUserSites(MobiTme.Web.Functions.AuthManager.GetWebServiceKey(), MobiTme.Web.Functions.AuthManager.GetCurrentUser());


            // listUserSites
            return JsonConvert.SerializeObject(listUserSites);
        }
    }
}