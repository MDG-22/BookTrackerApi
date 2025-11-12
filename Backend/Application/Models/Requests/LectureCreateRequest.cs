using System;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Requests
{
    public class LectureCreateRequest
    {
        [Required]
        public int BookId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;


        public Lecture ToEntity()
        {
            return new Lecture
            {
                BookId = BookId,
                StartDate = StartDate,
                Status = LectureStatus.PlanToRead
            };
        }
    }
}
