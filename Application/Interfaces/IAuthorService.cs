using Application.Models;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDto> GetAll();
        AuthorDto? GetById(int id);
        AuthorDto Create(AuthorDto author);
        AuthorDto? Update(int id, AuthorDto author);
        void Delete(int id);
    }
}
