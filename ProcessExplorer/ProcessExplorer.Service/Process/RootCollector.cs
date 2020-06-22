using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProcessExplorer.Service.Process
{
    /// <summary>
    /// Root process collector
    /// </summary>
    public abstract class RootCollector
    {
        protected readonly ILoggerWrapper _logger;
        protected readonly ISessionService _session;
        protected readonly IDateTime _time;

        protected RootCollector(ILoggerWrapper logger,
            ISessionService session,
            IDateTime time)
        {
            _logger = logger;
            _session = session;
            _time = time;
        }

        public abstract IEnumerable<ProcessInformation> PlatformSpecificHandler(IEnumerable<ProcessInformation> processes);

        /// <summary>
        /// Use all process filters
        /// </summary>
        /// <param name="processes">All fetched processes</param>
        /// <returns></returns>
        protected virtual List<ProcessInformation> CleanList(IEnumerable<ProcessInformation> processes)
        {
            //remove current process
            var filteredList = RemoveCurrentProcess(processes);

            //use platform specific handler
            filteredList = PlatformSpecificHandler(filteredList);

            //return
            return filteredList.ToList();
        }


        /// <summary>
        /// Remove current proccess from list
        /// </summary>
        /// <param name="processes">All fetched processes</param>
        /// <returns></returns>
        protected IEnumerable<ProcessInformation> RemoveCurrentProcess(IEnumerable<ProcessInformation> processes)
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();

            if(currentProcess != null)
                return processes.Where(i => i.ProcessId != currentProcess.Id);

            return processes;
        }

        /// <summary>
        /// Get all processes and make process name unique
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<System.Diagnostics.Process> GetAllProcesses()
        {
            return System.Diagnostics.Process.GetProcesses()
                    .GroupBy(i => i.ProcessName)
                    .Select(i => i.First());
        }
    }
}
