
using AutoMapper;
using ElenSoft.Application.ViewModels.Identity.User;
 
using ElenSoft.DataLayer.Models.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenSoft.Application.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
             CreateMap<UpsertUserCmd, ApplicationUser>();

            CreateMap<ApplicationUser, UserDto>();
            // .ForMember(dest => dest.Id, opt => opt.Ignore());
        }

    }
}
