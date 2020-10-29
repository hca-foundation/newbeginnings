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
    public class QuestionRepository
    {
        readonly IDbConnection _dbConnection;
        public QuestionRepository(IDbConnection connection)
        {
            _dbConnection = connection;
        }
        public QuestionRepository()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public IEnumerable<SurveyQuestion> GetQuestions()
        {
            var sql = @"
                select sq.Question_SID, sq.Question_Type, sq.Question_Text
                from dbo.SurveyQuestions sq
                where sq.Active = 1
                order by sq.DisplayOrder";

            var questions = (IEnumerable<SurveyQuestion>)_dbConnection.Query(sql);
            return questions;
        }
    }
}