using AutoMapper;
using ProcessExplorerWeb.Application.Common.Models.Security;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Api.Models.Authentication
{
    public class LoginMappingProfile : Profile
    {
        public LoginMappingProfile()
        {
            CreateMap<TokenInfo, LoginResponseModel>()
                .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.User.Id));
        }
    }
}
