using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IAuthenticationClient
    {
        public Task<bool> ValidateToken(string jwtToken);
        public Task<string> Login(string username, string password);
    }
}
