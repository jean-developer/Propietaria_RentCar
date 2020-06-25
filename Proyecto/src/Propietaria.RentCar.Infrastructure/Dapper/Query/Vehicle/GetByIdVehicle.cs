using Dapper;
using Propietaria.RentCar.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query
{
    public class GetByIdVehicle
    {

        private readonly string _connectionString;
        public GetByIdVehicle()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
        }

        public VehiculosVM Get(int id)
        {
            string sql = @"SELECT Vehiculos.Id, Vehiculos.Descripcion , Vehiculos.NoChasis, Vehiculos.NoMotor, Vehiculos.Estado, Vehiculos.NoPlaca, Vehiculos.Nombre,
							Marcas.Nombre AS Marca, TipoVehiculo.Nombre as TipoVehiculo, TipoCombustible.Nombre as TipoCombustible, Modelos.Nombre as Modelo
                             FROM Vehiculos 
							 INNER JOIN Marcas ON Marcas.Id=Vehiculos.IdMarca
							 INNER JOIN TipoVehiculo ON TipoVehiculo.Id=Vehiculos.IdTipoVehiculo
							 INNER JOIN TipoCombustible ON TipoCombustible.Id=Vehiculos.IdTipoCombustible
							 INNER JOIN Modelos ON Modelos.Id=Vehiculos.IdModelo
                            Where Vehiculos.Id = @Id;";

            using (var connection = new SqlConnection(_connectionString))
                return connection.QueryFirstOrDefault<VehiculosVM>(sql, GenerateParameters(id));
        }


        private DynamicParameters GenerateParameters(int corteId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", corteId, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }
    }
}