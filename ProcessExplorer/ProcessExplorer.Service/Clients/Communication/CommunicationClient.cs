using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Interfaces.Clients;
using ProcessExplorer.Application.Dtos.Responses.Communication;
using ProcessExplorer.Core.Enums;
using ProcessExplorer.Service.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Clients.Communication
{
    public class CommunicationClient : RootHttpClient, ICommunicationTypeClient
    {
        private readonly ITokenService _tokenService;

        public CommunicationClient(HttpClient client,
            IOptions<ProcessExplorerWebClientOptions> options,
            ITokenService tokenService) : base(client, options)
        {
            _tokenService = tokenService;
        }


        public async Task<CommunicationType> GetCommunicationType()
        {
            try
            {
                AddBearerToken(_tokenService.GetValidToken());

                var response = await _http.GetAsync("Communication");

                if(!response.IsSuccessStatusCode)
                    return CommunicationType.REST;

                var bodyObject = await GetInstanceFromBody<CommunicationResponseDto>(response);

                CommunicationType type = (CommunicationType)Enum.ToObject(typeof(CommunicationType), bodyObject.Type);

                return type;
            }
            catch(Exception)
            {
                return CommunicationType.REST;
            }
        }
    }
}
