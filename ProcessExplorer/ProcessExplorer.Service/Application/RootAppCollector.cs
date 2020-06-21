using ProcessExplorer.Application.Common.Interfaces;
using System.Linq;

namespace ProcessExplorer.Service.Application
{
    public abstract class RootAppCollector
    {
        protected readonly ILoggerWrapper _logger;
        protected readonly ISessionService _sessionService;
        protected readonly IDateTime _dateTime;

        public RootAppCollector(ILoggerWrapper logger, 
            ISessionService sessionService,
            IDateTime dateTime)
        {
            _logger = logger;
            _sessionService = sessionService;
            _dateTime = dateTime;
        }

        public string GetBasicApplicationTitle(string fullTitle)
        {
            if (string.IsNullOrEmpty(fullTitle))
                return string.Empty;

            return fullTitle.Split(" - ").Select(i => i.Trim()).Last();
        }
    }
}
