using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Clients.Sync;
using ProcessExplorer.Service.Options;
using System.Net.Http;

namespace ProcessExplorer.Service.Clients
{
    public class SyncClientFactory : ISynchronizationClientFactory
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ITokenService _tokenService;
        private readonly IOptions<ProcessExplorerWebClientOptions> _options;

        public SyncClientFactory(IHttpClientFactory clientFactory,
            IOptions<ProcessExplorerWebClientOptions> options,
            ITokenService tokenService)
        {
            _clientFactory = clientFactory;
            _options = options;
            _tokenService = tokenService;
        }    

        public ISyncClient GetClient()
        {
            return new RestSyncClient(_clientFactory.CreateClient(), _options, _tokenService);
        }
    }
}
