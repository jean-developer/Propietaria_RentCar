using Dapper;
using Propietaria.RentCar.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Inspection
{
    public class GetAllInspection
    {
        private readonly string _connectionString;
        public GetAllInspection()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public IEnumerable<InspeccionVM> Get()
        {
            string sql = @"
					SELECT i.Id
                                  ,(v.Nombre + ' | ' + v.NoChasis) as Vehiculo
                                  ,(c.Nombre  + ' | ' + c.Apellido) as Cliente
                                  ,Ralladuras
                                  ,CantidadCombustible
                                  ,GomaRepuesta
                                  ,Gato
                                  ,RoturaCristal
                                  ,GomaEstado
                                  ,Comentario
                                  ,FechaInspeccion
	                              ,(e.Nombre + ' | ' + e.Apellido  + ' | ' + CAST(e.Id AS varchar)) as Empleado
                                  ,i.Estado							 
                              FROM Inspeccion as i
							  Inner Join Empleados as e On e.Id = i.IdEmpleado
							  Inner Join Clientes as c On c.Id = i.IdCliente
							  Inner Join Vehiculos as v On v.Id = i.IdVehiculo
							  WHERE i.Estado = 'Activo';
                           ";

            using (var connection = new SqlConnection(_connectionString))
                return connection.Query<InspeccionVM>(sql);
        }
    }
}
