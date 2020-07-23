using Dapper;
using Propietaria.RentCar.Core.Application.Repositories;
using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Repositories
{
    public class CurstomerRepository : RepositoryBase, ICurstomerRepository
    {
        private readonly IDbTransaction _transaction;
        public CurstomerRepository(IDbTransaction transaction)
          : base(transaction)
        {
            _transaction = transaction;
        }

        public void Add(Customers customer)
        {
            string sql = @"INSERT INTO Clientes
	                                   (
	                                    Nombre
                                       ,Apellido
	                                   ,TipoDocumento
	                                   ,NoDocumento
	                                   ,NoTarjeta
	                                   ,LimiteCredito
	                                   ,TipoPersona
	                                   ,Estado
                                       )
	                             VALUES(
	                                    @Nombre
                                       ,@Apellido
	                                   ,@TipoDocumento
	                                   ,@NoDocumento
	                                   ,@NoTarjeta
	                                   ,@LimiteCredito
	                                   ,@TipoPersona
	                                   ,@Estado);";

            _transaction.Connection.Execute(sql, GenerateParametersForInsertion(customer), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForInsertion(Customers model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Nombre", model.Name, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@Apellido", model.LastName, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@TipoDocumento", model.DocumentType, DbType.String, ParameterDirection.Input, 10);
            parameters.Add("@NoDocumento", model.DocumentNumber, DbType.String, ParameterDirection.Input, 30);
            parameters.Add("@NoTarjeta", model.AccountNumber, DbType.String, ParameterDirection.Input, 20);
            parameters.Add("@LimiteCredito", model.CreditLimit, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("@TipoPersona", model.PersonType, DbType.String, ParameterDirection.Input, 20);
            parameters.Add("@Estado", model.Status, DbType.String, ParameterDirection.Input, 10);
            return parameters;
        }

        public void Delete(int id)
        {
            string sql = @"UPDATE Clientes
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

        public void Update(Customers customer)
        {
            string sql = @"UPDATE Clientes
                              SET       Nombre=  @Nombre
                                       ,Apellido = @Apellido
	                                   ,TipoDocumento = @TipoDocumento
	                                   ,NoDocumento= @NoDocumento
	                                   ,NoTarjeta = @NoTarjeta
	                                   ,LimiteCredito = @LimiteCredito
	                                   ,TipoPersona = @TipoPersona
	                                   ,Estado = @Estado
                            WHERE Id=@Id;";

            _transaction.Connection.Execute(sql, GenerateParametersForUpdate(customer), transaction: _transaction);
        }

        private DynamicParameters GenerateParametersForUpdate(Customers customers)
        {
            DynamicParameters parameters = GenerateParametersForInsertion(customers);
            parameters.Add("@Id", customers.Id, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }
    }
}
