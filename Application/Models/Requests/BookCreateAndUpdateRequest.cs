using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BookCreateAndUpdateRequest
    {
        [Required]
        [StringLength(200)]
        // [StringLength(50)]
        public string Title { get; set; }
        public int Pages { get; set; }
        [Required]
        [StringLength(200)]
        // [StringLength(1000)]
        public string Summary { get; set; }
        [Url]
        public string? CoverUrl { get; set; }
         
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public List<int> GenreIds { get; set; } = new List<int>();
    }
}