using Dapper;
using Propietaria.RentCar.Core.Application.Repositories;
using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Repositories
{
    public class VehicleTypeRepository : RepositoryBase, IVehicleTypeRepository
    {
        private readonly IDbTransaction _transaction;
        public VehicleTypeRepository(IDbTransaction transaction)
          : base(transaction)
        {
            _transaction = transaction;
        }

      

        public void Add(VehicleType model)
        {
            string sql = @"INSERT INTO TipoVehiculo
	                                   (
	                                    Descripcion
                                       ,Nombre
	                                   ,Estado
                                       )
	                             VALUES
	                                   (@Descripcion
                                       ,@Nombre
	                                   ,@Estado);";

            _transaction.Connection.Execute(sql, GenerateParametersForInsertion(model), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForInsertion(VehicleType model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Descripcion", model.Description, DbType.String, ParameterDirection.Input, 100);
            parameters.Add("@Nombre", model.Name, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@Estado", model.Status, DbType.String, ParameterDirection.Input, 10);
            return parameters;
        }


        public void Update(VehicleType model)
        {
            string sql = @"UPDATE TipoVehiculo
                              SET  Descripcion = @Descripcion
                                  ,Nombre = @Nombre
                                  ,Estado = @Estado
                            WHERE Id=@Id;";

            _transaction.Connection.Execute(sql, GenerateParametersForUpdate(model), transaction: _transaction);
        }

         private DynamicParameters GenerateParametersForUpdate(VehicleType model)
         {
            DynamicParameters parameters = GenerateParametersForInsertion(model);
            parameters.Add("Id", model.Id, DbType.Int32, ParameterDirection.Input);
            return parameters;
         }

        public void Delete(int id)
        {
            string sql = @"UPDATE TipoVehiculo
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
