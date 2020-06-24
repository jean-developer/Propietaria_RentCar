using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Application.Repositories
{
    public interface IVehicleRepository
    {
        void Add(Vehicle vehicle);
        void Update(Vehicle vehicle);
        void Delete(int id);
    }
}
