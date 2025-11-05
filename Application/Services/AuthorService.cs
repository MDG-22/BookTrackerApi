using Application.Interfaces;
using Application.Models;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repo;

        public AuthorService(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<AuthorDto> GetAll()
        {
            var authors = _repo.GetAll();
            return authors.Select(AuthorDto.ToDto);
        }

        public AuthorDto? GetbyId(int id)
        {
            var author = _repo.GetbyId(id);
            return author == null ? null : AuthorDto.ToDto(author);
        }
    }
}
