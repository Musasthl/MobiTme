using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Services;
using System.Data.SqlClient;

namespace MobiTime.WebServices
{
    
    [WebService(Namespace = "http://www.mobitime.co.za/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserLogin : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());

        [WebMethod(Description="MobiTime Web Service - Authenticate User Login")]
        public string Login(
            string ApplicationPassword,
            string Email,
            string Password)
        {
            string UserGuid = null;
            DateTime DateTimeNow = DateTime.UtcNow;

            
            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    UserGuid = "0";
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "GuidID " +
                                        "From " +
                                            "Users " +
                                        "Where " +
                                            "Email = '" + Email + "' " +
                                            "And Password = '" + Password + "' " +
                                            "And '" + DateTimeNow + "' >= EngagementDateUTC " +
                                            "And '" + DateTimeNow + "' <= IsNull(TerminationDateUTC, GetUtcDate())";
                    UserGuid = (string) com.ExecuteScalar();

                    Functions.MapIcon.MapIconColour(UserGuid);

                    if (string.IsNullOrEmpty(UserGuid))
                    {
                        UserGuid = "0";
                    }
                }
            }
            catch (SqlException Ex)
            {
                UserGuid = "0";
            }
            catch (Exception Ex)
            {
                UserGuid = "0";
            }
            finally
            {
                con.Close();
            }
            return UserGuid;
        }




        [WebMethod(Description = "Mobitime Web Service - Provide list of sites for Logged In User")]
        public List<ReturnData.ReturnUserSiteData> ListUserSites(
            string ApplicationPassword,
            string UserGuidID)
        {
            ReturnData.ReturnUserSiteData DSReturnUserSites = null;

            SqlDataReader UserSitesList = null;
            List<ReturnData.ReturnUserSiteData> ReturnedUserSites = new List<ReturnData.ReturnUserSiteData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedUserSites = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "cl.ClientID As ClientID, " + 
	                                        "cl.TradingName As Client, " + 
	                                        "up.SiteID As SiteID, " + 
	                                        "si.SiteName As SiteName, " + 
                                            "si.GGPS01, " + 
                                            "si.GGPS02, " +
                                            "si.MapIconColour " + 
                                        "From " + 
	                                        "UserPermissions up " + 
	                                        "Inner Join Users us on up.UserID = us.UserID " + 
	                                        "Inner Join Sites si on up.SiteID = si.SiteID " + 
	                                        "Inner Join Clients cl on si.ClientID = cl.ClientID " + 
                                        "Where " + 
	                                        "us.GuidID = '" + UserGuidID + "'";
                    UserSitesList = com.ExecuteReader();

                    while (UserSitesList.Read())
                    {
                        DSReturnUserSites = new ReturnData.ReturnUserSiteData();

                        DSReturnUserSites.ClientID = UserSitesList["ClientID"].ToString();
                        DSReturnUserSites.Client = UserSitesList["Client"].ToString();
                        DSReturnUserSites.SiteID = UserSitesList["SiteID"].ToString();
                        DSReturnUserSites.Site = UserSitesList["SiteName"].ToString();
                        DSReturnUserSites.GGPS01 = UserSitesList["GGPS01"].ToString();
                        DSReturnUserSites.GGPS02 = UserSitesList["GGPS02"].ToString();
                        DSReturnUserSites.MapIconColour = UserSitesList["MapIconColour"].ToString();

                        ReturnedUserSites.Add(DSReturnUserSites);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedUserSites = null;
            }
            catch (Exception Ex)
            {
                ReturnedUserSites = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedUserSites;
        }
    }
}
