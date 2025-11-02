using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LectureRepository : ILectureRepository
    {
        private static List<Lecture> lectureRepository = new List<Lecture>()
        {
            new Lecture
            {
                Id = 1,
                Rating = 3,
                PageCount = 10,
                StartDate = DateTime.Now
            },
            new Lecture
            {
                Id = 2,
                Rating = 5,
                PageCount = 123
            }
        };

        public IEnumerable<Lecture> GetAll()
        {
            return lectureRepository;
        }
        
        public Lecture? GetbyId(int id)
        {
            return lectureRepository.FirstOrDefault(l => l.Id == id);
        }
        
        public Lecture Create(Lecture lecture)
        {
            lecture.Id = lectureRepository.Max(l => l.Id) + 1;

            lectureRepository.Add(lecture);

            return lecture;
        }
        
        public Lecture? Update(Lecture lecture)
        {
            var updatedLecture = lectureRepository.FirstOrDefault(l => l.Id == lecture.Id);

            if (updatedLecture == null)
            {
                return null;
            }

            updatedLecture.Rating = lecture.Rating;
            updatedLecture.PageCount = lecture.PageCount;
            updatedLecture.StartDate = lecture.StartDate;
            updatedLecture.FinishDate = lecture.FinishDate;

            return updatedLecture;
        }
        
        public void Delete(int id)
        {
            var lecture = lectureRepository.FirstOrDefault(l => l.Id == id);

            lectureRepository.Remove(lecture);
        }

    }
}
