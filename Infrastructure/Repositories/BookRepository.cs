using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationContext _db;

        public BookRepository(ApplicationContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> GetAll()
        {
            return _db.Books.ToList();
        }

        public Book? GetbyId(int id)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == id);

            return book;
        }

        public Book Create(Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
            return book;
        }

        public Book? Update(Book book)
        {
            var existingBook = _db.Books.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                _db.Books.Update(book);
                _db.SaveChanges();
                return existingBook;
            }
            return null;
        }

        public void Delete(int id)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _db.Books.Remove(book);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Book> SearchByTitle(string titleForSearch)
        {
            if (string.IsNullOrWhiteSpace(titleForSearch))
            {
                return Enumerable.Empty<Book>();
            }
            return _db.Books.Where(b => EF.Functions.Like(b.Title, $"%{titleForSearch}%")).ToList();
        }
    }
}