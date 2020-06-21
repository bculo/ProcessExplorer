using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Dtos.Requests.Update;
using ProcessExplorer.Service.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Clients.Update
{
    public class RestUpdateClient : RootHttpClient, IUpdateClient
    {
        public RestUpdateClient(HttpClient client, 
            IOptions<ProcessExplorerWebClientOptions> options) : base(client, options)
        {
        }

        public async Task<bool> UpdateSession(UserSessionDto sessionDto)
        {
            throw new NotImplementedException();
        }
    }
}
