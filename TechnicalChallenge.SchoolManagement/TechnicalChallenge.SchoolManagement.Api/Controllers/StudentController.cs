using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        private readonly DeleteStudentUseCase<Student> _deleteStudentUseCase;
        private readonly GetAllStudentUseCase<Student, StudentViewModel> _getAllStudentsUseCase;
        private readonly CreateStudentUseCase<CreateStudentRequestDto> _createStudentUseCase;
        private readonly UpdateStudentUseCase<UpdateStudentRequestDto> _updateStudentUseCase;
        private readonly AddStudentToGradeGroupUseCase<AddStudentToGradeGroupRequestDto> _addStudentToGradeGroupUseCase;
        private readonly IValidator<CreateStudentRequestDto> _createStudentValidator;

        public StudentController(
            ILogger<StudentController> logger,
            CreateStudentUseCase<CreateStudentRequestDto> createStudentUseCase,
            GetAllStudentUseCase<Student, StudentViewModel> getAllStudentsUseCase,
            GetStudentByIdUseCase<Student, StudentViewModel> getStudentByIdUseCase,            
            UpdateStudentUseCase<UpdateStudentRequestDto> updateStudentUseCase,
            DeleteStudentUseCase<Student> deleteStudentUseCase,
            AddStudentToGradeGroupUseCase<AddStudentToGradeGroupRequestDto> addStudentToGradeGroupUseCase,
            IValidator<CreateStudentRequestDto> createStudentValidator)
        {
            _logger = logger;
            _getStudentByIdUseCase = getStudentByIdUseCase;
            _createStudentUseCase = createStudentUseCase;
            _getAllStudentsUseCase = getAllStudentsUseCase;
            _deleteStudentUseCase = deleteStudentUseCase;
            _updateStudentUseCase = updateStudentUseCase;
            _addStudentToGradeGroupUseCase = addStudentToGradeGroupUseCase;
            _createStudentValidator = createStudentValidator;
        }

        [HttpGet]
        [Route("GetAllStudents")]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<StudentViewModel>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<StudentViewModel>>), 404)]
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
        [ProducesResponseType(typeof(ResponseDto<StudentViewModel>), 404)]
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
        [ProducesResponseType(typeof(ResponseDto<int>), 500)]
        [ProducesResponseType(typeof(ResponseDto<int>), 400)]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequestDto createStudentDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            var result = _createStudentValidator.Validate(createStudentDto);

            if (!result.IsValid)
            {
                var dictErrors = result.ToDictionary();
                foreach (var property in dictErrors)
                {
                    foreach (var error in property.Value)
                    {
                        responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = error });
                    }
                }
                return BadRequest(responseDto);
            }

            try
            {
                responseDto = await _createStudentUseCase.ExecuteAsync(createStudentDto);
            }
            catch (Exception ex)
            {
                
                return StatusCode((int)HttpStatusCode.InternalServerError, responseDto);
            }
            return Created(string.Empty, responseDto);
        }

        [HttpPut]
        [Route("UpdateStudent")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 500)]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentRequestDto updateStudentDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                responseDto = await _updateStudentUseCase.ExecuteAsync(updateStudentDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, responseDto);
            }
            return Created(string.Empty, responseDto);
        }

        [HttpDelete]
        [Route("DeleteStudent/{studentId}")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 404)]
        public async Task<IActionResult> DeleteStudent([FromRoute] int studentId)
        {
            var responseDto = await _deleteStudentUseCase.ExecuteAsync(studentId);
            if (responseDto.Data == 0)
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpPost]
        [Route("AddStudentToGradeGroup")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 500)]
        public async Task<IActionResult> AddStudentToGradeGroup([FromBody] AddStudentToGradeGroupRequestDto addStudentToGradeGroupRequestDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                responseDto = await _addStudentToGradeGroupUseCase.ExecuteAsync(addStudentToGradeGroupRequestDto);
            }
            catch (Exception ex)
            {
                StatusCode((int)HttpStatusCode.InternalServerError, responseDto);
            }
            return Created(string.Empty, responseDto);
        }
    }
}
