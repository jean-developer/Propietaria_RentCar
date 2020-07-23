using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Application.Repositories
{
    public interface IInspectionRepository
    {
        void Update(Entities.Inspection model);
        void Add(Entities.Inspection model);
        void Delete(int id);
    }
}
