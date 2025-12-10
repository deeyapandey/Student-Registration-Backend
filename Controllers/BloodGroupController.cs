using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationAPI.Data;
using StudentRegistrationAPI.Models;
using System.Linq;
namespace StudentRegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodGroupController:ControllerBase
    {

        private readonly AppDbContext _context;
        public BloodGroupController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task <ActionResult> GetBloodGroup()
        {
            var list = await _context.BloodGroups.ToListAsync();
            return Ok(list);
        }
        [HttpPost]
        public async Task  <ActionResult> AddBloodGroup(BloodGroup bloodGroup)
        {
            if (bloodGroup == null || string.IsNullOrEmpty(bloodGroup.Name))
                return BadRequest("Invalid Blood Group");

            await _context.BloodGroups.AddAsync(bloodGroup);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Blood group added successfully" });

        }

    }
}
