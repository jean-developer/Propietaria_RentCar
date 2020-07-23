using Dapper;
using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query
{
    public class GetByIdFuelType
    {

        private readonly string _connectionString;
        public GetByIdFuelType()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public FuelType Get(int id)
        {
            string sql = @"SELECT Id, Descripcion as Description, Estado as Status, Nombre As Name
                             FROM TipoCombustible Where Id = @Id;";

            using (var connection = new SqlConnection(_connectionString))
                return connection.QueryFirstOrDefault<FuelType>(sql, GenerateParameters(id));
        }


        private DynamicParameters GenerateParameters(int corteId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("CorteSolicitudID", corteId, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }
    }
}
