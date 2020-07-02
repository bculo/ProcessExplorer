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

        public async Task<bool> Sync(UserSessionDto sessionDto)
        {
            try
            {
                //start connection
                await _connection.StartAsync();

                //send message
                await _connection.InvokeAsync("SyncSession", JsonConvert.SerializeObject(sessionDto));

                await _connection.StopAsync();

                return true;
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

                //start connection
                await _connection.StartAsync();

                //send message
                await _connection.InvokeAsync("SyncApplications", JsonConvert.SerializeObject(sessionDto));

                await _connection.StopAsync();

                return true;
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

                //start connection
                await _connection.StartAsync();

                //send message
                await _connection.InvokeAsync("SynProcesses", JsonConvert.SerializeObject(sessionDto));

                await _connection.StopAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
