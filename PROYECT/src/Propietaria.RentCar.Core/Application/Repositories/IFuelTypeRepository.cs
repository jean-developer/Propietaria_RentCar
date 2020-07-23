using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Application.Repositories
{
    public interface IFuelTypeRepository
    {
        void Update(Entities.FuelType model);
        void Add(Entities.FuelType model);
        void Delete(int id);
    }
}
