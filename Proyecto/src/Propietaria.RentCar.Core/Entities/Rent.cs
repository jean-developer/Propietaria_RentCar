using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Entities
{
    public class Rent : BaseEntity
    {
        public int IdEmployee { get; set; }
        public int IdVehicle { get; set; }
        public int IdClient { get; set; }
        public decimal AmountForDay { get; set; }
        public int Days { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
    }
}
