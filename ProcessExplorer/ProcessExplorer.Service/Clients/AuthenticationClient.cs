using Dtos.Requests.Authentication;
using Dtos.Responses.Authentication;
using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Clients
{
    public class AuthenticationClient : RootClient, IAuthenticationClient
    {
        private readonly HttpClient _client;
        private readonly AuthenticationClientOptions _options;

        public AuthenticationClient(HttpClient http, IOptions<AuthenticationClientOptions> options)
        {
            _options = options.Value;
            _client = http;

            _client.BaseAddress = new Uri(_options.BaseUri);
            _client.Timeout = TimeSpan.FromSeconds(_options.TimeOut);
        }

        /// <summary>
        /// Validate old token
        /// </summary>
        /// <param name="jwtToken">JWT token</param>
        /// <returns></returns>
        public async Task<LoginResponseDto> Login(string identifier, string password)
        {
            var requestModel = new LoginDto
            {
                Identifier = identifier,
                Password = password,
            };

            var response = await _client.PostAsync($"{_options.LoginMethod}", CreateContent(requestModel));

            if (response.IsSuccessStatusCode)
                return await GetInstanceFromBody<LoginResponseDto>(response);

            return null;
        }

        /// <summary>
        /// Login using username and password
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public async Task<bool> ValidateToken(string jwtToken)
        {
            var requestModel = new CheckTokenDto { Token = jwtToken };

            var response = await _client.PostAsync($"{_options.ValidateTokenMethod}", CreateContent(requestModel));

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}
