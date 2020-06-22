using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace ProcessExplorer.Service.Process.Windows
{
    public class Kernel32ProcessCollector : WindowsRootProcessCollector, IProcessCollector
    {
        public Kernel32ProcessCollector(ILoggerWrapper logger,
            ISessionService session, 
            IDateTime time) 
            : base(logger, session, time)
        {
        }

        /// <summary>
        /// Get all processes (Works only on Windows 32 and 64 bit)
        /// Method use kernel32.dll
        /// </summary>
        /// <returns></returns>
        public List<ProcessInformation> GetProcesses()
        {
            _logger.LogInfo($"Started fetching processes in {nameof(Kernel32ProcessCollector)}");

            DateTime fetchedTime = _time.Now;
            Guid sessionId = _session.SessionInformation.SessionId;

            var processList = new List<ProcessInformation>();
            foreach (var proc in GetAllProcesses())
            {
                try
                {
                    processList.Add(new ProcessInformation
                    {
                        ProcessId = proc.Id,
                        ProcessName = proc.ProcessName,
                        ProcessPath = GetMainModuleFileName(proc),
                        Fetched = fetchedTime,
                        Session = sessionId
                    });
                }
                catch (Win32Exception) { } //Not allowed to read MainModule (important file!!!)
            }

            var filteredList = CleanList(processList);
            _logger.LogInfo($"Processes fetched in {nameof(Kernel32ProcessCollector)} : {processList.Count}");
            return filteredList;
        }

        [DllImport("Kernel32.dll")]
        private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);

        /// <summary>
        /// Get main module filename using Windows API -> working on 32 and 64 system
        /// </summary>
        /// <param name="process"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetMainModuleFileName(System.Diagnostics.Process process, int buffer = 1024)
        {
            var fileNameBuilder = new StringBuilder(buffer);
            uint bufferLength = (uint)fileNameBuilder.Capacity + 1;
            return QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength) ? fileNameBuilder.ToString() : null;
        }
    }
}
