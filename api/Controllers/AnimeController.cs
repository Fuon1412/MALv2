using api.Data;
using api.DTOs;
using api.Models;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace api.Controllers
{
    [Route("api/anime")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public AnimeController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Anime>> GetAnimes()
        {
            return await _context.Animes.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Anime>> GetAnime(int id)
        {
            var result = await _context.Animes.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> PostAnime([FromBody] CreateAnimeDTO newAnime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addAnime = newAnime.toCreatedAnimeDTO();
            _context.Animes.Add(addAnime);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnime), new { id = addAnime.Id }, addAnime);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Anime>> PutAnime(int id, Anime anime)
        {
            if (id != anime.Id)
            {
                return BadRequest();
            }
            _context.Entry(anime).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Anime>> DeleteAnime(int id)
        {
            var anime = await _context.Animes.FindAsync(id);
            if (anime == null)
            {
                return NotFound();
            }
            _context.Animes.Remove(anime);
            await _context.SaveChangesAsync();
            return anime;
        }
    }
}