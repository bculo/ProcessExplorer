using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Session.Linux;
using ProcessExplorer.Service.Session.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorer.Service.Session
{
    public class UserSessionFactory : IUserSessionFactory
    {
        private readonly ILoggerWrapper _logger;

        public UserSessionFactory(ILoggerWrapper logger)
        {
            _logger = logger;
        }

        public IUserSession GetUserSessionCollector(Platform platform)
        {
            switch (platform)
            {
                case Platform.Unix:
                    return new LinuxSession(_logger);
                case Platform.Win:
                    return new WindowsSession(_logger);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
