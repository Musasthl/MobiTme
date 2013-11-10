using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace MobiTime.Functions
{
    public static class AuditTrails
    {
        public static bool BackupRecord(
            int ClientID,
            int SiteID,
            string UserGuid,
            string TableName,
            string IDColumnName,
            int LineID)
        {
            SqlCommand com = new SqlCommand();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());
            
            bool Successful = false;

            try
            {
                com.Connection = con;
                con.Open();

                com.CommandText = "Insert Into AuditTrails (" +
                                        "ClientID, " +                    
                                        "SiteID, " +
                                        "UpdatedAtUTC, " +
                                        "UpdatedBy, " +
                                        "TableName, " +
                                        "LineID, " +
                                        "RecordDetail) " +
                                    "Select " +
                                        "" + ClientID + ", " +
                                        "" + SiteID + ", " +
                                        "'" + DateTime.UtcNow + "', " +
                                        "'" + UserGuid + "', " +
                                        "'" + TableName + "', " +
                                        "'" + LineID + "', " +
                                        "(Select * From " + TableName + " Where " + IDColumnName + " = '" + LineID + "' For XML Raw)";
                com.ExecuteNonQuery();
                Successful = true;
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