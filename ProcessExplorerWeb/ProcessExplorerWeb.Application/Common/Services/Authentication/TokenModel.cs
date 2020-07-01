using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Models.Authentication
{
    public class TokenModel
    {
        public IProcessExplorerUser User { get; private set; }
        public long ExpireIn { get; private set; }
        public string JwtToken { get; private set; }

        public TokenModel(IProcessExplorerUser user, string jwtToken, long expiration)
        {
            User = user;
            JwtToken = jwtToken;
            ExpireIn = expiration;
        }
    }
}
