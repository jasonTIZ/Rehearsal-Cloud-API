using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetlistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SetlistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Setlist
        [HttpGet]
        public async Task<IActionResult> GetSetlists()
        {
            var setlists = await _context.Setlists
                .Include(s => s.Songs)
                .ToListAsync();
            return Ok(setlists);
        }

        // POST: api/Setlist
        [HttpPost]
        public async Task<IActionResult> CreateSetlist([FromBody] Setlist setlist)
        {
            _context.Setlists.Add(setlist);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSetlists), new { id = setlist.Id }, setlist);
        }

        // PUT: api/Setlist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSetlist(int id, [FromBody] Setlist updatedSetlist)
        {
            var setlist = await _context.Setlists.Include(s => s.Songs).FirstOrDefaultAsync(s => s.Id == id);
            if (setlist == null) return NotFound();

            setlist.Name = updatedSetlist.Name;
            setlist.Songs = updatedSetlist.Songs;

            _context.Setlists.Update(setlist);
            await _context.SaveChangesAsync();
            return Ok(setlist);
        }

        // DELETE: api/Setlist/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetlist(int id)
        {
            var setlist = await _context.Setlists.Include(s => s.Songs).FirstOrDefaultAsync(s => s.Id == id);
            if (setlist == null) return NotFound();

            _context.Setlists.Remove(setlist);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
