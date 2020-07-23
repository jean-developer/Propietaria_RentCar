using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Core.Entities
{
    public class Inspection : BaseEntity
    {
        public int IdVehicle { get; set; }
        public int IdClient { get; set; }
        public string FuelQuantity { get; set; }
        public bool IsScratched { get; set; }
        public bool SubstituteRubber { get; set; }
        public bool Cat { get; set; }
        public bool HydraulicJack { get; set; }
        public bool GlassBreak { get; set; }
        public DateTime InspectionDate { get; set; }
        public string StatusRubber { get; set; }
        public int IdEmployee { get; set; }
        public string Description { get; set; }

    }
}
