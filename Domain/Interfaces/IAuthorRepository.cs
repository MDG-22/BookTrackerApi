using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    // Repositorio SOLO LECTURA (no hereda de IRepository<T>)
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();
        Author? GetbyId(int id);
    }
}
