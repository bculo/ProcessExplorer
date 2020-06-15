using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ProcessExplorer.Service.Application
{
    public abstract class RootAppCollector
    {
        protected readonly ILoggerWrapper _logger;

        public RootAppCollector(ILoggerWrapper logger)
        {
            _logger = logger;
        }

        public string GetBasicApplicationTitle(string fullTitle)
        {
            if (string.IsNullOrEmpty(fullTitle))
                return string.Empty;

            return fullTitle.Split(" - ").Select(i => i.Trim()).Last();
        }
    }
}
