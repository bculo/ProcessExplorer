using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Core.Enums;
using System;

namespace ProcessExplorer.Service.Services.System
{
    /// <summary>
    /// Singleton for platform information
    /// </summary>
    public class SystemInformationService : IPlatformInformationService
    {
        private readonly ILoggerWrapper _logger;

        private PlatformInformation systemInformation;

        public PlatformInformation PlatformInformation 
        { 
            get
            {
                if (systemInformation == null)
                    throw new ArgumentNullException(nameof(PlatformInformation));
                return systemInformation;
            } 
        }

        public SystemInformationService(ILoggerWrapper logger)
        {
            _logger = logger;

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

            _logger.LogInfo("Platform information _ {@data}", systemInformation);
        }

        /// <summary>
        /// Set platform type
        /// </summary>
        private void SetType()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    systemInformation.Type = Platform.WIN;
                    break;
                case PlatformID.Unix:
                    systemInformation.Type = Platform.UNIX;
                    break;
                default:
                    systemInformation.Type = Platform.UNKNOWN;
                    break;
            }
        }
    }
}
