using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OnlineBanking.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Delete(T entity);
    }
}