using Dapper;
using Propietaria.RentCar.Core.Application.Repositories;
using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Repositories
{
    public class EmployeeRepository : RepositoryBase, IEmployeeRepository
    {
        private readonly IDbTransaction _transaction;
        public EmployeeRepository(IDbTransaction transaction)
          : base(transaction)
        {
            _transaction = transaction;
        }

        public void Add(Employee model)
        {
            string sql = @"INSERT INTO Empleados
	                                   (
	                                    Nombre
                                       ,Apellido
	                                   ,TipoDocumento
	                                   ,NoDocumento
	                                   ,TandaLaboral
	                                   ,Comision
	                                   ,FechaIngreso
	                                   ,Estado
                                       )
	                             VALUES(
	                                    @Nombre
                                       ,@Apellido
	                                   ,@TipoDocumento
	                                   ,@NoDocumento
	                                   ,@TandaLaboral
	                                   ,@Comision
	                                   ,@FechaIngreso
	                                   ,@Estado);";

            _transaction.Connection.Execute(sql, GenerateParametersForInsertion(model), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForInsertion(Employee model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Nombre", model.Name, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@Apellido", model.LastName, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@TipoDocumento", model.DocumentType, DbType.String, ParameterDirection.Input, 10);
            parameters.Add("@NoDocumento", model.DocumentNumber, DbType.String, ParameterDirection.Input, 30);
            parameters.Add("@TandaLaboral", model.WorkShift, DbType.String, ParameterDirection.Input, 30);
            parameters.Add("@Comision", model.Commission, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@FechaIngreso", model.CreatedOn, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@Estado", model.Status, DbType.String, ParameterDirection.Input, 10);
            return parameters;
        }

        public void Delete(int id)
        {
            string sql = @"UPDATE Empleados
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

        public void Update(Employee model)
        {
            string sql = @"UPDATE Empleados
                              SET       Nombre = @Nombre
                                       ,Apellido = @Apellido
	                                   ,TipoDocumento = @TipoDocumento
	                                   ,NoDocumento = @NoDocumento
	                                   ,TandaLaboral = @TandaLaboral
	                                   ,Comision = @Comision
	                                   ,FechaIngreso = @FechaIngreso
	                                   ,Estado = @Estado
                            WHERE Id=@Id;";

            _transaction.Connection.Execute(sql, GenerateParametersForUpdate(model), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForUpdate(Employee model)
        {
            DynamicParameters parameters = GenerateParametersForInsertion(model);
            parameters.Add("Id", model.Id, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }
    }
}
