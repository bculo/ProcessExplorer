using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Interfaces.Services;
using ProcessExplorer.Core.Enums;
using ProcessExplorer.Service.Clients.Sync;
using ProcessExplorer.Service.Options;
using System.Net.Http;

namespace ProcessExplorer.Service.Clients
{
    public class SyncClientFactory : ISynchronizationClientFactory
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ITokenService _tokenService;
        private readonly ICommunicationTypeService _typeService;
        private readonly IOptions<ProcessExplorerWebClientOptions> _options;

        public SyncClientFactory(IHttpClientFactory clientFactory,
            IOptions<ProcessExplorerWebClientOptions> options,
            ITokenService tokenService,
            ICommunicationTypeService typeService)
        {
            _clientFactory = clientFactory;
            _options = options;
            _tokenService = tokenService;
            _typeService = typeService;
        }    

        public ISyncClient GetClient()
        {
            CommunicationType currentType = _typeService.GetCommunicationType();

            switch(currentType)
            {
                case CommunicationType.REST:
                    return new RestSyncClient(_clientFactory.CreateClient(), _options, _tokenService);
                case CommunicationType.SOCKET:
                    return new SignalRClient(_options, _tokenService);
                case CommunicationType.SOAP:
                    return new SoapClient(_tokenService);
                default:
                    return new RestSyncClient(_clientFactory.CreateClient(), _options, _tokenService);
            }
        }
    }
}
