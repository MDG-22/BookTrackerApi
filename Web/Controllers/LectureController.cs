using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            var lectures = _lectureService.GetAll();

            return Ok(lectures);
        }

        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            var lecture = _lectureService.GetbyId(id);

            return Ok(lecture);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateLecture([FromBody] LectureCreateRequest request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var lecture = _lectureService.CreateLecture(userId, request);
            return Ok(lecture);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] LectureUpdateRequest dto)
        {
            var updatedLecture = _lectureService.Update(id, dto);
            if (updatedLecture == null) return NotFound();

            return Ok(updatedLecture);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _lectureService.Delete(id);

            return NoContent();
        }

        [HttpGet("filter")]
        public IActionResult FilterByStatus([FromQuery] LectureStatus? status, [FromQuery] int userId)
        {
            var result = _lectureService.FilterByStatus(status, userId);
            return Ok(result);
        }
    }
}
