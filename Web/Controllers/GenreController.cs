using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var genres = _genreService.GetAll();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var genre = _genreService.GetbyId(id);
            return Ok(genre);
        }

        [HttpPost]
        public IActionResult Create([FromBody] GenreDto dto)
        {
            var created = _genreService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] GenreDto dto)
        {
            var updated = _genreService.Update(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _genreService.Delete(id);
            return NoContent();
        }
    }
}
