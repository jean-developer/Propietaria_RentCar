using Dapper;
using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.TradeMark
{
    public class GetByIdTrademark
    {

        private readonly string _connectionString;
        public GetByIdTrademark()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public Trademark Get(int id)
        {
            string sql = @"SELECT Id, Descripcion as Description, Estado as Status, Nombre As Name
                             FROM Marcas Where Id = @Id;";

            using (var connection = new SqlConnection(_connectionString))
                return connection.QueryFirstOrDefault<Trademark>(sql, GenerateParameters(id));
        }


        private DynamicParameters GenerateParameters(int corteId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", corteId, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }
    }
}
