using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Authentication
{
    public class AuthenticationTokenService : ITokenService
    {
        private readonly IUnitOfWork _work;
        private readonly IDateTime _time;

        public AuthenticationTokenService(IUnitOfWork work, IDateTime time)
        {
            _work = work;
            _time = time;
        }

        public async Task<string> GetToken()
        {
            var result = await _work.Authentication.GetLastToken();

            if (result == null)
                return null;

            return result.Content;
        }

        public async Task SetNewToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return;

            var authInstance = new Core.Entities.Authentication
            {
                Content = token,
                Inserted = _time.Now
            };

            _work.Authentication.Add(authInstance);
            await _work.CommitAsync();
        }

        public Task<bool> TokenAvailable()
        {
            if (_work.Authentication.GetAll().Count() > 0)
                return Task.FromResult(true);
            return Task.FromResult(false);
        }
    }
}
