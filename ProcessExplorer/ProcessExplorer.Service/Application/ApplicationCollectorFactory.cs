﻿using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Utils;
using ProcessExplorer.Service.Application.Linux;
using ProcessExplorer.Service.Application.Windows;
using ProcessExplorer.Service.Options;
using System;
using System.Collections.Generic;

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

        public IApplicationCollector GetApplicationCollector()
        {
            switch (_platform.PlatformInformation.Type)
            {
                case Platform.Win:
                    return GetWindowsApplicationCollector();
                case Platform.Unix:
                    return GetLinuxApplicationCollector();
                default:
                    throw new NotSupportedException("Platform not supported");
            }
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
