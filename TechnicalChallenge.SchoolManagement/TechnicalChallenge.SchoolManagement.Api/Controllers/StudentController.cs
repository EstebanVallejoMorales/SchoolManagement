using Microsoft.AspNetCore.Mvc;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.Dto.Student;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Presenters.Student;
using TechnicalChallenge.SchoolManagement.UseCases.Student;

namespace TechnicalChallenge.SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly GetStudentByIdUseCase<Student, StudentViewModel> _getStudentByIdUseCase;
        private readonly GetAllStudentUseCase<Student, StudentViewModel> _getAllStudentsUseCase;
        private readonly CreateStudentUseCase _createStudentUseCase;

        public StudentController(
            ILogger<StudentController> logger,
            GetAllStudentUseCase<Student, StudentViewModel> getAllStudentsUseCase,
            GetStudentByIdUseCase<Student, StudentViewModel> getStudentByIdUseCase,
            CreateStudentUseCase createStudentUseCase)
        {
            _logger = logger;
            _getStudentByIdUseCase = getStudentByIdUseCase;
            _createStudentUseCase = createStudentUseCase;
        }

        [HttpGet(Name = "GetAllStudents")]
        [ProducesResponseType(typeof(IEnumerable<Student>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _getAllStudentsUseCase.ExecuteAsync();
            if (students == null || !students.Any())
            {
                return NotFound("No students found.");
            }
            return Ok(students);
        }

        [HttpGet(Name = "GetStudentById")]
        [ProducesResponseType(typeof(Student), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetStudents([FromRoute] int studentId)
        {
            var student = await _getStudentByIdUseCase.ExecuteAsync(studentId);
            if (student == null)
            {
                return NotFound("No student found.");
            }
            return Ok(student);
        }

        [HttpPost(Name = "CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequestDto createStudentDto)
        {
            ResponseDto<int> responseDto;

            try
            {
                responseDto = await _createStudentUseCase.ExecuteAsync(createStudentDto);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex) {StatusCode = 500 };
            }

            return Ok(responseDto);
        }
    }
}
