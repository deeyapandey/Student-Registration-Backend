using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationAPI.Data;
using StudentRegistrationAPI.Models;

namespace StudentRegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NationalityController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Nationality
        [HttpGet]
        public async Task<IActionResult> GetNationalities()
        {
            var nationalities = await _context.Nationalities
                .Select(n => new { n.NationalityId, n.Name })
                .ToListAsync();

            return Ok(nationalities);
        }

        // POST: api/Nationality
        [HttpPost]
        public async Task<IActionResult> AddNationality([FromBody] Nationality nationality)
        {
            if (nationality == null || string.IsNullOrWhiteSpace(nationality.Name))
            {
                return BadRequest("Nationality is required.");
            }

            _context.Nationalities.Add(nationality);
            await _context.SaveChangesAsync();

            // Return created nationality with 201 status
            return CreatedAtAction(nameof(GetNationalities), new { id = nationality.NationalityId }, nationality);
        }
    }
}