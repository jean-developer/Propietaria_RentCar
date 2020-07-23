using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Entities
{
    public class Vehicle : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string NoChasis { get; set; }
        public string NoMotor { get; set; }
        public string NoPlaca { get; set; }
        public int VehicleTypeId { get; set; }
        public int TrademarkId { get; set; }
        public int ModelsId { get; set; }
        public int FuelType { get; set; }
    }
}
