using Dapper;
using Propietaria.RentCar.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Rent
{
    public class GetAllRents
    {
        private readonly string _connectionString;
        public GetAllRents()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public IEnumerable<RentaVM> Get()
        {
            string sql = @"SELECT r.Id
                                  ,(v.Nombre + ' | ' + v.NoChasis) as Vehiculo
                                  ,(c.Nombre  + ' | ' + c.Apellido) as Cliente
                                  ,Comentario
                                  ,FechaRenta
                                  ,FechaDevolucion
                                  ,MontoDiario
                                  ,Dias
	                              ,(e.Nombre + ' | ' + e.Apellido  + ' | ' + CAST(e.Id AS varchar)) as Empleado
                                  ,r.Estado							 
                              FROM Renta as r
							  Inner Join Empleados as e On e.Id = r.IdEmpleado
							  Inner Join Clientes as c On c.Id = r.IdCliente
							  Inner Join Vehiculos as v On v.Id = r.IdVehiculo
							  WHERE r.Estado = 'Activo';
                            ";

            using (var connection = new SqlConnection(_connectionString))
                return connection.Query<RentaVM>(sql);
        }
    }
}
