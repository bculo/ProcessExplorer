using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Options;
using ProcessExplorer.Service.Interfaces;
using ProcessExplorer.Service.Process.Windows;
using System;

namespace ProcessExplorer.Service.Process
{
    public class ProcessCollectorFactory : IProcessCollectorFactory
    {
        private readonly IOptions<ProcessCollectorOptions> _options;
        private readonly IPlatformInformationService _platform;
        private readonly IPlatformProcessRecognizer _recognizer;

        public ProcessCollectorFactory(IOptions<ProcessCollectorOptions> options, 
            IPlatformInformationService platform,
            IPlatformProcessRecognizer recognizer)
        {
            _options = options;
            _platform = platform;
            _recognizer = recognizer;
        }

        public IProcessCollector GetProcessCollector()
        {
            switch (_platform.PlatformInformation.Type)
            {
                case Application.Common.Enums.Platform.Win:
                    return GetWindowsCollector();
                case Application.Common.Enums.Platform.Unix:
                    return GetLinuxCollector();
                default:
                    throw new NotSupportedException("Platform not supported");
            }
        }

        private IProcessCollector GetWindowsCollector()
        {
            return new WMIProcessCollector(_recognizer);
        }

        private IProcessCollector GetLinuxCollector()
        {
            return new AllProcessCollector(_recognizer);
        }
    }
}
