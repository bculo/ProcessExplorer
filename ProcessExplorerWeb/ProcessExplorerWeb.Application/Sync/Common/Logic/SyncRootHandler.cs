using ProcessExplorerWeb.Application.Common.Interfaces;
using ProcessExplorerWeb.Core.Entities;
using ProcessExplorerWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
