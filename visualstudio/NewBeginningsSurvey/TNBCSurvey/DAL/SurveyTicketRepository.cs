using TNBCSurvey.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using TNBCSurvey.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;

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
            var sql = @"insert into SurveyTickets
                            values (@ClientId, @Token, @ExpirationDate , @TokenUsed)";

             _dbConnection.Execute(sql, new { ClientId = client.Client_SID, Token = tokenString, ExpirationDate = DateTime.Now.AddDays(14), TokenUsed = false });

            var emailService = new EmailService();
            var link = "https://example.com/survey?clientid=" + client.Client_SID.ToString() + "&token=" + tokenString;
            var body = emailService.getMailBody(link);
            emailService.sendMail("New Beginnings Follow Up Survey", body, client.Email);
        }

        public int GetOneByToken(int id, string token)
        {
            var sql = @"select count(1) from SurveyTickets
                            where Client_SID = @id and Token = @token and getdate() < ExpirationDate and TokenUsed <> 1;";

            return Convert.ToInt32(_dbConnection.ExecuteScalar(sql, new { Id = id, Token = token }));
        }
    }
}