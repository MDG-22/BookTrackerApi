using Application.Models;
using Application.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetUsers();
        UserDto? GetUserById(int id);
        UserDto CreateUser(UserCreateRequest dto);
        UserDto? UpdateUser(int id, UserUpdateRequest dto);
        void DeleteUser(int id);
    }
}
