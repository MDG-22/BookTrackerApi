using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BookCreateAndUpdateRequest
    {
        public string Title { get; set; }
        public int Pages { get; set; }
        public string Summary { get; set; }
        public string? CoverUrl { get; set; }

        public int AuthorId { get; set; }
        public List<int> GenreIds { get; set; } = new List<int>();
    }
}