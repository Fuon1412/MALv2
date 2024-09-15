using api.Data;
using api.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace api.Controllers {
    [Route("api/anime")]
    [ApiController]
    public class AnimeController : ControllerBase{
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
        public async Task<Anime> PostAnime(Anime anime)
        {
            _context.Animes.Add(anime);
            await _context.SaveChangesAsync();
            return anime;
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
    }
}