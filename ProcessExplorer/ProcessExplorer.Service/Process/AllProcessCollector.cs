using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Common.Options;
using ProcessExplorer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ProcessExplorer.Service.Process
{
    public class AllProcessCollector : RootCollector, IProcessCollector
    {
        public AllProcessCollector(IPlatformProcessRecognizer recognizer) : base(recognizer)
        {
        }

        public IList<ProcessInformation> GetProcesses()
        {
            var processList = new List<ProcessInformation>();

            foreach (var proc in System.Diagnostics.Process.GetProcesses())
            {
                try
                {
                    processList.Add(new ProcessInformation
                    {
                        ProcessTitle = string.IsNullOrEmpty(proc.MainWindowTitle) ? null : proc.MainWindowTitle,
                        ProcessName = proc.ProcessName,
                        ProcessPath = proc.MainModule.FileName,
                    });
                }
                catch (Win32Exception e) //Not allowed to read MainModule (important file!!!)
                {
                    //TODO : LOG
                }
            }

            return FilterPlatformProcesses(processList).ToList();
        }
    }
}
