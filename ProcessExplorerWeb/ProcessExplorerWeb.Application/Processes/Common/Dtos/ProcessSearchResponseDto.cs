using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Common.Dtos
{
    public class SingleProcessResponseDto
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
