using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }



        public static GenreDto ToDto(Genre genre)
        {
            return new GenreDto
            {
                Id = genre.Id,
                Description = genre.Description
            };
        }

    }
}