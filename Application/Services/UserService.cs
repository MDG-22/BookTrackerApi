using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public UserDto GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                throw new NotFoundException($"User with id {id} not found", "USER_NOT_FOUND");

            return UserDto.ToDto(user);
        }

        public UserDto CreateUser(UserCreateRequest dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username))
                throw new AppValidationException("Username cannot be empty", "USERNAME_REQUIRED");

            var user = dto.ToEntity();
            var newUser = _userRepository.Create(user);
            return UserDto.ToDto(newUser);
        }

        public UserDto UpdateUser(int id, UserUpdateRequest dto)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                throw new NotFoundException($"User with id {id} not found", "USER_NOT_FOUND");

            if (!string.IsNullOrWhiteSpace(dto.Username))
                user.Username = dto.Username;

            if (!string.IsNullOrWhiteSpace(dto.AvatarUrl))
                user.AvatarUrl = dto.AvatarUrl;
            // if (dto.AvatarUrl != null)
            //     user.AvatarUrl = dto.AvatarUrl;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                user.Description = dto.Description;

            var updatedUser = _userRepository.Update(user);
            return UserDto.ToDto(updatedUser);
        }


        public void DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                throw new NotFoundException($"User with id {id} not found", "USER_NOT_FOUND");

            _userRepository.Delete(id);
        }

        public IEnumerable<UserAdminDto> GetUsersAdmin()
        {
            return _userRepository.GetAll()
                .Select(u => new UserAdminDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role
                })
                .ToList();
        }

        public void UpdateUserRole(int userId, UserRole newRole)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            user.Role = newRole;
            _userRepository.Update(user);
        }
    }
}
