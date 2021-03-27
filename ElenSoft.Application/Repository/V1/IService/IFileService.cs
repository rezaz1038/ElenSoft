using System.Threading.Tasks;
using ElenSoft.Application.Profiles;
using ElenSoft.Application.ViewModels;

namespace ElenSoft.Application.Repository.V1.IService
{
    public interface IFileService
    {
        Task<Response> Upload(UploadFileCommand request);
    }
}