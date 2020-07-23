using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers
{
    public class GetAllTypePersons
    {
        public string[] Get()
        {
            string[] response = new string[2];
            response[0] = "Persona Fisica";
            response[1] = "Persona Juridica";
            return response;
        }
    }
}
