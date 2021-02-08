using TNBCSurvey.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using TNBCSurvey.Service;

namespace TNBCSurvey.DAL
{
    public class SurveyTicketRepository
    {
        readonly IDbConnection _dbConnection;
        public SurveyTicketRepository(IDbConnection connection)
        {
            _dbConnection = connection;
        }
        public SurveyTicketRepository()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public void CreateSurveyTicket(Client client)
        {
            string tokenString = Guid.NewGuid().ToString();
            var sql = @"insert into SurveyTickets (Client_SID, Token, ExpirationDate, TokenUsed, TokenUsedDate)
                            values (@Client_SID, @Token, @ExpirationDate , @TokenUsed, null)";

            _dbConnection.Execute(sql, new { Client_SID = client.Client_SID, Token = tokenString, ExpirationDate = DateTime.Now.AddDays(21), TokenUsed = false });
            String link = "https://newbegininingcenter.azurewebsites.net/#!/survey/" + client.Client_SID.ToString() + "/" + tokenString;

            var emailTemplateService = new EmailTemplateService();
            var body = emailTemplateService.getMailBody(link);

            var emailService = new EmailService();
            emailService.sendMail("New Beginnings Follow Up Survey", body, client.Email);
        }

        public string CreateandCopySurveyTicket(Client client)
        {
            var sql = @"Select Token from SurveyTickets
                            where Client_SID = @ClientId";

            var token = Convert.ToString(_dbConnection.ExecuteScalar(sql, new { ClientId = client.Client_SID }));

            String link = "https://newbegininingcenter.azurewebsites.net/#!/survey/" + client.Client_SID.ToString() + "/" + token;

            return link;
        }

        public void ResendSurveyTicket(Client client)
        {
            string link = "";
            var sql = @"select Ticket_SID, Client_SID, Token, ExpirationDate, TokenUsed
                        from SurveyTickets where Client_SID = @client_SID";
            var reader = _dbConnection.ExecuteReader(sql, new { Client_SID = client.Client_SID });
            DataTable dt = new DataTable();
            dt.Load(reader);
            if (dt.Rows.Count > 0)
            {
                link = "https://newbeginningscenter.azurewebsites.net/survey/" + client.Client_SID.ToString() + "/" + dt.Rows[0]["Token"].ToString();

                var emailTemplateService = new EmailTemplateService();
                var body = emailTemplateService.getMailBody(link);

                EmailService emailService = new EmailService();
                emailService.sendMail("New Beginnings Follow Up Survey", body, client.Email);
            }
        }

        public DataRow GetOneByToken(int id, string token)
        {
            var sql = @"select * from SurveyTickets
                            where Client_SID = @Client_SID and Token = @Token and getdate() <= ExpirationDate and TokenUsed <> 1;";
            var reader = _dbConnection.ExecuteReader(sql, new { Client_SID = id, Token = token });
            DataTable dt = new DataTable();
            dt.Load(reader);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public int SetTokenUsed(int id, string token)
        {
            var sql = @"update SurveyTickets set TokenUsed = 1, TokenUsedDate = GETDATE()
                            where Client_SID = @Client_SID and Token = @Token and getdate() <= ExpirationDate and TokenUsed <> 1;";

            return Convert.ToInt32(_dbConnection.ExecuteScalar(sql, new { Client_SID = id, Token = token }));
        }

        public void SetAppStatus(string status)
        {
            var sql = @"update dbo.ApplicationParameters set Para_Value = @Para_Value
                            where Para_Desc = 'Survey_Enabled';";

            _dbConnection.ExecuteScalar(sql, new { Para_Value = status });
        }

        public bool GetAppStatus()
        {
            var sql = @"select Para_Value from dbo.ApplicationParameters
                            where Para_Desc = 'Survey_Enabled';";

            var reader = _dbConnection.ExecuteReader(sql);
            DataTable dt = new DataTable();
            dt.Load(reader);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["Para_Value"].ToString() == "1";
            return false;
        }

        public List<object> GetTicketTimePeriods()
        {
            var sql = @"exec dbo.GetTicketTimePeriods;";
            var reader = _dbConnection.ExecuteReader(sql);
            DataTable dt = new DataTable();
            dt.Load(reader);
            List<object> ct = new List<object>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ct.Add(
                        new
                        {
                            Time_Period = dt.Rows[i]["Time_Period"],
                            Expiration_Date = dt.Rows[i]["Expiration_Date"]
                        });
                }
            }

            return ct;
        }

        public List<object> GetClientTickets(string tiemPeriod)
        {
            var sql = @"exec dbo.getClientTicketList @TimePeriod;";
            var reader = _dbConnection.ExecuteReader(sql, new { TimePeriod = tiemPeriod });
            DataTable dt = new DataTable();
            dt.Load(reader);
            List<object> ct = new List<object>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ct.Add(
                        new
                        {
                            Client_SID = dt.Rows[i]["Client_SID"],
                            FirstName = dt.Rows[i]["FirstName"],
                            LastName = dt.Rows[i]["LastName"],
                            GroupNumber = dt.Rows[i]["GroupNumber"],
                            Email = dt.Rows[i]["Email"],
                            TimePeriod = dt.Rows[i]["TimePeriod"],
                            Ticket_SID = dt.Rows[i]["Ticket_SID"],
                            Token = dt.Rows[i]["Token"],
                            TokenUsed = dt.Rows[i]["TokenUsed"],
                            TokenUsedDate = dt.Rows[i]["TokenUsedDate"],
                            TokenExpired = dt.Rows[i]["TokenExpired"]
                        });
                }
            }

            return ct;
        }
    }
}