using System;
using System.Collections.Generic;
using System.Linq;
using FORUM.DAL.Entities;
using FORUM.DAL.EF;
using FORUM.DAL.Interfaces;
using System.Data.Entity;

namespace FORUM.DAL.Repositories.General
{
    public abstract class BaseRepository<T> : IDisposable, IRepository<T> where T : Entity
    {
        protected readonly ForumDbContext _dbContext;
        protected readonly IDbSet<T> _dbSet;

        public BaseRepository(ForumDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual T Get(int id)
        {
            return _dbSet.FirstOrDefault(e => e.Id == id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.OrderBy(e => e.Id).ToList();
        }

        public virtual IEnumerable<T> Find(Func<T, Boolean> predicate)
        {
            return _dbSet.Where(predicate).OrderBy(e => e.Id).ToList();
        }

        public virtual void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            T entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
