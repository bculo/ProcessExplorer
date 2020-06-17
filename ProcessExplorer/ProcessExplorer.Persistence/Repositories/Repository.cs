using Microsoft.EntityFrameworkCore;
using ProcessExplorer.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProcessExplorer.Persistence.Repositories
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

        public virtual void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public virtual TEntity Get(object id)
        {
            return _entities.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public virtual ValueTask<TEntity> GetAsync(object id)
        {
            return _entities.FindAsync(id);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefaultAsync(predicate);
        }

        public virtual void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }
    }
}
