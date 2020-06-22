using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Options;
using ProcessExplorer.Service.Process.Linux;
using ProcessExplorer.Service.Process.Windows;
using System;

namespace ProcessExplorer.Service.Process
{
    public class ProcessCollectorFactory : IProcessCollectorFactory
    {
        private readonly IPlatformInformationService _platform;
        private readonly ILoggerWrapper _logger;
        private readonly ISessionService _session;
        private readonly IDateTime _time;
        private readonly ProcessCollectorUsageOptions _usageOptions;

        public ProcessCollectorFactory(IPlatformInformationService platform,
            ILoggerWrapper logger,
            IOptions<ProcessCollectorUsageOptions> usageOptions,
            ISessionService session,
            IDateTime time)
        {
            _platform = platform;
            _logger = logger;
            _usageOptions = usageOptions.Value;
            _session = session;
            _time = time;
        }

        /// <summary>
        /// Get process collector based on platform and settings
        /// </summary>
        /// <returns></returns>
        public IProcessCollector GetProcessCollector()
        {
            switch (_platform.PlatformInformation.Type)
            {
                case Platform.Win:
                    return GetWindowsCollector();
                case Platform.Unix:
                    return GetLinuxCollector();
                default:
                    throw new NotSupportedException("Platform not supported");
            }
        }

        /// <summary>
        /// Get Collector for windows
        /// </summary>
        /// <returns></returns>
        private IProcessCollector GetWindowsCollector()
        {
            var result = _usageOptions.GetActiveOptionFor(Platform.Win);

            switch (result)
            {
                case nameof(WMIProcessCollector):
                    return new WMIProcessCollector(_logger, _session, _time);
                case nameof(Kernel32ProcessCollector):
                    return new Kernel32ProcessCollector(_logger, _session, _time);
                case nameof(AllProcessCollector):
                    return new AllProcessCollector(_logger, _session, _time);
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Get Collector for Linux
        /// </summary>
        /// <returns></returns>
        private IProcessCollector GetLinuxCollector()
        {
            var result = _usageOptions.GetActiveOptionFor(Platform.Unix);

            switch(result)
            {
                case nameof(AllProcessCollector):
                    return new AllProcessCollector(_logger, _session, _time);
                case nameof(PsAuxProcessCollector):
                    return new PsAuxProcessCollector(_logger, _platform, _session, _time);
                default:
                    throw new NotImplementedException();
            }

        }
    }
}
