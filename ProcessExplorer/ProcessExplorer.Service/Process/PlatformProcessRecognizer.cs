using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Interfaces;
using System;

namespace ProcessExplorer.Service.Process
{
    public class PlatformProcessRecognizer : IPlatformProcessRecognizer
    {
        private readonly IPlatformInformationService _platform;

        public PlatformProcessRecognizer(IPlatformInformationService platform)
        {
            _platform = platform;
        }  

        public bool IsPlatfromProcess(string processPath)
        {
            if (processPath == null)
                throw new ArgumentNullException(nameof(processPath));

            switch (_platform.PlatformInformation.Type)
            {
                case Application.Common.Enums.Platform.Win:
                    return IsWindowsPath(processPath);
                case Application.Common.Enums.Platform.Unix:
                    return IsLinuxPath(processPath);
                default:
                    throw new NotSupportedException("Platform not supported");
            }
        }

        public bool IsWindowsPath(string processPath)
        {
            if (processPath.ToLower().Contains("windows"))
                return true;

            return false;
        }

        public bool IsLinuxPath(string processPath)
        {
            return false;
        }
    }
}
