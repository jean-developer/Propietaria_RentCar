using Dapper;
using Propietaria.RentCar.Core.Entities;
using Propietaria.RentCar.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query
{
    public class GetAllEmployee
    {
        private readonly string _connectionString;
        public GetAllEmployee()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public IEnumerable<EmpleadoVM> Get()
        {
            string sql = @"
                            SELECT Id, Nombre, Apellido, TipoDocumento, NoDocumento, TandaLaboral, Comision, FechaIngreso, Estado 
                            FROM Empleados
                            WHERE Estado = 'Activo'; 
                          ";

            using (var connection = new SqlConnection(_connectionString))
                return connection.Query<EmpleadoVM>(sql);
        }
    }
}
