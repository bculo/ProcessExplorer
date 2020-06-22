using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Common.Options;
using System;
using System.Collections.Generic;

namespace ProcessExplorer.Service.Process
{
    public class AllProcessCollector : RootCollector, IProcessCollector
    {
        public AllProcessCollector(ILoggerWrapper logger,
            IOptions<ProcessCollectorOptions> options) 
            : base(logger, options)
        {
        }

        /// <summary>
        /// Get all processes (WORKS ON LINUX AND WINDOWS 64 bit)
        /// Problem with paths on 32 bit windows !!!
        /// </summary>
        /// <returns></returns>
        public List<ProcessInformation> GetProcesses()
        {
            _logger.LogInfo($"Started fetching processes in {nameof(AllProcessCollector)}");

            var processList = new List<ProcessInformation>();
            foreach (var proc in GetAllProcesses())
            {
                
                if(proc.MainModule == null) //Some Linux MainModule are null, so skip
                    continue;

                try
                {
                    processList.Add(new ProcessInformation
                    {
                        ProcessId = proc.Id,
                        ProcessPath = proc.MainModule.ModuleName,
                        ProcessName = proc.ProcessName,
                    });
                }
                catch (Exception e) //Not allowed to read MainModule (windows permission)
                {
                    _logger.LogError(e);
                }
            }

            _logger.LogInfo($"Processes fetched in {nameof(AllProcessCollector)} : {processList.Count}");
            return processList;
        }
    }
}
