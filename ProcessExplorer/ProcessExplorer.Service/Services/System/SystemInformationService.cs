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

        public void Set()
        {
            if (PlatformInformation != null)
                throw new InvalidOperationException($"Operation { nameof(Set) } can be executed only once");

            
        }
    }
}
