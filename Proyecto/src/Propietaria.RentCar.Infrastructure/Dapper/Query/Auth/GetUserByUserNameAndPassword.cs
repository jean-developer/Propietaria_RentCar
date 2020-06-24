﻿using Dapper;
using Propietaria.RentCar.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Auth
{
    public class GetUserByUserNameAndPassword
    {
        private readonly string _connectionString;
        public GetUserByUserNameAndPassword()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public UserVM Get(string userName, string password)
        {
            string sql = @"
                            SELECT U.Id, U.UserName, U.Password, R.Nombre as Rol, U.Estado 
                            FROM AppUser as U
                            INNER JOIN AppRole as R ON R.Id = U.IdRole
                            WHERE U.Estado = 'Activo' and U.UserName = @UserName and U.Password = @Password; 
                          ";


            using (var connection = new SqlConnection(_connectionString))
                return connection.QueryFirstOrDefault<UserVM>(sql, GenerateParameters(userName, password));
        }


        private DynamicParameters GenerateParameters(string userName, string password)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userName", userName, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@Password", password, DbType.String, ParameterDirection.Input, 50);
            return parameters;
        }
    }
}
