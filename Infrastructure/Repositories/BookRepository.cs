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


        public Book GetBookById(int id)
        {
            return _dbSet
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _dbSet
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .ToList();
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

        public IEnumerable<Book> GetByGenre(int genreId)
        {
            return _dbSet
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .Where(b => b.Genres.Any(g => g.Id == genreId))
                .ToList();
        }
    }
}