using StudentRegistrationAPI.DTOs;
using StudentRegistrationAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentRegistrationAPI.Services.Interfaces
{
    public interface IStudentService
    {
        Task<Student> RegisterStudentAsync(StudentRegistrationDto dto, string uploadRoot);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentAsync(int id);

        Task<Student?> UpdateStudentAsync(int id, StudentRegistrationDto dto, string uploadRoot);
        Task<bool> DeleteStudentAsync(int id);
    }
}
