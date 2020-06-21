using Mapster;
using ProcessExplorer.Application.Common.Interfaces;
using ProcessExplorer.Application.Common.Models;
using ProcessExplorer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public class ApplicationCollectorBehaviour : IApplicationCollectorBehaviour
    {
        private readonly IApplicationCollectorFactory _appFactory;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInternet _internet;
        private readonly ISessionService _session;

        public ApplicationCollectorBehaviour(IApplicationCollectorFactory appFactory,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IInternet internet,
            ISessionService session)
        {
            _appFactory = appFactory;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _internet = internet;
            _session = session;
        }

        public async Task Collect()
        {
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

            //TODO: Push to backend
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
            var applications = await _unitOfWork.Application.GetEntitesForSession(_session.SessionInformation.SessionId);

            //no records for this session so enter everything
            if(applications.Count == 0)
            {
                var newApps = fetchedApps.Adapt<List<ApplicationEntity>>();
                _unitOfWork.Application.BulkAdd(newApps);
                await _unitOfWork.CommitAsync();
                return;
            }

            //update old and add new apps
            var (oldApps, newApplications) = GetModifiedAndNewApps(fetchedApps, applications);
            _unitOfWork.Application.BulkAdd(newApplications);
            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Modify old records and create new records
        /// </summary>
        /// <param name="fetchedApps">apps fetched from collector</param>
        /// <param name="storedApps">apps for this session fetched from store</param>
        /// <returns></returns>
        private (List<ApplicationEntity> oldAps, List<ApplicationEntity> newApps) GetModifiedAndNewApps(List<ApplicationInformation> fetchedApps, List<ApplicationEntity> storedApps)
        {
            List<ApplicationEntity> oldApps = new List<ApplicationEntity>();
            List<ApplicationEntity> newApps = new List<ApplicationEntity>();
            Guid sessionId = _session.SessionInformation.SessionId;

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
                        oldApps.Add(storedApp);
                        newApp = false;
                        break;
                    }
                }

                // new app
                if (newApp)
                    newApps.Add(fetchedApp.Adapt<ApplicationEntity>());
            }

            return (oldApps, newApps);
        }
    }
}
