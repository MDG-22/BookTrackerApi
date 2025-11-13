using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _applicationContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationContext context)
        {
            _applicationContext = context;
            _dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual T Create(T entity)
        {
            _dbSet.Add(entity);
            _applicationContext.SaveChanges();

            return entity;
        }

        public virtual T? Update(T entity)
        {
            _dbSet.Update(entity);
            _applicationContext.SaveChanges();

            return entity;
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
                _applicationContext.SaveChanges();
            }
        }
    }
}