using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Clients
{
    public class InternetConnectionChecker : IInternet
    {
        private readonly HttpClient _client;
        private readonly InternetCheckOptions _options;

        public InternetConnectionChecker(HttpClient client, IOptions<InternetCheckOptions> options)
        {

        }

        public bool CheckForInternetConnection()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckForInternetConnectionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
