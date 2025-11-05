using Domain.Entities;
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

        public static LectureDto ToDto(Lecture entity)
        {
            return new LectureDto
            {
                Id = entity.Id,
                Rating = entity.Rating,
                PageCount = entity.PageCount,
                StartDate = entity.StartDate,
                FinishDate = entity.FinishDate
            };
        }
    }
}
