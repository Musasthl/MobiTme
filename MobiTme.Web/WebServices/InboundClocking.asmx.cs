using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace MobiTime.WebServices
{
    [WebService(Namespace = "http://www.mobitime.co.za/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InboundClocking : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description="MobiTime Web Service - Basic add Clocking to MobiTime. Returns Accepted = True or False")]
        public bool InsertClocking(
            string ApplicationPassword,
            string Terminal,
            string Originator,
            string ClockingNumber,
            DateTime ClockingDate)
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

                    com.CommandText = "Insert Into Clockings (" +
                                            "Terminal, " +
                                            "Originator, " +
                                            "ClockingNumber, " +
                                            "ClockingDate, " +
                                            "ClockingType, " +
                                            "DateAcceptedUTC, " +
                                            "CapturedAtUTC, " +
                                            "CapturedBy)" +
                                        "Select " +
                                            "'" + Terminal + "', " +
                                            "'" + Originator + "', " +
                                            "'" + ClockingNumber + "', " +
                                            "'" + ClockingDate + "', " +
                                            "'Clocking', " +
                                            "'" + DateTime.UtcNow + "', " +
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
