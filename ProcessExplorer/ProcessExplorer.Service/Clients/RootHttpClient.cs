using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Options;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Clients
{
    public abstract class RootHttpClient
    {
        protected readonly HttpClient _http;
        private readonly ProcessExplorerWebClientOptions _options;
        
        protected bool TimeOccurred { get; private set; }

        public RootHttpClient(HttpClient client, IOptions<ProcessExplorerWebClientOptions> options)
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

        #region CONSOLE APPLICATION

        /// <summary>
        /// Wrapper for POST REQUEST
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post(string uri, object content, string jwttoken)
        {
            try
            {
                if (jwttoken != null)
                    AddBearerToken(jwttoken);

                var response = await _http.PostAsync(uri, CreateContent(content));

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new Exception("Unauthorized");

                return response;
            }
            catch(TimeoutException) //timeout
            {
                TimeOccurred = true;
                return null;
            }
        }

        #endregion
    }
}
