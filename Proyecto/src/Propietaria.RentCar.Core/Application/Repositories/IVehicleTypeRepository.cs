using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Application.Repositories
{
    public interface IVehicleTypeRepository
    {
        void Update(Entities.VehicleType model);
        void Add(Entities.VehicleType model);
        void Delete(int id);
    }
}
