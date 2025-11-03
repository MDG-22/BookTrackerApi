using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book? GetbyId(int id);
        Book Create(Book book);
        Book? Update(Book book);
        void Delete(int id);
        IEnumerable<Book> SearchByTitle(string titleForSearch);
    }

}
