using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System;

namespace ProcessExplorer.Service.Services.System
{
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

        /// <summary>
        /// Set platform information
        /// </summary>
        public void Set()
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
        }
    }
}
