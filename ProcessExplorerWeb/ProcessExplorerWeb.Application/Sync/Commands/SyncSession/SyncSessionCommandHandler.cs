using MediatR;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
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
            throw new NotImplementedException();
        }
    }
}
