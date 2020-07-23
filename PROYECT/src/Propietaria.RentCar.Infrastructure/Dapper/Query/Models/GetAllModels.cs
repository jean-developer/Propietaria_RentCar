using Dapper;
using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query
{
    public class GetAllModels
    {
        private readonly string _connectionString;
        public GetAllModels()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public IEnumerable<Core.Entities.Models> Get()
        {
            string sql = @"SELECT Modelos.Id, Modelos.Nombre As Name, Modelos.Descripcion as Description, Modelos.Estado as Status, Marcas.Nombre as TradeMark
                             FROM Modelos 
							 INNER JOIN Marcas ON Marcas.Id=Modelos.IdMarcas
							 Where Modelos.Estado = 'Activo';";

            using (var connection = new SqlConnection(_connectionString))
                return connection.Query<Core.Entities.Models>(sql);
        }
    }
}
