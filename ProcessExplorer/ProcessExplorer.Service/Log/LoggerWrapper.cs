using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Service.Options;
using System;

namespace ProcessExplorer.Service.Log
{
    public class LoggerWrapper : ILoggerWrapper
    {
        private readonly ILogger<LoggerWrapper> _logger;
        private readonly LoggerOptions _options;
        public LoggerWrapper(ILogger<LoggerWrapper> logger,
            IOptions<LoggerOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        public void LogError(string content, Exception e)
        {
            if (!_options.UseLog)
                return;

            _logger.LogError(content, e);
        }

        public void LogInfo(string content, params object[] param)
        {
            if (!_options.UseLog)
                return;

            _logger.LogInformation(content, param);
        }
    }
}
