using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class ProcessCollectorBehaviour : IProcessBehaviour
    {
        private readonly IProcessCollectorFactory _processFactory;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInternet _internet;
        private readonly ISessionService _session;
        private readonly ILoggerWrapper _logger;
        private readonly IDateTime _time;

        private CollectStatus CollectStatus { get; set; } = CollectStatus.SUCCESS;

        public ProcessCollectorBehaviour(IProcessCollectorFactory processFactory,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IInternet internet,
            ISessionService session,
            ILoggerWrapper wrapper,
            IDateTime dateTime)
        {
            _processFactory = processFactory;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _internet = internet;
            _session = session;
            _logger = wrapper;
            _time = dateTime;
        }

        public async Task Collect()
        {
            _logger.LogInfo($"Started collecting processes: {_time.Now}");

            //get processes collector
            IProcessCollector collector = _processFactory.GetProcessCollector();

            //get running applications
            List<ProcessInformation> apps = collector.GetProcesses();

            //check internet connection
            if (!await _internet.CheckForInternetConnectionAsync())
            {
                //store records to local database
            }

            //TODO: Push to backend


            _logger.LogInfo($"Finished collecting processes: {_time.Now} with status: {CollectStatus}");
        }
    }
}
