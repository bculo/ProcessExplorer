﻿using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Common.Options;
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
        protected readonly ILoggerWrapper _logger;
        protected readonly ProcessCollectorOptions _options;

        protected RootCollector(IPlatformProcessRecognizer recognizer,
            ILoggerWrapper logger,
            IOptions<ProcessCollectorOptions> options)
        {
            _recognizer = recognizer;
            _logger = logger;
            _options = options.Value;
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
        /// Make process name unique
        /// </summary>
        /// <param name="processes">All fetched processes</param>
        /// <returns></returns>
        protected virtual IEnumerable<ProcessInformation> RemoveDuplicates(IEnumerable<ProcessInformation> processes){
            return processes.GroupBy(i => i.ProcessName)
                        .Select(i => i.First());
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

        protected IEnumerable<System.Diagnostics.Process> GetAllProcesses()
        {
            if(!_options.RemoveDuplicates) 
                return System.Diagnostics.Process.GetProcesses();
            else 
            {
                var processes = System.Diagnostics.Process.GetProcesses();

                return processes.GroupBy(i => i.ProcessName)
                            .Select(i => i.First());
            }
        }
    }
}