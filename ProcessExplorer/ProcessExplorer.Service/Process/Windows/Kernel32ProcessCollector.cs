using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
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
        public Kernel32ProcessCollector(IPlatformProcessRecognizer recognizer) : base(recognizer)
        {
        }

        public IList<ProcessInformation> GetProcesses()
        {
            var processList = new List<ProcessInformation>();

            //remove duplicates
            var allProcceses = System.Diagnostics.Process.GetProcesses()
                .GroupBy(i => i.ProcessName)
                .Select(i => i.First())
                .ToArray();

            foreach (var proc in allProcceses)
            {
                try
                {
                    processList.Add(new ProcessInformation
                    {
                        ProcessTitle = string.IsNullOrEmpty(proc.MainWindowTitle) ? null : proc.MainWindowTitle,
                        ProcessName = proc.ProcessName,
                        ProcessPath = GetMainModuleFileName(proc),
                    });
                }
                catch (Win32Exception e) //Not allowed to read MainModule (important file!!!)
                {
                    //TODO : LOG
                }
            }

            return FilterPlatformProcesses(processList).ToList();
        }


        [DllImport("Kernel32.dll")]
        private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);

        /// <summary>
        /// Get main module filename -> working on 32 and 64 system
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
