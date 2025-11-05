using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;

        public AuthController(IUserRepository userRepository, IJwtService jwtService, IUserService userService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticationRequest request)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var token = _jwtService.GenerateToken(user);

            return Ok(new AuthResponse
            {
                Token = token,
                //User = UserDto.ToDto(user)
            });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserCreateRequest request)
        {
            var user = _userRepository
                .GetAll()
                .FirstOrDefault(u => u.Email == request.Email);
            
            if (user != null)
            {
                return Conflict("Email is already registered");
            }

            request.CreatedAt = DateTime.Now;

            var newUser = _userService.CreateUser(request);

            var token = _jwtService.GenerateToken(UserDto.ToEntity(newUser));

            return Ok(new AuthResponse
            {
                Token = token,
                //User = newUser
            });
        }
    }
}
