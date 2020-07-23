using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Model
{
    public class InspeccionVM
    {
        public int Id { get; set; }
        public string Vehiculo { get; set; }
        public string Cliente { get; set; }
        public bool Ralladuras { get; set; }
        public string CantidadCombustible { get; set; }
        public bool GomaRepuesta { get; set; }
        public bool Gato { get; set; }
        public bool RoturaCristal { get; set; }
        public string GomaEstado { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaInspeccion { get; set; }
        public string Empleado { get; set; }
        public string Estado { get; set; }
    }
}
