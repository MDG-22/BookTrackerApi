using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static ApplicationContext _db;

        public UserRepository(ApplicationContext db)
        {
            _db = db;
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users.ToList();
        }

        public User? GetbyId(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);

            return user;
        }

        public User Create(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }
        
        public User? Update(User newData)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == newData.Id);

            if (user == null)
            {
                return null;
            }

            user.Username = newData.Username;
            user.Email = newData.Email;
            user.Description = newData.Description;
            user.AvatarUrl = newData.AvatarUrl;
            user.Role = newData.Role;

            return user;
        }

        public void Delete(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);

            _db.Users.Remove(user);
        }
    }
}
