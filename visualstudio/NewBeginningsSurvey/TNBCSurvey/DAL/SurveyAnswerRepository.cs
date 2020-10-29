using TNBCSurvey.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using TNBCSurvey.Controllers;
using System.Configuration;
using System.Data.SqlClient;

namespace TNBCSurvey.DAL
{
    public class SurveyAnswerRepository
    {
        readonly IDbConnection _dbConnection;
        public SurveyAnswerRepository(IDbConnection connection)
        {
            _dbConnection = connection;
        }
        public SurveyAnswerRepository()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public void Add(int client_SID, string question_Period, int question_SID, string answer_Text)
        {
            var sql = @"insert into SurveyAnswers (Client_SID, Question_Period, Question_SID, Answer_Text)
                            values (@Client_SID, @Question_Period, @Question_SID, @Answer_Text);";

            _dbConnection.Execute(sql, new { Client_SID = client_SID, Question_Period = question_Period, Question_SID = question_SID, Answer_Text = answer_Text });
        }
    }
}