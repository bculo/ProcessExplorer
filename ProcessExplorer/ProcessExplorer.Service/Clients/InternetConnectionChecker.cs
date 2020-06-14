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

        public InternetConnectionChecker(HttpClient client, 
            IOptions<InternetCheckOptions> options)
        {
            _options = options.Value;
            _client = client;

            _client.Timeout = TimeSpan.FromSeconds(_options.Timeout);
        }

        public async Task<bool> CheckForInternetConnectionAsync()
        {
            var response = await _client.GetAsync(_options.Uri);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}
