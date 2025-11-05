using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create(LectureDto dto)
        {
            var newLecture = _lectureService.Create(dto);

            return CreatedAtAction(nameof(GetbyId), new { id = newLecture.Id }, newLecture);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]LectureUpdateRequest dto)
        {
            var lecture = _lectureService.GetbyId(id);

            var updatedLecture = _lectureService.Update(id, dto);

            return Ok(updatedLecture);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _lectureService.Delete(id);

            return NoContent();
        }
    }
}
