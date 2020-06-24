using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string WorkShift { get; set; }
        public decimal Commission { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
