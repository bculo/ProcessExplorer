using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProcessExplorer.Service.Process.Linux
{
    public class PsAuxProcessCollector : LinuxRootProcessCollector, IProcessCollector
    {
        public PsAuxProcessCollector(ILoggerWrapper logger, 
            IPlatformInformationService info,
            ISessionService session,
            IDateTime time) 
            : base(logger, session, info, time)
        {
        }
        
        public List<ProcessInformation> GetProcesses()
        {
            _logger.LogInfo($"Started fetching processes in {nameof(PsAuxProcessCollector)}");

            //command ps axco user,pid,command
            var startInfo = new ProcessStartInfo 
            {
                FileName = "/bin/bash",
                Arguments = $"-c \" ps axco user,pid,command \"",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            //Start process
            var process = new System.Diagnostics.Process
            {
                StartInfo = startInfo
            };
            process.Start();

            //read content
            var terminalContent = process.StandardOutput.ReadToEnd();

            //parse content
            IEnumerable<ProcessInformation> processesInfo = ParseProcessInformation(terminalContent);

            //clean list
            var processList = CleanList(processesInfo);
            _logger.LogInfo($"Application fetched in {nameof(PsAuxProcessCollector)} : {processList.Count}");

            return processList;
        }

        /// <summary>
        /// Parse content
        /// </summary>
        /// <param name="terminalContent"></param>
        /// <returns></returns>
        private IEnumerable<ProcessInformation> ParseProcessInformation(string terminalContent)
        {
            string username = _info.PlatformInformation.UserName;
            var rowsWithSpecificUser = terminalContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries) //split on /r/n
                                                .Skip(1) //skip header
                                                .Where(i => i.Contains(username)); //get only processes for currently loged in user

            DateTime fetchedTime = _time.Now;
            Guid sessionId = _session.SessionInformation.SessionId;

            //got throught each row
            foreach (var row in rowsWithSpecificUser)
            {
                //get row columns
                var rowColumns = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                //length should be 3
                if(rowColumns.Length != 3)
                    continue;

                /// user -> rowColumns[0]
                /// pid -> rowColumns[1]
                /// command -> rowColumns[2]

                //pid needs to be integer
                if (!int.TryParse(rowColumns[1], out int pid))
                    continue;

                //return new ProcessInformation instance
                yield return new ProcessInformation
                {
                    ProcessId = pid,
                    ProcessName = rowColumns[2],
                    Session = sessionId,
                    Fetched = fetchedTime,
                };
            }
        }
    }
}