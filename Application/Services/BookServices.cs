using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            var books = _bookRepository.GetAll();

            if (!books.Any())
                throw new NotFoundException("No books found in the database.", "NO_BOOKS_FOUND");

            return books.Select(BookDto.ToDto);
        }

        public BookDto GetBookbyId(int id)
        {
            var book = _bookRepository.GetbyId(id);

            if (book == null)
                throw new NotFoundException($"Book with ID {id} not found.", "BOOK_NOT_FOUND");

            return BookDto.ToDto(book);
        }

        public BookDto CreateBook(BookCreateAndUpdateRequest bookRequest)
        {
            if (string.IsNullOrWhiteSpace(bookRequest.Title))
                throw new AppValidationException("Book title cannot be empty.", "INVALID_TITLE");

            var newBook = new Book
            {
                Title = bookRequest.Title,
                Pages = bookRequest.Pages,
                Summary = bookRequest.Summary,
                CoverUrl = bookRequest.CoverUrl
            };

            _bookRepository.Create(newBook);
            return BookDto.ToDto(newBook);
        }

        public BookDto UpdateBook(int id, BookCreateAndUpdateRequest bookDto)
        {
            var book = _bookRepository.GetbyId(id);
            if (book == null)
                throw new NotFoundException($"Book with ID {id} not found.", "BOOK_NOT_FOUND");

            if (string.IsNullOrWhiteSpace(bookDto.Title))
                throw new AppValidationException("Book title cannot be empty.", "INVALID_TITLE");

            book.Title = bookDto.Title;
            book.Pages = bookDto.Pages;
            book.Summary = bookDto.Summary;
            book.CoverUrl = bookDto.CoverUrl;

            var updated = _bookRepository.Update(book);
            return BookDto.ToDto(updated);
        }

        public void DeleteBook(int id)
        {
            var book = _bookRepository.GetbyId(id);
            if (book == null)
                throw new NotFoundException($"Book with ID {id} not found.", "BOOK_NOT_FOUND");

            _bookRepository.Delete(id);
        }

        public IEnumerable<BookDto> SearchByTitle(string titleForSearch)
        {
            if (string.IsNullOrWhiteSpace(titleForSearch))
                throw new AppValidationException("Search title cannot be empty.", "EMPTY_SEARCH");

            var books = _bookRepository.SearchByTitle(titleForSearch);
            if (!books.Any())
                throw new NotFoundException($"No books found with title '{titleForSearch}'.", "NO_RESULTS");

            return books.Select(BookDto.ToDto);
        }
    }
}