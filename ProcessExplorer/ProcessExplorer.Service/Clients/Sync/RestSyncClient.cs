using Mapster;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Dtos.Requests.Update;
using ProcessExplorer.Service.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Clients.Sync
{
    public class RestSyncClient : RootHttpClient, ISyncClient
    {
        private readonly ITokenService _tokenService;

        public RestSyncClient(HttpClient client, 
            IOptions<ProcessExplorerWebClientOptions> options,
            ITokenService tokenService) : base(client, options)
        {
            _tokenService = tokenService;
        }

        public async Task<bool> Sync(UserSessionDto sessionDto)
        {
            var response = await Post("Synchronization/sync", sessionDto, _tokenService.GetValidToken());

            if (TimeOccurred) //Timeout occurred
                return false;

            if (response.IsSuccessStatusCode)
                return true;

            var result = await GetInstanceFromBody<object>(response);

            return false;
        }
    }
}
