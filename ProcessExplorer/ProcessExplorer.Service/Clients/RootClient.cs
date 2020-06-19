using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Service.Clients
{
    public abstract class RootClient
    {
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
    }
}
