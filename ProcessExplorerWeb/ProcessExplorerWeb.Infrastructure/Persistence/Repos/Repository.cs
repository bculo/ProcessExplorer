using Microsoft.EntityFrameworkCore;
using ProcessExplorerWeb.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProcessExplorerWeb.Infrastructure.Persistence.Repos
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        /// <summary>
        /// Add single entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        /// <summary>
        /// Add multiple entities
        /// </summary>
        /// <param name="entities"></param>
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public virtual TEntity Get(object id)
        {
            return _entities.Find(id);
        }

        /// <summary>
        /// Get all entites
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _entities.AsEnumerable();
        }


        /// <summary>
        /// Get specific entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ValueTask<TEntity> GetAsync(object id)
        {
            return _entities.FindAsync(id);
        }

        /// <summary>
        /// Get specific entity for given predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Get specific entity for given predicate async
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Remove given entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        /// <summary>
        /// Remove given entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }
    }
}
