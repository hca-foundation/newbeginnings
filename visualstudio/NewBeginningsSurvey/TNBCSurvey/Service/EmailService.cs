using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Configuration;

namespace TNBCSurvey.Service
{
    public class EmailService
    {

        public EmailService()
        {
        }

        public void sendMail(string subject, string msg, string receiver)
        {
            var emailHost = ConfigurationManager.AppSettings["emailHost"];
            var emailUsername = ConfigurationManager.AppSettings["emailUsername"];
            var emailPassword = ConfigurationManager.AppSettings["emailPassword"];

            MailMessage objeto_mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = emailHost;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(emailUsername, emailPassword);
            objeto_mail.From = new MailAddress(emailUsername, "The New Beginnings Center");
            objeto_mail.To.Add(new MailAddress(receiver));
            objeto_mail.IsBodyHtml = true;
            objeto_mail.Subject = subject;
            objeto_mail.Body = msg;
            client.Send(objeto_mail);
        }
    }
}