using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Service.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProcessExplorer.Service.Process
{
    /// <summary>
    /// Root process collector
    /// </summary>
    public abstract class RootCollector
    {
        protected readonly IPlatformProcessRecognizer _recognizer;

        protected RootCollector(IPlatformProcessRecognizer recognizer)
        {
            _recognizer = recognizer;
        }

        /// <summary>
        /// Use all process filters
        /// </summary>
        /// <param name="processes">All fetched processes</param>
        /// <returns></returns>
        protected virtual IList<ProcessInformation> FilterList(IEnumerable<ProcessInformation> processes)
        {
            var filteredList = FilterPlatformProcesses(processes);
            filteredList = RemoveCurrentProcess(filteredList);
            return filteredList.ToList();
        }

        /// <summary>
        /// Remove platform specific processes
        /// </summary>
        /// <param name="processes">All fetched processes</param>
        /// <returns></returns>
        protected IEnumerable<ProcessInformation> FilterPlatformProcesses(IEnumerable<ProcessInformation> processes)
        {
            foreach(var process in processes)
            {
                if (!_recognizer.IsPlatfromProcess(process.ProcessPath))
                    yield return process;
            }
        }

        /// <summary>
        /// Remove current proccess from list
        /// </summary>
        /// <param name="processes">All fetched processes</param>
        /// <returns></returns>
        protected IEnumerable<ProcessInformation> RemoveCurrentProcess(IEnumerable<ProcessInformation> processes)
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            return processes.Where(i => i.ProcessId != currentProcess.Id);
        }
    }
}
