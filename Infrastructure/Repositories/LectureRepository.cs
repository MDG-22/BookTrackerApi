using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LectureRepository : ILectureRepository
    {
        private readonly ApplicationContext _db;

        public LectureRepository(ApplicationContext db)
        {
            _db = db;
        }

        public IEnumerable<Lecture> GetAll()
        {
            return _db.Lectures.ToList();
        }
        
        public Lecture? GetbyId(int id)
        {
            var lecture = _db.Lectures.FirstOrDefault(l => l.Id == id);

            return lecture;
        }
        
        public Lecture Create(Lecture lecture)
        {
            _db.Lectures.Add(lecture);
            _db.SaveChanges();
            return lecture;
        }
        
        public Lecture? Update(Lecture lecture)
        {
            var updatedLecture = _db.Lectures.FirstOrDefault(l => l.Id == lecture.Id);
            if (updatedLecture == null)
            {

                return null;
            }

            _db.Lectures.Update(lecture);
            _db.SaveChanges();
            return updatedLecture;
        }
        
        public void Delete(int id)
        {
            var lecture = _db.Lectures.FirstOrDefault(l => l.Id == id);

            _db.Lectures.Remove(lecture);
            _db.SaveChanges();
        }

    }
}
