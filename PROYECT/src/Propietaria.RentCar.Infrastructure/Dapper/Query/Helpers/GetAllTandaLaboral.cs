using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers
{
    public class GetAllTandaLaboral
    {
        public string[] Get()
        {
            string[] response = new string[3];
            response[0] = "Vespertino";
            response[1] = "Matutino";
            response[2] = "Diurno";
            return response;
        }
    }
}
