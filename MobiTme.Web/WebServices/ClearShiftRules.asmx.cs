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
    public class ClearShiftRules : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Clear Shift Rules")]
        public bool ClearTheShiftRules(
            string ApplicationPassword,
            int SiteID,
            int PayRuleID,
            string UserGuid)
        {
            bool Successful = false;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    Successful = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "PayAdjustments", "PayRuleID", PayRuleID) == true)
                    {
                        com.CommandText = "Delete From " +
                                                "PayAdjustments " +
                                            "Where " +
                                                "PayRuleID = " + PayRuleID + "";
                        com.ExecuteNonQuery();

                        Successful = true;
                    }
                    else
                    {
                        Successful = false;
                    }

                    if (Successful == true)
                    {
                        if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "PayBrackets", "PayRuleID", PayRuleID) == true)
                        {
                            com.CommandText = "Delete From " +
                                                   "PayBrackets " +
                                               "Where " +
                                                   "PayRuleID = " + PayRuleID + "";
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            Successful = false;
                        }
                    }

                    if (Successful == true)
                    {
                        if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "PayRoundings", "PayRuleID", PayRuleID) == true)
                        {
                            com.CommandText = "Delete From " +
                                                   "PayRoundings " +
                                               "Where " +
                                                   "PayRuleID = " + PayRuleID + "";
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            Successful = false;
                        }
                    }

                    if (Successful == true)
                    {
                        if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "PaySchedules", "PayRuleID", PayRuleID) == true)
                        {
                            com.CommandText = "Delete From " +
                                                   "PaySchedules " +
                                               "Where " +
                                                   "PayRuleID = " + PayRuleID + "";
                            com.ExecuteNonQuery();
                        }
                        else
                        {
                            Successful = false;
                        }
                    }

                    if (Successful == true)
                    {
                        if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "PayShiftDateAdjustment", "PayRuleID", PayRuleID) == true)
                        {
                            com.CommandText = "Delete From " +
                                                   "PayShiftDateAdjustment " +
                                               "Where " +
                                                   "PayRuleID = " + PayRuleID + "";
                            com.ExecuteNonQuery();

                            Successful = true;
                        }
                        else
                        {
                            Successful = false;
                        }
                    }
                }
            }
            catch (SqlException Ex)
            {
                Successful = false;
            }
            catch (Exception Ex)
            {
                Successful = false;
            }
            finally
            {
                con.Close();
            }
            return Successful;
        }
    }
}
