using Microsoft.EntityFrameworkCore;
using OnlineBanking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OnlineBanking.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _entities;
        public Repository(DbContext dbContext)
        {
            _entities = dbContext.Set<T>();
        }

        public T GetById(int id)
        {
            return _entities.Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }
        
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }
    }
}