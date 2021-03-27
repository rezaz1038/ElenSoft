using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.DataLayer.Models.Entities
{
   public class Equipment :TEntity
    {
        public string Name { get; set; }
        public string Amval { get; set; }
        public string Code { get; set; }
        public bool IsInUse { get; set; }
        public string Description { get; set; }


        public string PlaceId { get; set; }
        public EquipmentPlace Place { get; set; }


        public ICollection<Device>  Devices { get; set; }
    }
}
