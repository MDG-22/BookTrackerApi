using Application.Interfaces;
using Application.Models;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repo;

        public GenreService(IGenreRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<GenreDto> GetAll()
        {
            var genres = _repo.GetAll();
            return genres.Select(GenreDto.ToDto);
        }

        public GenreDto? GetbyId(int id)
        {
            var genre = _repo.GetbyId(id);
            return genre == null ? null : GenreDto.ToDto(genre);
        }
    }
}
