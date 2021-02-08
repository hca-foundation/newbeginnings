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
    public class ClientRepository
    {
        readonly IDbConnection _dbConnection;
        public ClientRepository(IDbConnection connection)
        {
            _dbConnection = connection;
        }
        public ClientRepository()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public int GetOneByEmail(string email)
        {
            var sql = @"select count(1) from Users
                            where Email = @email and UserType_SID = 1;";

            return Convert.ToInt32(_dbConnection.ExecuteScalar(sql, new { Email = email }));
        }

        public IEnumerable<Client> GetAllActiveClients()
        {
            var sql = @"
                select Client_SID, FirstName, LastName, GroupNumber, Email
                from dbo.Clients";
            return _dbConnection.Query<Client>(sql);
        }
    }
}