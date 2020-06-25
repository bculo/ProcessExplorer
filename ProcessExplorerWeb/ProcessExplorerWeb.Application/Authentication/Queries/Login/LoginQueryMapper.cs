﻿using Mapster;
using ProcessExplorerWeb.Application.Common.Models.Security;

namespace ProcessExplorerWeb.Application.Authentication.Queries.Login
{
    public class LoginQueryMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TokenInfo, LoginQueryResponseDto>()
                .Map(dst => dst.UserId, src => src.User.Id)
                .Map(dst => dst.UserName, src => src.User.UserName)
                .Map(dst => dst.JwtToken, src => src.JwtToken)
                .Map(dst => dst.ExpireIn, src => src.ExpireIn);
        }
    }
}
