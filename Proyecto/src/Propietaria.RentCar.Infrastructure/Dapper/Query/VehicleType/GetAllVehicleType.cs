using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using Propietaria.RentCar.Core.Entities;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query
{
    public class GetAllVehicleType
    {
        private readonly string _connectionString;
        public GetAllVehicleType()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public IEnumerable<VehicleType> Get()
        {
            string sql = @"SELECT Id, Nombre As Name, Descripcion as Description, Estado as Status
                             FROM TipoVehiculo Where Estado = 'Activo';";

            using (var connection = new SqlConnection(_connectionString))
                return connection.Query<VehicleType>(sql);
        }
    }
}
