using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Application.Repositories
{
    public interface IModelsRepository 
    {
        void Update(Entities.Models model);
        void Add(Entities.Models model);
        void Delete(int id);
    }
}
