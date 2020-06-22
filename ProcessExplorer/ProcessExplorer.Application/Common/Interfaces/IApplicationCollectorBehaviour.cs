using ProcessExplorer.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IApplicationCollectorBehaviour
    {
        Task Collect();
    }
}
