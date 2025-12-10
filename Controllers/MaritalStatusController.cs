using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationAPI.Data;
using StudentRegistrationAPI.Models;

namespace StudentRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaritalStatusController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MaritalStatusController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var statuses = await _context.MaritalStatuses
                .OrderBy(s => s.Status)
                .ToListAsync();
            return Ok(statuses);        
        }
    }
}
