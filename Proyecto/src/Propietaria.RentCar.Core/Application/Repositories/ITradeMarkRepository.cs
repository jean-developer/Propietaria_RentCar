using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Application.Repositories
{
    public interface ITradeMarkRepository
    {
        void Update(Entities.Trademark model);
        void Add(Entities.Trademark model);
        void Delete(int id);
    }
}
