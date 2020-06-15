using Microsoft.Extensions.DependencyInjection;
using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProcessExplorer
{
    /*
     * https://stackoverflow.com/questions/5071894/c-sharp-kill-all-processes-not-essential-to-running-windows
     */
    public class Startup
    {
        public async Task StartApplication(IServiceProvider provider)
        {
            var factory = provider.GetRequiredService<IProcessCollectorFactory>();

            var info = factory.GetProcessCollector().GetProcesses();

            foreach(var proc in info)
            {
                Console.WriteLine($"{proc.ProcessName}");
            }
        }
    }
}
 