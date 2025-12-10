using StudentRegistrationAPI.Models;
using System.Threading.Tasks;

namespace StudentRegistrationAPI.Repositories.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student?> GetStudentWithDetailsAsync(int id);
        new Task<IEnumerable<Student>> GetAllAsync();
    }
}
