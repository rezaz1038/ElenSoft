using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ElenSoft.Application.Profiles;
using ElenSoft.Application.Repository.V1.IService;
using ElenSoft.Application.ViewModels;
using ElenSoft.DataLayer.Models.Context;
using ElenSoft.DataLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElenSoft.Application.Repository.V1.Services
{
    public class FileService : IFileService
    {
        private readonly AppDBContext _context;
        private const string _server = @"\\192.168.121.15\d$\software";

        public FileService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Response> Upload(UploadFileCommand request)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId);
            var directory = GetUploadDirectory(category.Title, request.Title);
            var uploadPath = Path.Combine(_server, directory);

            var archive = new Archive();


            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }


            foreach (var file in request.Files)
            {
                await using var fileStream =
                    new FileStream(Path.Combine(uploadPath, file.FileName), FileMode.Create, FileAccess.Write);
                
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            archive.Id = Guid.NewGuid().ToString();
            archive.CreatedAt = DateTime.Now;
            archive.Category = category;
            archive.Name = request.Title;
            archive.Descriptiion = request.Description;
            archive.Path = directory;
            await _context.Archives.AddAsync(archive);
            await _context.SaveChangesAsync();

            return new Response()
            {
                Status = true,
                Message = ""
            };
        }

        private string GetUploadDirectory(string caregory, string name)
        {
            var folderName = name + "_" + DateTime.Now.ToString("MM_dd_yyyy");
            return Path.Combine(caregory, folderName);
        }
    }
}