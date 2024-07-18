using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthucung.Models;

namespace petstoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangCotroller : ControllerBase
    {
        private readonly AppDbContext _context;

        public KhachHangCotroller(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SanPhams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetKhachHang()
        {
            if (_context.KhachHangs == null)
            {
                return NotFound();
            }
            return await _context.KhachHangs.ToListAsync();
        }

        // GET: api/DonHang/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhachHang>> GetKhachHang(int id)
        {
            if (_context.KhachHangs == null)
            {
                return NotFound();
            }
            var kh = await _context.KhachHangs.FindAsync(id);

            if (kh == null)
            {
                return NotFound();
            }

            return kh;
        }

        // PUT: api/SanPhams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhachHang(int id, KhachHang khachHang)
        {
            if (id != khachHang.Makh)
            {
                return BadRequest();
            }

            _context.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
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
        public async Task<ActionResult<KhachHang>> PostKhachHang(KhachHang khachHang)
        {
            if (_context.KhachHangs == null)
            {
                return Problem("Entity set 'AppDbContext.KhachHangs'  is null.");
            }
            _context.KhachHangs.Add(khachHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKhachHang", new { id = khachHang.Makh }, khachHang);
        }

        // DELETE: api/SanPhams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachHang(int id)
        {
            if (_context.KhachHangs == null)
            {
                return NotFound();
            }
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            _context.KhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhachHangExists(int id)
        {
            return (_context.KhachHangs?.Any(e => e.Makh == id)).GetValueOrDefault();
        }
    }
}
