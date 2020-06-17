using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Utils;
using ProcessExplorer.Service.Session.Windows;
using System;
using System.Collections.Generic;

namespace ProcessExplorer.Service.Services.System
{
    /// <summary>
    /// Singleton
    /// </summary>
    public class SystemInformationService : IPlatformInformationService
    {
        private static Dictionary<Platform, Func<IUserSession>> ExecutionMethods;

        private readonly ILoggerWrapper _logger;
        private readonly IUserSessionFactory _sessionFactory;

        private PlatformInformation systemInformation;
        private SessionInformation sessionInformation;

        public PlatformInformation PlatformInformation 
        { 
            get
            {
                if (systemInformation == null)
                    throw new ArgumentNullException(nameof(PlatformInformation));
                return systemInformation;
            } 
        }

        public SessionInformation SessionInformation
        {
            get
            {
                if (sessionInformation == null)
                    throw new ArgumentNullException(nameof(SessionInformation));
                return sessionInformation;
            }
        }

        public SystemInformationService(ILoggerWrapper logger, IUserSessionFactory sessionFactory)
        {
            _logger = logger;
            _sessionFactory = sessionFactory;

            Set();
        }

        /// <summary>
        /// Set platform information
        /// </summary>
        private void Set()
        {
            systemInformation = new PlatformInformation
            {
                MachineName = Environment.MachineName,
                Platform = Environment.OSVersion.Platform.ToString(),
                PlatformVersion = Environment.OSVersion.VersionString,
                UserName = Environment.UserName,
                UserDomainName = Environment.UserDomainName
            };

            SetType();
            SetSession();

            _logger.LogInfo("Platform information _ {@data}", systemInformation);
        }

        private void SetSession()
        {
            sessionInformation = _sessionFactory.GetUserSessionCollector(systemInformation.Type).GetSession();
            sessionInformation.User = systemInformation.UserName;
        }

        /// <summary>
        /// Set platform type
        /// </summary>
        private void SetType()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    systemInformation.Type = Platform.Win;
                    break;
                case PlatformID.Unix:
                    systemInformation.Type = Platform.Unix;
                    break;
                default:
                    systemInformation.Type = Platform.Unknown;
                    break;
            }
        }
    }
}
