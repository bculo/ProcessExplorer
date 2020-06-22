using Mapster;
using Newtonsoft.Json;
using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Dtos.Requests.Update;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class ProcessCollectorBehaviour : IProcessBehaviour
    {
        private readonly ISynchronizationClientFactory _syncFactory;
        private readonly IProcessCollectorFactory _processFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInternet _internet;
        private readonly ISessionService _session;
        private readonly ILoggerWrapper _logger;
        private readonly IDateTime _time;

        private CollectStatus CollectStatus { get; set; } = CollectStatus.SUCCESS;

        public ProcessCollectorBehaviour(IProcessCollectorFactory processFactory,
            ISynchronizationClientFactory syncFactory,
            IUnitOfWork unitOfWork,
            IInternet internet,
            ISessionService session,
            ILoggerWrapper wrapper,
            IDateTime dateTime)
        {
            _processFactory = processFactory;
            _syncFactory = syncFactory;
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
            List<ProcessInformation> processes = collector.GetProcesses();

            //check internet connection
            if (!await _internet.CheckForInternetConnectionAsync() || _session.SessionInformation.Offline)
            {
                //store records to local database
                await StoreRecords(processes);
                _logger.LogInfo($"Finished collecting processes: {_time.Now} with status: {CollectStatus}");
                return;
            }

            //get sync client
            var syncClient = _syncFactory.GetClient();

            //create user session dto
            var dto = _session.SessionInformation.Adapt<UserSessionDto>();
            dto.Processes = processes.Adapt<IEnumerable<ProcessDto>>();

            //send to server
            if (!await syncClient.Sync(dto))
            {
                //store records to local database
                await StoreRecords(processes);
            }

            _logger.LogInfo($"Finished collecting processes: {_time.Now} with status: {CollectStatus}");
        }

        /// <summary>
        /// Store records to local database
        /// </summary>
        /// <param name="apps"></param>
        /// <returns></returns>
        private async Task StoreRecords(List<ProcessInformation> fetchedApps)
        {
            //get all applications records for this session
            //we are working here with max 1000 records
            try
            {
                var storedProcesses = await _unitOfWork.Process.GetEntitesForSession(_session.SessionInformation.SessionId);

                //no records for this session so enter everything
                if (storedProcesses.Count == 0)
                {
                    var newProcesses = fetchedApps.Adapt<List<ProcessEntity>>();
                    _unitOfWork.Process.BulkAdd(newProcesses);
                    return;
                }

                //O(m + n)
                //get processes that are not stored
                HashSet<string> hashset = new HashSet<string>(storedProcesses.Select(i => i.ProcessName));
                var notStoredProcesses = fetchedApps.Where(i => !hashset.Contains(i.ProcessName));

                //map them to Entity
                var processesToStore = notStoredProcesses.Adapt<List<ProcessEntity>>();
                if (processesToStore.Count == 0)
                    return;

                //save changes of modified entites
                await _unitOfWork.CommitAsync();

                //Store them
                _unitOfWork.Process.BulkAdd(processesToStore);
            }
            catch (Exception e)
            {
                CollectStatus = CollectStatus.FAILURE;
                _logger.LogError(e);
            }
        }
    }
}
