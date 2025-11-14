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
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();
            return books.Select(BookDto.ToDto);

        }

        public BookDto GetBookbyId(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
                throw new NotFoundException($"Book with id {id} not found", "BOOK_NOT_FOUND");

            return BookDto.ToDto(book);
        }

        public BookDto CreateBook(BookCreateAndUpdateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new AppValidationException("Book title cannot be empty", "BOOK_TITLE_REQUIRED");

            var author = _authorRepository.GetById(request.AuthorId);
            if (author == null)
                throw new NotFoundException($"Author with id {request.AuthorId} not found", "AUTHOR_NOT_FOUND");

            var genres = _genreRepository.GetAll()
        .Where(g => request.GenreIds.Contains(g.Id))
        .ToList();

            var newBook = new Book
            {
                Title = request.Title,
                Pages = request.Pages,
                Summary = request.Summary,
                CoverUrl = request.CoverUrl,
                AuthorId = request.AuthorId,
                Author = author,
                Genres = genres
            };

            var created = _bookRepository.Create(newBook);

            return BookDto.ToDto(created);
        }

public BookDto UpdateBook(int id, BookCreateAndUpdateRequest request)
{
 
    var book = _bookRepository.GetBookById(id); 
    if (book == null)
        throw new NotFoundException($"Book with id {id} not found", "BOOK_NOT_FOUND");

    
    if (!string.IsNullOrWhiteSpace(request.Title))
        book.Title = request.Title;

    book.Pages   = request.Pages;
    book.Summary = request.Summary;
    book.CoverUrl = request.CoverUrl;

    
    var author = _authorRepository.GetById(request.AuthorId);
    if (author == null)
        throw new NotFoundException($"Author with id {request.AuthorId} not found", "AUTHOR_NOT_FOUND");

    book.AuthorId = request.AuthorId;
    book.Author   = author;

    
    var genres = _genreRepository.GetAll()
        .Where(g => request.GenreIds.Contains(g.Id))
        .ToList();

    
    book.Genres.Clear();           
    foreach (var g in genres)
    {
        book.Genres.Add(g);       
    }

    var updated = _bookRepository.Update(book);

    return BookDto.ToDto(updated);
}



        public void DeleteBook(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
                throw new NotFoundException($"Book with id {id} not found", "BOOK_NOT_FOUND");

            _bookRepository.Delete(id);
        }

        public IEnumerable<BookDto> SearchByTitle(string titleForSearch)
        {
            var books = _bookRepository.SearchByTitle(titleForSearch);
            return books.Select(BookDto.ToDto);
        }

        public IEnumerable<BookDto> GetByGenre(int genreId)
        {
            return _bookRepository.GetByGenre(genreId)
                .Select(BookDto.ToDto)
                .ToList();
        }
    }
}
