using Dapper;
using Propietaria.RentCar.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Clients
{
    public class GetAllClients
    {
        private readonly string _connectionString;
        public GetAllClients()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }
        public IEnumerable<ClientesVM> Get()
        {
            string sql = @"SELECT Id, Nombre, Apellido,TipoDocumento, NoDocumento, NoTarjeta, LimiteCredito, TipoPersona, Estado 
                            FROM Clientes
                            WHERE Estado = 'Activo';";

            using (var connection = new SqlConnection(_connectionString))
                return connection.Query<ClientesVM>(sql);
        }


    }
}
