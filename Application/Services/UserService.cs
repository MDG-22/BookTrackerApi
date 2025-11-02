using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public IEnumerable<UserDto> GetUsers()
        {
            var users = _userRepository.GetAll();

            return users.Select(UserDto.ToDto);
        }
        public UserDto? GetUserById(int id)
        {
            var user = _userRepository.GetbyId(id);

            return UserDto.ToDto(user);
        }
        public UserDto CreateUser(UserDto dto)
        {
            var newUser = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                AvatarUrl = dto.AvatarUrl,
                Description = dto.Description,
                Role = dto.Role,
                CreatedAt = dto.CreatedAt
            };

            _userRepository.Create(newUser);

            return UserDto.ToDto(newUser);
        }
        public UserDto? UpdateUser(int id, UserUpdateRequest dto)
        {
            var user = _userRepository.GetbyId(id);

            if (!string.IsNullOrWhiteSpace(dto.Username))
            {
                user.Username = dto.Username;
            }

            if (!string.IsNullOrWhiteSpace(dto.AvatarUrl))
            {
                user.AvatarUrl= dto.AvatarUrl;
            }

            if (!string.IsNullOrWhiteSpace(dto.Description))
            {
                user.Description = dto.Description;
            }

            // var updatedUser = _userRepository.Update(user);

            return UserDto.ToDto(user);
        }
        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }
    }
}
