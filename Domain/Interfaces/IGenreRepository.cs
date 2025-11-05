using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    // Repositorio SOLO LECTURA (no hereda de IRepository<T>)
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAll();
        Genre? GetbyId(int id);
    }
}
