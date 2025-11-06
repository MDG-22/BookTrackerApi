using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetBook(int id)
        {
            var book = _bookService.GetBookbyId(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateBook(BookCreateAndUpdateRequest request)
        {
            var book = _bookService.CreateBook(request);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookCreateAndUpdateRequest book)
        {
            _bookService.UpdateBook(id, book);
            var updatedBook = _bookService.GetBookbyId(id);
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult SearchBooks(string title)
        {
            var books = _bookService.SearchByTitle(title);
            return Ok(books);
        }
    }

}