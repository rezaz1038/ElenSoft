using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.DataLayer.Models.Entities
{
    public class FolderSecurity : TEntity
    {
        public string Name { get; set; }
        public ShareType Share { get; set; }
        public PermissionType Permission { get; set; }

        public string FolderInfoId { get; set; }
        public FolderInfo  FolderInfo { get; set; }
    }

    public enum ShareType
    {
        Deny = 0,
        ReadOnly = 1,
        IsFull = 2
    }

    public enum PermissionType
    {
        Deny = 0,
        ReadOnly = 1,
        IsFull = 2,
        NotAbleDeleted=3
    }

}
