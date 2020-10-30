﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace TNBCSurvey.Service
{
    public class EmailService
    {

        public EmailService()
        {
        }

        public void sendMail(string subject, string msg, string receiver)
        {
            //MailMessage objeto_mail = new MailMessage();
            //objeto_mail.To.Add(new MailAddress(receiver));
            //objeto_mail.From = new MailAddress("andrewparttwo@gmail.com");
            //objeto_mail.Subject = subject;
            //objeto_mail.Body = msg;
            //SmtpClient client = new SmtpClient();
            //client.UseDefaultCredentials = true;
            //client.Send(objeto_mail);

            MailMessage objeto_mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("andrewparttwo@gmail.com", "moonimoonio0");
            objeto_mail.From = new MailAddress("NoReply@tnbcenter.org", "The New Beginnings Center");
            objeto_mail.To.Add(new MailAddress(receiver));
            objeto_mail.IsBodyHtml = true;
            objeto_mail.Subject = subject;
            objeto_mail.Body = msg;
            client.Send(objeto_mail);


            //MailMessage objeto_mail = new MailMessage();
            //SmtpClient client = new SmtpClient();
            //client.Port = 587;
            //client.Host = "smtp-relay.gmail.com";
            //client.Timeout = 10000;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = true;
            //client.EnableSsl = true;
            //client.Credentials = new NetworkCredential("andrewparttwo@gmail.com", "moonimoonio0");
            //objeto_mail.From = new MailAddress("andrewparttwo@gmail.com", "Andrew Hill");
            //objeto_mail.To.Add(new MailAddress(receiver));
            //objeto_mail.Subject = subject;
            //objeto_mail.Body = msg;
            //client.Send(objeto_mail);
        }
    }
}