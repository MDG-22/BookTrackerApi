using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static List<User> userRepository = new List<User>()
        {
            new User
            {
                Id = 1,
                Username = "johndoe",
                Email = "johndoe@example.com",
                AvatarUrl = "https://example.com/avatar1.png",
                Description = "Primer usuario de prueba",
                Role = UserRole.Admin,
                CreatedAt = DateTime.Now.AddDays(-10)
            },
            new User
            {
                Id = 2,
                Username = "janedoe",
                Email = "janedoe@example.com",
                AvatarUrl = "https://example.com/avatar2.png",
                Description = "Segundo usuario de prueba",
                Role = UserRole.Reader,
                CreatedAt = DateTime.Now.AddDays(-5)
            }
        };

        public IEnumerable<User> GetAll()
        {
            return userRepository;
        }

        public User? GetbyId(int id)
        {
            var user = userRepository.FirstOrDefault(u => u.Id == id);

            return user;
        }

        public User Create(User user)
        {
            user.Id = userRepository.Max(u => u.Id) + 1;

            userRepository.Add(user);

            return user;
        }
        
        public User? Update(User newData)
        {
            var user = userRepository.FirstOrDefault(u => u.Id == newData.Id);

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
            var user = userRepository.FirstOrDefault(u => u.Id == id);

            userRepository.Remove(user);
        }
    }
}
