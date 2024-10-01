using api.Data;
using api.DTOs;
using api.Models;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/tag")]
    [ApiController]
    public class TagController : ControllerBase{
        private readonly ApplicationDBContext _context;
        public TagController(ApplicationDBContext context){
            _context = context;
        }

        [HttpGet]
        public async Task<List<Tag>> GetTags(){
            return await _context.Tags.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id){
            var result = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null){
                return NotFound();
            }
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> PostTag([FromBody] CreatedTagDTO newTag){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            var tag = newTag.toCreatedTagDTO();
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, newTag);
        }
    }
}