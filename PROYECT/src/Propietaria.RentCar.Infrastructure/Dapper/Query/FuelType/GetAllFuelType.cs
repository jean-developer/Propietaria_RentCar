using Dapper;
using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query
{
    public class GetAllFuelType
    {
        private readonly string _connectionString;
        public GetAllFuelType()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public IEnumerable<FuelType> Get()
        {
            string sql = @"SELECT Id, Nombre As Name, Descripcion as Description, Estado as Status
                             FROM TipoCombustible Where Estado = 'Activo';";

            using (var connection = new SqlConnection(_connectionString))
                return connection.Query<FuelType>(sql);
        }
    }

}
