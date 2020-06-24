using Dapper;
using Propietaria.RentCar.Core.Application.Repositories;
using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Repositories
{
    public class ModelsRepository : RepositoryBase, IModelsRepository
    {
        private readonly IDbTransaction _transaction;
        public ModelsRepository(IDbTransaction transaction)
          : base(transaction)
        {
            _transaction = transaction;
        }

        public void Add(Models model)
        {
            string sql = @"INSERT INTO Modelos
	                                   (
	                                    Descripcion
                                       ,IdMarcas
                                       ,Nombre
	                                   ,Estado
                                       )
	                             VALUES
	                                   (@Descripcion
                                       ,@IdMarcas
                                       ,@Nombre
	                                   ,@Estado);";

            _transaction.Connection.Execute(sql, GenerateParametersForInsertion(model), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForInsertion(Models model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Descripcion", model.Description, DbType.String, ParameterDirection.Input, 100);
            parameters.Add("@IdMarcas", model.IdTrademark, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Nombre", model.Name, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@Estado", model.Status, DbType.String, ParameterDirection.Input, 10);
            return parameters;
        }
        public void Update(Models model)
        {
            string sql = @"UPDATE Modelos
                              SET  Descripcion = @Descripcion
                                  ,Nombre = @Nombre
                                  ,Estado = @Estado
                                  ,IdMarcas = @IdMarcas
                            WHERE Id=@Id;";

            _transaction.Connection.Execute(sql, GenerateParametersForUpdate(model), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForUpdate(Models model)
        {
            DynamicParameters parameters = GenerateParametersForInsertion(model);
            parameters.Add("Id", model.Id, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }
        public void Delete(int id)
        {
            string sql = @"UPDATE Modelos
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
