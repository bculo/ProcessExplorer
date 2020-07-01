using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Application.Modules.Application.Common.Dtos
{
    public class AppSearchItemResponseDto
    {
        /// <summary>
        /// Name of process
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// For google search
        /// </summary>
        public string GoogleSearchQuery => $"https://www.google.com/search?q={ ApplicationName.Replace(' ', '+') }";

        /// <summary>
        /// occures in number of sessions
        /// </summary>
        public int OccuresNumOfTime { get; set; }
    }
}
