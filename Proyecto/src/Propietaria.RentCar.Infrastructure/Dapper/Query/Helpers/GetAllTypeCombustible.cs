using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers
{

        public class GetAllTypeCombustible
        {
            private readonly string _connectionString;
            public GetAllTypeCombustible()
            {
                _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
            }

            public string[] Get()
            {
                string sql = @"SELECT Nombre As Name
                             FROM TipoCombustible Where Estado = 'Activo';";

                using (var connection = new SqlConnection(_connectionString))
                    return connection.Query<string>(sql).AsList().ToArray();
            }
        }
    
}
