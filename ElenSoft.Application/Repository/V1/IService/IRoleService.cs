using ElenSoft.Application.ViewModels;
using ElenSoft.Application.ViewModels.Identity.Role.Cmd;
using ElenSoft.Application.ViewModels.Identity.Role.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.Repository.V1.IService
{
    public interface IRoleService
    {
        Task<Response> UpsertRole(UpsertRoleCmd request);
        Task<Response> DeleteRole(string request);
        Task<Response<RoleDto>> GetRole(string request);
        Task<Response<RolesDto>> GetRoles(RolesQuery request);



    }
}
