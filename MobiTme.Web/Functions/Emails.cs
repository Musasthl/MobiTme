using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Data.SqlClient;

namespace MobiTime.Functions
{
    public class Emails
    {
        
        
        public static void Send()//(string[] args)
        {
            SqlCommand com = new SqlCommand();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MobiTimeCon"].ToString());
            MailMessage Email = new MailMessage();
            SmtpClient SmtpC = new SmtpClient();

            SqlDataReader FromDetail = null;
            string[] EmailFrom = new string[]{};

            try
            {
                //if (From != "System")
                //{
                //    com.CommandText = "Select " +
                //                            "Email, " +
                //                            "FirstNames, " +
                //                            "Surname " +
                //                        "From " +
                //                            "Users " +
                //                        "Where " +
                //                            "GuidID = '" + From + "'";
                //    FromDetail = com.ExecuteReader();

                //    while (FromDetail.Read())
                //    {

                //    }
                //    EmailFrom = {"application@powerup.co.za"; "Application"};
                //}
                //else
                //{
                //    string [] EmailFrom = {"application@powerup.co.za", "Application"};
                //    SendEmailFrom = EmailFrom[2];
                //}

                Email.From = new MailAddress("application@powerup.co.za", "Application@PowerUp");
                Email.To.Add(new MailAddress("clint.mason@enersyspowertech.co.za", "Clint Mason"));
                //m.CC.Add(new MailAddress("CC@yahoo.com", "Display name CC"));
                //similarly BCC

                Email.Subject = "Test1";
                Email.IsBodyHtml = true;

                Email.Body = "<html><body><b>This</b> is a Test Mail</body></html>";

                //FileStream fs = new FileStream("E:\\TestFolder\\test.pdf", FileMode.Open, FileAccess.Read);
                //Attachment a = new Attachment(fs, "test.pdf", MediaTypeNames.Application.Octet);
                //m.Attachments.Add(a);
                //string str = "<html><body><h1>Picture</h1><br/><img src=\"cid:image1\"></body></html>";
                //AlternateView av = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
                //LinkedResource lr = new LinkedResource("E:\\Photos\\hello.jpg", MediaTypeNames.Image.Jpeg);
                //lr.ContentId = "image1"; av.LinkedResources.Add(lr);
                //m.AlternateViews.Add(av);
                SmtpC.Host = "smtp.powerup.co.za";
                SmtpC.Port = 587;
                SmtpC.Credentials = new System.Net.NetworkCredential("clint.mason@powerup.co.za", "Nos4mCl1nt");
                SmtpC.EnableSsl = false;
                SmtpC.Send(Email);

                //Console.ReadLine();

            }
            catch (Exception Ex)
            {
            }
            finally
            {
            }
        }
    }
}