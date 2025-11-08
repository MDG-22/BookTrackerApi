using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class UserCreateRequest
    {
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(120)]
        public string Email { get; set; }
        [Required]
        [StringLength(256)]
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
    

    public User ToEntity()
        {
            return new User
            {
                Id = this.Id,
                Username = this.Username,
                Email = this.Email,
                Password = this.Password,
                Role = UserRole.Reader,
                CreatedAt = this.CreatedAt,
                AvatarUrl = string.Empty,
                Description = string.Empty
            };
        }
    }
}
