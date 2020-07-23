using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Entities
{
    public class Customers : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string AccountNumber { get; set; }
        public decimal CreditLimit { get; set; }
        public string PersonType { get; set; }
    }
}
