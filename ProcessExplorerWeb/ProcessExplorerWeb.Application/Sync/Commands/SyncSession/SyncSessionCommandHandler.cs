using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using ProcessExplorerWeb.Application.Common.Exceptions;
using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Application.Sync.SharedDtos;
using ProcessExplorerWeb.Application.Sync.SharedLogic;
using ProcessExplorerWeb.Core.Entities;
using ProcessExplorerWeb.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Sync.Commands.SyncSession
{
    class SyncSessionCommandHandler : SyncRootHandler, IRequestHandler<SyncSessionCommand>
    {
        private readonly ILogger<SyncSessionCommandHandler> _logger;

        public SyncSessionCommandHandler(IUnitOfWork work,
            ICurrentUserService currentUser,
            IDateTime dateTime,
            ILogger<SyncSessionCommandHandler> logger) : base(work, currentUser, dateTime)
        {
            _logger = logger;
        }

        public async Task<Unit> Handle(SyncSessionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting to handle session {0} for user {1}", request.SessionId, _currentUser.UserId);

            //get session with given id and belongs to specific user
            var session = await _work.Session.GetSessionWithAppsAndProcesses(request.SessionId, _currentUser.UserId);

            //wrong request
            if (session != null && session.ExplorerUserId != _currentUser.UserId)
                throw new ProcessExplorerException("Invalid request");

            //Session not found, so create it
            if (session == null)
            {
                //Map to entity model
                var entity = request.Adapt<ProcessExplorerUserSession>();

                //set user id on session instance
                FillSessionEntity(entity);

                _logger.LogInformation("Session with given id {0} doesnt exists (INSERT...)", request.SessionId);

                //add to database
                _work.Session.Add(entity);
                await _work.CommitAsync();

                _logger.LogInformation("Session with given id {0} doesnt exists (INSERTED)", request.SessionId);

                //exit
                return Unit.Value;
            }

            _logger.LogInformation("Session with given id {0} exists (FILTERING...)", request.SessionId);

            //get new apps for this session and modify old entites if change detected
            //first parameter -> fetched apps
            //second parameter -> application from stored session
            var newApps = ModifyOldAndGetNewApps(request?.Applications ?? EmptyList<ApplicationInstanceDto>(), session.Applications, out bool modificationHappend);

            //get processes that are not yet stored in database
            var newProcesses = GetNewProcessesToStore(request?.Processes ?? EmptyList<ProcessInstanceDto>(), session.Processes);

            _logger.LogInformation("Session with given id {0} exists (UPDATE|INSERT...)", request.SessionId);

            //add apps
            if (newApps.Count > 0)
                _work.Applications.AddRange(newApps);

            //add processes
            if (newProcesses.Count > 0)
                _work.Process.AddRange(newProcesses);

            //save changes
            await _work.CommitAsync();

            _logger.LogInformation("Session with given id {0} exists (UPDATED|INSERTED)", request.SessionId);

            //success
            return Unit.Value;
        }

        /// <summary>
        /// Modify old records and create new records
        /// </summary>
        /// <param name="fetchedApps">apps fetched from collector</param>
        /// <param name="storedApps">apps for this session fetched from store</param>
        /// <returns></returns>
        private List<ApplicationEntity> ModifyOldAndGetNewApps(List<ApplicationInstanceDto> fetchedApps, 
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
    }
}
