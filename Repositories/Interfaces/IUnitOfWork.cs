using StudentRegistrationAPI.Models;
using System.Threading.Tasks;

namespace StudentRegistrationAPI.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }
        Task<int> SaveChangesAsync();
    }
}
