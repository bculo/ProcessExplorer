using Mapster;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Sync.SharedDtos;
using ProcessExplorerWeb.Core.Entities;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Sync.SharedLogic
{
    public abstract class SyncRootHandler
    {
        protected readonly IUnitOfWork _work;
        protected readonly ICurrentUserService _currentUser;
        protected readonly IDateTime _dateTime;

        protected SyncRootHandler(IUnitOfWork work,
            ICurrentUserService currentUser,
            IDateTime dateTime)
        {
            _work = work;
            _currentUser = currentUser;
            _dateTime = dateTime;
        }

        /// <summary>
        /// Create empty list for given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected List<T> EmptyList<T>() => new List<T>();

        /// <summary>
        /// Set UserId and inserted time on entity model
        /// </summary>
        /// <param name="entity"></param>
        protected void FillSessionEntity(ProcessExplorerUserSession entity)
        {
            entity.ExplorerUserId = _currentUser.UserId;
            entity.Inserted = _dateTime.Now;
        }

        /// <summary>
        /// Modify old records and create new records
        /// </summary>
        /// <param name="fetchedApps">apps fetched from collector</param>
        /// <param name="storedApps">apps for this session fetched from store</param>
        /// <returns></returns>
        protected List<ApplicationEntity> ModifyOldAndGetNewApps(List<ApplicationInstanceDto> fetchedApps,
            ICollection<ApplicationEntity> storedApps, out bool modificationOnDatabaseEntites)
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

                //is there any app in datrabase with same name and same start time
                //we are talking about same app if it has same applicaiton name and same starttime
                var foundEntity = storedApps.FirstOrDefault(i => i.Name == fetchedApp.Name && i.Started == fetchedApp.Started);

                if (foundEntity != null)
                {
                    newApp = false;

                    //Modify entity only if Last use is greater than closeD time of found entity
                    if (foundEntity.Closed < fetchedApp.LastUse)
                    {
                        //update entity
                        foundEntity.Closed = fetchedApp.LastUse;

                        //set modification flag to true
                        modificationOnDatabaseEntites = true;
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
        /// Get only processes that are not yet stored in database O(m + n)
        /// </summary>
        /// <param name="fetchedProcesses"></param>
        /// <param name="storedProcesses"></param>
        /// <returns></returns>
        protected List<ProcessEntity> GetNewProcessesToStore(List<ProcessInstanceDto> fetchedProcesses, ICollection<ProcessEntity> storedProcesses)
        {
            //get stored processes names and put them in hashset
            var storedProccessesName = new HashSet<string>(storedProcesses.Select(i => i.ProcessName));

            //get processes that are not in hashset
            var notStoredProcesses = fetchedProcesses.Where(i => !storedProccessesName.Contains(i.Name));

            //map them to process entities
            return notStoredProcesses.Adapt<List<ProcessEntity>>();
        }

        /// <summary>
        /// Add new session to database
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        protected async Task AddNewSession(ProcessExplorerUserSession session)
        {
            //set user id on session instance
            FillSessionEntity(session);


            //add to database
            _work.Session.Add(session);
            await _work.CommitAsync();
        }
    }
}
