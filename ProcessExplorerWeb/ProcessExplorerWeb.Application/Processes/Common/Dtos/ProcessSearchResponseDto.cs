using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Processes.Common.Dtos
{
    public class ProcessSearchResponseDto
    {
        /// <summary>
        /// Name of process
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// For google search
        /// </summary>
        public string GoogleSearchQuery => ProcessName.Replace(' ', '+');

        /// <summary>
        /// occures in number of sessions
        /// </summary>
        public int OccuresInNumOfSessions { get; set; }
    }
}
