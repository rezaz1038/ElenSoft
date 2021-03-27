﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.DataLayer.Models.Entities
{
    public class EquipmentPlace : TEntity

    {
        public string Title { get; set; }
        public ICollection<Equipment>  Equipment { get; set; }
    }
}
