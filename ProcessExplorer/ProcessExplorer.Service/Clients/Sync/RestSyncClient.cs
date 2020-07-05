using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Dtos.Requests.Update;
using ProcessExplorer.Service.Options;
using System.Net.Http;
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

        /// <summary>
        /// Execute rest api method
        /// Method can throw exception !!!
        /// </summary>
        /// <param name="endPoint"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task<bool> ExecuteRestApiMethod(string endPoint, UserSessionDto dto)
        {
            var response = await Post(endPoint, dto, _tokenService.GetValidToken());

            if (TimeOccurred) //Timeout occurred
                return false;

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> Sync(UserSessionDto sessionDto)
        {
            return await ExecuteRestApiMethod("Synchronization/sync", sessionDto);
        }

        public async Task<bool> SyncApplications(UserSessionDto sessionDto)
        {
            sessionDto.Processes = null;
            return await ExecuteRestApiMethod("Synchronization/sync/applications", sessionDto);
        }

        public async Task<bool> SyncProcesses(UserSessionDto sessionDto)
        {
            sessionDto.Applications = null;
            return await ExecuteRestApiMethod("Synchronization/sync/processes", sessionDto);
        }
    }
}
