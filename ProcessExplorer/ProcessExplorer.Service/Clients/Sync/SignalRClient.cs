using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Dtos.Requests.Update;
using ProcessExplorer.Service.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Clients.Sync
{
    public class SignalRClient : ISyncClient
    {
        private readonly ProcessExplorerWebClientOptions _options;
        private readonly ITokenService _tokenService;

        private readonly HubConnection _connection;

        public SignalRClient(IOptions<ProcessExplorerWebClientOptions> options,
            ITokenService tokenService)
        {
            _options = options.Value;
            _tokenService = tokenService;

            //build hub
            _connection = new HubConnectionBuilder()
                .WithUrl(_options.SocketUri, options =>
                {
                    //add JWT token
                    options.AccessTokenProvider = () => Task.FromResult(_tokenService.GetValidToken());
                    //options.CloseTimeout
                })
                .Build();
        }

        /// <summary>
        /// Invoke web socket method
        /// Method can throw Exception !!!
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task<bool> InvokeSocketMethod(string methodName, UserSessionDto dto)
        {
            await _connection.StartAsync();
            await _connection.InvokeAsync("SyncSession", JsonConvert.SerializeObject(dto));
            await _connection.StopAsync();
            return true;
        }

        public async Task<bool> Sync(UserSessionDto sessionDto)
        {
            try
            {
                return await InvokeSocketMethod("SyncSession", sessionDto);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SyncApplications(UserSessionDto sessionDto)
        {
            try
            {
                sessionDto.Processes = null;
                return await InvokeSocketMethod("SyncApplications", sessionDto);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SyncProcesses(UserSessionDto sessionDto)
        {
            try
            {
                sessionDto.Applications = null;
                return await InvokeSocketMethod("SynProcesses", sessionDto);
            }
            catch
            {
                return false;
            }
        }
    }
}
