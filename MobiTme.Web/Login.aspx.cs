using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using MobiTime.WebServices;

namespace MobiTme
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string LoginUser(string username, string password, bool rememberMe)
        {
            String userGuid =
                (new UserLogin().Login(MobiTme.Web.Functions.AuthManager.GetWebServiceKey(), username, password));
 
            if (userGuid != "0")
            {
                //FormsAuthentication.SetAuthCookie(username, rememberMe);
                ////string number = "12345";
                ////FormsAuthentication.SetAuthCookie(number, true);
                HttpContext.Current.Session["UserGuid"] = userGuid;

            } 
 
            return userGuid;
        }
    }
}