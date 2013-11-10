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
    public class Clients : System.Web.Services.WebService
    {
        SqlCommand com = new SqlCommand();
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());




        [WebMethod(Description = "Mobitime Web Service - Insert new Client detail into MobiTime")]
        public bool Insert(
            string ApplicationPassword,
            string RegisteredName,
            string TradingName,
            string Country_Registration,
            string RegistrationNumber,
            string RegistrationType,
            string Physical01,
            string Physical02,
            string Physical03,
            string Physical04,
            string Country_Physical,
            string PhysicalCode,
            string Postal01,
            string Postal02,
            string Postal03,
            string Postal04,
            string Country_Postal,
            string PostalCode,
            string BillingAttention,
            string BillingTo,
            string Billing01,
            string Billing02,
            string Billing03,
            string Billing04,
            string Country_Billing,
            string BillingCode,
            string BillingEmail,
            string BillingOrderNumber,
            string UserGuid)
        {
            int ClientID = 0;
            bool InsertedClient = true;
            int InsertedID = 0;

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    InsertedClient = false;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "Count (ClientID) " +
                                        "From " +
                                            "Clients " +
                                        "Where " +
                                            "Country_Registration = '" + Country_Registration + "' " +
                                            "And RegistrationNumber = '" + RegistrationNumber + "' " +
                                            "And RegistrationType = '" + RegistrationType + "'";
                    int a = (int)com.ExecuteScalar();

                    if (a > 0)
                    {
                        com.CommandText = "Select Top 1 " +
                                           "ClientID " +
                                       "From " +
                                           "Clients " +
                                       "Where " +
                                           "Country_Registration = '" + Country_Registration + "' " +
                                           "And RegistrationNumber = '" + RegistrationNumber + "' " +
                                           "And RegistrationType = '" + RegistrationType + "'";
                        ClientID = (int)com.ExecuteScalar();

                        if (Functions.AuditTrails.BackupRecord(ClientID, 0, UserGuid, "Clients", "ClientID", ClientID) == true)
                        {
                            com.CommandText = "Update Clients " +
                                                "Set " +
                                                    "RegisteredName = '" + RegisteredName + "', " +
                                                    "TradingName = '" + TradingName + "', " +
                                                    "Country_Registration = '" + Country_Registration + "', " +
                                                    "RegistrationNumber = '" + RegistrationNumber + "', " +
                                                    "RegistrationType = '" + RegistrationType + "', " +
                                                    "Physical01 = '" + Physical01 + "', " +
                                                    "Physical02 = '" + Physical02 + "', " +
                                                    "Physical03 = '" + Physical03 + "', " +
                                                    "Physical04 = '" + Physical04 + "', " +
                                                    "Country_Physical = '" + Country_Physical + "', " +
                                                    "PhysicalCode = '" + PhysicalCode + "', " +
                                                    "Postal01 = '" + Postal01 + "', " +
                                                    "Postal02 = '" + Postal02 + "', " +
                                                    "Postal03 = '" + Postal03 + "', " +
                                                    "Postal04 = '" + Postal04 + "', " +
                                                    "Country_Postal = '" + Country_Postal + "', " +
                                                    "PostalCode = '" + PostalCode + "', " +
                                                    "BillingAttention = '" + BillingAttention + "', " +
                                                    "BillingTo = '" + BillingTo + "', " +
                                                    "Billing01 = '" + Billing01 + "', " +
                                                    "Billing02 = '" + Billing02 + "', " +
                                                    "Billing03 = '" + Billing03 + "', " +
                                                    "Billing04 = '" + Billing04 + "', " +
                                                    "Country_Billing = '" + Country_Billing + "', " +
                                                    "BillingCode = '" + BillingCode + "', " +
                                                    "BillingEmail = '" + BillingEmail + "', " +
                                                    "BillingOrderNumber  = '" + BillingOrderNumber + "', " +
                                                    "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " + 
                                                "Where " + 
                                                    "ClientID = " + ClientID +"";
                            com.ExecuteNonQuery();

                            InsertedID = ClientID;
                            InsertedClient = true;
                        }
                        else
                        {
                            InsertedID = 0;
                            InsertedClient = false;
                        }
                    }
                    else
                    {
                        com.CommandText = "Insert Into Clients (" +
                                                "RegisteredName, " +
                                                "TradingName, " +
                                                "Country_Registration, " +
                                                "RegistrationNumber, " +
                                                "RegistrationType, " +
                                                "Physical01, " +
                                                "Physical02, " +
                                                "Physical03, " +
                                                "Physical04, " +
                                                "Country_Physical, " +
                                                "PhysicalCode, " +
                                                "Postal01, " +
                                                "Postal02, " +
                                                "Postal03, " +
                                                "Postal04, " +
                                                "Country_Postal, " +
                                                "PostalCode, " +
                                                "BillingAttention, " +
                                                "BillingTo, " +
                                                "Billing01, " +
                                                "Billing02, " +
                                                "Billing03, " +
                                                "Billing04, " +
                                                "Country_Billing, " +
                                                "BillingCode, " +
                                                "BillingEmail, " +
                                                "BillingOrderNumber, " +
                                                "CapturedAtUTC, " +
                                                "CapturedBy) " +
                                            "Output " +
                                                "Inserted.ClientID " +
                                            "Select " +
                                                "'" + RegisteredName + "', " +
                                                "'" + TradingName + "', " +
                                                "'" + Country_Registration + "', " +
                                                "'" + RegistrationNumber + "', " +
                                                "'" + RegistrationType + "', " +
                                                "'" + Physical01 + "', " +
                                                "'" + Physical02 + "', " +
                                                "'" + Physical03 + "', " +
                                                "'" + Physical04 + "', " +
                                                "'" + Country_Physical + "', " +
                                                "'" + PhysicalCode + "', " +
                                                "'" + Postal01 + "', " +
                                                "'" + Postal02 + "', " +
                                                "'" + Postal03 + "', " +
                                                "'" + Postal04 + "', " +
                                                "'" + Country_Postal + "', " +
                                                "'" + PostalCode + "', " +
                                                "'" + BillingAttention + "', " +
                                                "'" + BillingTo + "', " +
                                                "'" + Billing01 + "', " +
                                                "'" + Billing02 + "', " +
                                                "'" + Billing03 + "', " +
                                                "'" + Billing04 + "', " +
                                                "'" + Country_Billing + "', " +
                                                "'" + BillingCode + "', " +
                                                "'" + BillingEmail + "', " +
                                                "'" + BillingOrderNumber + "', " +
                                                "'" + DateTime.UtcNow + "', " +
                                                "'" + UserGuid + "'";
                        InsertedID = (int)com.ExecuteScalar();
                        InsertedClient = true;
                    }
                }
            }
            catch (SqlException Ex)
            {
                InsertedID = 0;
                InsertedClient = false;
            }
            catch (Exception Ex)
            {
                InsertedID = 0;
                InsertedClient = false;
            }
            finally
            {
                con.Close();
            }
            return InsertedClient;
        }




        [WebMethod(Description = "Mobitime Web Service - Select Client detail (1 Client Record) from MobiTime")]
        public List<ReturnData.ReturnClientData> Select(
            string ApplicationPassword,
            int ClientID)
        {
            ReturnData.ReturnClientData DSReturnCleints = null;

            SqlDataReader ClientList = null;
            List<ReturnData.ReturnClientData> ReturnedClients = new List<ReturnData.ReturnClientData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedClients = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "ClientID, " +
                                            "RegisteredName, " +
                                            "TradingName, " +
                                            "Country_Registration, " +
                                            "RegistrationNumber, " +
                                            "RegistrationType, " +
                                            "Physical01, " +
                                            "Physical02, " +
                                            "Physical03, " +
                                            "Physical04, " +
                                            "Country_Physical, " +
                                            "PhysicalCode, " +
                                            "Postal01, " +
                                            "Postal02, " +
                                            "Postal03, " +
                                            "Postal04, " +
                                            "Country_Postal, " +
                                            "PostalCode, " +
                                            "BillingAttention, " +
                                            "BillingTo, " +
                                            "Billing01, " +
                                            "Billing02, " +
                                            "Billing03, " +
                                            "Billing04, " +
                                            "Country_Billing, " +
                                            "BillingCode, " +
                                            "BillingEmail, " +
                                            "BillingOrderNumber " +
                                        "From " +
                                            "Clients " +
                                        "Where " +
                                            "ClientID = " + ClientID + " " +
                                            "And DeletedAtUTC is null";
                    ClientList = com.ExecuteReader();

                    while (ClientList.Read())
                    {
                        DSReturnCleints = new ReturnData.ReturnClientData();
                        DSReturnCleints.ClientID = ClientList["ClientID"].ToString();
                        DSReturnCleints.RegisteredName = ClientList["RegisteredName"].ToString();
                        DSReturnCleints.TradingName = ClientList["TradingName"].ToString();
                        DSReturnCleints.Country_Registration = ClientList["Country_Registration"].ToString();
                        DSReturnCleints.RegistrationNumber = ClientList["RegistrationNumber"].ToString();
                        DSReturnCleints.RegistrationType = ClientList["RegistrationType"].ToString();
                        DSReturnCleints.Physical01 = ClientList["Physical01"].ToString();
                        DSReturnCleints.Physical02 = ClientList["Physical02"].ToString();
                        DSReturnCleints.Physical03 = ClientList["Physical03"].ToString();
                        DSReturnCleints.Physical04 = ClientList["Physical04"].ToString();
                        DSReturnCleints.Country_Physical = ClientList["Country_Physical"].ToString();
                        DSReturnCleints.PhysicalCode = ClientList["PhysicalCode"].ToString();
                        DSReturnCleints.Postal01 = ClientList["Postal01"].ToString();
                        DSReturnCleints.Postal02 = ClientList["Postal02"].ToString();
                        DSReturnCleints.Postal03 = ClientList["Postal03"].ToString();
                        DSReturnCleints.Postal04 = ClientList["Postal04"].ToString();
                        DSReturnCleints.Country_Postal = ClientList["Country_Postal"].ToString();
                        DSReturnCleints.PostalCode = ClientList["PostalCode"].ToString();
                        DSReturnCleints.BillingAttention = ClientList["BillingAttention"].ToString();
                        DSReturnCleints.BillingTo = ClientList["BillingTo"].ToString();
                        DSReturnCleints.Billing01 = ClientList["Billing01"].ToString();
                        DSReturnCleints.Billing02 = ClientList["Billing02"].ToString();
                        DSReturnCleints.Billing03 = ClientList["Billing03"].ToString();
                        DSReturnCleints.Billing04 = ClientList["Billing04"].ToString();
                        DSReturnCleints.Country_Billing = ClientList["Country_Billing"].ToString();
                        DSReturnCleints.BillingCode = ClientList["BillingCode"].ToString();
                        DSReturnCleints.BillingEmail = ClientList["BillingEmail"].ToString();
                        DSReturnCleints.BillingOrderNumber = ClientList["BillingOrderNumber"].ToString();

                        ReturnedClients.Add(DSReturnCleints);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedClients = null;
            }
            catch (Exception Ex)
            {
                ReturnedClients = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedClients;
        }



        
        [WebMethod(Description = "MobiTime Web Service - Select a Client Detail List from MobiTime")]
        public List<ReturnData.ReturnClientData> ListClients(
            string ApplicationPassword)
        {
            ReturnData.ReturnClientData DSReturnCleints = null;

            SqlDataReader ClientList = null;
            List<ReturnData.ReturnClientData> ReturnedClients = new List<ReturnData.ReturnClientData>();

            try
            {
                if (Functions.Authentication.Authenticate(ApplicationPassword) == false)
                {
                    ReturnedClients = null;
                }
                else
                {
                    com.Connection = con;
                    con.Open();

                    com.CommandText = "Select " +
                                            "ClientID, " +
                                            "RegisteredName, " +
                                            "TradingName, " +
                                            "Country_Registration, " +
                                            "RegistrationNumber, " +
                                            "RegistrationType " +
                                        "From " +
                                            "Clients " +
                                        "Where " +
                                            "DeletedAtUTC is null " + 
                                        "Order By " + 
                                            "TradingName, RegisteredName";
                    ClientList = com.ExecuteReader();

                    while (ClientList.Read())
                    {
                        DSReturnCleints = new ReturnData.ReturnClientData();
                        
                        DSReturnCleints.ClientID = ClientList["ClientID"].ToString();
                        DSReturnCleints.RegisteredName = ClientList["RegisteredName"].ToString();
                        DSReturnCleints.TradingName = ClientList["TradingName"].ToString();
                        DSReturnCleints.Country_Registration = ClientList["Country_Registration"].ToString();
                        DSReturnCleints.RegistrationNumber = ClientList["RegistrationNumber"].ToString();
                        DSReturnCleints.RegistrationType = ClientList["RegistrationType"].ToString();

                        ReturnedClients.Add(DSReturnCleints);
                    }
                }
            }
            catch (SqlException Ex)
            {
                ReturnedClients = null;
            }
            catch (Exception Ex)
            {
                ReturnedClients = null;
            }
            finally
            {
                con.Close();
            }
            return ReturnedClients;
        }




        [WebMethod(Description = "Mobitime Web Service - Update Client detail in MobiTime")]
        public bool Update(
            string ApplicationPassword,
            int ClientID,
            string RegisteredName,
            string TradingName,
            int Country_Registration,
            string RegistrationNumber,
            int RegistrationType,
            string Physical01,
            string Physical02,
            string Physical03,
            string Physical04,
            int Country_Physical,
            string PhysicalCode,
            string Postal01,
            string Postal02,
            string Postal03,
            string Postal04,
            int Country_Postal,
            string PostalCode,
            string BillingAttention,
            string BillingTo,
            string Billing01,
            string Billing02,
            string Billing03,
            string Billing04,
            int Country_Billing,
            string BillingCode,
            string BillingEmail,
            string BillingOrderNumber,
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, 0, UserGuid, "Clients", "ClientID", ClientID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Clients " +
                                            "Set " +
                                                "RegisteredName = '" + RegisteredName + "', " +
                                                "TradingName = '" + TradingName + "', " +
                                                "Country_Registration = '" + Country_Registration + "', " +
                                                "RegistrationNumber = '" + RegistrationNumber + "', " +
                                                "RegistrationType = '" + RegistrationType + "', " +
                                                "Physical01 = '" + Physical01 + "', " +
                                                "Physical02 = '" + Physical02 + "', " +
                                                "Physical03 = '" + Physical03 + "', " +
                                                "Physical04 = '" + Physical04 + "', " +
                                                "Country_Physical = '" + Country_Physical + "', " +
                                                "PhysicalCode = '" + PhysicalCode + "', " +
                                                "Postal01 = '" + Postal01 + "', " +
                                                "Postal02 = '" + Postal02 + "', " +
                                                "Postal03 = '" + Postal03 + "', " +
                                                "Postal04 = '" + Postal04 + "', " +
                                                "Country_Postal = '" + Country_Postal + "', " +
                                                "PostalCode = '" + PostalCode + "', " +
                                                "BillingAttention = '" + BillingAttention + "', " +
                                                "BillingTo = '" + BillingTo + "', " +
                                                "Billing01 = '" + Billing01 + "', " +
                                                "Billing02 = '" + Billing02 + "', " +
                                                "Billing03 = '" + Billing03 + "', " +
                                                "Billing04 = '" + Billing04 + "', " +
                                                "Country_Billing = '" + Country_Billing + "', " +
                                                "BillingCode = '" + BillingCode + "', " +
                                                "BillingEmail = '" + BillingEmail + "', " +
                                                "BillingOrderNumber  = '" + BillingOrderNumber + "', " +
                                                "UpdatedAtUTC = '" + DateTime.UtcNow + "', " +
                                                "UpdatedBy = '" + UserGuid + "', " +
                                                "DeletedAtUTC = null, " +
                                                "DeletedBy = null " +
                                            "Where " +
                                                "ClientID = " + ClientID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Delete a Client from MobiTime")]
        public bool Delete(
            string ApplicationPassword,
            int ClientID,
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, 0, UserGuid, "Clients", "ClientID", ClientID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Clients " +
                                                "Set " +
                                                    "DeletedAtUTC = '" + DateTime.UtcNow + "', " +
                                                    "DeletedBy = '" + UserGuid + "' " +
                                                "Where " +
                                                    "ClientID = " + ClientID + "";
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




        [WebMethod(Description = "Mobitime Web Service - Recover a Client in MobiTime")]
        public bool Recover(
            string ApplicationPassword,
            int ClientID,
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
                    if (Functions.AuditTrails.BackupRecord(ClientID, 0, UserGuid, "Clients", "ClientID", ClientID) == true)
                    {
                        com.Connection = con;
                        con.Open();

                        com.CommandText = "Update Clients " +
                                                "Set " +
                                                    "UpdatedAtUTC  = '" + DateTime.UtcNow + "', " +
                                                    "UpdatedBy = '" + UserGuid + "', " +
                                                    "DeletedAtUTC = null, " +
                                                    "DeletedBy = null " +
                                                "Where " +
                                                    "ClientID = " + ClientID + "";
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
