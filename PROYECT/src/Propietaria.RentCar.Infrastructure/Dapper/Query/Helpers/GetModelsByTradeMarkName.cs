using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers
{
    public class GetModelsByTradeMarkName
    {
        private readonly string _connectionString;
        public GetModelsByTradeMarkName()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public string[] Get(string nombreMarca)
        {
            string sql = @"select Modelos.Nombre from Modelos Inner Join Marcas on Marcas.Id = Modelos.IdMarcas
							 where Marcas.Nombre = @Nombre and Modelos.Estado = 'Activo';";

            using (var connection = new SqlConnection(_connectionString))
                return connection.Query<string>(sql, GenerateParameters(nombreMarca)).AsList().ToArray();
        }

        private DynamicParameters GenerateParameters(string nombreMarca)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Nombre", nombreMarca, DbType.String, ParameterDirection.Input,50);
            return parameters;
        }
    }
}
