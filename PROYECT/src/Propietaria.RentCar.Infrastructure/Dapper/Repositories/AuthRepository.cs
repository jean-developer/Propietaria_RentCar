using Dapper;
using Propietaria.RentCar.Core.Application.Repositories;
using Propietaria.RentCar.Infrastructure.Dapper.Query.Auth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Repositories
{
    public class AuthRepository : RepositoryBase, IAuthRepository
    {
        private readonly IDbTransaction _transaction;
        public AuthRepository(IDbTransaction transaction)
          : base(transaction)
        {
            _transaction = transaction;
        }
        public bool Login(string username, string password)
        {
            GetUserByUserNameAndPassword repository = new GetUserByUserNameAndPassword();
            var user = repository.Get(username, password);
            return (user != null);
        }

        public bool Register(Core.Entities.User model)
        {
            string sql = @"INSERT INTO AppUser
	                                   (
	                                    UserName
                                       ,Password
	                                   ,IdRole
                                       ,Estado
                                       )
	                             VALUES
	                                   (@UserName
                                       ,@Password
                                       ,@IdRole
	                                   ,@Estado); Select @@IDENTITY;";

            int records = _transaction.Connection.Execute(sql, GenerateParametersForInsertion(model), transaction: _transaction);
            return (records != 0);
        }


        private DynamicParameters GenerateParametersForInsertion(Core.Entities.User model)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@IdRole", model.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@UserName", model.UserName, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@Password", model.Password, DbType.String, ParameterDirection.Input, 50);
            parameters.Add("@Estado", model.Estado, DbType.String, ParameterDirection.Input, 20);
            return parameters;
        }
    }

}
