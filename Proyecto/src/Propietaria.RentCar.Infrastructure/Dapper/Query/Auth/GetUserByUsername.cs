using Dapper;
using Propietaria.RentCar.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Auth
{
    public class GetUserByUsername
    {
        private readonly string _connectionString;
        public GetUserByUsername()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public UserVM Get(string userName)
        {
            string sql = @"
                            SELECT U.Id, U.UserName, U.Password, R.Nombre as Rol, U.Estado 
                            FROM AppUser as U
                            INNER JOIN AppRole as R ON R.Id = U.IdRole
                            WHERE  U.UserName = @UserName; 
                          ";


            using (var connection = new SqlConnection(_connectionString))
                return connection.QueryFirstOrDefault<UserVM>(sql, GenerateParameters(userName));
        }


        private DynamicParameters GenerateParameters(string userName)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userName", userName, DbType.String, ParameterDirection.Input, 50);
            return parameters;
        }
    }
}
