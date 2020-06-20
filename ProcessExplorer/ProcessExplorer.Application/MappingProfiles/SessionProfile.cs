using AutoMapper;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Application.MappingProfiles
{
    /// <summary>
    /// Profile for session entity
    /// </summary>
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            //SessionInformation -> Session
            CreateMap<SessionInformation, Session>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.SessionId))
                .ForMember(dst => dst.Started, opt => opt.MapFrom(src => src.SessionStarted))
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.User));
        }  
    }
}
