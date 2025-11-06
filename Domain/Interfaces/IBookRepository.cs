using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Book GetBookById(int id);
        IEnumerable<Book> GetAllBooks();

        IEnumerable<Book> SearchByTitle(string titleForSearch);

        IEnumerable<Book> GetByGenre(int genreId);
    }

}
