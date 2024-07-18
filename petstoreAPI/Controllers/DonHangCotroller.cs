using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthucung.Models;

namespace petstoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonHangCotroller : ControllerBase
    {
        private readonly AppDbContext _context;

        public DonHangCotroller(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SanPhams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHang()
        {
            if (_context.DonHangs == null)
            {
                return NotFound();
            }
            return await _context.DonHangs.ToListAsync();
        }

        // GET: api/DonHang/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonHang>> GetDonHang(int id)
        {
            if (_context.DonHangs == null)
            {
                return NotFound();
            }
            var sanPham = await _context.DonHangs.FindAsync(id);

            if (sanPham == null)
            {
                return NotFound();
            }

            return sanPham;
        }

        // PUT: api/SanPhams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonHang(int id, DonHang donHang)
        {
            if (id != donHang.Madon)
            {
                return BadRequest();
            }

            _context.Entry(donHang).State = EntityState.Modified;

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
        public async Task<ActionResult<DonHang>> PostSanPham(DonHang donHang)
        {
            if (_context.DonHangs == null)
            {
                return Problem("Entity set 'AppDbContext.DonHangs'  is null.");
            }
            _context.DonHangs.Add(donHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSanPham", new { id = donHang.Madon }, donHang);
        }

        // DELETE: api/SanPhams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonHang(int id)
        {
            if (_context.DonHangs == null)
            {
                return NotFound();
            }
            var donhang = await _context.DonHangs.FindAsync(id);
            if (donhang == null)
            {
                return NotFound();
            }

            _context.DonHangs.Remove(donhang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonHangExists(int id)
        {
            return (_context.DonHangs?.Any(e => e.Madon == id)).GetValueOrDefault();
        }
    }
}
