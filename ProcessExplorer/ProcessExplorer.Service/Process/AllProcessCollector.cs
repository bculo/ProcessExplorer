using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System;
using System.Collections.Generic;

namespace ProcessExplorer.Service.Process
{
    public class AllProcessCollector : RootCollector, IProcessCollector
    {
        public AllProcessCollector(ILoggerWrapper logger,
            ISessionService session, 
            IDateTime time) 
            : base(logger, session, time)
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

            Guid sessionId = _session.SessionInformation.SessionId;
            DateTime fetchTime = _time.Now;

            var processList = new List<ProcessInformation>();
            foreach (var proc in GetAllProcesses())
            {
                try
                {
                    processList.Add(new ProcessInformation
                    {
                        ProcessId = proc.Id,
                        ProcessName = proc.ProcessName,
                        Session = sessionId,
                        Fetched = fetchTime
                    });
                }
                catch (Exception) { } //Not allowed to read MainModule (windows permission)

            }

            _logger.LogInfo($"Processes fetched in {nameof(AllProcessCollector)} : {processList.Count}");
            return CleanList(processList);
        }

        public override IEnumerable<ProcessInformation> PlatformSpecificHandler(IEnumerable<ProcessInformation> processes) => processes;
    }
}
