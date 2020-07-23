using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Application.Repositories
{
    public interface IEmployeeRepository
    {
        void Add(Employee model);
        void Update(Employee model);
        void Delete(int id);

    }
}
