using Dapper;
using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers
{
    public class GetIdByName
    {
        private readonly string _connectionString;
        public GetIdByName()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public int Get(string nombre, string tableName)
        {
            string sql = @"SELECT Id 
                             FROM Table Where Nombre = @Nombre;";

            sql = sql.Replace("Table", tableName);

            using (var connection = new SqlConnection(_connectionString))
                return connection.QueryFirstOrDefault<int>(sql, GenerateParameters(nombre));
        }
        private DynamicParameters GenerateParameters(string nombre)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Nombre", nombre, DbType.String, ParameterDirection.Input, 50);
            return parameters;
        }

        public int GetClientId(string filter)
        {
            var filters = filter.Split('|');

            string sql = @"SELECT Id 
                             FROM Clientes Where Nombre = @Nombre And Apellido = @Apellido;";

            using (var connection = new SqlConnection(_connectionString))
                return connection.QueryFirstOrDefault<int>(sql, GenerateClientsParameters(filters[0].Trim(), filters[1].Trim()));
        }
        private DynamicParameters GenerateClientsParameters(string nombre, string apellido)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Nombre", nombre, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@Apellido", apellido, DbType.String, ParameterDirection.Input, 50);
            return parameters;
        }
        public int GetEmployeeId(string filter)
        {
            var filters = filter.Split('|');
            return Convert.ToInt32(filters[2]);
        }

        public int GetVehicleId(string filter)
        {
            var filters = filter.Split('|');

            string sql = @"SELECT Id 
                             FROM Vehiculos Where Nombre = @Nombre And NoChasis = @NoChasis;";

            using (var connection = new SqlConnection(_connectionString))
                return connection.QueryFirstOrDefault<int>(sql, GenerateVehicleParameters(filters[0], filters[1]));
        }

        private DynamicParameters GenerateVehicleParameters(string nombre, string noChasis)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Nombre", nombre.Trim(), DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@NoChasis", noChasis.Trim(), DbType.String, ParameterDirection.Input, 20);
            return parameters;
        }


    }
}
