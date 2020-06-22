using ProcessExplorer.Application.Dtos.Requests.Update;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface ISyncClient
    {
        /// <summary>
        /// Sync applications and processes
        /// </summary>
        /// <param name="sessionDto"></param>
        /// <returns></returns>
        Task<bool> SyncSessionAll(UserSessionDto sessionDto);

        /// <summary>
        /// Sync only applications
        /// </summary>
        /// <param name="sessionDto"></param>
        /// <returns></returns>
        Task<bool> SyncApplications(UserSessionDto sessionDto);

        /// <summary>
        /// Sync only processes
        /// </summary>
        /// <param name="sessionDto"></param>
        /// <returns></returns>
        Task<bool> SyncProcesses(UserSessionDto sessionDto);
    }
}
