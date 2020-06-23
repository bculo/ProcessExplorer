using Mapster;
using MediatR;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncSession
{
    class SyncSessionCommandHandler : IRequestHandler<SyncSessionCommand>
    {
        private readonly IUnitOfWork _work;
        private readonly ICurrentUserService _currentUser;

        public SyncSessionCommandHandler(IUnitOfWork work,
            ICurrentUserService currentUser)
        {
            _work = work;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(SyncSessionCommand request, CancellationToken cancellationToken)
        {
            //get session with given id and belongs to specific user
            var session = await _work.Session.GetSessionWithAppsAndProcesses(request.SessionId, _currentUser.UserId);

            //Session not found, so create it
            if(session == null)
            {
                //Map to entity model
                var entity = request.Adapt<ProcessExplorerUserSession>();

                //set user id on session instance
                SetUserIdOnSession(entity);

                //add to database
                _work.Session.Add(entity);
                await _work.CommitAsync();

                //exit
                return Unit.Value;
            }

            //get new apps for this session and modify old entites if change detected
            //first parameter -> fetched apps
            //second parameter -> application from stored session
            var newApps = ModifyOldAndGetNewApps(request?.Applications ?? EmptyList<SyncSessionApplicationInfoCommand>(), session.Applications, out bool modificationHappend);

            //get processes that are not yet stored in database
            var newProcesses = GetNewProcessesToStore(request?.Processes ?? EmptyList<SyncSessionProcessInfoCommand>(), session.Processes);

            //add apps
            if (newApps.Count > 0)
                _work.Applications.AddRange(newApps);

            //add processes
            if (newProcesses.Count > 0)
                _work.Process.AddRange(newProcesses);

            //save changes
            await _work.CommitAsync();

            //success
            return Unit.Value;
        }

        /// <summary>
        /// Set UserId on entity model for session
        /// </summary>
        /// <param name="entity"></param>
        private void SetUserIdOnSession(ProcessExplorerUserSession entity)
        {
            entity.ExplorerUserId = _currentUser.UserId;
        }

        /// <summary>
        /// Modify old records and create new records
        /// </summary>
        /// <param name="fetchedApps">apps fetched from collector</param>
        /// <param name="storedApps">apps for this session fetched from store</param>
        /// <returns></returns>
        private List<ApplicationEntity> ModifyOldAndGetNewApps(List<SyncSessionApplicationInfoCommand> fetchedApps, 
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
        /// Create empty list for given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> EmptyList<T>() => new List<T>();

        /// <summary>
        /// Get only processes that are not yet stored in database O(m + n)
        /// </summary>
        /// <param name="fetchedProcesses"></param>
        /// <param name="storedProcesses"></param>
        /// <returns></returns>
        private List<ProcessEntity> GetNewProcessesToStore(List<SyncSessionProcessInfoCommand> fetchedProcesses, ICollection<ProcessEntity> storedProcesses)
        {
            //get stored processes names and put them in hashset
            var storedProccessesName = new HashSet<string>(storedProcesses.Select(i => i.ProcessName));

            //get processes that are not in hashset
            var notStoredProcesses = fetchedProcesses.Where(i => !storedProccessesName.Contains(i.Name));

            //map them to process entities
            return notStoredProcesses.Adapt<List<ProcessEntity>>();
        }
    }
}
