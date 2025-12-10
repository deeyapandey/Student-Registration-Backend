using Microsoft.EntityFrameworkCore;
using StudentRegistrationAPI.Data;
using StudentRegistrationAPI.Models;
using StudentRegistrationAPI.Repositories.Interfaces;
using System.Threading.Tasks;

namespace StudentRegistrationAPI.Repositories.Implementations
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context) { }

        public async Task<Student?> GetStudentWithDetailsAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Addresses)
                    .ThenInclude(a => a.Province)      // Include province
                .Include(s => s.Addresses)
                    .ThenInclude(a => a.District)      // Include district
                .Include(s => s.Addresses)
                    .ThenInclude(a => a.Municipality)  // Include municipality
                .Include(s => s.Parents)
                .Include(s => s.Enrollment)
                .Include(s => s.Financial)
                .Include(s => s.PreviousAcademics)
                .Include(s => s.Awards)
                .Include(s => s.Files)
                .Include(s=>s.Nationality)
                .Include(s=>s.BloodGroup)
                .Include(s=>s.MaritalStatus)
                .Include(s=>s.DisabilityStatus)
               
                .FirstOrDefaultAsync(s => s.StudentId == id);
        }

    }
}
