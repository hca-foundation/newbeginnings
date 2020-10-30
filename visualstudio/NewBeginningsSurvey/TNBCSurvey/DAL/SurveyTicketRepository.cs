﻿using TNBCSurvey.Models;
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
                            values (@Client_SID, @Token, @ExpirationDate , @TokenUsed)";

             _dbConnection.Execute(sql, new { Client_SID = client.Client_SID, Token = tokenString, ExpirationDate = DateTime.Now.AddDays(21), TokenUsed = false });
            // Send the email:
            String link = "https://newbeginningscenter.azurewebsites.net/survey/" + client.Client_SID.ToString() + "/" + tokenString;

            (new Email()).sendMail("TEST", link, client.Email);
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
                var body = link;
                //var body = emailService.getMailBody(link);
                (new Email()).sendMail("New Beginnings Follow Up Survey", body, client.Email);
            }
        }

        public int GetOneByToken(int id, string token)
        {
            var sql = @"select count(1) from SurveyTickets
                            where Client_SID = @Client_SID and Token = @Token and getdate() <= ExpirationDate and TokenUsed <> 1;";

            return Convert.ToInt32(_dbConnection.ExecuteScalar(sql, new { Client_SID = id, Token = token }));
        }

        public int SetTokenUsed(int id, string token)
        {
            var sql = @"update SurveyTickets set TokenUsed = 1
                            where Client_SID = @Client_SID and Token = @Token and getdate() <= ExpirationDate and TokenUsed <> 1;";

            return Convert.ToInt32(_dbConnection.ExecuteScalar(sql, new { Client_SID = id, Token = token }));
        }
    }
}