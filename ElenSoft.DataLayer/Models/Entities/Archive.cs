using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.DataLayer.Models.Entities
{
   public class Archive:TEntity
    {
        public string Name { get; set; }
        public string Descriptiion { get; set; }
        public string Filesize { get; set; }
        public string Path { get; set; }
        public Tage Tage { get; set; }
        public string TageId { get; set; }


        public Category Category { get; set; }
        public string CategoryId { get; set; }

    }
}
