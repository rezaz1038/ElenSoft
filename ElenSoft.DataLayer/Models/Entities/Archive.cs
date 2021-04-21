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
        public string Description { get; set; }
        public string Filesize { get; set; }
        public string Path { get; set; }
        public Tag Tag { get; set; }
        public string TagId { get; set; }


        public Category Category { get; set; }
        public string CategoryId { get; set; }

    }
}
