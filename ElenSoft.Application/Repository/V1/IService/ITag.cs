using ElenSoft.Application.ViewModels;
using ElenSoft.Application.ViewModels.Category.Query;
using ElenSoft.Application.ViewModels.Tag.Cmd;
using ElenSoft.Application.ViewModels.Tag.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.Repository.V1.IService
{
    public interface ITag
    {
        Task<Response> UpsertTag(UpsertTagCmd request);
        Task<Response> DeleteTag(string request);
        Task<Response<TagsDto>> GetTags(TagsQuery request);
        Task<Response<TagDto>> GetTag(string request);
    }
}
