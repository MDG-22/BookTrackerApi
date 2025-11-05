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

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T? GetbyId(int id)
        {
            return _dbSet.Find(id);
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            _applicationContext.SaveChanges();

            return entity;
        }

        public T? Update(T entity)
        {
            _dbSet.Update(entity);
            _applicationContext.SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            var entity = GetbyId(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
                _applicationContext.SaveChanges();
            }
        }
    }
}