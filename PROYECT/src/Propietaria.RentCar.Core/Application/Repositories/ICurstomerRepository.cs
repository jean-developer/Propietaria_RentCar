using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Application.Repositories
{
    public interface ICurstomerRepository
    {
        void Add(Customers customer);
        void Update(Customers customer);
        void Delete(int id);
    }
}
