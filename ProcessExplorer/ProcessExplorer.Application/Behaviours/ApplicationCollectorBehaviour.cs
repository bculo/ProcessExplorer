using Mapster;
using Mapster.Adapters;
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
    /// <summary>
    /// Application collecting behaviour
    /// </summary>
    public class ApplicationCollectorBehaviour : CommonCollectorBehaviour<ProcessEntity>, IApplicationCollectorBehaviour
    {
        private readonly IApplicationCollectorFactory _appFactory;

        private CollectStatus CollectStatus { get; set; } = CollectStatus.SUCCESS;

        public ApplicationCollectorBehaviour(
            ISynchronizationClientFactory syncFactory,
            IApplicationCollectorFactory appFactory,
            IUnitOfWork unitOfWork,
            IInternet internet,
            ISessionService session,
            ILoggerWrapper wrapper,
            IDateTime dateTime)
            : base(syncFactory, unitOfWork, internet, session, wrapper, dateTime)
        {
            _appFactory = appFactory;
        }

        public async Task Collect()
        {
            _logger.LogInfo($"Started collecting apps: {_time.Now}");

            //get app collector
            IApplicationCollector collector = _appFactory.GetApplicationCollector();

            //get running applications
            List<ApplicationInformation> apps = collector.GetApplications();

            //check internet connection
            if(!await _internet.CheckForInternetConnectionAsync() || _session.SessionInformation.Offline)
            {
                //store records to local database
                await StoreRecords(apps);
                _logger.LogInfo($"Finished collecting apps: {_time.Now} with status: {CollectStatus}");
                return;
            }

            //get sync client
            var syncClient = _syncFactory.GetClient();

            //create user session dto
            var dto = _session.SessionInformation.Adapt<UserSessionDto>();
            dto.Applications = apps.Adapt<IEnumerable<ApplicationDto>>();

            //sync fetched apps with server if possible
            if (!await syncClient.Sync(dto))
            {
                //store records to local database if sync with server failes
                await StoreRecords(apps);
            }

            _logger.LogInfo($"Finished collecting apps: {_time.Now} with status: {CollectStatus}");
        }

        /// <summary>
        /// Store applications records to local database (we are working here with max 200 records)
        /// </summary>
        /// <param name="apps"></param>
        /// <returns></returns>
        private async Task StoreRecords(List<ApplicationInformation> fetchedApps)
        {
            try
            {
                //zero apps, exit
                if (fetchedApps.Count == 0)
                    return;

                //Get stored applications in this session
                var applications = await _unitOfWork.Application.GetEntitesForSession(_session.SessionInformation.SessionId);

                //no application records in database for this session insert all application
                if (applications.Count == 0)
                {
                    //map to entity objects
                    var newApps = fetchedApps.Adapt<List<ApplicationEntity>>();
                    
                    //Insert to database
                    await InsertLogic(newApps);

                    //exit
                    return;
                }

                //update old and get new opened applications
                var newApplications = ModifyOldAndGetNewApps(fetchedApps, applications, out bool databaseEntitesModified);

                //no modification and no new apps
                if (newApplications.Count == 0 && !databaseEntitesModified)
                    return;

                //some entites from database modified and zero new application fetched
                if (databaseEntitesModified && newApplications.Count == 0)
                {
                    _logger.LogInfo("Application entites modified");

                    //save changes for modifed entites
                    await _unitOfWork.CommitAsync();

                    //exit
                    return;
                }

                //save everything
                await InsertLogic(newApplications);
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
        private List<ApplicationEntity> ModifyOldAndGetNewApps(List<ApplicationInformation> fetchedApps, List<ApplicationEntity> storedApps, out bool modificationOnDatabaseEntites)
        {
            //no modification yet
            modificationOnDatabaseEntites = false;

            //prepare empty collection for new apps
            List<ApplicationEntity> newApps = new List<ApplicationEntity>();

            //iteratre throught fetched apps
            foreach (var fetchedApp in fetchedApps)
            {
                //flag, for new app recognation
                bool newApp = true;

                //iterate throught stored apps
                foreach(var storedApp in storedApps)
                {
                    //we are talking about same app if it has same applicaiton name and same starttime
                    if(storedApp.ApplicationName == fetchedApp.ApplicationName && storedApp.StartTime == fetchedApp.StartTime)
                    {
                        //change last active time of app
                        storedApp.Saved = fetchedApp.FetchTime;

                        //set flag
                        newApp = false;

                        //set modification flag
                        modificationOnDatabaseEntites = true;

                        //exit inner for loop
                        break;
                    }
                }

                //if new app, add to collection
                if (newApp)
                    newApps.Add(fetchedApp.Adapt<ApplicationEntity>());
            }

            //return new opened apps
            return newApps;
        }

        /// <summary>
        /// Insert records to dabase
        /// </summary>
        /// <param name="newEntities"></param>
        /// <returns></returns>
        protected async Task InsertLogic(List<ApplicationEntity> newEntities)
        {
            _unitOfWork.Application.AddRange(newEntities);
            await _unitOfWork.CommitAsync();
        }
    }
}
