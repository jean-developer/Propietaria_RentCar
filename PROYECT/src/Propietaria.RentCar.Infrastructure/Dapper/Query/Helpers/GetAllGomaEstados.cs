using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers
{
    public class GetAllGomaEstados
    {
        public string[] Get()
        {
            string[] response = new string[3];
            response[0] = "Nueva";
            response[1] = "Semi dañada";
            response[2] = "Dañada";
            return response;
        }
    }
}
