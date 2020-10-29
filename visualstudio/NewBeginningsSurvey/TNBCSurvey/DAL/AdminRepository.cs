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
    public class AdminRepository
    {
        readonly IDbConnection _dbConnection;
        public AdminRepository(IDbConnection connection)
        {
            _dbConnection = connection;
        }
        public AdminRepository()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public int GetOneByEmail(string email)
        {
            var sql = @"select count(1) from Admins
                            where Email = @email;";

            return Convert.ToInt32(_dbConnection.ExecuteScalar(sql, new { Email = email }));
        }

    }
}