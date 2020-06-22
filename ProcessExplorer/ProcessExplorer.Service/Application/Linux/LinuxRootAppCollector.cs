using ProcessExplorer.Application.Common.Interfaces;

namespace ProcessExplorer.Service.Application.Linux
{
    public abstract class LinuxRootAppCollector : RootAppCollector
    {
        public LinuxRootAppCollector(ILoggerWrapper logger,
            ISessionService sessionService,
            IDateTime dateTime) : base(logger, sessionService, dateTime)
        { }


        /// <summary>
        /// sSpecial handler for Linux
        /// </summary>
        /// <param name="fullTitle"></param>
        /// <param name="processName"></param>
        /// <returns></returns>
        public override string PlatformSpecificTitleHandler(string fullTitle, string processName)
        {
            return fullTitle;
        }  
    }
}
