using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Entities
{
    public class Models : BaseEntity
    {
        public int IdTrademark { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string TradeMark { get; set; }
    }
}
