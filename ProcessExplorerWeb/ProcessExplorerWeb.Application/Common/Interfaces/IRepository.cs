using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Application.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(object id);
        ValueTask<TEntity> GetAsync(object id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
