using ElenSoft.Application.ViewModels;
using ElenSoft.Application.ViewModels.Archive.Cmd;
using ElenSoft.Application.ViewModels.Archive.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.Repository.V1.IService
{
    public interface IArchive
    {
        Task<Response> UpsertArchive(UpsertArchiveCmd request);
        Task<Response> DeleteArchive(string request);
        Task<Response<ArchivesDto>> GetArchives(ArchivesQuery request);
        Task<Response<ArchiveDto>> GetArchive(string request);
    }
}
