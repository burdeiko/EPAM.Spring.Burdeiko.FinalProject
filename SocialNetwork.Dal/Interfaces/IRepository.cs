using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SocialNetwork.Dal.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByPredicate(Expression<Func<T, bool>> predicate);
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
        T GetById(int id);
    }
}
