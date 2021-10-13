using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCancionesApiSim.Data;
using WebCancionesApiSim.Models;

namespace WebCancionesApiSim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancionnesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CancionnesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cancionnes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cancionn>>> GetCancionn()
        {
            return await _context.Cancionn.ToListAsync();
        }

        // GET: api/Cancionnes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cancionn>> GetCancionn(string id)
        {
            var cancionn = await _context.Cancionn.FindAsync(id);

            if (cancionn == null)
            {
                return NotFound();
            }

            return cancionn;
        }

        // PUT: api/Cancionnes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCancionn(string id, Cancionn cancionn)
        {
            if (id != cancionn.Nombre)
            {
                return BadRequest();
            }

            _context.Entry(cancionn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CancionnExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cancionnes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cancionn>> PostCancionn(Cancionn cancionn)
        {
            _context.Cancionn.Add(cancionn);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CancionnExists(cancionn.Nombre))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCancionn", new { id = cancionn.Nombre }, cancionn);
        }

        // DELETE: api/Cancionnes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCancionn(string id)
        {
            var cancionn = await _context.Cancionn.FindAsync(id);
            if (cancionn == null)
            {
                return NotFound();
            }

            _context.Cancionn.Remove(cancionn);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CancionnExists(string id)
        {
            return _context.Cancionn.Any(e => e.Nombre == id);
        }
    }
}
