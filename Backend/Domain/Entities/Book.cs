using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book
    {
        [Key]
        //asigna la base de datos el valor de id
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int Pages { get; set; }
        public string Summary { get; set; } = string.Empty;
        public string? CoverUrl { get; set; }

        // Autor
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
