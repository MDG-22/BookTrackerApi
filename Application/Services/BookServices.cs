using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        // Usamos una lista estática para almacenar libros en memoria temporalmente
        private static List<Book> _tempBooks = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", Pages = 100, Summary = "Summary 1" },
            new Book { Id = 2, Title = "Book 2", Author = "Author 2", Pages = 150, Summary = "Summary 2" }
        };

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            // Aquí decidimos si usamos el repositorio o los datos temporales
            // Para pruebas, puedes retornar los libros temporales
            return _tempBooks.Select(BookDto.ToDto);
            // Para la versión normal, usa el repositorio:
            // var books = _bookRepository.GetAll();
            // return books.Select(BookDto.ToDto);
        }

        public BookDto GetBookbyId(int id)
        {
            var book = _tempBooks.FirstOrDefault(b => b.Id == id);
            // Para pruebas:
            if (book != null)
            {
                return BookDto.ToDto(book);
            }
            // Para la versión normal:
            // var book = _bookRepository.GetbyId(id);
            // return BookDto.ToDto(book);
            return null; // Si no se encuentra, puedes retornar null o lanzar una excepción.
        }

        public BookDto CreateBook(BookDto bookRequest)
        {
            // Crear un libro en memoria
            var newBook = new Book
            {
                Id = new Random().Next(1000, 9999), // Generamos un id aleatorio
                Title = bookRequest.Title,
                Author = bookRequest.Author,
                Pages = bookRequest.Pages,
                Summary = bookRequest.Summary,
                CoverUrl = bookRequest.CoverUrl
            };

            // Agregarlo a la lista de libros en memoria
            _tempBooks.Add(newBook);

            return BookDto.ToDto(newBook);
            // Si quieres usar el repositorio en su lugar, puedes hacer algo como:
            // _bookRepository.Add(newBook);
            // return BookDto.ToDto(newBook);
        }

        public BookDto UpdateBook(BookDto bookDto)
        {
            var book = _tempBooks.FirstOrDefault(b => b.Id == bookDto.Id);
            if (book != null)
            {
                book.Title = bookDto.Title;
                book.Author = bookDto.Author;
                book.Pages = bookDto.Pages;
                book.Summary = bookDto.Summary;
                book.CoverUrl = bookDto.CoverUrl;

                return BookDto.ToDto(book);
            }
            return null; // Puedes manejar esto como prefieras, por ejemplo, lanzando una excepción si no se encuentra.
        }

        public void DeleteBook(int id)
        {
            var book = _tempBooks.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _tempBooks.Remove(book);
            }
            // Si quieres interactuar con la base de datos:
            // _bookRepository.Delete(id);
        }

        public IEnumerable<BookDto> SearchByTitle(string titleForSearch)
        {
            var books = _tempBooks.Where(b => b.Title.Contains(titleForSearch, StringComparison.OrdinalIgnoreCase));
            return books.Select(BookDto.ToDto);
            // Si quieres usar el repositorio:
            // var books = _bookRepository.SearchByTitle(titleForSearch);
            // return books.Select(BookDto.ToDto);
        }
    }
}
