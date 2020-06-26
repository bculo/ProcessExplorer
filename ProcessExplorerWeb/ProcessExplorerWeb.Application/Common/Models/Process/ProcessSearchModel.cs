using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Common.Models.Process
{
    public class ProcessSearchModel
    {
        /// <summary>
        /// Name of process
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// occures in number of sessions
        /// </summary>
        public int OccuresInNumOfSessions { get; set; }
    }
}
