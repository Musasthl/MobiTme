using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace MobiTime.WebServices
{
    [WebService(Namespace = "http:www.mobitime.co.za/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PayTypes : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Select a PayType Detail List from MobiTime")]
        public List<ReturnData.ReturnPayTypeData> ListPayTypes(
            string ApplicationPassword)
        {
            ReturnData.ReturnPayTypeData DSReturnPayTypes = null;

            SqlDataReader PayTypeList = null;
            List<ReturnData.ReturnPayTypeData> ReturnedPayTypes = new List<ReturnData.ReturnPayTypeData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedPayTypes = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "PayType " +
                                        "From " +
                                            "PayTypes " +
                                        "Where " +
                                            "DeletedAtUTC is null";
                    PayTypeList = com.ExecuteReader();

                    while (PayTypeList.Read())
                    {
                        DSReturnPayTypes = new ReturnData.ReturnPayTypeData();

                        DSReturnPayTypes.PayType = PayTypeList["PayType"].ToString();

                        ReturnedPayTypes.Add(DSReturnPayTypes);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedPayTypes = null;
            }
            catch (Exception Ex)
            {
                ReturnedPayTypes = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedPayTypes;
        }
    }
}
