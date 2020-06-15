using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Common.Options;
using ProcessExplorer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ProcessExplorer.Service.Process.Windows
{
    public class Kernel32ProcessCollector : RootCollector, IProcessCollector
    {
        public Kernel32ProcessCollector(IPlatformProcessRecognizer recognizer,
            ILoggerWrapper logger,
             IOptions<ProcessCollectorOptions> options) : base(recognizer, logger, options)
        {
        }

        /// <summary>
        /// Get all processes (Works only on Windows 32 and 64 bit)
        /// Method use kernel32.dll
        /// </summary>
        /// <returns></returns>
        public IList<ProcessInformation> GetProcesses()
        {
            _logger.LogInfo($"Started fetching processes in {nameof(Kernel32ProcessCollector)}");
            
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
                    });
                }
                catch (Win32Exception e) //Not allowed to read MainModule (important file!!!)
                {
                    _logger.LogError(e);
                }
            }

            var filteredList = FilterList(processList);
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
            return QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength) ?
                fileNameBuilder.ToString() :
                null;
        }
    }
}
