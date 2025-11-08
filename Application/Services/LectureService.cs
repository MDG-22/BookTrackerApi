using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class LectureService : ILectureService
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly IBookRepository _bookRepository;

        public LectureService(ILectureRepository lectureRepository, IBookRepository bookRepository)
        {
            _lectureRepository = lectureRepository;
            _bookRepository = bookRepository;
        }

        public IEnumerable<LectureDto> GetAll()
        {
            return _lectureRepository.GetAll().Select(LectureDto.ToDto);
        }

        public LectureDto? GetbyId(int id)
        {
            var lecture = _lectureRepository.GetById(id);
            if (lecture == null)
                throw new NotFoundException($"Lecture with id {id} not found", "LECTURE_NOT_FOUND");

            return LectureDto.ToDto(lecture);
        }

        public LectureDto CreateLecture(int userId, LectureCreateRequest request)
        {
            var book = _bookRepository.GetById(request.BookId);
            if (book == null)
                throw new NotFoundException($"Book with id {request.BookId} not found", "BOOK_NOT_FOUND");

            var lecture = request.ToEntity();
            lecture.UserId = userId;
            lecture.BookTitle = book.Title;

            _lectureRepository.Create(lecture);
            return LectureDto.ToDto(lecture);
        }

        public LectureDto? Update(int id, LectureUpdateRequest dto)
        {
            var lecture = _lectureRepository.GetById(id);
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
            var lecture = _lectureRepository.GetById(id);
            if (lecture == null)
                throw new NotFoundException($"Lecture with id {id} not found", "LECTURE_NOT_FOUND");

            _lectureRepository.Delete(id);
        }

        public IEnumerable<LectureDto> FilterByStatus(LectureStatus? status, int userId)
        {
            return _lectureRepository.FilterByStatus(status, userId).Select(LectureDto.ToDto);
        }
    }
}
