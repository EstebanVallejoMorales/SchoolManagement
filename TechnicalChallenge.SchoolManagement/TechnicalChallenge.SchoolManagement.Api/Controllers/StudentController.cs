using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly CreateStudentUseCase<CreateStudentRequestDto> _createStudentUseCase;

        public StudentController(
            ILogger<StudentController> logger,
            GetAllStudentUseCase<Student, StudentViewModel> getAllStudentsUseCase,
            GetStudentByIdUseCase<Student, StudentViewModel> getStudentByIdUseCase,
            CreateStudentUseCase<CreateStudentRequestDto> createStudentUseCase)
        {
            _logger = logger;
            _getStudentByIdUseCase = getStudentByIdUseCase;
            _createStudentUseCase = createStudentUseCase;
            _getAllStudentsUseCase = getAllStudentsUseCase;
        }

        [HttpGet]
        [Route("GetAllStudents")]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<Student>>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetAllStudents()
        {
            var responseDto = await _getAllStudentsUseCase.ExecuteAsync();
            if (responseDto.Data == null || !responseDto.Data.Any())
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpGet]
        [Route("GetAllStudents/{studentId}")]
        [ProducesResponseType(typeof(ResponseDto<StudentViewModel>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetStudents([FromRoute] int studentId)
        {
            var responseDto = await _getStudentByIdUseCase.ExecuteAsync(studentId);
            if (responseDto.Data == null)
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpPost]
        [Route("CreateStudent")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ObjectResult), 500)]
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
            return Created(string.Empty, responseDto);
        }
    }
}
