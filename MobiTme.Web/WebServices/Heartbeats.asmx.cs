using System;
using System.Web.Services;
using System.Data.SqlClient;

namespace MobiTime.WebServices
{
    /// <summary>
    /// Summary description for Heartbeats
    /// </summary>
    [WebService(Namespace = "http://www.mobitime.co.za/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Heartbeats : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Department detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            string Terminal)
        {
            bool Accepted = false;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    Accepted = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Insert Into Heartbeats (" +
                                            "Terminal, " +
                                            "CapturedAtUTC, " +
                                            "CapturedBy) " +
                                        "Select " +
                                            "'" + Terminal + "', " +
                                            "'" + DateTime.UtcNow + "', " +
                                            "'System'";
                    com.ExecuteNonQuery();

                    Accepted = true;
                }
            }
            catch (SqlException Ex)
            {
                Accepted = false;
            }
            catch (Exception Ex)
            {
                Accepted = false;
            }
            finally
            {
                con.Close();
            }
            return Accepted;
        }
    }
}
