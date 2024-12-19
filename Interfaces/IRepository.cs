using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace asbEvent.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        // IEnumerable<T> Find(Expression<Func<T, bool>>? predicate);
        T GetById(long id);
        T Add(T entity);
        // void AddRange(IEnumerable<T> entities);
        // void Remove(T entity);
        // void RemoveRange(IEnumerable<T> entities);
    }
}