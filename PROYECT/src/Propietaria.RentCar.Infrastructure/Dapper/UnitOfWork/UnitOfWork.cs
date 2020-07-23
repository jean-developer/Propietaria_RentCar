using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
namespace Propietaria.RentCar.Infrastructure.Dapper.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private Core.Application.Repositories.IVehicleTypeRepository _vehicleTypeRepository;
        private Core.Application.Repositories.ITradeMarkRepository _tradeMarkRepository;
        private Core.Application.Repositories.IFuelTypeRepository _fuelTypeRepository;
        private Core.Application.Repositories.IModelsRepository _modelsRepository;
        private Core.Application.Repositories.IVehicleRepository _vehicleRepository;
        private Core.Application.Repositories.ICurstomerRepository _curstomerRepository;
        private Core.Application.Repositories.IEmployeeRepository _employeeRepository;
        private Core.Application.Repositories.IInspectionRepository _inspectionRepository;
        private Core.Application.Repositories.IRentRepository _rentRepository;
        private Core.Application.Repositories.IAuthRepository _authRepository;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _connection = new System.Data.SqlClient.SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public Core.Application.Repositories.IVehicleTypeRepository VehicleTypeRepository
        {
            get { return _vehicleTypeRepository ?? (_vehicleTypeRepository = new Repositories.VehicleTypeRepository(_transaction)); }
        }
        public Core.Application.Repositories.ITradeMarkRepository TradeMarkRepository
        {
            get { return _tradeMarkRepository ?? ( _tradeMarkRepository = new Repositories.TradeMarkRepository(_transaction)); }
        }
        public Core.Application.Repositories.IFuelTypeRepository FuelTypeRepository
        {
            get { return _fuelTypeRepository ?? (_fuelTypeRepository = new Repositories.FuelTypeRepository (_transaction)); }
        }
        public Core.Application.Repositories.IModelsRepository ModelsRepository
        {
            get { return _modelsRepository ?? (_modelsRepository = new Repositories.ModelsRepository(_transaction)); }
        }
        public Core.Application.Repositories.IVehicleRepository VehicleRepository
        {
            get { return _vehicleRepository ?? (_vehicleRepository = new Repositories.VehicleRepository(_transaction)); }
        }
        public Core.Application.Repositories.ICurstomerRepository CurstomerRepository
        {
            get { return _curstomerRepository ?? (_curstomerRepository = new Repositories.CurstomerRepository(_transaction)); }
        }
        public Core.Application.Repositories.IEmployeeRepository EmployeeRepository
        {
            get { return _employeeRepository ?? (_employeeRepository = new Repositories.EmployeeRepository(_transaction)); }
        }
        public Core.Application.Repositories.IInspectionRepository InspectionRepository
        {
            get { return _inspectionRepository ?? (_inspectionRepository = new Repositories.InspectionRepository(_transaction)); }
        }
        public Core.Application.Repositories.IRentRepository RentRepository
        {
            get { return _rentRepository ?? (_rentRepository = new Repositories.RentRepository(_transaction)); }
        }

        public Core.Application.Repositories.IAuthRepository AuthRepository
        {
            get { return _authRepository ?? (_authRepository = new Repositories.AuthRepository(_transaction)); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _vehicleTypeRepository = null;
            _tradeMarkRepository = null;
            _fuelTypeRepository = null;
            _modelsRepository = null;
            _vehicleRepository = null;
            _curstomerRepository = null;
            _employeeRepository = null;
            _inspectionRepository = null;
            _rentRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
