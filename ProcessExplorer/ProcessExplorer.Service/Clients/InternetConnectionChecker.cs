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
        private readonly ILoggerWrapper _logger;
        private readonly IDateTime _time;

        public InternetConnectionChecker(HttpClient client, 
            IOptions<InternetCheckOptions> options,
            ILoggerWrapper logger,
            IDateTime time)
        {
            _options = options.Value;
            _client = client;
            _logger = logger;
            _time = time;

            _client.Timeout = TimeSpan.FromSeconds(_options.Timeout);
        }

        public async Task<bool> CheckForInternetConnectionAsync()
        {
            var response = await _client.GetAsync(_options.Uri);

            if (response.IsSuccessStatusCode) 
            {
                _logger.LogInfo($"Internet connection available on {_time.Now}");
                return true;
            }

            _logger.LogInfo($"Internet connection unvailable on {_time.Now}");
            return false;
        }
    }
}
