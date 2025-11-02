using Application.Interfaces;
using Application.Models;
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

        // GET: api/book
        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetBooks()
        {
            var books = _bookService.GetAllBooks();
            return Ok(books);
        }

        // GET: api/book/5
        [HttpGet("{id}")]
        public ActionResult<BookDto> GetBook(int id)
        {
            var book = _bookService.GetBookbyId(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST: api/book
        [HttpPost]
        public ActionResult<BookDto> CreateBook(BookDto request)
        {
            var book = _bookService.CreateBook(request);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // PUT: api/book/5
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookDto book)
        {
            _bookService.UpdateBook(book);
            return NoContent();
        }

        // DELETE: api/book/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
            return NoContent();
        }

        // GET: api/book/search?title=book
        [HttpGet("search")]
        public ActionResult<IEnumerable<BookDto>> SearchBooks(string title)
        {
            var books = _bookService.SearchByTitle(title);
            return Ok(books);
        }
    }

}