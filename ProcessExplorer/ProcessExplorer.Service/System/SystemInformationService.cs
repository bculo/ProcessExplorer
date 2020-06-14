using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System;

namespace ProcessExplorer.Service.Services.System
{
    /// <summary>
    /// Singleton
    /// </summary>
    public class SystemInformationService : IPlatformInformationService
    {
        private PlatformInformation information;

        public PlatformInformation PlatformInformation 
        { 
            get
            {
                if (information == null)
                    throw new ArgumentNullException(nameof(PlatformInformation));
                return information;
            } 
        }

        public SystemInformationService()
        {
            Set();
        }

        /// <summary>
        /// Set platform information
        /// </summary>
        private void Set()
        {
            if (information != null)
                throw new InvalidOperationException($"Operation { nameof(Set) } can be executed only once");

            information = new PlatformInformation
            {
                MachineName = Environment.MachineName,
                Platform = Environment.OSVersion.Platform.ToString(),
                PlatformVersion = Environment.OSVersion.VersionString,
                UserName = Environment.UserName,
                UserDomainName = Environment.UserDomainName
            };

            SetType();
        }

        /// <summary>
        /// Set platform type
        /// </summary>
        private void SetType()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    information.Type = Platform.Win;
                    break;
                case PlatformID.Unix:
                    information.Type = Platform.Unix;
                    break;
                default:
                    information.Type = Platform.Unknown;
                    break;
            }

        }
    }
}
