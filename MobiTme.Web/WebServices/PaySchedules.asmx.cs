using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace MobiTime.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PaySchedules : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Pay Schedule detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            int PayRuleID,
            string PayType,
            DateTime DateFrom,
            DateTime DateTo,
            string UserGuid)
        {
            bool InsertedPaySchedules = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedPaySchedules = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Insert Into PaySchedules (" +
                                            "PayRuleID, " +
                                            "PayType, " +
                                            "DateFrom, " +
                                            "DateTo, " +
                                            "CapturedAtUTC, " +
                                            "CapturedBy) " +
                                        "Output " +
                                            "Inserted.PayScheduleID " +
                                        "Select " +
                                            "" + PayRuleID + ", " +
                                            "'" + PayType + "', " +
                                            "'" + DateFrom + "', " +
                                            "'" + DateTo + "', " +
                                            "'" + DateTime.UtcNow + "', " +
                                            "'" + UserGuid + "'";
                    InsertedID = (int)com.ExecuteScalar();
                    InsertedPaySchedules = true;
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedPaySchedules = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedPaySchedules = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedPaySchedules;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of the Pay Schedule detail")]
        public List<ReturnData.ReturnPayScheduleData> ListHours(
            string ApplicationPassword,
            int PayRuleID)
        {
            ReturnData.ReturnPayScheduleData DSReturnPaySchedules = null;

            SqlDataReader PaySchedulesList = null;
            List<ReturnData.ReturnPayScheduleData> ReturnedPayScheduless = new List<ReturnData.ReturnPayScheduleData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedPayScheduless = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "PayScheduleID, " +
                                            "PayRuleID, " +
                                            "PayType, " +
                                            "DateFrom, " +
                                            "DateTo " +
                                        "From " +
                                            "PaySchedules " +
                                        "Where " +
                                            "PayRuleID = " + PayRuleID + " " +
                                            "And PayType In (" +
                                                "'Pay Hours Worked at Rate 0.0000', " +
                                                "'Pay Hours Worked at Rate 1.0000', " +
                                                "'Pay Hours Worked at Rate 1.3333', " +
                                                "'Pay Hours Worked at Rate 1.5000', " +
                                                "'Pay Hours Worked at Rate 2.0000', " +
                                                "'Pay Hours Worked at Rate 2.3333', " +
                                                "'Pay Hours Worked at Rate 2.5000', " +
                                                "'Pay Hours Worked at Rate 3.0000', " +
                                                "'Pay Hours Worked on Public Holiday')";
                    PaySchedulesList = com.ExecuteReader();

                    while (PaySchedulesList.Read())
                    {
                        DSReturnPaySchedules = new ReturnData.ReturnPayScheduleData();

                        DSReturnPaySchedules.PayScheduleID = PaySchedulesList["PayScheduleID"].ToString();
                        DSReturnPaySchedules.PayRuleID = PaySchedulesList["PayRuleID"].ToString();
                        DSReturnPaySchedules.PayType = PaySchedulesList["PayType"].ToString();
                        DSReturnPaySchedules.DateFrom = PaySchedulesList["DateFrom"].ToString();
                        DSReturnPaySchedules.DateTo = PaySchedulesList["DateTo"].ToString();

                        ReturnedPayScheduless.Add(DSReturnPaySchedules);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedPayScheduless = null;
            }
            catch (Exception Ex)
            {
                ReturnedPayScheduless = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedPayScheduless;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of the Pay Schedule detail")]
        public List<ReturnData.ReturnPayScheduleData> ListShiftAllowance(
            string ApplicationPassword,
            int PayRuleID)
        {
            ReturnData.ReturnPayScheduleData DSReturnPaySchedules = null;

            SqlDataReader PaySchedulesList = null;
            List<ReturnData.ReturnPayScheduleData> ReturnedPayScheduless = new List<ReturnData.ReturnPayScheduleData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedPayScheduless = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "PayScheduleID, " +
                                            "PayRuleID, " +
                                            "PayType, " +
                                            "DateFrom, " +
                                            "DateTo " +
                                        "From " +
                                            "PaySchedules " +
                                        "Where " +
                                            "PayRuleID = " + PayRuleID + " " +
                                            "And PayType In (" +
                                                "'Pay Hours Worked to Shift Allowance 01', " +
                                                "'Pay Hours Worked to Shift Allowance 02', " +
                                                "'Pay Hours Worked to Shift Allowance 03')";
                    PaySchedulesList = com.ExecuteReader();

                    while (PaySchedulesList.Read())
                    {
                        DSReturnPaySchedules = new ReturnData.ReturnPayScheduleData();

                        DSReturnPaySchedules.PayScheduleID = PaySchedulesList["PayScheduleID"].ToString();
                        DSReturnPaySchedules.PayRuleID = PaySchedulesList["PayRuleID"].ToString();
                        DSReturnPaySchedules.PayType = PaySchedulesList["PayType"].ToString();
                        DSReturnPaySchedules.DateFrom = PaySchedulesList["DateFrom"].ToString();
                        DSReturnPaySchedules.DateTo = PaySchedulesList["DateTo"].ToString();

                        ReturnedPayScheduless.Add(DSReturnPaySchedules);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedPayScheduless = null;
            }
            catch (Exception Ex)
            {
                ReturnedPayScheduless = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedPayScheduless;
        }
        
    }
}

