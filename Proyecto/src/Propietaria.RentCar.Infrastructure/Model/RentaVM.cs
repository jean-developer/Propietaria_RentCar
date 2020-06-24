using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Model
{
    public class RentaVM
    {
        public int Id { get; set; }
        public string Empleado { get; set; }
        public string Vehiculo { get; set; }
        public string Cliente { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaRenta { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public decimal MontoDiario { get; set; }
        public int Dias { get; set; }
        public string Estado { get; set; }
    }
}
