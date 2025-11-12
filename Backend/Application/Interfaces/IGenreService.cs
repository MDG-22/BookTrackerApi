using Application.Models;
using Application.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGenreService
    {
        IEnumerable<GenreDto> GetAll();
        GenreDto? GetbyId(int id);
        GenreDto Create(GenreDto genre);
        GenreDto? Update(int id, GenreDto genre);
        void Delete(int id);
    }
}
