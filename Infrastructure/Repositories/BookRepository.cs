using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        // Usamos una lista estática para almacenar los libros en memoria
        private static List<Book> _tempBooks = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", Pages = 100, Summary = "Summary 1" },
            new Book { Id = 2, Title = "Book 2", Author = "Author 2", Pages = 150, Summary = "Summary 2" }
        };

        public IEnumerable<Book> GetAll()
        {
            return _tempBooks;
        }

        public Book? GetbyId(int id)
        {
            return _tempBooks.FirstOrDefault(b => b.Id == id);
        }

        public Book Create(Book book)
        {
            book.Id = new Random().Next(1000, 9999);  // Generamos un ID aleatorio
            _tempBooks.Add(book);
            return book;
        }

        public Book? Update(Book book)
        {
            var existingBook = _tempBooks.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Pages = book.Pages;
                existingBook.Summary = book.Summary;
                existingBook.CoverUrl = book.CoverUrl;
                return existingBook;
            }
            return null;
        }

        public void Delete(int id)
        {
            var book = _tempBooks.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _tempBooks.Remove(book);
            }
        }

        public IEnumerable<Book> SearchByTitle(string titleForSearch)
        {
            return _tempBooks.Where(b => b.Title.Contains(titleForSearch, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}