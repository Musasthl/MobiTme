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
    public class Sites : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Site detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            int ClientID,
            string SiteName,
            string Telephone,
            string Facsimile,
            string Email,
            string Physical01,
            string Physical02,
            string Physical03,
            string Physical04,
            string PhysicalCountry,
            string PhysicalCode,
            string Postal01,
            string Postal02,
            string Postal03,
            string Postal04,
            string PostalCountry,
            string PostalCode,
            string GGPS01,
            string GGPS02,
            DateTime EngagementDate,
            int AuthoTA,
            int AuthoTS,
            int AuthoTR,
            string SMTPAddress,
            bool SMTPSSL,
            bool SMTPAuthenticate,
            string SMTPUsername,
            string SMTPPassword,
            DateTime TerminationDate, 
            string UserGuid)
        {
            int SiteID = 0;
            bool InsertedSite = false;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedSite = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (SiteID) " +
                                        "From " +
                                            "Sites " +
                                        "Where " +
                                            "ClientID = " + ClientID + " " +
                                            "And SiteName = '" + SiteName + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                                "SiteID " +
                                            "From " +
                                                "Sites " +
                                            "Where " +
                                                "ClientID = " + ClientID + " " +
                                                "And SiteName = '" + SiteName + "'";
                        SiteID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Sites", "SiteID", SiteID) == true)
                        {
                            com.CommandText = "Update Sites " +
                                                "Set " +
                                                    "SiteName = '" + SiteName + "', " +
                                                    "Telephone = '" + Telephone + "', " +
                                                    "Facsimile = '" + Facsimile + "', " +
                                                    "Email = '" + Email + "', " +
                                                    "Physical01 = '" + Physical01 + "', " +
                                                    "Physical02 = '" + Physical02 + "', " +
                                                    "Physical03 = '" + Physical03 + "', " +
                                                    "Physical04 = '" + Physical04 + "', " +
                                                    "PhysicalCountry = '" + PhysicalCountry + "', " +
                                                    "PhysicalCode = '" + PhysicalCode + "', " +
                                                    "Postal01 = '" + Postal01 + "', " +
                                                    "Postal02 = '" + Postal02 + "', " +
                                                    "Postal03 = '" + Postal03 + "', " +
                                                    "Postal04 = '" + Postal04 + "', " +
                                                    "PostalCountry = '" + PostalCountry + "', " +
                                                    "PostalCode = '" + PostalCode + "', " +
                                                    "GGPS01 = '" + GGPS01 + "', " +
                                                    "GGPS02 = '" + GGPS02 + "', " +
                                                    "EngagementDate = '" + EngagementDate + "', " +
                                                    "AuthoTA = '" + AuthoTA + "', " +
                                                    "AuthoTS = '" + AuthoTS + "', " +
                                                    "AuthoTR = '" + AuthoTR + "', " +
                                                    "SMTPAddress = '" + SMTPAddress + "', " +
                                                    "SMTPSSL = '" + SMTPSSL + "', " +
                                                    "SMTPAuthenticate = '" + SMTPAuthenticate + "', " +
                                                    "SMTPUsername = '" + SMTPUsername + "', " +
                                                    "SMTPPassword = '" + SMTPPassword + "', " +
                                                    "TerminationDate = '" + TerminationDate + "', " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "ClientID = " + ClientID + " " +
                                                    "And SiteID = '" + SiteID + "'";
                            com.ExecuteNonQuery();

                            InsertedID = SiteID;
                            InsertedSite = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedSite = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into Sites (" +
                                                "ClientID, " + 
                                                "SiteName, " + 
                                                "Telephone, " + 
                                                "Facsimile, " + 
                                                "Email, " + 
                                                "Physical01, " + 
                                                "Physical02, " + 
                                                "Physical03, " + 
                                                "Physical04, " + 
                                                "PhysicalCountry, " + 
                                                "PhysicalCode, " + 
                                                "Postal01, " + 
                                                "Postal02, " + 
                                                "Postal03, " + 
                                                "Postal04, " + 
                                                "PostalCountry, " + 
                                                "PostalCode, " + 
                                                "GGPS01, " + 
                                                "GGPS02, " + 
                                                "EngagementDate, " + 
                                                "AuthoTA, " + 
                                                "AuthoTS, " + 
                                                "AuthoTR, " + 
                                                "SMTPAddress, " + 
                                                "SMTPSSL, " + 
                                                "SMTPAuthenticate, " + 
                                                "SMTPUsername, " + 
                                                "SMTPPassword, " + 
                                                "TerminationDate, " + 
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.SiteID " +
                                            "Select " +
                                                "" + ClientID + ", " +
                                                "'" + SiteName + "', " + 
                                                "'" + Telephone + "', " + 
                                                "'" + Facsimile + "', " + 
                                                "'" + Email + "', " + 
                                                "'" + Physical01 + "', " + 
                                                "'" + Physical02 + "', " + 
                                                "'" + Physical03 + "', " + 
                                                "'" + Physical04 + "', " + 
                                                "'" + PhysicalCountry + "', " + 
                                                "'" + PhysicalCode + "', " + 
                                                "'" + Postal01 + "', " + 
                                                "'" + Postal02 + "', " + 
                                                "'" + Postal03 + "', " + 
                                                "'" + Postal04 + "', " + 
                                                "'" + PostalCountry + "', " + 
                                                "'" + PostalCode + "', " + 
                                                "'" + GGPS01 + "', " + 
                                                "'" + GGPS02 + "', " + 
                                                "'" + EngagementDate + "', " + 
                                                "'" + AuthoTA + "', " + 
                                                "'" + AuthoTS + "', " + 
                                                "'" + AuthoTR + "', " + 
                                                "'" + SMTPAddress + "', " + 
                                                "'" + SMTPSSL + "', " + 
                                                "'" + SMTPAuthenticate + "', " + 
                                                "'" + SMTPUsername + "', " + 
                                                "'" + SMTPPassword + "', " + 
                                                "'" + TerminationDate + "', " + 
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedSite = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedSite = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedSite = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedSite;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a list of the Site detail")]
        public List<ReturnData.ReturnSiteData> Select(
            string ApplicationPassword,
            int ClientID,
            int SiteID)
        {
            ReturnData.ReturnSiteData DSReturnSites = null;

            SqlDataReader SiteList = null;
            List<ReturnData.ReturnSiteData> ReturnedSites = new List<ReturnData.ReturnSiteData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedSites = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "SiteName, " +
                                            "Telephone, " +
                                            "Facsimile, " +
                                            "Email, " +
                                            "Physical01, " +
                                            "Physical02, " +
                                            "Physical03, " +
                                            "Physical04, " +
                                            "PhysicalCountry, " +
                                            "PhysicalCode, " +
                                            "Postal01, " +
                                            "Postal02, " +
                                            "Postal03, " +
                                            "Postal04, " +
                                            "PostalCountry, " +
                                            "PostalCode, " +
                                            "GGPS01, " +
                                            "GGPS02, " +
                                            "EngagementDate, " +
                                            "AuthoTA, " +
                                            "AuthoTS, " +
                                            "AuthoTR, " +
                                            "SMTPAddress, " +
                                            "SMTPSSL, " +
                                            "SMTPAuthenticate, " +
                                            "SMTPUsername, " +
                                            "SMTPPassword, " +
                                            "TerminationDate " + 
                                        "From " +
                                            "Sites " +
                                        "Where " +
                                            "ClientID = " + ClientID + " " +
                                            "And SiteID = " + SiteID + " " +
                                            "And DeletedAtUTC is null";
                    SiteList = com.ExecuteReader();

                    while (SiteList.Read())
                    {
                        DSReturnSites = new ReturnData.ReturnSiteData();

                        DSReturnSites.SiteName = SiteList["SiteName"].ToString(); 
                        DSReturnSites.Telephone = SiteList["Telephone"].ToString(); 
                        DSReturnSites.Facsimile = SiteList["Facsimile"].ToString(); 
                        DSReturnSites.Email = SiteList["Email"].ToString(); 
                        DSReturnSites.Physical01 = SiteList["Physical01"].ToString(); 
                        DSReturnSites.Physical02 = SiteList["Physical02"].ToString(); 
                        DSReturnSites.Physical03 = SiteList["Physical03"].ToString(); 
                        DSReturnSites.Physical04 = SiteList["Physical04"].ToString(); 
                        DSReturnSites.PhysicalCountry = SiteList["PhysicalCountry"].ToString(); 
                        DSReturnSites.PhysicalCode = SiteList["PhysicalCode"].ToString(); 
                        DSReturnSites.Postal01 = SiteList["Postal01"].ToString(); 
                        DSReturnSites.Postal02 = SiteList["Postal02"].ToString(); 
                        DSReturnSites.Postal03 = SiteList["Postal03"].ToString(); 
                        DSReturnSites.Postal04 = SiteList["Postal04"].ToString(); 
                        DSReturnSites.PostalCountry = SiteList["PostalCountry"].ToString(); 
                        DSReturnSites.PostalCode = SiteList["PostalCode"].ToString(); 
                        DSReturnSites.GGPS01 = SiteList["GGPS01"].ToString(); 
                        DSReturnSites.GGPS02 = SiteList["GGPS02"].ToString(); 
                        DSReturnSites.EngagementDate = SiteList["EngagementDate"].ToString(); 
                        DSReturnSites.AuthoTA = SiteList["AuthoTA"].ToString(); 
                        DSReturnSites.AuthoTS = SiteList["AuthoTS"].ToString(); 
                        DSReturnSites.AuthoTR = SiteList["AuthoTR"].ToString(); 
                        DSReturnSites.SMTPAddress = SiteList["SMTPAddress"].ToString(); 
                        DSReturnSites.SMTPSSL = SiteList["SMTPSSL"].ToString(); 
                        DSReturnSites.SMTPAuthenticate = SiteList["SMTPAuthenticate"].ToString(); 
                        DSReturnSites.SMTPUsername = SiteList["SMTPUsername"].ToString(); 
                        DSReturnSites.SMTPPassword = SiteList["SMTPPassword"].ToString(); 
                        DSReturnSites.TerminationDate = SiteList["TerminationDate"].ToString(); 

                        ReturnedSites.Add(DSReturnSites);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedSites = null;
            }
            catch (Exception Ex)
            {
                ReturnedSites = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedSites;
        }




        [WebMethod(Description = "Mobitime Web Service - Select a Site Detail List from MobiTime")]
        public List<ReturnData.ReturnSiteData> ListSites(
            string ApplicationPassword,
            int ClientID)
        {
            ReturnData.ReturnSiteData DSReturnSites = null;

            SqlDataReader SiteList = null;
            List<ReturnData.ReturnSiteData> ReturnedSites = new List<ReturnData.ReturnSiteData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedSites = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "SiteName, " +
                                            "Telephone, " +
                                            "Facsimile, " +
                                            "Email, " +
                                            "EngagementDate, " +
                                            "TerminationDate " + 
                                        "From " +
                                            "Sites " +
                                        "Where " +
                                            "ClientID = " + ClientID + " " +
                                            "And DeletedAtUTC is null " +
                                        "Order By " +
                                            "SiteName";
                    SiteList = com.ExecuteReader();

                    while (SiteList.Read())
                    {
                        DSReturnSites = new ReturnData.ReturnSiteData();

                        DSReturnSites.SiteName = SiteList["SiteName"].ToString();
                        DSReturnSites.Telephone = SiteList["Telephone"].ToString();
                        DSReturnSites.Facsimile = SiteList["Facsimile"].ToString();
                        DSReturnSites.Email = SiteList["Email"].ToString();
                        DSReturnSites.EngagementDate = SiteList["EngagementDate"].ToString();
                        DSReturnSites.TerminationDate = SiteList["TerminationDate"].ToString(); 

                        ReturnedSites.Add(DSReturnSites);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedSites = null;
            }
            catch (Exception Ex)
            {
                ReturnedSites = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedSites;
        }




        [WebMethod(Description = "Mobitime Web Service - Update Site detail in MobiTime")]
        public bool Update(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
            string SiteName,
            string Telephone,
            string Facsimile,
            string Email,
            string Physical01,
            string Physical02,
            string Physical03,
            string Physical04,
            string PhysicalCountry,
            string PhysicalCode,
            string Postal01,
            string Postal02,
            string Postal03,
            string Postal04,
            string PostalCountry,
            string PostalCode,
            string GGPS01,
            string GGPS02,
            DateTime EngagementDate,
            int AuthoTA,
            int AuthoTS,
            int AuthoTR,
            string SMTPAddress,
            bool SMTPSSL,
            bool SMTPAuthenticate,
            string SMTPUsername,
            string SMTPPassword,
            DateTime TerminationDate,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Sites", "SiteID", SiteID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Sites " +
                                            "Set " +
                                                "SiteName = '" + SiteName + "', " +
                                                "Telephone = '" + Telephone + "', " +
                                                "Facsimile = '" + Facsimile + "', " +
                                                "Email = '" + Email + "', " +
                                                "Physical01 = '" + Physical01 + "', " +
                                                "Physical02 = '" + Physical02 + "', " +
                                                "Physical03 = '" + Physical03 + "', " +
                                                "Physical04 = '" + Physical04 + "', " +
                                                "PhysicalCountry = '" + PhysicalCountry + "', " +
                                                "PhysicalCode = '" + PhysicalCode + "', " +
                                                "Postal01 = '" + Postal01 + "', " +
                                                "Postal02 = '" + Postal02 + "', " +
                                                "Postal03 = '" + Postal03 + "', " +
                                                "Postal04 = '" + Postal04 + "', " +
                                                "PostalCountry = '" + PostalCountry + "', " +
                                                "PostalCode = '" + PostalCode + "', " +
                                                "GGPS01 = '" + GGPS01 + "', " +
                                                "GGPS02 = '" + GGPS02 + "', " +
                                                "EngagementDate = '" + EngagementDate + "', " +
                                                "AuthoTA = '" + AuthoTA + "', " +
                                                "AuthoTS = '" + AuthoTS + "', " +
                                                "AuthoTR = '" + AuthoTR + "', " +
                                                "SMTPAddress = '" + SMTPAddress + "', " +
                                                "SMTPSSL = '" + SMTPSSL + "', " +
                                                "SMTPAuthenticate = '" + SMTPAuthenticate + "', " +
                                                "SMTPUsername = '" + SMTPUsername + "', " +
                                                "SMTPPassword = '" + SMTPPassword + "', " +
                                                "TerminationDate = '" + TerminationDate + "', " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "ClientID = " + ClientID + " " +
                                                "And SiteID = " + SiteID + "";
                        com.ExecuteNonQuery();
                        Successful = true;
                    }
                    else
                    {
                        Successful = false;
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




        [WebMethod(Description = "Mobitime Web Service - Delete a Site from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Sites", "SiteID", SiteID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Sites " +
                                            "Set " +
                                                "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "DeletedBy = '" + UserGuid + "' " +
                                            "Where " +
                                                "ClientID = " + ClientID + " " +
                                                "And SiteID = " + SiteID + "";
                        com.ExecuteNonQuery();
                        Successful = true;
                    }
                    else
                    {
                        Successful = false;
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




        [WebMethod(Description = "Mobitime Web Service - Recover a Site in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int ClientID,
            int SiteID,
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
                    if (Functions.AuditTrails.BackupRecord(0, SiteID, UserGuid, "Sites", "SiteID", SiteID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Sites " +
                                            "Set " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "ClientID = " + SiteID + " " +
                                                "And SiteID = " + SiteID + "";
                        com.ExecuteNonQuery();
                        Successful = true;
                    }
                    else
                    {
                        Successful = false;
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