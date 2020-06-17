using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Clients
{
    public class AuthenticationClient : IAuthenticationClient
    {
        public AuthenticationClient(HttpClient http)
        {

        }

        public async Task<string> Login(string username, string password)
        {
            return Guid.NewGuid().ToString();
        }

        public async Task<bool> ValidateToken(string jwtToken)
        {
            return true;
        }
    }
}
