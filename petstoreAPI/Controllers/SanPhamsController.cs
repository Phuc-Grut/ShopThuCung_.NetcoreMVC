using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthucung.Models;

namespace petstoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SanPhamsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SanPhams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanPham>>> GetSanPhams()
        {
          if (_context.SanPhams == null)
          {
              return NotFound();
          }
            return await _context.SanPhams.ToListAsync();
        }

        // GET: api/SanPhams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SanPham>> GetSanPham(int id)
        {
          if (_context.SanPhams == null)
          {
              return NotFound();
          }
            var sanPham = await _context.SanPhams.FindAsync(id);

            if (sanPham == null)
            {
                return NotFound();
            }

            return sanPham;
        }

        // PUT: api/SanPhams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanPham(int id, SanPham sanPham)
        {
            if (id != sanPham.Masp)
            {
                return BadRequest();
            }

            _context.Entry(sanPham).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanPhamExists(id))
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

        // POST: api/SanPhams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SanPham>> PostSanPham(SanPham sanPham)
        {
          if (_context.SanPhams == null)
          {
              return Problem("Entity set 'AppDbContext.SanPhams'  is null.");
          }
            _context.SanPhams.Add(sanPham);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSanPham", new { id = sanPham.Masp }, sanPham);
        }

        // DELETE: api/SanPhams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanPham(int id)
        {
            if (_context.SanPhams == null)
            {
                return NotFound();
            }
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            _context.SanPhams.Remove(sanPham);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SanPhamExists(int id)
        {
            return (_context.SanPhams?.Any(e => e.Masp == id)).GetValueOrDefault();
        }
    }
}
