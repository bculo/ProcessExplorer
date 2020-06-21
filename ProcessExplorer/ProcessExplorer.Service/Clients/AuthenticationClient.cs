﻿using Dtos.Requests.Authentication;
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
        private readonly ITokenService _tokenService;
        private readonly ILoggerWrapper _logger;

        public AuthenticationClient(HttpClient http, 
            IOptions<ProcessExplorerWebClientOptions> options,
            ITokenService tokenService,
            ILoggerWrapper logger) : base(http, options)
        {
            _tokenService = tokenService;
            _logger = logger;
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

            var response = await _http.PostAsync("authentication/login", CreateContent(requestModel));

            if (response.IsSuccessStatusCode)
                return await GetInstanceFromBody<LoginResponseDto>(response);

            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
            {
                _logger.LogInfo("Server not available!!!");
                throw new HttpRequestException("Server not available!!!");
            }

            return null;
        }

        /// <summary>
        /// Register user session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> RegisterSession(SessionInformation session)
        {
            var requestModel = new SessionDto
            {
                SesssionId = session.SessionId,
                UserName = session.User,
                Started = session.SessionStarted
            };

            AddBearerToken(_tokenService.GetValidToken());
            var response = await _http.PostAsync("authentication/sessionregistration", CreateContent(requestModel));

            if (response.IsSuccessStatusCode)
                return true;


            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
            {
                _logger.LogInfo("Server not available!!!");
                throw new HttpRequestException("Server not available!!!");
            }

            return false;
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

            var response = await _http.PostAsync($"authentication/checktoken", CreateContent(requestModel));

            if (response.IsSuccessStatusCode)
                return true;


            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
            {
                _logger.LogInfo("Server not available!!!");
                throw new HttpRequestException("Server not available!!!");
            }

            return false;
        }
    }
}