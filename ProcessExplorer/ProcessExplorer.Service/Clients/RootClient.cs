using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProcessExplorer.Service.Options;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Clients
{
    public abstract class RootClient
    {
        protected readonly HttpClient _http;
        private readonly ProcessExplorerWebClientOptions _options;

        public RootClient(HttpClient client, IOptions<ProcessExplorerWebClientOptions> options)
        {
            _http = client;
            _options = options.Value;

            _http.BaseAddress = new Uri(_options.BaseUri);
            _http.Timeout = TimeSpan.FromSeconds(_options.TimeOut);
        }

        public StringContent CreateContent(object item, string contentType = "application/json")
        {
            return new StringContent(
                JsonConvert.SerializeObject(item),
                Encoding.UTF8,
                contentType);
        }

        public async Task<T> GetInstanceFromBody<T>(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }

        public void AddBearerToken(string token)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
