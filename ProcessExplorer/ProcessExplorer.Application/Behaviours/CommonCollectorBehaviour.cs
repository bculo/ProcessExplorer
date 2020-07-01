using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Application.Behaviours
{
    public abstract class CommonCollectorBehaviour<T>
    {
        protected readonly ISynchronizationClientFactory _syncFactory;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IInternet _internet;
        protected readonly ILoggerWrapper _logger;
        protected readonly IDateTime _time;
        protected readonly ISessionService _session;

        protected CommonCollectorBehaviour(
            ISynchronizationClientFactory syncFactory,
            IUnitOfWork unitOfWork,
            IInternet internet,
            ISessionService session,
            ILoggerWrapper wrapper,
            IDateTime dateTime)
        {
            _syncFactory = syncFactory;
            _unitOfWork = unitOfWork;
            _internet = internet;
            _session = session;
            _logger = wrapper;
            _time = dateTime;
        }
    }
}
