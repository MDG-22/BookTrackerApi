using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
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

            if (!users.Any())
                throw new NotFoundException("No users found in the database.", "NO_USERS_FOUND");

            return users.Select(UserDto.ToDto);
        }

        public UserDto GetUserById(int id)
        {
            var user = _userRepository.GetbyId(id);
            if (user == null)
                throw new NotFoundException($"User with ID {id} not found.", "USER_NOT_FOUND");

            return UserDto.ToDto(user);
        }

        public UserDto CreateUser(UserCreateRequest dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new AppValidationException("Email cannot be empty.", "INVALID_EMAIL");

            if (string.IsNullOrWhiteSpace(dto.Username))
                throw new AppValidationException("Username cannot be empty.", "INVALID_USERNAME");

            var existingUser = _userRepository.GetAll().FirstOrDefault(u => u.Email == dto.Email);
            if (existingUser != null)
                throw new AppValidationException($"Email '{dto.Email}' is already registered.", "EMAIL_ALREADY_EXISTS");

            var newUser = dto.ToEntity();
            _userRepository.Create(newUser);

            return UserDto.ToDto(newUser);
        }

        public UserDto UpdateUser(int id, UserUpdateRequest dto)
        {
            var user = _userRepository.GetbyId(id);
            if (user == null)
                throw new NotFoundException($"User with ID {id} not found.", "USER_NOT_FOUND");

            if (!string.IsNullOrWhiteSpace(dto.Username))
                user.Username = dto.Username;

            if (!string.IsNullOrWhiteSpace(dto.AvatarUrl))
                user.AvatarUrl = dto.AvatarUrl;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                user.Description = dto.Description;

            var updatedUser = _userRepository.Update(user);
            return UserDto.ToDto(updatedUser);
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetbyId(id);
            if (user == null)
                throw new NotFoundException($"User with ID {id} not found.", "USER_NOT_FOUND");

            _userRepository.Delete(id);
        }
    }
}