using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Common.Options;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace ProcessExplorer.Service.Process.Windows
{
    /// <summary>
    /// WMI (Windows Management Instrumentation)
    /// </summary>
    public class WMIProcessCollector : RootCollector, IProcessCollector
    {
        public WMIProcessCollector(ILoggerWrapper logger,
            IOptions<ProcessCollectorOptions> options) 
            : base(logger, options)
        {
        }

        /// <summary>
        /// Get all processes (Works only on Windows 32 and 64 bit)
        /// Method use WMI
        /// </summary>
        /// <returns></returns>
        public List<ProcessInformation> GetProcesses()
        {
            _logger.LogInfo($"Started fetching processes in {nameof(WMIProcessCollector)}");

            var wmiQueryString = "SELECT ProcessId, ExecutablePath, Name, Description FROM Win32_Process";
            using var searcher = new ManagementObjectSearcher(wmiQueryString);
            using var results = searcher.Get();

            var filteredProcesses = from p in GetAllProcesses()
                        join mo in results.Cast<ManagementObject>()
                        on p.Id equals (int)(uint)mo["ProcessId"]
                        where (mo.GetPropertyValue("ExecutablePath") != null)
                        select new ProcessInformation
                        {
                            ProcessId = p.Id,
                            ProcessPath = (string)mo["ExecutablePath"],
                            ProcessName = p.ProcessName,
                        };

            var filteredList = FilterList(filteredProcesses);
            _logger.LogInfo($"Processes fetched in {nameof(WMIProcessCollector)} : {filteredList.Count}");
            return filteredList;
        }
    }
}
