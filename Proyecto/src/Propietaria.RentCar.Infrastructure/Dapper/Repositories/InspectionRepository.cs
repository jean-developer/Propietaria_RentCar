using Dapper;
using Propietaria.RentCar.Core.Application.Repositories;
using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Repositories
{
    public class InspectionRepository : RepositoryBase,  IInspectionRepository
    {
        private readonly IDbTransaction _transaction;
        public InspectionRepository(IDbTransaction transaction)
          : base(transaction)
        {
            _transaction = transaction;
        }

        public void Add(Inspection model)
        {
            string sql = @"INSERT INTO Inspeccion
	                                   (
	                                    IdVehiculo
                                       ,IdCliente 
	                                   ,Ralladuras
                                       ,CantidadCombustible
                                       ,GomaRepuesta
                                       ,Gato
                                       ,RoturaCristal
                                       ,GomaEstado
                                       ,Comentario
                                       ,FechaInspeccion
                                       ,IdEmpleado
                                       ,Estado
                                       )
	                             VALUES
	                                   (@IdVehiculo
                                       ,@IdCliente 
	                                   ,@Ralladuras
                                       ,@CantidadCombustible
                                       ,@GomaRepuesta
                                       ,@Gato
                                       ,@RoturaCristal
                                       ,@GomaEstado
                                       ,@Comentario
                                       ,@FechaInspeccion
                                       ,@IdEmpleado
                                       ,@Estado);";

            _transaction.Connection.Execute(sql, GenerateParametersForInsertion(model), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForInsertion(Inspection model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IdVehiculo", model.IdVehicle, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@IdCliente ", model.IdClient, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Ralladuras", model.IsScratched, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@CantidadCombustible", model.FuelQuantity, DbType.String, ParameterDirection.Input, 30);
            parameters.Add("@GomaRepuesta", model.SubstituteRubber, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@Gato", model.Cat, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@RoturaCristal", model.GlassBreak, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@GomaEstado", model.StatusRubber, DbType.String, ParameterDirection.Input, 30);
            parameters.Add("@Comentario", model.StatusRubber, DbType.String, ParameterDirection.Input, 100);
            parameters.Add("@FechaInspeccion", model.InspectionDate, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@IdEmpleado", model.IdEmployee, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Estado", model.Status, DbType.String, ParameterDirection.Input, 10);
            return parameters;
        }

        public void Delete(int id)
        {
            string sql = @"UPDATE Inspeccion
                              SET Estado = 'Eliminado' 
                            WHERE Id= @Id;";

            _transaction.Connection.Execute(sql, GenerateParametersForDelete(id), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForDelete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }

        public void Update(Inspection model)
        {
            string sql = @"UPDATE Inspeccion
                                    SET IdVehiculo = @IdVehiculo
                                       ,IdCliente = @IdCliente
	                                   ,Ralladuras = @Ralladuras
                                       ,CantidadCombustible = @CantidadCombustible
                                       ,GomaRepuesta  = @GomaRepuesta
                                       ,Gato = @Gato
                                       ,RoturaCristal = @RoturaCristal
                                       ,GomaEstado = @GomaEstado
                                       ,Comentario = @Comentario
                                       ,FechaInspeccion = @FechaInspeccion
                                       ,IdEmpleado = @IdEmpleado
                                       ,Estado = @Estado 
                            WHERE Id = @Id;";

            _transaction.Connection.Execute(sql, GenerateParametersForUpdate(model), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForUpdate(Inspection model)
        {
            DynamicParameters parameters = GenerateParametersForInsertion(model);
            parameters.Add("@Id", model.Id, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }
    }
}
