using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;

namespace MobiTime.Library.Authentication
{
    public static class Manager
    {
        public static string GetWebServiceKey()
        {
            return "07E1567E-D5D7-440C-AB2E-6A97481B2AF8";
        }

        public static string GetCurrentUser()
        {
            if (HttpContext.Current.Session["UserGuid"] != null)
                return HttpContext.Current.Session["UserGuid"].ToString();
            return "";
            //return System.Web.HttpContext.Current.User.Identity.Name;
        }
    }
}