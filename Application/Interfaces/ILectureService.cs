using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILectureService
    {
        IEnumerable<LectureDto> GetAll();
        LectureDto? GetbyId(int id);
        LectureDto CreateLecture(int userId, LectureCreateRequest request);
        LectureDto? Update(int id, LectureUpdateRequest lecture);
        void Delete(int id);
        IEnumerable<LectureDto> FilterByStatus(LectureStatus? status, int userId);
    }
}
