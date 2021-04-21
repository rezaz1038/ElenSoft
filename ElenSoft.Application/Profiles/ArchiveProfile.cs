using AutoMapper;
using ElenSoft.Application.ViewModels.Archive.Cmd;
using ElenSoft.DataLayer.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.Profiles
{
    public class ArchiveProfile : Profile

    {

        public ArchiveProfile()
        {
           CreateMap<UpsertArchiveCmd, Archive>() 
           .ForMember(dest => dest.Id, opt => opt.Ignore());


        }
    }
}
