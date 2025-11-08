using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class LectureDto
    {
        public int Id { get; set; }
        public int? Rating { get; set; }
        public int? PageCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public LectureStatus Status { get; set; }

        public int UserId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;

        public static LectureDto ToDto(Lecture entity)
        {
            return new LectureDto
            {
                Id = entity.Id,
                Rating = entity.Rating,
                PageCount = entity.PageCount,
                StartDate = entity.StartDate,
                FinishDate = entity.FinishDate,
                BookTitle = entity.Book?.Title,
                UserId = entity.UserId,
                Status = entity.Status,
                BookId = entity.BookId
            };
        }
    }
}
