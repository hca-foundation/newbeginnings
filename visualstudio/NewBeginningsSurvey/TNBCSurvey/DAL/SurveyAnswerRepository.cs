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

        public IEnumerable<SurveyExportAnswer> GetSurveyResultsByClientId(string clientId)
        {
            var sql = @"
                select c.Client_SID, c.LastName, c.FirstName, sq.Question_Text, sa.Answer_Text
                from dbo.SurveyQuestions sq
                left join dbo.SurveyAnswers sa on sa.Question_SID = sq.Question_SID
                left join dbo.Clients c on sa.Client_SID = c.Client_SID
                where sa.Client_SID = @clientId
                and sq.Active = 1
                order by c.LastName, c.FirstName, c.Client_SID, sq.DisplayOrder";

            var surveyResults = _dbConnection.Query<SurveyExportAnswer>(sql, new { clientId = clientId });
            return surveyResults;
        }

        public IEnumerable<SurveyExportAnswer> GetSurveyResultsByPeriod(string period)
        {
            var sql = @"
                select c.Client_SID, c.LastName, c.FirstName, sq.Question_Text, sa.Answer_Text
                from dbo.SurveyQuestions sq
                left join dbo.SurveyAnswers sa on sa.Question_SID = sq.Question_SID
                left join dbo.Clients c on sa.Client_SID = c.Client_SID
                where sa.Question_Period = @period
                and sq.Active = 1
                order by c.LastName, c.FirstName, c.Client_SID, sq.DisplayOrder";

            var surveyResults = _dbConnection.Query<SurveyExportAnswer>(sql, new { period = period });
            return surveyResults;
        }
    }
}