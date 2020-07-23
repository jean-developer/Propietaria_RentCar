using Dapper;
using Propietaria.RentCar.Core.Application.Repositories;
using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Repositories
{
    public class RentRepository : RepositoryBase, IRentRepository
    {
        private readonly IDbTransaction _transaction;
        public RentRepository(IDbTransaction transaction)
          : base(transaction)
        {
            _transaction = transaction;
        }

        public void Add(Rent model)
        {
            string sql = @"INSERT INTO Renta
	                                   (
                                        IdEmpleado
	                                   ,IdVehiculo
                                       ,IdCliente 
	                                   ,Comentario
                                       ,FechaRenta
                                       ,FechaDevolucion
                                       ,MontoDiario
                                       ,Dias
                                       ,Estado
                                       )
	                             VALUES
	                                   (
                                        @IdEmpleado
	                                   ,@IdVehiculo
                                       ,@IdCliente 
	                                   ,@Comentario
                                       ,@FechaRenta
                                       ,@FechaDevolucion
                                       ,@MontoDiario
                                       ,@Dias
                                       ,@Estado);";

            _transaction.Connection.Execute(sql, GenerateParametersForInsertion(model), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForInsertion(Rent model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IdEmpleado", model.IdEmployee, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@IdVehiculo", model.IdVehicle, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@IdCliente ", model.IdClient, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Comentario ", model.Comment, DbType.String, ParameterDirection.Input, 100);
            parameters.Add("@FechaRenta ", model.DateStart, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@FechaDevolucion ", model.DateEnd, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@MontoDiario ", model.AmountForDay, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Dias", model.Days, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Estado", model.Status, DbType.String, ParameterDirection.Input, 10);
            return parameters;
        }

        public void Delete(int id)
        {
            string sql = @"UPDATE Renta
                                     SET  Estado = 'Eliminado'
                            WHERE Id=@Id;";

            _transaction.Connection.Execute(sql, GenerateParametersForDelete(id), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForDelete(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }
        public void Update(Rent model)
        {
            string sql = @"UPDATE Renta
                              SET  IdEmpleado=  @IdEmpleado
	                                   ,IdVehiculo = @IdVehiculo
                                       ,IdCliente= @IdCliente 
	                                   ,Comentario= @Comentario
                                       ,FechaRenta = @FechaRenta
                                       ,FechaDevolucion = @FechaDevolucion
                                       ,MontoDiario = @MontoDiario
                                       ,Dias = @Dias
                                       ,Estado = @Estado
                            WHERE Id=@Id;";

            _transaction.Connection.Execute(sql, GenerateParametersForUpdate(model), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForUpdate(Rent model)
        {
            DynamicParameters parameters = GenerateParametersForInsertion(model);
            parameters.Add("@Id", model.Id, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }
    }
}
