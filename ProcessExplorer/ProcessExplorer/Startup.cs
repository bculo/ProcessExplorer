using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProcessExplorer
{
    public class Startup
    {
        public async Task StartApplication(IConfiguration configuration, IServiceCollection collection)
        {
            var processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if(process.MainWindowHandle != IntPtr.Zero)
                {
                    Console.WriteLine(process.ProcessName);
                }
            }
        }
    }
}
