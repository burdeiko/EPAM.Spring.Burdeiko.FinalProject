using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Dal.Interfaces;
using System.Linq.Expressions;
using System.Data.Entity;

namespace SocialNetwork.Dal.Infrastructure
{
    public abstract class Repository<T> : IRepository<T> where T: class
    {
        protected readonly DbContext context;
        public Repository(DbContext context)
        {
            this.context = context;
        }

        public void Create(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().AsEnumerable();
        }
        public T GetByPredicate(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }

        public abstract void Update(T entity);

        public abstract T GetById(int id);
    }
}
