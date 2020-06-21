using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class ProcessCollectorBehaviour : IProcessBehaviour
    {
        public ProcessCollectorBehaviour()
        {

        }

        public Task Collect()
        {
            Console.WriteLine("COLLECTING PROCESSES");

            return Task.CompletedTask;
        }
    }
}
