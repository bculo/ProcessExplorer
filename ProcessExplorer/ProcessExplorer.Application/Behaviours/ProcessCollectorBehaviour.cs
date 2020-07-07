using Mapster;
using Newtonsoft.Json;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Interfaces.Notifications;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Dtos.Requests.Update;
using ProcessExplorer.Core.Entities;
using ProcessExplorer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    /// <summary>
    /// Process collecting behaviour
    /// </summary>
    public class ProcessCollectorBehaviour : CommonCollectorBehaviour<ProcessEntity>, IProcessBehaviour
    {
        private readonly IProcessCollectorFactory _processFactory;

        private CollectStatus CollectStatus { get; set; } = CollectStatus.SUCCESS;

        public ProcessCollectorBehaviour(IProcessCollectorFactory processFactory,
            ISynchronizationClientFactory syncFactory,
            IUnitOfWork unitOfWork,
            IInternet internet,
            ISessionService session,
            ILoggerWrapper wrapper,
            IDateTime dateTime,
            INotificationService notification)
            : base(syncFactory, unitOfWork, internet, session, wrapper, dateTime, notification)
        {
            _processFactory = processFactory;
        }

        public async Task Collect()
        {
            try
            {
                _logger.LogInfo($"Started collecting processes: {_time.Now}");

                //get processes collector
                IProcessCollector collector = _processFactory.GetProcessCollector();

                //get running processes
                List<ProcessInformation> processes = collector.GetProcesses();

                var jsonTest = JsonConvert.SerializeObject(processes);
                _logger.LogInfo(jsonTest);

                //check internet connection and user work mode
                if (!await _internet.CheckForInternetConnectionAsync() || _session.SessionInformation.Offline)
                {
                    //store records to local database
                    await StoreRecords(processes);

                    _logger.LogInfo($"Finished collecting processes: {_time.Now} with status: {CollectStatus}");

                    _notification.ShowStatusMessage(nameof(ProcessCollectorBehaviour), $"Processes saved locally (No internet connection | offline mode): {_time.Now}");

                    return;
                }

                //get sync client
                var syncClient = _syncFactory.GetClient();

                //create user session dto
                var dto = _session.SessionInformation.Adapt<UserSessionDto>();
                dto.Processes = processes.Adapt<IEnumerable<ProcessDto>>();

                var jsonTest2 = JsonConvert.SerializeObject(dto);
                _logger.LogInfo(jsonTest2);

                //send to server
                if (!await syncClient.SyncProcesses(dto))
                {
                    //store records to local database
                    await StoreRecords(processes);

                    _notification.ShowStatusMessage(nameof(ProcessCollectorBehaviour), $"Processes saved locally (Server error): {_time.Now}");

                    return;
                }

                _notification.ShowStatusMessage(nameof(ProcessCollectorBehaviour), $"Processes sync finished: {_time.Now}");

                _logger.LogInfo($"Finished collecting processes: {_time.Now} with status: {CollectStatus}");
            }
            catch (Exception e)
            {
                _logger.LogError(e);
            }
        }

        /// <summary>
        /// Store records to local database (working with max 5000 processes). That case is very rare
        /// </summary>
        /// <param name="apps"></param>
        /// <returns></returns>
        private async Task StoreRecords(List<ProcessInformation> fetchedProcesses)
        {
            try
            {
                //zero processes fetched, so exit
                if (fetchedProcesses.Count == 0)
                    return;

                //Get all stored processes for current session
                var storedProcesses = await _unitOfWork.Process.GetEntitesForSession(_session.SessionInformation.SessionId);

                //no records for this session so add every process to database
                if (storedProcesses.Count == 0)
                {
                    //map them to process entities
                    var newProcesses = fetchedProcesses.Adapt<List<ProcessEntity>>();

                    //save changes of modified entites
                    await InsertLogic(newProcesses);

                    //done
                    return;
                }

                //get processes that are not yet stored in database
                var notStoredProcesses = GetNewProcessesToStore(fetchedProcesses, storedProcesses);

                //map them to process entities
                var processesToStore = notStoredProcesses.Adapt<List<ProcessEntity>>();

                //nothing to store -> exit
                if (processesToStore.Count == 0)
                    return;

                //save changes of modified entites
                await InsertLogic(processesToStore);
            }
            catch (Exception e)
            {
                //on failure change status and log error
                CollectStatus = CollectStatus.FAILURE;
                _logger.LogError(e);
            }
        }

        /// <summary>
        /// Get only processes that are not yet stored in database O(m + n)
        /// </summary>
        /// <param name="fetchedProcesses"></param>
        /// <param name="storedProcesses"></param>
        /// <returns></returns>
        private IEnumerable<ProcessInformation> GetNewProcessesToStore(List<ProcessInformation> fetchedProcesses, List<ProcessEntity> storedProcesses)
        {
            //get stored processes names and put them in hashset
            var storedProccessesName = new HashSet<string>(storedProcesses.Select(i => i.ProcessName));

            //get processes that are not in hashset
            var notStoredProcesses = fetchedProcesses.Where(i => !storedProccessesName.Contains(i.ProcessName));
            return notStoredProcesses;
        }

        /// <summary>
        /// Insert records to dabase
        /// </summary>
        /// <param name="newEntities"></param>
        /// <returns></returns>
        protected async Task InsertLogic(List<ProcessEntity> newEntities)
        {
            //Use bulk insert (faster)
            if (newEntities.Count >= 1000)
            {
                _unitOfWork.Process.BulkAdd(newEntities);
            }
            else //faster for less then 1000 records
            {
                _unitOfWork.Process.AddRange(newEntities);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
