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
        private static Dictionary<Platform, Func<IApplicationCollector>> ExecutionMethods;

        private readonly ILoggerWrapper _logger;
        private readonly IPlatformInformationService _platform;
        private readonly ApplicationCollectorUsageOptions _options;

        public ApplicationCollectorFactory(ILoggerWrapper logger,
            IPlatformInformationService platform,
            IOptions<ApplicationCollectorUsageOptions> options)
        {
            _logger = logger;
            _platform = platform;
            _options = options.Value;
        }

        /// <summary>
        /// Define execution method for each platform
        /// </summary>
        /// <returns></returns>
        private Dictionary<Platform, Func<IApplicationCollector>> GetExecutionMethods()
        {
            if (ExecutionMethods != null)
                return ExecutionMethods;

            ExecutionMethods = new Dictionary<Platform, Func<IApplicationCollector>>
            {
                { Platform.Win,  GetWindowsApplicationCollector },
                { Platform.Unix,  GetLinuxApplicationCollector }
            };

            return ExecutionMethods;
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
                case nameof(TerminalApplicationGetter):
                    return new TerminalApplicationGetter(_logger);
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
                    return new WindowsViaProcessApplicationCollector(_logger);
                case nameof(DllUsageApplicationCollector):
                    return new DllUsageApplicationCollector(_logger);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
