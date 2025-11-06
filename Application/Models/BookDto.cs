using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Pages { get; set; }
        public string Summary { get; set; } = string.Empty;
        public string CoverUrl { get; set; } = string.Empty;
        
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }

        // Lista de id de generos
        public List<int> GenreIds { get; set; } = new List<int>();

        // Lista de nombres de los generos
        public List<string> Genres { get; set; } = new List<string>();

        public static BookDto ToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Pages = book.Pages,
                Summary = book.Summary,
                CoverUrl = book.CoverUrl,
                AuthorId = book.AuthorId,
                AuthorName = book.Author?.Name,
                GenreIds = book.Genres.Select(g => g.Id).ToList(),
                Genres = book.Genres.Select(g => g.GenreName).ToList()
            };
        }

    }
}