using Application.Models;
using Application.Models.Requests;
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
        LectureDto Create(LectureDto lecture);
        LectureDto? Update(int id, LectureUpdateRequest lecture);
        void Delete(int id);
    }
}
