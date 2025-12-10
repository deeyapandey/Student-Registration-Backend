using Microsoft.AspNetCore.Mvc;
using StudentRegistrationAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace StudentRegistrationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisabilityStatusController:ControllerBase
    {
        private readonly AppDbContext _context;
        public DisabilityStatusController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var statuses = await _context.DisabilityStatuses
                .OrderBy(s => s.Status)
                .ToListAsync();
            return Ok(statuses);
        }
    }
}
