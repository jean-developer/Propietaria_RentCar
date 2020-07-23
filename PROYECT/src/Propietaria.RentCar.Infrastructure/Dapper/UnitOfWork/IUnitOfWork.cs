using Propietaria.RentCar.Core.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IVehicleTypeRepository VehicleTypeRepository { get; }
        ITradeMarkRepository TradeMarkRepository { get; }
        IFuelTypeRepository FuelTypeRepository { get; }
        IModelsRepository ModelsRepository { get; }
        IVehicleRepository VehicleRepository { get; }

        ICurstomerRepository CurstomerRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IInspectionRepository InspectionRepository { get; }
        IRentRepository RentRepository { get; }
        IAuthRepository AuthRepository { get; }
        void Commit();
    }
}
