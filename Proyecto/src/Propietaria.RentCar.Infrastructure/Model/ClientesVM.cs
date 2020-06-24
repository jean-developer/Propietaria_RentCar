using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Model
{
    public class ClientesVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDocumento { get; set; }
        public string NoDocumento { get; set; }
        public string NoTarjeta { get; set; }
        public decimal LimiteCredito { get; set; }
        public string TipoPersona { get; set; }
        public string Estado { get; set; }
    }
}
