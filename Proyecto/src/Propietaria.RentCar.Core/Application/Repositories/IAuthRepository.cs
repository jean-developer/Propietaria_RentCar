using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Application.Repositories
{
    public interface IAuthRepository
    {
        bool Login(string user, string password);
        bool Register(Core.Entities.User user);
    }
}
