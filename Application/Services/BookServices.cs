using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
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
            return books.Select(BookDto.ToDto);
        }

        public BookDto GetBookbyId(int id)
        {
            var book = _bookRepository.GetbyId(id);
            if (book == null)
                throw new NotFoundException($"Book with id {id} not found", "BOOK_NOT_FOUND");

            return BookDto.ToDto(book);
        }

        public BookDto CreateBook(BookCreateAndUpdateRequest bookRequest)
        {
            if (string.IsNullOrWhiteSpace(bookRequest.Title))
                throw new AppValidationException("Book title cannot be empty", "BOOK_TITLE_REQUIRED");

            var newBook = new Book
            {
                Title = bookRequest.Title,
                Pages = bookRequest.Pages,
                Summary = bookRequest.Summary,
                CoverUrl = bookRequest.CoverUrl
            };

            var created = _bookRepository.Create(newBook);
            return BookDto.ToDto(created);
        }

        public BookDto UpdateBook(int id, BookCreateAndUpdateRequest bookRequest)
        {
            var book = _bookRepository.GetbyId(id);
            if (book == null)
                throw new NotFoundException($"Book with id {id} not found", "BOOK_NOT_FOUND");

            if (!string.IsNullOrWhiteSpace(bookRequest.Title))
                book.Title = bookRequest.Title;

            book.Pages = bookRequest.Pages;
            book.Summary = bookRequest.Summary;
            book.CoverUrl = bookRequest.CoverUrl;

            var updated = _bookRepository.Update(book);
            return BookDto.ToDto(updated);
        }

        public void DeleteBook(int id)
        {
            var book = _bookRepository.GetbyId(id);
            if (book == null)
                throw new NotFoundException($"Book with id {id} not found", "BOOK_NOT_FOUND");

            _bookRepository.Delete(id);
        }

        public IEnumerable<BookDto> SearchByTitle(string titleForSearch)
        {
            var books = _bookRepository.SearchByTitle(titleForSearch);
            return books.Select(BookDto.ToDto);
        }
    }
}
