using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Model
{
    public class EmpleadoVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDocumento { get; set; }
        public string NoDocumento { get; set; }
        public string TandaLaboral { get; set; }
        public decimal Comision { get; set; }
        public string  FechaIngreso { get; set; }
        public string Estado { get; set; }
    }
}
