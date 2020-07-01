using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Dtos.Requests.Update;
using System;
using System.Collections.Generic;
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
        Task<bool> Sync(UserSessionDto sessionDto);

        /// <summary>
        /// Sync applications
        /// </summary>
        /// <param name="sessionDto"></param>
        /// <returns></returns>
        Task<bool> SyncApplications(UserSessionDto sessionDto);


        /// <summary>
        /// Sync processes
        /// </summary>
        /// <param name="sessionDto"></param>
        /// <returns></returns>
        Task<bool> SyncProcesses(UserSessionDto sessionDto);

    }
}
