using Application.Models;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDto> GetAll();
        AuthorDto GetbyId(int id);
    }
}
