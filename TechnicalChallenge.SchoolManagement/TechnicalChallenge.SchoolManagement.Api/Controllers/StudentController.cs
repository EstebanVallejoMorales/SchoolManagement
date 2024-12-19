using Microsoft.AspNetCore.Mvc;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Student;

namespace TechnicalChallenge.SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly GetStudentUseCase<Student> _getStudentUseCase;

        public StudentController(ILogger<StudentController> logger, GetStudentUseCase<Student> getStudentUseCase)
        {
            _logger = logger;
            _getStudentUseCase = getStudentUseCase;
        }

        [HttpGet(Name = "GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _getStudentUseCase.ExecuteAsync();
            if (students == null || !students.Any())
            {
                return NotFound("No students found.");
            }
            return Ok(students);
        }
    }
}
