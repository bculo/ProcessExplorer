using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Dtos.Requests.Update;
using ProcessExplorer.Service.Options;
using System;
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

        public Task<bool> SyncApplications(UserSessionDto sessionDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SyncProcesses(UserSessionDto sessionDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncSessionAll(UserSessionDto sessionDto)
        {
            var response = await Post("Synchronization/sync", CreateContent(sessionDto), _tokenService.GetValidToken());

            if (TimeOccurred) //Timeout occurred
                return false;

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}
