using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public List<BookDto> Books { get; set; } = new List<BookDto>();

        public static AuthorDto ToDto(Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description,
                Books = author.Books?.Select(BookDto.ToDto).ToList() ?? new List<BookDto>(),
                ImageUrl = author.ImageUrl
            };
        }

    }
}