using ElenSoft.Application.Repository.V1.IService;
using ElenSoft.Application.ViewModels;
using ElenSoft.Application.ViewModels.Archive.Query;
using ElenSoft.Application.ViewModels.Category.Cmd;
using ElenSoft.Application.ViewModels.Category.Query;
using ElenSoft.Application.ViewModels.Tag.Query;
using ElenSoft.DataLayer.Models.Context;
using ElenSoft.DataLayer.Models.Entities;
using ElenSoft.Insfrastrcture;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.Repository.V1.Services
{
    public class CategoryService : ICategory
    {

        private readonly AppDBContext _context;
    public CategoryService(AppDBContext context)
    {
        _context = context;
    }


    #region delete
    public async Task<Response> DeleteCategory(string request)
        {
        if (!string.IsNullOrEmpty(request))
        {
            var item = await _context.Categories.SingleOrDefaultAsync(x => x.Id == request);
            if (item == null)
            {
                throw new BusinessLogicException("رکوردی یافت نشد");
            }
            _context.Categories.Remove(item);

        }
        await _context.SaveChangesAsync();
        return new Response
        {
            Status = true,
            Message = "success"
        };
    }

    #endregion

    #region upsert
    public async Task<Response> UpsertCategory(UpsertCategoryCmd request)
        {
        if (!string.IsNullOrEmpty(request.Id))
        {
            var item = await _context.Categories.SingleOrDefaultAsync(x => x.Id == request.Id);
            if (item == null)
            {
                throw new BusinessLogicException("رکوردی یافت نشد");
            }

            item.Title = request.Title;
            _context.Categories.Update(item);

        }
        else
        {
            var item = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.Title,
                CreatedAt = DateTime.Now
            };
            await _context.Categories.AddAsync(item);
        }
        await _context.SaveChangesAsync();


        return new Response
        {
            Status = true,
            Message = "success"
        };

    }
    #endregion

    #region single
    public async Task<Response<CategoryDto>> GetCategory(string request)
        {
        var item = await _context.Categories.SingleOrDefaultAsync(x => x.Id == request);
        if (item == null)
        {
            throw new BusinessLogicException("رکوردی یافت نشد");
        }

        var result = new CategoryDto

        {
            Id = item.Id,
            Title = item.Title,


        };

        return new Response<CategoryDto>
        {
            Status = true,
            Message = "success",
            Data = result
        };


    }

    #endregion

    #region Get Categories
    public async Task<Response<CategoriesDto>> GetCategories(CategoriesQuery request)
        {
        var result = _context.Categories.AsQueryable();

        if (!string.IsNullOrEmpty(request.Title))
        {
            result = result.Where(x => x.Title.Contains(request.Title));
        }





        ///pagenating
        int take = request.PageSize;
        int skip = (request.PageId - 1) * take;

        int totalPages = (int)Math.Ceiling(result.Count() / (double)take);

        var finalResult = result.OrderBy(x => x.Title).Skip(skip).Take(take).AsQueryable();


        //----------------


        var resultData = new CategoriesDto
        {
            Dtos = await finalResult.Select(d => new CategoryDto()
            {
                Id = d.Id,
                Title = d.Title
            }).ToListAsync(),
            PageId = request.PageId,
            PageSize = request.PageSize,
            Total = await result.CountAsync()
        };

        return new Response<CategoriesDto>
        {
            Data = resultData,


            Status = true,
            Message = "success"
        };


    }

    }




    #endregion


}


     

  
