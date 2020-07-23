using Dapper;
using Propietaria.RentCar.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Vehicle
{
    public class GetAllVehicle
    {
        

        private readonly string _connectionString;
        public GetAllVehicle()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }


        public IEnumerable<VehiculosVM> Get()
        {
            string sql = @"SELECT Vehiculos.Id, Vehiculos.Descripcion , Vehiculos.NoChasis, Vehiculos.NoMotor, Vehiculos.Estado, Vehiculos.NoPlaca, Vehiculos.Nombre,
							Marcas.Nombre AS Marca, TipoVehiculo.Nombre as TipoVehiculo, TipoCombustible.Nombre as TipoCombustible, Modelos.Nombre as Modelo
                             FROM Vehiculos 
							 INNER JOIN Marcas ON Marcas.Id=Vehiculos.IdMarca
							 INNER JOIN TipoVehiculo ON TipoVehiculo.Id=Vehiculos.IdTipoVehiculo
							 INNER JOIN TipoCombustible ON TipoCombustible.Id=Vehiculos.IdTipoCombustible
							 INNER JOIN Modelos ON Modelos.Id=Vehiculos.IdModelo
							 Where Vehiculos.Estado = 'Activo' OR Vehiculos.Estado = 'Rentado'";

            using (var connection = new SqlConnection(_connectionString))
                return connection.Query<VehiculosVM>(sql);
        }

    }
}
