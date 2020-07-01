using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IInternet
    {
        Task<bool> CheckForInternetConnectionAsync();
    }
}
