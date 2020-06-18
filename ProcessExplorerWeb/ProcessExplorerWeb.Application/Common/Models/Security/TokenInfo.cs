using ProcessExplorerWeb.Core.Interfaces;

namespace ProcessExplorerWeb.Application.Common.Models.Security
{
    public class TokenInfo
    {
        public IProcessExplorerUser User { get; private set; }
        public long ExpireIn { get; private set; }
        public string JwtToken { get; private set; }

        public TokenInfo(IProcessExplorerUser user, string jwtToken, long expiration)
        {
            User = user;
            JwtToken = jwtToken;
            ExpireIn = expiration;
        }
    }
}
