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
        [Authorize(Roles = "SuperAdmin")]
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

      // [HttpPost]
      // public IActionResult CreateUser(UserCreateRequest dto)
      // {
      //     var newUser = _userService.CreateUser(dto);

      //     return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
      // }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateUser(int id, UserUpdateRequest dto)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null) return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            if (userId != id)
                return Forbid("You are not allowed to modify another user's data.");

            var updatedUser = _userService.UpdateUser(id, dto);
            return Ok(updatedUser);
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);

            return NoContent();
        }

        // [HttpDelete("{id}")]
        // [Authorize]
        // public IActionResult DeleteUser(int id)
        // {
        //     var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
        //     if (userIdClaim == null) return Unauthorized();

        //     int userId = int.Parse(userIdClaim.Value);

        //     // Obtener el rol desde el JWT
        //     var roleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        //     var userRole = roleClaim?.Value ?? "User"; // Por default usuario normal

        //     // 1. Si es SuperAdmin → puede borrar a cualquiera
        //     if (userRole == "SuperAdmin")
        //     {
        //         _userService.DeleteUser(id);
        //         return NoContent();
        //     }

        //     // 2. Si NO es SuperAdmin → solo puede borrarse a sí mismo
        //     if (userId != id)
        //         return Forbid("You cannot delete another user's account.");

        //     _userService.DeleteUser(id);
        //     return NoContent();
        // }


        [HttpGet("admin")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetUsersAdmin();
            return Ok(users);
        }

        [HttpPut("{id}/role")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult UpdateUserRole(int id, [FromBody] UserUpdateRoleRequest request)
        {
            _userService.UpdateUserRole(id, request.Role);
            return NoContent();
        }
    }
}
