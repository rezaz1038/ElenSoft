using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.DataLayer.Models.Entities
{
   public class Device:TEntity
    {
        public string Model { get; set; }
        public string Serial { get; set; }
        public bool IsActive { get; set; }
        
        public string Description { get; set; }

        public string EquipmentId { get; set; }
        public Equipment  Equipment { get; set; }

        public string DeviceBrandId { get; set; }
        public DeviceBrand  DeviceBrand { get; set; }

        public string DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; }

        public int Quantity { get; set; }


    }
}
