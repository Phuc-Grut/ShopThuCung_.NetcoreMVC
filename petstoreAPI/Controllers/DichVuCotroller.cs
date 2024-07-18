using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthucung.Models;

namespace petstoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuCotroller : ControllerBase
    {
        private readonly AppDbContext _context;

        public DichVuCotroller(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SanPhams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DichVu>>> GetDichVu()
        {
            if (_context.DichVus == null)
            {
                return NotFound();
            }
            return await _context.DichVus.ToListAsync();
        }

        // GET: api/DonHang/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DichVu>> GetDichVu(int id)
        {
            if (_context.DichVus == null)
            {
                return NotFound();
            }
            var sanPham = await _context.DichVus.FindAsync(id);

            if (sanPham == null)
            {
                return NotFound();
            }

            return sanPham;
        }

        // PUT: api/SanPhams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDichVu(int id, DichVu dichVu)
        {
            if (id != dichVu.Iddichvu)
            {
                return BadRequest();
            }

            _context.Entry(dichVu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonHangExists(id))
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
        public async Task<ActionResult<DichVu>> PostDichVu(DichVu dichVu)
        {
            if (_context.DichVus == null)
            {
                return Problem("Entity set 'AppDbContext.DichVus'  is null.");
            }
            _context.DichVus.Add(dichVu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDichVu", new { id = dichVu.Iddichvu }, dichVu);
        }

        // DELETE: api/SanPhams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDichVu(int id)
        {
            if (_context.DichVus == null)
            {
                return NotFound();
            }
            var donhang = await _context.DichVus.FindAsync(id);
            if (donhang == null)
            {
                return NotFound();
            }

            _context.DichVus.Remove(donhang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonHangExists(int id)
        {
            return (_context.DichVus?.Any(e => e.Iddichvu == id)).GetValueOrDefault();
        }
    }
}
