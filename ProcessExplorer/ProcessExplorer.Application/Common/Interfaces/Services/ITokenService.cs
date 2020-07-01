using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface ITokenService
    {
        public Task<bool> TokenAvailable();
        public Task<string> GetLastToken();
        public Task AddNewToken(string token);
        public string GetValidToken();
        public void SetValidToken(string token);
    }
}
