using ProcessExplorer.Application.Common.Interfaces;
using System.Linq;

namespace ProcessExplorer.Service.Application
{
    public abstract class RootAppCollector
    {
        protected readonly ILoggerWrapper _logger;
        protected readonly ISessionService _sessionService;

        public RootAppCollector(ILoggerWrapper logger, 
            ISessionService sessionService)
        {
            _logger = logger;
            _sessionService = sessionService;
        }

        public string GetBasicApplicationTitle(string fullTitle)
        {
            if (string.IsNullOrEmpty(fullTitle))
                return string.Empty;

            return fullTitle.Split(" - ").Select(i => i.Trim()).Last();
        }
    }
}
