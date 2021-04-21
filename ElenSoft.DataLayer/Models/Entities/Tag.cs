using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.DataLayer.Models.Entities
{
    public class Tag: TEntity
    {
        public string Title { get; set; }

        public ICollection<Archive>   Archives { get; set; }

    }
}
