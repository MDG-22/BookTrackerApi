using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class LectureService : ILectureService
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly IBookRepository _bookRepository;
        public LectureService(IBookRepository bookRepository, ILectureRepository lectureRepository)
        {
            _bookRepository = bookRepository;
            _lectureRepository = lectureRepository;
        }

        public IEnumerable<LectureDto> GetAll()
        {
            var lectures = _lectureRepository.GetAll();

            if (!lectures.Any())
                throw new NotFoundException("NO_LECTURES_FOUND");

            return lectures.Select(LectureDto.ToDto);
        }

        public LectureDto? GetbyId(int id)
        {
            var lecture = _lectureRepository.GetbyId(id);

            if (lecture == null)
                throw new NotFoundException($"LECTURE_{id}_NOT_FOUND");

            return LectureDto.ToDto(lecture);
        }

        public Lecture CreateLecture(int userId, LectureCreateRequest request)
        {
            var book = _bookRepository.GetbyId(request.BookId);
            if (book == null)
                throw new NotFoundException($"Book with id {request.BookId} not found", "BOOK_NOT_FOUND");

            var lecture = request.ToEntity();
            lecture.UserId = userId;
            lecture.BookTitle = book.Title;

            _lectureRepository.Create(lecture);
            return lecture;
        }

        public LectureDto? Update(int id, LectureUpdateRequest dto)
        {
            var lecture = _lectureRepository.GetbyId(id);
            if (lecture == null)
                throw new NotFoundException($"Lecture with id {id} not found", "LECTURE_NOT_FOUND");

            if (dto.Rating.HasValue) lecture.Rating = dto.Rating.Value;
            if (dto.PageCount.HasValue) lecture.PageCount = dto.PageCount.Value;
            if (dto.StartDate.HasValue) lecture.StartDate = dto.StartDate.Value;
            if (dto.FinishDate.HasValue) lecture.FinishDate = dto.FinishDate.Value;
            lecture.Status = dto.Status;

            _lectureRepository.Update(lecture);
            return LectureDto.ToDto(lecture);
        }

        public void Delete(int id)
        {
            _lectureRepository.Delete(id);
        }

        public IEnumerable<LectureDto> FilterByStatus(LectureStatus? status, int userId)
        {
            var lectures = _lectureRepository.FilterByStatus(status, userId);

            if (!lectures.Any())
                throw new NotFoundException("NO_LECTURES_FOUND_FOR_FILTER");

            return lectures.Select(LectureDto.ToDto);
        }
    }
}
