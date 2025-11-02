using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; private set; }


        public static UserDto ToDto(User entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                Username = entity.Username,
                Email = entity.Email,
                AvatarUrl = entity.AvatarUrl,
                Description = entity.Description,
                Role = entity.Role,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
