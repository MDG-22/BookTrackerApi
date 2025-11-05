using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

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
        public UserDto CreateUser(UserCreateRequest dto)
        {
            var newUser = dto.ToEntity();

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
