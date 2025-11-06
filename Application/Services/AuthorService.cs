using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IEnumerable<AuthorDto> GetAll()
        {
            var authors= _authorRepository.GetAll();
            return authors.Select(AuthorDto.ToDto);
        }

        public AuthorDto? GetbyId(int id)
        {
            var author = _authorRepository.GetbyId(id);
            if (author == null)
            {
                throw new NotFoundException("AUTHOR_NOT_FOUND", $"ID_{id}");
            }
            return AuthorDto.ToDto(author);
        }

        public AuthorDto Create(AuthorDto author)
        {
            var newAuthor = new Author
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description
            };

            _authorRepository.Create(newAuthor);

            return AuthorDto.ToDto(newAuthor);
        }

        public AuthorDto? Update(int id, AuthorDto dto)
        {
            var author = _authorRepository.GetbyId(id);
            if (author == null)
            {
                throw new NotFoundException("AUTHOR_NOT_FOUND", $"ID_{id}");
            }

            author.Name = dto.Name;
            author.Description = dto.Description;

            var updated = _authorRepository.Update(author);
            return AuthorDto.ToDto(updated);
        }

        public void Delete(int id)
        {
            var author = _authorRepository.GetbyId(id);
            if (author == null)
            {
                throw new NotFoundException("AUTHOR_NOT_FOUND", $"ID_{id}");
            }

            _authorRepository.Delete(id);
        }
    }
}
