using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace ProcessExplorer.Service.Process.Windows
{
    /// <summary>
    /// WMI
    /// https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-process
    /// https://stackoverflow.com/questions/5497064/how-to-get-the-full-path-of-running-process
    /// </summary>
    public class WMIProcessCollector : RootCollector, IProcessCollector
    {
        public WMIProcessCollector(IPlatformProcessRecognizer recognizer) : base(recognizer)
        {
        }

        public IList<ProcessInformation> GetProcesses()
        {
            var wmiQueryString = "SELECT ProcessId, ExecutablePath, Name, Description FROM Win32_Process";

            using var searcher = new ManagementObjectSearcher(wmiQueryString);
            using var results = searcher.Get();

            var filteredProcesses = from p in System.Diagnostics.Process.GetProcesses()
                        join mo in results.Cast<ManagementObject>()
                        on p.Id equals (int)(uint)mo["ProcessId"]
                        where (mo.GetPropertyValue("ExecutablePath") != null)
                        select new ProcessInformation
                        {
                            ProcessPath = (string)mo["ExecutablePath"],
                            ProcessTitle = string.IsNullOrEmpty(p.MainWindowTitle) ? null : p.MainWindowTitle,
                            ProcessName = p.ProcessName,
                        };

            //remove duplicates
            filteredProcesses = filteredProcesses.GroupBy(i => i.ProcessName)
                                                .Select(i => i.First());

            return FilterPlatformProcesses(filteredProcesses).ToList();
        }
    }
}
