using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Utils;
using ProcessExplorer.Service.Application.Linux;
using ProcessExplorer.Service.Application.Windows;
using ProcessExplorer.Service.Interfaces;
using ProcessExplorer.Service.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProcessExplorer.Service.Application
{
    public class ApplicationCollectorFactory : IApplicationCollectorFactory
    {
        private readonly IDateTime _time;
        private readonly ISessionService _sessionService;
        private readonly ILoggerWrapper _logger;
        private readonly IPlatformInformationService _platform;
        private readonly ApplicationCollectorUsageOptions _options;

        public ApplicationCollectorFactory(ILoggerWrapper logger,
            IPlatformInformationService platform,
            IOptions<ApplicationCollectorUsageOptions> options,
            IDateTime time,
            ISessionService sessionService)
        {
            _logger = logger;
            _platform = platform;
            _options = options.Value;
            _time = time;
            _sessionService = sessionService;
        }

        /// <summary>
        /// Define execution method for each platform
        /// </summary>
        /// <returns></returns>
        private Dictionary<Platform, Func<IApplicationCollector>> GetExecutionMethods()
        {
            return new Dictionary<Platform, Func<IApplicationCollector>>
            {
                { Platform.Win,  GetWindowsApplicationCollector },
                { Platform.Unix,  GetLinuxApplicationCollector }
            };
        }

        public IApplicationCollector GetApplicationCollector()
        {
            return _platform.PlatformInformation.ExecuteFunWithReturnValue(GetExecutionMethods());
        }

        private IApplicationCollector GetLinuxApplicationCollector()
        {
            var result = _options.GetActiveOptionFor(Platform.Unix);

            switch (result)
            {
                case nameof(WmctrlApplicationCollector):
                    return new WmctrlApplicationCollector(_logger, _time, _sessionService);
                default:
                    throw new NotImplementedException();
            }
        }

        private IApplicationCollector GetWindowsApplicationCollector()
        {
            var result = _options.GetActiveOptionFor(Platform.Win);

            switch (result)
            {
                case nameof(WindowsViaProcessApplicationCollector):
                    return new WindowsViaProcessApplicationCollector(_logger, _sessionService, _time);
                case nameof(DllUsageApplicationCollector):
                    return new DllUsageApplicationCollector(_logger, _sessionService, _time);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
