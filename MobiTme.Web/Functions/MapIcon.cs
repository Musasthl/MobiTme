using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace MobiTime.Functions
{
    public class MapIcon
    {
        public static void MapIconColour(
            string UserGuidID)
        {
            SqlCommand com = new SqlCommand();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());

            com.Connection = con;
            con.Open();

            

            try
            {
                //First complete count of People Absent from Clockings or Timesheet Table
                //Update the count to the MapIconColour
                //Using the Updated value, calculate as % of total employed people
                //If 0% - 1% = Green
                //If 1.01% to 3% = Orange
                //If 3.01% and greater = Red

                com.CommandText = "Update Sites " +
                                    "Set " +
                                        "MapIconColour = 'Yellow' " +
                                    "Where " +
                                        "SiteID In (Select " +
                                                        "up.SiteID " +
                                                    "From " +
                                                        "Users us " +
                                                        "Inner Join UserPermissions up on us.UserID = up.UserID " +
                                                    "Where " +
                                                        "us.GuidID = '" + UserGuidID + "')";
                com.ExecuteNonQuery();
            }
            catch (SqlException Ex)
            {
            }
            catch (Exception Ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
    }
}