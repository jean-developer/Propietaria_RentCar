using Dapper;
using Propietaria.RentCar.Core.Application.Repositories;
using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Repositories
{
    public class VehicleRepository : RepositoryBase, IVehicleRepository
    {
        private readonly IDbTransaction _transaction;
        public VehicleRepository(IDbTransaction transaction)
          : base(transaction)
        {
            _transaction = transaction;
        }

        public void Add(Vehicle vehicle)
        {
            string sql = @"INSERT INTO Vehiculos
	                                   (
	                                    Descripcion
                                       ,NoChasis
	                                   ,NoMotor
                                       ,NoPlaca
	                                   ,Estado
	                                   ,IdTipoVehiculo
	                                   ,IdMarca
	                                   ,IdModelo
	                                   ,IdTipoCombustible
	                                   ,Nombre
                                       )
	                             VALUES
	                                   (
                                        @Descripcion
                                       ,@NoChasis
	                                   ,@NoMotor
                                       ,@NoPlaca
	                                   ,@Estado
	                                   ,@IdTipoVehiculo
	                                   ,@IdMarca
	                                   ,@IdModelo
	                                   ,@IdTipoCombustible
	                                   ,@Nombre);";

            _transaction.Connection.Execute(sql, GenerateParametersForInsertion(vehicle), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForInsertion(Vehicle model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Descripcion", model.Description, DbType.String, ParameterDirection.Input, 100);
            parameters.Add("@NoChasis", model.NoChasis, DbType.String, ParameterDirection.Input, 20);
            parameters.Add("@NoMotor", model.NoMotor, DbType.String, ParameterDirection.Input, 20);
            parameters.Add("@NoPlaca", model.NoPlaca, DbType.String, ParameterDirection.Input, 20);
            parameters.Add("@Estado", model.Status, DbType.String, ParameterDirection.Input, 10);
            parameters.Add("@IdTipoVehiculo", model.VehicleTypeId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@IdMarca", model.TrademarkId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@IdModelo", model.ModelsId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@IdTipoCombustible", model.FuelType, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Nombre", model.Name, DbType.String, ParameterDirection.Input, 50);
            return parameters;
        }

        public void Update(Vehicle vehicle)
        {
            string sql = @"UPDATE Vehiculos
                              SET       Descripcion = @Descripcion
                                       ,Nombre = @Nombre
                                       ,NoChasis = @NoChasis
	                                   ,NoMotor = @NoMotor
                                       ,NoPlaca = @NoPlaca
	                                   ,Estado = @Estado
	                                   ,IdTipoVehiculo = @IdTipoVehiculo
	                                   ,IdMarca = @IdMarca
	                                   ,IdModelo = @IdModelo
	                                   ,IdTipoCombustible = @IdTipoCombustible
                            WHERE Id=@Id;";

            _transaction.Connection.Execute(sql, GenerateParametersForUpdate(vehicle), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForUpdate(Vehicle vehicle)
        {
            DynamicParameters parameters = GenerateParametersForInsertion(vehicle);
            parameters.Add("Id", vehicle.Id, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }

        public void Delete(int id)
        {
            string sql = @"UPDATE Vehiculos
                              SET Estado = 'Eliminado'
                            WHERE Id=@Id;";


            _transaction.Connection.Execute(sql, GenerateParametersForDelete(id), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForDelete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }
    }
}
