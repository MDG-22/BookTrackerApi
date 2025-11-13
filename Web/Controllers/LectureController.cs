using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
    {
        private readonly ILectureService _lectureService;

        public LectureController(ILectureService lectureService)
        {
            _lectureService = lectureService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();

            var lectures = _lectureService.GetAll(userId.Value);
            return Ok(lectures);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();

            var lecture = _lectureService.GetbyId(id);

            if (lecture == null || lecture.UserId != userId.Value)
                return NotFound();

            return Ok(lecture);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateLecture([FromBody] LectureCreateRequest request)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();

            var lecture = _lectureService.CreateLecture(userId.Value, request);
            return Ok(lecture);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] LectureUpdateRequest dto)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();

            var lecture = _lectureService.GetbyId(id);
            if (lecture == null || lecture.UserId != userId.Value)
                return NotFound();

            var updatedLecture = _lectureService.Update(id, dto);
            return Ok(updatedLecture);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();

            var lecture = _lectureService.GetbyId(id);
            if (lecture == null || lecture.UserId != userId.Value)
                return NotFound();

            _lectureService.Delete(id);
            return NoContent();
        }

        [HttpGet("filter")]
        [Authorize]
        public IActionResult FilterByStatus([FromQuery] LectureStatus? status)
        {
            var userId = GetUserIdFromToken();
            if (userId == null) return Unauthorized();

            var result = _lectureService.FilterByStatus(status, userId.Value);
            return Ok(result);
        }

        private int? GetUserIdFromToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier || c.Type == "id" || c.Type == JwtRegisteredClaimNames.Sub);

            if (userIdClaim == null) return null;

            return int.TryParse(userIdClaim.Value, out var userId) ? userId : null;
        }
    }
}
