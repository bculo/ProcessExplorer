﻿using Mapster;
using ProcessExplorer.Application.Common.Enums;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Application.Dtos.Requests.Update;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class ApplicationCollectorBehaviour : IApplicationCollectorBehaviour
    {
        private readonly ISynchronizationClientFactory _syncFactory;
        private readonly IApplicationCollectorFactory _appFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInternet _internet;
        private readonly ISessionService _session;
        private readonly ILoggerWrapper _logger;
        private readonly IDateTime _time;

        private CollectStatus CollectStatus { get; set; } = CollectStatus.SUCCESS;

        public ApplicationCollectorBehaviour(ISynchronizationClientFactory syncFactory,
            IApplicationCollectorFactory appFactory,
            IUnitOfWork unitOfWork,
            IInternet internet,
            ISessionService session,
            ILoggerWrapper wrapper,
            IDateTime dateTime)
        {
            _syncFactory = syncFactory;
            _appFactory = appFactory;
            _unitOfWork = unitOfWork;
            _internet = internet;
            _session = session;
            _logger = wrapper;
            _time = dateTime;
        }

        public async Task Collect()
        {
            if (_session.SessionInformation.Offline)
                return;

            _logger.LogInfo($"Started collecting apps: {_time.Now}");

            //get app collector
            IApplicationCollector collector = _appFactory.GetApplicationCollector();

            //get running applications
            List<ApplicationInformation> apps = collector.GetApplications();

            //check internet connection
            if(!await _internet.CheckForInternetConnectionAsync())
            {
                //store records to local database
                await StoreRecords(apps);
            }

            //get sync client
            var syncClient = _syncFactory.GetClient();

            //create user session dto
            var dto = _session.SessionInformation.Adapt<UserSessionDto>();
            dto.Applications = apps.Adapt<IEnumerable<ApplicationDto>>();

            //send to server
            if (!await syncClient.Sync(dto))
            {
                //store records to local database
                await StoreRecords(apps);
            }

            _logger.LogInfo($"Finished collecting apps: {_time.Now} with status: {CollectStatus}");
        }

        /// <summary>
        /// Store records to local database
        /// </summary>
        /// <param name="apps"></param>
        /// <returns></returns>
        private async Task StoreRecords(List<ApplicationInformation> fetchedApps)
        {
            //get all applications records for this session
            //we are working here with max 200 records
            try
            {
                var applications = await _unitOfWork.Application.GetEntitesForSession(_session.SessionInformation.SessionId);

                //no records for this session so enter everything
                if (applications.Count == 0)
                {
                    var newApps = fetchedApps.Adapt<List<ApplicationEntity>>();
                    _unitOfWork.Application.BulkAdd(newApps);
                    return;
                }

                //update old and add new apps
                var newApplications = ModifyOldAndGetNewApps(fetchedApps, applications);

                //Modife old aps
                await _unitOfWork.CommitAsync();

                //add new apps
                _unitOfWork.Application.BulkAdd(newApplications);
            }
            catch(Exception e)
            {
                CollectStatus = CollectStatus.FAILURE;
                _logger.LogError(e);
            }
        }

        /// <summary>
        /// Modify old records and create new records
        /// </summary>
        /// <param name="fetchedApps">apps fetched from collector</param>
        /// <param name="storedApps">apps for this session fetched from store</param>
        /// <returns></returns>
        private List<ApplicationEntity> ModifyOldAndGetNewApps(List<ApplicationInformation> fetchedApps, List<ApplicationEntity> storedApps)
        {
            List<ApplicationEntity> newApps = new List<ApplicationEntity>();

            foreach (var fetchedApp in fetchedApps)
            {
                bool newApp = true;
                foreach(var storedApp in storedApps)
                {
                    //we are talking about same app if it has same applicaiton name and same starttime
                    if(storedApp.ApplicationName == fetchedApp.ApplicationName && storedApp.StartTime == fetchedApp.StartTime)
                    {
                        //change last active time of app
                        storedApp.Saved = fetchedApp.FetchTime;
                        newApp = false;
                        break;
                    }
                }

                // new app
                if (newApp)
                    newApps.Add(fetchedApp.Adapt<ApplicationEntity>());
            }

            return newApps;
        }
    }
}
