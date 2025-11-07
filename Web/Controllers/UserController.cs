using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser(UserCreateRequest dto)
        {
            var newUser = _userService.CreateUser(dto);

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserUpdateRequest dto)
        {
            var user = _userService.GetUserById(id);

            var updatedUser = _userService.UpdateUser(id, dto);

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);

            return NoContent();
        }

        [HttpGet("admin")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetUsersAdmin();
            return Ok(users);
        }

        [HttpPut("{id}/role")]
        public IActionResult UpdateUserRole(int id, [FromBody] UserUpdateRoleRequest request)
        {
            _userService.UpdateUserRole(id, request.Role);
            return NoContent();
        }
    }
}
