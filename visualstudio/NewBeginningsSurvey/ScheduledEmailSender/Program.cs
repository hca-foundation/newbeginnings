﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledEmailSender
{
    class Program
    {
        private static string _EmailTemplate = @"
<html>
<head>
    <style type='text/css'>
        body {
            font-family: avenir-lt-w01_35-light1475496,Arial,Helvetica,sans-serif;
            line-height: 1.5;
        }

        p {
            margin-bottom: 20px;
        }

        .content {
            max-width: 500px;
            margin-left: auto;
            margin-right: auto;
            margin-top: 15px;
        }

        .header-image {
            margin-left: auto;
            margin-right: auto;
            display: block;
            width: 195px;
        }
    </style>
</head>
<body>
    <div class='content'>
        <div>
            <img src='https://newbegininingcenter.azurewebsites.net/Content/images/newBeginnings.png' alt='New Beginnings logo' class='header-image' />
        </div>
        <p>
            Greetings from The New Beginnings Center!
        </p>
        <p>
            I hope this finds you healthy and well. We are reaching out to all of our New Beginnings clients and would love to hear how you’re doing. Please take a moment to complete our
            <a href='{surveyLinkUrl}'>New Beginnings Client Follow Up Survey</a>.The survey link expires in three weeks.
        </p>
        <p>
            If you would like a check-in call from a coach, please reach to me via below email or phone. Do not reply this email directly. We’d be very happy to set up a call!
        </p>
        <p>
            Be well,
            <br />
            Karen
        </p>
        <img src='https://newbegininingcenter.azurewebsites.net/Content/images/newBeginnings-small.png' alt='New Beginnings logo' />
        <div>
            <a href='http://www.thenewbeginningscenter.org'>http://www.thenewbeginningscenter.org</a>
        </div>
    </div>
</body>
</html>
";
        static void Main(string[] args)
        {
            var defaultConnection = "placeholder";
            SqlConnection DBConnection = new SqlConnection(defaultConnection);
            string sql = @"exec dbo.makeBatchTickets";
            SqlCommand cmd = new SqlCommand(sql, DBConnection);
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection.Open();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows.Count > 0)
                {
                    var a = new Program();
                    foreach (DataRow r in dt.Rows)
                    {
                        string Token = r["Token"].ToString();
                        string Client_SID = r["Client_SID"].ToString();
                        string Email = r["Email"].ToString();
                        String link = "https://newbegininingcenter.azurewebsites.net/#!/survey/" + Client_SID.ToString() + "/" + Token;

                        var body = a.getMailBody(link);
                        a.sendMail("New Beginnings Follow Up Survey", body, Email);
                    }
                }
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public void sendMail(string subject, string msg, string receiver)
        {
            var emailHost = "placeholder";
            var emailUsername = "placeholder";
            var emailPassword = "placeholder";

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

        public string getMailBody(string surveyLinkUrl)
        {
        var body = _EmailTemplate.Replace("{surveyLinkUrl}", surveyLinkUrl);
            return body;
        }
    }
}
