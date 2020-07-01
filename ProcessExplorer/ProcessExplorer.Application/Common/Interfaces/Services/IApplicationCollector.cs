using ProcessExplorer.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Application.Common.Interfaces
{
    public interface IApplicationCollector
    {
        /// <summary>
        /// Get currently opened applications
        /// </summary>
        /// <returns></returns>
        List<ApplicationInformation> GetApplications();
    }
}
