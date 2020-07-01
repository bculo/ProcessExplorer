using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Core.Enums;
using ProcessExplorer.Service.Session.Linux;
using ProcessExplorer.Service.Session.Windows;
using System;

namespace ProcessExplorer.Service.Session
{
    public class UserSessionFactory : IUserSessionFactory
    {
        private readonly ILoggerWrapper _logger;
        private readonly IPlatformInformationService _platformInformation;

        public UserSessionFactory(ILoggerWrapper logger, IPlatformInformationService platformInformation)
        {
            _logger = logger;
            _platformInformation = platformInformation;
        }

        /// <summary>
        /// Get session collector based on platform
        /// </summary>
        /// <returns></returns>
        public IUserSession GetUserSessionCollector()
        {
            switch (_platformInformation.PlatformInformation.Type)
            {
                case Platform.UNIX:
                    return new LinuxSession(_logger, _platformInformation);
                case Platform.WIN:
                    return new WindowsSession(_logger, _platformInformation);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
