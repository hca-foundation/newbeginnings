using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TNBCSurvey.Services
{
    class Email
    {
        public Email()
        {
        }
        public void sendMail(string subject, string msg, string receiver)
        {
            MailMessage objeto_mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.Host = "smtp-relay.gmail.com";
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            objeto_mail.From = new MailAddress("NoReply@TheNewBeginningsCenter.com");
            objeto_mail.To.Add(new MailAddress(receiver));
            objeto_mail.Subject = subject;
            objeto_mail.Body = msg;
            client.Send(objeto_mail);
        }
    }
}
