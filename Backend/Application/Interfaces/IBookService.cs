using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookService
    {
        IEnumerable<BookDto> GetAllBooks();
        BookDto GetBookbyId(int id);
        BookDto CreateBook(BookCreateAndUpdateRequest book);
        BookDto UpdateBook(int id, BookCreateAndUpdateRequest bookUpdateRequest);
        void DeleteBook(int id);
        IEnumerable<BookDto> SearchByTitle(string titleForSearch);
        IEnumerable<BookDto> GetByGenre(int genreId);
    }
}
