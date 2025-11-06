using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LectureRepository : Repository<Lecture>, ILectureRepository
    {
        private readonly ApplicationContext _applicationContext;

        public LectureRepository(ApplicationContext db) : base(db)
        {
            _applicationContext = db;
        }

        public IEnumerable<Lecture> FilterByStatus(LectureStatus? status, int userId)
        {
            var lectures = _applicationContext.Lectures
                .Include(l => l.Book)
                .Include(l => l.User)
                .AsQueryable();

            lectures = lectures.Where(l => l.UserId == userId);

            if (status.HasValue)
                lectures = lectures.Where(l => l.Status == status.Value);

            return lectures.ToList();
        }

    }
}