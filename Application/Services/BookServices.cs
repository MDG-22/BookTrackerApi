using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;

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
            {
                throw new NotFoundException("BOOK_NOT_FOUND", $"ID_{id}");
            }
            return BookDto.ToDto(book);
        }

        public BookDto CreateBook(BookCreateAndUpdateRequest bookRequest)
        {
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

        public BookDto UpdateBook(int Id, BookCreateAndUpdateRequest bookDto)
        {
            var book = _bookRepository.GetbyId(Id);
            if (book != null)
            {
                book.Title = bookDto.Title;
                book.Pages = bookDto.Pages;
                book.Summary = bookDto.Summary;
                book.CoverUrl = bookDto.CoverUrl;

                var updated =_bookRepository.Update(book);
                return BookDto.ToDto(updated);
            }
            return null;
        }

        public void DeleteBook(int id)
        {

            _bookRepository.Delete(id);
        }

        public IEnumerable<BookDto> SearchByTitle(string titleForSearch)
        {
            var books = _bookRepository.SearchByTitle(titleForSearch);
            return books.Select(BookDto.ToDto);
        }
    }
}
