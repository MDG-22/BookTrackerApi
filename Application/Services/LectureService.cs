using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LectureService : ILectureService
    {
        private readonly ILectureRepository _lectureRepository;

        public LectureService(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
        }

        public IEnumerable<LectureDto> GetAll()
        {
            var lectures = _lectureRepository.GetAll();

            return lectures.Select(LectureDto.ToDto);
        }

        public LectureDto? GetbyId(int id)
        {
            var lecture = _lectureRepository.GetbyId(id);

            return LectureDto.ToDto(lecture);
        }
        
        public LectureDto Create(LectureDto dto)
        {
            var newLecture = new Lecture
            {
                Rating = dto.Rating,
                PageCount = dto.PageCount,
                StartDate = dto.StartDate,
                FinishDate = dto.FinishDate
            };

            _lectureRepository.Create(newLecture);

            return LectureDto.ToDto(newLecture);
        }
        
        public LectureDto? Update(int id, LectureUpdateRequest dto)
        {
            var lecture = _lectureRepository.GetbyId(id);

            if (dto.Rating.HasValue)
                lecture.Rating = dto.Rating;

            if (dto.PageCount.HasValue)
                lecture.PageCount = dto.PageCount;

            if (dto.StartDate.HasValue)
                lecture.StartDate = dto.StartDate;

            if (dto.FinishDate.HasValue)
                lecture.FinishDate = dto.FinishDate;

            //var updatedLecture = _lectureRepository.Update(lecture);

            return LectureDto.ToDto(lecture);
        }
        
        public void Delete(int id)
        {
            _lectureRepository.Delete(id);
        }
    }
}
