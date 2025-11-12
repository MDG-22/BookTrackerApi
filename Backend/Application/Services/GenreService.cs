using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public IEnumerable<GenreDto> GetAll()
        {
            var genres = _genreRepository.GetAll();
            return genres.Select(GenreDto.ToDto);
        }

        public GenreDto? GetbyId(int id)
        {
            var genre = _genreRepository.GetById(id);
            if (genre == null)
            {
                throw new NotFoundException("GENRE_NOT_FOUND", $"ID_{id}");
            }
            return GenreDto.ToDto(genre);
        }

        public GenreDto Create(GenreDto genre)
        {
            var newGenre = new Genre
            {
                Id = genre.Id,
                GenreName = genre.GenreName
            };

            _genreRepository.Create(newGenre);

            return GenreDto.ToDto(newGenre);
        }

        public GenreDto? Update(int id, GenreDto dto)
        {
            var genre = _genreRepository.GetById(id);
            if (genre == null)
            {
                throw new NotFoundException("GENRE_NOT_FOUND", $"ID_{id}");
            }

            genre.GenreName = dto.GenreName;

            var updated = _genreRepository.Update(genre);
            return GenreDto.ToDto(updated);
        }

        public void Delete(int id)
        {
            var genre = _genreRepository.GetById(id);
            if (genre == null)
            {
                throw new NotFoundException("GENRE_NOT_FOUND", $"ID_{id}");
            }

            _genreRepository.Delete(id);
        }
    }
}
