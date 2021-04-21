using AutoMapper;
using AutoMapper.QueryableExtensions;
using ElenSoft.Application.Repository.V1.IService;
using ElenSoft.Application.ViewModels;
using ElenSoft.Application.ViewModels.Archive.Cmd;
using ElenSoft.Application.ViewModels.Archive.Query;
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
using ArchivesQuery = ElenSoft.Application.ViewModels.Archive.Query.ArchivesQuery;

namespace ElenSoft.Application.Repository.V1.Services
{
    public class ArchiveService : IArchive
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;
        public ArchiveService(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        #region delete
        public async Task<Response> DeleteArchive(string request)
        {
            if (!string.IsNullOrEmpty(request))
            {
                var item = await _context.Archives.SingleOrDefaultAsync(x => x.Id == request);
                if (item == null)
                {
                    throw new BusinessLogicException("رکوردی یافت نشد");
                }
                _context.Archives.Remove(item);

            }
            await _context.SaveChangesAsync();
            return new Response
            {
                Status = true,
                Message = "success"
            };
        }

        #endregion

        #region GetArchive
        public async Task<Response<ArchiveDto>> GetArchive(string request)

        {
            var item = await _context.Archives.SingleOrDefaultAsync(x => x.Id == request);

            var itemTag =
            await _context.Tags.SingleOrDefaultAsync(x => x.Id == item.TagId);

            var itemCategory =
            await _context.Categories.SingleOrDefaultAsync(x => x.Id == item.CategoryId);

            if (item == null)
            {
                throw new BusinessLogicException("رکوردی یافت نشد");
            }

            if (itemTag == null)
            {
                throw new BusinessLogicException("چنین تگی یافت نشد");
            }

            if (itemCategory == null)
            {
                throw new BusinessLogicException("چنین گروهی یافت نشد");
            }

            var resultTag = new TagDto
            {
                Id = itemTag.Id,
                Title = itemTag.Title
            };

            var resultCategory = new CategoryDto
            {
                Id = itemCategory.Id,
                Title = itemCategory.Title
            };



            var result = new ArchiveDto

            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Path = item.Path,
                Filesize = item.Filesize,
                Category = resultCategory,
                Tag = resultTag


            };

            return new Response<ArchiveDto>
            {
                Status = true,
                Message = "success",
                Data = result
            };

        }
        #endregion

        #region  GetArchives
        public async Task<Response<ArchivesDto>> GetArchives(ArchivesQuery request)
        {
            var result = _context.Archives.
                Include(x => x.Category).Include(y => y.Tag).
                AsQueryable();


            if (!string.IsNullOrEmpty(request.Name))
            {
                result = result.Where(x => x.Name.Contains(request.Name));
            }

            if (!string.IsNullOrEmpty(request.CategoryTitle))
            {

                result = result.Where(x => x.Category.Title.Contains(request.CategoryTitle));
            }

            if (!string.IsNullOrEmpty(request.TagTitle))
            {

                result = result.Where(x => x.Tag.Title.Contains(request.TagTitle));
            }

            ///pagenating
            int take = request.PageSize;
            int skip = (request.PageId - 1) * take;

            int totalPages = (int)Math.Ceiling(result.Count() / (double)take);

            var finalResult = result.OrderBy(x => x.Name).Skip(skip).Take(take).AsQueryable();


            //----------------
            //var resultDepartment=await 



            var resultData = new ArchivesDto
            {
                Dtos = await finalResult.Select(a => new ArchiveDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Filesize = a.Filesize,
                    Description = a.Description,
                    Path = a.Path,
                    Category = new CategoryDto
                    {
                        Id = a.Category.Id,
                        Title = a.Category.Title
                    },
                    Tag = new TagDto
                    {
                        Id = a.Category.Id,
                        Title = a.Category.Title
                    },
                }).ToListAsync(),
                PageId = request.PageId,
                PageSize = request.PageSize,
                Total = await result.CountAsync()
            };

            return new Response<ArchivesDto>
            {
                Data = resultData,


                Status = true,
                Message = "success"
            };


        }
 


        #endregion

        #region Upsert
        public async Task<Response> UpsertArchive(UpsertArchiveCmd request)
        {
            {
                ///////////////////////////////////////
                //var technicalCode = await _context.Equipment.AnyAsync(x => x.TechnicalCode == request.TechnicalCode);
                //if (technicalCode)
                //{
                //    throw new BusinessLogicException("کد فنی قبلا استفاده شده است ");
                //}

                //var amval = await _context.Equipment.AnyAsync(x => x.Amval == request.Amval);
                //if (amval)
                //{
                //    throw new BusinessLogicException("کداموال قبلا استفاده شده است ");
                //}
                //////////////////////////////////////////

                var category = await _context.Categories.SingleOrDefaultAsync(x => x.Id == request.CategoryId);
                var tag = await _context.Tags.SingleOrDefaultAsync(x => x.Id == request.TageId);
                

                ///updat insert

                if (!string.IsNullOrEmpty(request.Id))
                {
                    var item = await _context.Archives.SingleOrDefaultAsync(x => x.Id == request.Id);
                    if (item == null)
                    {
                        throw new BusinessLogicException("رکوردی یافت نشد");
                    }

                    item = _mapper.Map<Archive>(request);
                 

                    if (category != null) item.Category = category;
                    if (tag != null) item.Tag =tag;
                   

                    
                    _context.Archives.Update(item);
                }
                else
                {
                    var item = new Archive();
                    item = _mapper.Map<Archive>(request);

                    if (category != null) item.Category = category;
                    if (tag != null) item.Tag = tag;

                    
                    item.Id = Guid.NewGuid().ToString();
                    item.CreatedAt = DateTime.Now;
                    await _context.Archives.AddAsync(item);
                }

                await _context.SaveChangesAsync();
                return new Response
                {
                    Status = true,
                    Message = "success"
                };
            }

        }
            #endregion

        }


    }