using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

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

            if (!lectures.Any())
                throw new NotFoundException("No lectures found in the database.", "NO_LECTURES_FOUND");

            return lectures.Select(LectureDto.ToDto);
        }

        public LectureDto GetbyId(int id)
        {
            var lecture = _lectureRepository.GetbyId(id);
            if (lecture == null)
                throw new NotFoundException($"Lecture with ID {id} not found.", "LECTURE_NOT_FOUND");

            return LectureDto.ToDto(lecture);
        }

        public LectureDto Create(LectureDto dto)
        {
            if (dto.Rating is < 0 or > 10)
                throw new AppValidationException("Rating must be between 0 and 10.", "INVALID_RATING");

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

        public LectureDto Update(int id, LectureUpdateRequest dto)
        {
            var lecture = _lectureRepository.GetbyId(id);
            if (lecture == null)
                throw new NotFoundException($"Lecture with ID {id} not found.", "LECTURE_NOT_FOUND");

            if (dto.Rating.HasValue && (dto.Rating < 0 || dto.Rating > 10))
                throw new AppValidationException("Rating must be between 0 and 10.", "INVALID_RATING");

            if (dto.Rating.HasValue)
                lecture.Rating = dto.Rating;

            if (dto.PageCount.HasValue)
                lecture.PageCount = dto.PageCount;

            if (dto.StartDate.HasValue)
                lecture.StartDate = dto.StartDate;

            if (dto.FinishDate.HasValue)
                lecture.FinishDate = dto.FinishDate;

            var updated = _lectureRepository.Update(lecture);
            return LectureDto.ToDto(updated);
        }

        public void Delete(int id)
        {
            var lecture = _lectureRepository.GetbyId(id);
            if (lecture == null)
                throw new NotFoundException($"Lecture with ID {id} not found.", "LECTURE_NOT_FOUND");

            _lectureRepository.Delete(id);
        }
    }
}