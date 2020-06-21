using ProcessExplorer.Application.Dtos.Requests.Update;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface ISyncClient
    {
        Task<bool> SyncSession(UserSessionDto sessionDto);
    }
}
