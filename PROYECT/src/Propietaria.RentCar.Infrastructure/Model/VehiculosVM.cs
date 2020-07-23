using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Model
{
    public class VehiculosVM
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string  NoChasis { get; set; }
        public string NoMotor { get; set; }
        public string  NoPlaca { get; set; }
        public string Estado { get; set; }
        public string TipoVehiculo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string TipoCombustible { get; set; }
        public string Nombre { get; set; }
    }
}
