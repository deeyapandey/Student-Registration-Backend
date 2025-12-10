using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationAPI.Data;
using StudentRegistrationAPI.DTOs;
using StudentRegistrationAPI.Helpers;
using StudentRegistrationAPI.Models;
using StudentRegistrationAPI.Services.Implementations;
using StudentRegistrationAPI.Services.Interfaces;

namespace StudentRegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly IWebHostEnvironment _env;

        public RegistrationController(IStudentService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] StudentRegistrationDto dto)
        {
           

            string uploadRoot = Path.Combine(_env.ContentRootPath, "Uploads");
            var student = await _service.RegisterStudentAsync(dto, uploadRoot);

            return Ok(new
            {
                message = "Registration successful",
                studentId=student.StudentId
            });

            
        }

        [HttpGet("students")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _service.GetAllStudentsAsync();
            return Ok(students);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {

            var student = await _service.GetStudentAsync(id);
       

            if (student == null) return NotFound();

            return Ok(student);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromForm] StudentRegistrationDto dto)
        {
            string uploadRoot = Path.Combine(_env.ContentRootPath, "Uploads");
            var updatedStudent = await _service.UpdateStudentAsync(id, dto, uploadRoot);
            if(updatedStudent==null)
            {
                return NotFound(new { message = "Student not found" });
            }
            return Ok(new
            {
                message = "Student updated successfully",
                studentId = updatedStudent.StudentId
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _service.DeleteStudentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }



    }
}
