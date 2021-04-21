using ElenSoft.Application.Repository.V1.IService;
using ElenSoft.Application.ViewModels;
using ElenSoft.Application.ViewModels.Tag.Cmd;
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
    public class TagService : ITag
    {

        private readonly AppDBContext _context;
        public TagService(AppDBContext context)
        {
            _context = context;
        }


        #region DeleteTag
        public async Task<Response> DeleteTag(string request)
        {
            if (!string.IsNullOrEmpty(request))
            {
                var item = await _context.Tags.SingleOrDefaultAsync(x => x.Id == request);
                if (item == null)
                {
                    throw new BusinessLogicException("رکوردی یافت نشد");
                }
                _context.Tags.Remove(item);

            }
            await _context.SaveChangesAsync();
            return new Response
            {
                Status = true,
                Message = "success"
            };
        }
        #endregion


        #region Tags      
        public async Task<Response<TagDto>> GetTag(string request)
        {
            var item = await _context.Tags.SingleOrDefaultAsync(x => x.Id == request);
            if (item == null)
            {
                throw new BusinessLogicException("رکوردی یافت نشد");
            }

            var result = new TagDto

            {
                Id = item.Id,
                Title = item.Title,


            };

            return new Response<TagDto>
            {
                Status = true,
                Message = "success",
                Data = result
            };
        }

        #endregion

        #region Get Tags
        public async Task<Response<TagsDto>> GetTags(TagsQuery request)
        {
            var result = _context.Tags.AsQueryable();

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


            var resultData = new TagsDto
            {
                Dtos = await finalResult.Select(d => new TagDto()
                {
                    Id = d.Id,
                    Title = d.Title
                }).ToListAsync(),
                PageId = request.PageId,
                PageSize = request.PageSize,
                Total = await result.CountAsync()
            };

            return new Response<TagsDto>
            {
                Data = resultData,


                Status = true,
                Message = "success"
            };
        }
        #endregion

        #region upsert
        public async Task<Response> UpsertTag(UpsertTagCmd request)
        {
            if (!string.IsNullOrEmpty(request.Id))
            {
                var item = await _context.Tags.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (item == null)
                {
                    throw new BusinessLogicException("رکوردی یافت نشد");
                }

                item.Title = request.Title;
                _context.Tags.Update(item);

            }
            else
            {
                var item = new Tag
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = request.Title,
                    CreatedAt = DateTime.Now
                };
                await _context.Tags.AddAsync(item);
            }
            await _context.SaveChangesAsync();


            return new Response
            {
                Status = true,
                Message = "success"
            };

        }
        #endregion


    }
}
