using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Application.Repositories
{
    public interface IRentRepository
    {
        void Add(Rent customer);
        void Update(Rent customer);
        void Delete(int id);
    }
}
