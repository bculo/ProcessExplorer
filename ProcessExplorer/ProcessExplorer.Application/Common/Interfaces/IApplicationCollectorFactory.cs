using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IApplicationCollectorFactory
    {
        IApplicationCollector GetApplicationCollector();
    }
}
