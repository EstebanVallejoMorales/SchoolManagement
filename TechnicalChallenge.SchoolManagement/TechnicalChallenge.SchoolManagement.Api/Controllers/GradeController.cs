using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.Dto.Grade;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
using TechnicalChallenge.SchoolManagement.UseCases.Grade;

namespace TechnicalChallenge.SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {
        private readonly ILogger<GradeController> _logger;
        private readonly GetGradeByIdUseCase<Grade, GradeViewModel> _getGradeByIdUseCase;
        private readonly DeleteGradeUseCase<Grade> _deleteGradeUseCase;
        private readonly GetAllGradesUseCase<Grade, GradeViewModel> _getAllGradesUseCase;
        private readonly CreateGradeUseCase<CreateGradeRequestDto> _createGradeUseCase;
        private readonly UpdateGradeUseCase<UpdateGradeRequestDto> _updateGradeUseCase;

        public GradeController(
            ILogger<GradeController> logger, 
            GetAllGradesUseCase<Grade, GradeViewModel> getAllGradesUseCase,
            CreateGradeUseCase<CreateGradeRequestDto> createGradeUseCase,
            GetGradeByIdUseCase<Grade, GradeViewModel> getGradeByIdUseCase,
            UpdateGradeUseCase<UpdateGradeRequestDto> updateGradeUseCase,
            DeleteGradeUseCase<Grade> deleteGradeUseCase
            )
        {
            _logger = logger;
            _getAllGradesUseCase = getAllGradesUseCase;
            _getGradeByIdUseCase = getGradeByIdUseCase;
            _createGradeUseCase = createGradeUseCase;
            _deleteGradeUseCase = deleteGradeUseCase;
            _updateGradeUseCase = updateGradeUseCase;
        }

        [HttpGet]
        [Route("GetAllGrades")]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<GradeViewModel>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<GradeViewModel>>), 404)]
        public async Task<IActionResult> GetAllGrades()
        {
            var responseDto = await _getAllGradesUseCase.ExecuteAsync();
            if (responseDto.Data == null || !responseDto.Data.Any())
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpGet]
        [Route("GetAllGrades/{gradeId}")]
        [ProducesResponseType(typeof(ResponseDto<GradeViewModel>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GradeViewModel>), 404)]
        public async Task<IActionResult> GetGrades([FromRoute] int gradeId)
        {
            var responseDto = await _getGradeByIdUseCase.ExecuteAsync(gradeId);
            if (responseDto.Data == null)
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpPost]
        [Route("CreateGrade")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 500)]
        public async Task<IActionResult> CreateGrade([FromBody] CreateGradeRequestDto createGradeDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                responseDto = await _createGradeUseCase.ExecuteAsync(createGradeDto);
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, responseDto);
            }
            return Created(string.Empty, responseDto);
        }

        [HttpPut]
        [Route("UpdateGrade")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 500)]
        public async Task<IActionResult> UpdateGrade([FromBody] UpdateGradeRequestDto updateGradeDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                responseDto = await _updateGradeUseCase.ExecuteAsync(updateGradeDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, responseDto);
            }
            return Created(string.Empty, responseDto);
        }

        [HttpDelete]
        [Route("DeleteGrade/{gradeId}")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 404)]
        public async Task<IActionResult> DeleteGrade([FromRoute] int gradeId)
        {
            var responseDto = await _deleteGradeUseCase.ExecuteAsync(gradeId);
            if (responseDto.Data == 0)
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }
    }
}
