using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var authors = _authorService.GetAll();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var author = _authorService.GetbyId(id);
            return Ok(author);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AuthorDto dto)
        {
            var created = _authorService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AuthorDto dto)
        {
            var updated = _authorService.Update(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _authorService.Delete(id);
            return NoContent();
        }
    }
}
