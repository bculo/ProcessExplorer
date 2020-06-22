using ProcessExplorer.Application.Common.Interfaces;
using System;

namespace ProcessExplorer.Service.Application.Windows
{
    public abstract class WindowsRootAppCollector : RootAppCollector
    {
        public WindowsRootAppCollector(ILoggerWrapper logger,
            ISessionService sessionService,
            IDateTime dateTime) : base(logger, sessionService, dateTime)
        { }

        private static string WINDOWS_EXPLORER = "explorer";

        /// <summary>
        /// sSpecial handler for windows
        /// </summary>
        /// <param name="fullTitle"></param>
        /// <param name="processName"></param>
        /// <returns></returns>
        public override string PlatformSpecificTitleHandler(string fullTitle, string processName)
        {
            ///User is using goingh throught folders
            if (processName == WINDOWS_EXPLORER)
                return FirstLetterToUppercase(WINDOWS_EXPLORER);

            return fullTitle;
        }  
    }
}
