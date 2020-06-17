using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface ITokenService
    {
        public Task<bool> TokenAvailable();
        public Task<string> GetToken();
        public Task SetNewToken(string token);
    }
}
