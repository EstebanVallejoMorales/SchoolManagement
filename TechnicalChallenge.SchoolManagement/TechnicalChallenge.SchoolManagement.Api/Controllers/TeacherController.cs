using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.Dto.Teacher;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
using TechnicalChallenge.SchoolManagement.UseCases.Teacher;

namespace TechnicalChallenge.SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ILogger<TeacherController> _logger;
        private readonly GetTeacherByIdUseCase<Teacher, TeacherViewModel> _getTeacherByIdUseCase;
        private readonly DeleteTeacherUseCase<Teacher> _deleteTeacherUseCase;
        private readonly GetAllTeachersUseCase<Teacher, TeacherViewModel> _getAllTeachersUseCase;
        private readonly CreateTeacherUseCase<CreateTeacherRequestDto> _createTeacherUseCase;
        private readonly UpdateTeacherUseCase<UpdateTeacherRequestDto> _updateTeacherUseCase;
        private readonly AssignTeacherToGradeGroupClassUseCase<AssignTeacherToGradeGroupClassRequestDto> _assignTeacherToGradeGroupClassUseCase;
        private readonly AssignTeacherToGradeGroupOwnershipUseCase<AssignTeacherToGradeGroupOwnershipRequestDto> _assignTeacherToGradeGroupOwnershipUseCase;

        public TeacherController(
            ILogger<TeacherController> logger,
            CreateTeacherUseCase<CreateTeacherRequestDto> createTeacherUseCase,
            GetAllTeachersUseCase<Teacher, TeacherViewModel> getAllTeachersUseCase,
            GetTeacherByIdUseCase<Teacher, TeacherViewModel> getTeacherByIdUseCase,
            UpdateTeacherUseCase<UpdateTeacherRequestDto> updateTeacherUseCase,
            DeleteTeacherUseCase<Teacher> deleteTeacherUseCase,
            AssignTeacherToGradeGroupClassUseCase<AssignTeacherToGradeGroupClassRequestDto> assignTeacherToGradeGroupClassUseCase,
            AssignTeacherToGradeGroupOwnershipUseCase<AssignTeacherToGradeGroupOwnershipRequestDto> assignTeacherToGradeGroupOwnershipUseCase
            )
        {
            _logger = logger;
            _getTeacherByIdUseCase = getTeacherByIdUseCase;
            _createTeacherUseCase = createTeacherUseCase;
            _getAllTeachersUseCase = getAllTeachersUseCase;
            _deleteTeacherUseCase = deleteTeacherUseCase;
            _updateTeacherUseCase = updateTeacherUseCase;
            _assignTeacherToGradeGroupClassUseCase = assignTeacherToGradeGroupClassUseCase;
            _assignTeacherToGradeGroupOwnershipUseCase = assignTeacherToGradeGroupOwnershipUseCase;
        }

        [HttpGet]
        [Route("GetAllTeachers")]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<TeacherViewModel>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<TeacherViewModel>>), 404)]
        public async Task<IActionResult> GetAllTeachers()
        {
            var responseDto = await _getAllTeachersUseCase.ExecuteAsync();
            if (responseDto.Data == null || !responseDto.Data.Any())
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpGet]
        [Route("GetAllTeachers/{TeacherId}")]
        [ProducesResponseType(typeof(ResponseDto<TeacherViewModel>), 200)]
        [ProducesResponseType(typeof(ResponseDto<TeacherViewModel>), 404)]
        public async Task<IActionResult> GetTeachers([FromRoute] int TeacherId)
        {
            var responseDto = await _getTeacherByIdUseCase.ExecuteAsync(TeacherId);
            if (responseDto.Data == null)
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpPost]
        [Route("CreateTeacher")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 500)]
        public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherRequestDto createTeacherDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                responseDto = await _createTeacherUseCase.ExecuteAsync(createTeacherDto);
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, responseDto);
            }
            return Created(string.Empty, responseDto);
        }

        [HttpPut]
        [Route("UpdateTeacher")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 500)]
        public async Task<IActionResult> UpdateTeacher([FromBody] UpdateTeacherRequestDto updateTeacherDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                responseDto = await _updateTeacherUseCase.ExecuteAsync(updateTeacherDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, responseDto);
            }
            return Created(string.Empty, responseDto);
        }

        [HttpDelete]
        [Route("DeleteTeacher/{TeacherId}")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 404)]
        public async Task<IActionResult> DeleteTeacher([FromRoute] int TeacherId)
        {
            var responseDto = await _deleteTeacherUseCase.ExecuteAsync(TeacherId);
            if (responseDto.Data == 0)
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpPost]
        [Route("AssignTeacherToGradeGroupClass")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 500)]
        public async Task<IActionResult> AssignTeacherToGradeGroup([FromBody] AssignTeacherToGradeGroupClassRequestDto addTeacherToGradeGroupRequestDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                responseDto = await _assignTeacherToGradeGroupClassUseCase.ExecuteAsync(addTeacherToGradeGroupRequestDto);
            }
            catch (Exception ex)
            {
                StatusCode((int)HttpStatusCode.InternalServerError, responseDto);
            }
            return Created(string.Empty, responseDto);
        }

        [HttpPost]
        [Route("AssignTeacherToGradeGroupOwnership")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 500)]
        public async Task<IActionResult> AssignTeacherToGradeGroupOwnership([FromBody] AssignTeacherToGradeGroupOwnershipRequestDto addTeacherToGradeGroupOwnershipRequestDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                responseDto = await _assignTeacherToGradeGroupOwnershipUseCase.ExecuteAsync(addTeacherToGradeGroupOwnershipRequestDto);
            }
            catch (Exception ex)
            {
                StatusCode((int)HttpStatusCode.InternalServerError, responseDto);
            }
            return Created(string.Empty, responseDto);
        }
    }
}
