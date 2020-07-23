using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers
{
    public class GetAllVehicle
    {
        private readonly string _connectionString;
        public GetAllVehicle()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public string[] Get()
        {
            string sql = @"SELECT (Nombre + ' | ' + NoChasis) As Name
                             FROM Vehiculos Where Estado = 'Activo';";

            using (var connection = new SqlConnection(_connectionString))
                return connection.Query<string>(sql).AsList().ToArray();
        }
    }
}
