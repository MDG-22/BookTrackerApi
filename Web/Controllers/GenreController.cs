using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            if (genre == null) 
            {
                return NotFound();
            }
            return Ok(genre);
        }
    }
}
