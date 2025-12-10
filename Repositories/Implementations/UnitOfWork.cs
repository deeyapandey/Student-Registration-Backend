using StudentRegistrationAPI.Data;
using StudentRegistrationAPI.Repositories.Interfaces;
using System.Threading.Tasks;

namespace StudentRegistrationAPI.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IStudentRepository Students { get; }

        public UnitOfWork(AppDbContext context, IStudentRepository studentRepository)
        {
            _context = context;
            Students = studentRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
