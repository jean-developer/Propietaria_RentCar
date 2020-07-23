using System;
using System.Collections.Generic;
using System.Text;

namespace Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers
{
    public class GetAllDocumentType
    {
        public string[] Get()
        {
            string[] response = new string[2];
            response[0] = "Cedula";
            response[1] = "Pasaporte";
            return response;
        }
    }
}
