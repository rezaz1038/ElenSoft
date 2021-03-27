using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.DataLayer.Models.Entities
{
   public class FolderInfo:TEntity
    {
        public string  Title { get; set; }
        public string Path { get; set; }
        public string Quota { get; set; }
        public string OSName { get; set; }

        public ICollection<FolderSecurity>  FolderSecurities { get; set; }
    }
}
