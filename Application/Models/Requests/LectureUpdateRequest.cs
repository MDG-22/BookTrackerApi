using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class LectureUpdateRequest
    {
        public int? Rating { get; set; }
        public int? PageCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        [Required]
        public LectureStatus Status { get; set; }
    }
}
