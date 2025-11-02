using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetbyId(int id);
        T Create(T entity);
        T? Update(T entity);
        void Delete(int id);
    }
}
