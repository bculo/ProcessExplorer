using Dtos.Requests.Authentication;
using Dtos.Responses.Authentication;
using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Dtos.Requests.Authentication;
using ProcessExplorer.Service.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Clients
{
    public class AuthenticationClient : RootHttpClient, IAuthenticationClient
    {
        private readonly ILoggerWrapper _logger;

        public AuthenticationClient(HttpClient http, 
            IOptions<ProcessExplorerWebClientOptions> options,
            ILoggerWrapper logger) : base(http, options)
        {
            _logger = logger;
        }

        /// <summary>
        /// Validate old token
        /// </summary>
        /// <param name="jwtToken">JWT token</param>
        /// <returns></returns>
        public async Task<LoginResponseDto> Login(string identifier, string password)
        {
            try
            {
                var requestModel = new LoginDto
                {
                    Identifier = identifier,
                    Password = password,
                };

                var response = await _http.PostAsync("authentication/login", CreateContent(requestModel));

                _logger.LogInfo("SUCCESS LOGIN");

                if (response.IsSuccessStatusCode)
                    return await GetInstanceFromBody<LoginResponseDto>(response);

                if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    _logger.LogInfo("Server not available!!!");
                    throw new HttpRequestException("Server not available!!!");
                }

                return null;
            }
            catch(Exception e)
            {
                _logger.LogError(e);
                throw new HttpRequestException("Server not available!!!");
            }
        }

        /// <summary>
        /// Login using username and password
        /// </summary>
        /// <param name="identifier">identifier</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public async Task<bool> ValidateToken(string jwtToken)
        {
            try
            {
                var requestModel = new CheckTokenDto { Token = jwtToken };

                var response = await _http.PostAsync($"authentication/checktoken", CreateContent(requestModel));

                if (response.IsSuccessStatusCode)
                    return true;

                _logger.LogInfo("SUCCESS TOKEN CHECK");

                if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                {
                    _logger.LogInfo("Server not available!!!");
                    throw new HttpRequestException("Server not available!!!");
                }

                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                throw new HttpRequestException("Server not available!!!");
            }
        }
    }
}
