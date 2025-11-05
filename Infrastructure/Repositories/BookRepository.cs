using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationContext db) : base(db)
        {
        }

        public IEnumerable<Book> SearchByTitle(string titleForSearch)
        {
            if (string.IsNullOrWhiteSpace(titleForSearch))
            {
                return Enumerable.Empty<Book>();
            }
            return _dbSet
                .Where(b => EF.Functions.Like(b.Title, $"%{titleForSearch}%"))
                .ToList();
        }
    }
}