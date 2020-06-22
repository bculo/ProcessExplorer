using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Authentication
{
    public class AuthenticationTokenService : ITokenService
    {
        private readonly IUnitOfWork _work;
        private readonly IDateTime _time;

        /// <summary>
        /// Valid token for authentication
        /// </summary>
        private string Token { get; set; }

        public AuthenticationTokenService(IUnitOfWork work, IDateTime time)
        {
            _work = work;
            _time = time;
        }

        /// <summary>
        /// Get last token from store
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetLastToken()
        {
            var result = await _work.Authentication.GetLastToken();

            if (result == null)
                return null;

            return result.Content;
        }

        /// <summary>
        /// Add new token to store
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task AddNewToken(string token)
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

            Token = token;
        }

        /// <summary>
        /// Check if there are any available token in store
        /// </summary>
        /// <returns></returns>
        public Task<bool> TokenAvailable()
        {
            if (_work.Authentication.GetAll().Count() > 0)
                return Task.FromResult(true);
            return Task.FromResult(false);
        }

        /// <summary>
        /// Get valid token
        /// Method throws Exception if Token pproperty is null
        /// </summary>
        /// <returns></returns>
        public string GetValidToken()
        {
            if (Token == null)
                throw new ArgumentNullException(nameof(Token));

            return Token;
        }

        /// <summary>
        /// Set valid token
        /// </summary>
        /// <param name="token"></param>
        public void SetValidToken(string token)
        {
            Token = token;
        }
    }
}
