using Propietaria.RentCar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propietaria.RentCar.Infrastructure.Model
{
    public class ModelsVM
    {
        public int Id { get; set; }
        public string Estatus { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }

        public static ModelsVM Map(Models entity)
        {
            ModelsVM response = new ModelsVM();
            response.Id = entity.Id;
            response.Estatus = entity.Status;
            response.Descripcion = entity.Description;
            response.Marca = entity.TradeMark;
            response.Nombre = entity.Name;
            return response;
        }

        public static IList<ModelsVM> MapList(IList<Models> list)
        {
            IList<ModelsVM> response = new List<ModelsVM>();
            foreach (var item in list)
            {
                response.Add(Map(item));
            }
            return response;
        }
    }
}
