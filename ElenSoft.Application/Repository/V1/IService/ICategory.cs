using ElenSoft.Application.ViewModels;
using ElenSoft.Application.ViewModels.Category.Cmd;
using ElenSoft.Application.ViewModels.Category.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.Repository.V1.IService
{
    public interface ICategory
    {
        Task<Response> UpsertCategory(UpsertCategoryCmd request);
        Task<Response> DeleteCategory(string request);
        Task<Response<CategoriesDto>> GetCategories(CategoriesQuery request);
        Task<Response<CategoryDto>> GetCategory(string request);
    }
}
