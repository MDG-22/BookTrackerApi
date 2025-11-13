using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class LectureRepository : Repository<Lecture>, ILectureRepository
    {
        private readonly ApplicationContext _applicationContext;

        public LectureRepository(ApplicationContext context) : base(context)
        {
            _applicationContext = context;
        }

        public override IEnumerable<Lecture> GetAll()
        {
            return _applicationContext.Lectures
                .Include(l => l.Book)
                    .ThenInclude(b => b.Author)
                .Include(l => l.User)
                .ToList();
        }

        public override Lecture? GetById(int id)
        {
            return _applicationContext.Lectures
                .Include(l => l.Book)
                    .ThenInclude(b => b.Author)
                .Include(l => l.User)
                .FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<Lecture> FilterByStatus(LectureStatus? status, int userId)
        {
            var lectures = _applicationContext.Lectures
                .Include(l => l.Book)
                    .ThenInclude(b => b.Author)
                .Include(l => l.User)
                .Where(l => l.UserId == userId);

            if (status.HasValue)
                lectures = lectures.Where(l => l.Status == status.Value);

            return lectures.ToList();
        }
    }
}
