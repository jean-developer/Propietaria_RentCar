using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers
{
    public class GetAllClients
    {
        private readonly string _connectionString;
        public GetAllClients()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public string[] Get()
        {
            string sql = @"SELECT (Nombre  + ' | ' + Apellido) As Name
                             FROM Clientes Where Estado = 'Activo';";

            using (var connection = new SqlConnection(_connectionString))
                return connection.Query<string>(sql).AsList().ToArray();
        }
    }
}
