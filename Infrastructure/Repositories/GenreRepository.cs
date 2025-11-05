using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationContext _db;
        private readonly DbSet<Genre> _dbSet;

        public GenreRepository(ApplicationContext db)
        {
            _db = db;
            _dbSet = _db.Set<Genre>();
        }

        public IEnumerable<Genre> GetAll() => _dbSet.AsNoTracking().ToList();

        public Genre? GetbyId(int id) => _dbSet.AsNoTracking().FirstOrDefault(g => g.Id == id);
    }
}
