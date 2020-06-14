using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IInternet
    {
        bool CheckForInternetConnection();
        Task<bool> CheckForInternetConnectionAsync();
    }
}
