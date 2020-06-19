using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class ApplicationBehaviour : IApplicationBehaviour
    {
        public ApplicationBehaviour()
        {

        }

        public Task Collect()
        {
            Console.WriteLine("COLLECTING APPLICATIONS");

            return Task.CompletedTask;
        }
    }
}
