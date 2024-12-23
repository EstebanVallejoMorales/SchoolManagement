using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.Dto.Grade;
using TechnicalChallenge.SchoolManagement.Dto.GradeGroup;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
using TechnicalChallenge.SchoolManagement.UseCases.GradeGroup;

namespace TechnicalChallenge.SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeGroupController : ControllerBase
    {
        private readonly ILogger<GradeGroupController> _logger;
        private readonly GetAllGradeGroupsUseCase<GradeGroup, GradeGroupViewModel> _getAllGradeGroupsUseCase;
        private readonly GetGradeGroupByIdUseCase<GradeGroup, GradeGroupViewModel> _getGradeGroupByIdUseCase;
        private readonly DeleteGradeGroupUseCase<GradeGroup> _deleteGradeGroupUseCase;
        private readonly CreateGradeGroupUseCase<CreateGradeGroupRequestDto> _createGradeGroupUseCase;
        private readonly UpdateGradeGroupUseCase<UpdateGradeGroupRequestDto> _updateGradeGroupUseCase;

        public GradeGroupController(
            ILogger<GradeGroupController> logger,
            GetAllGradeGroupsUseCase<GradeGroup, GradeGroupViewModel> getAllGradeGroupsUseCase,
            CreateGradeGroupUseCase<CreateGradeGroupRequestDto> createGradeGroupUseCase,
            GetGradeGroupByIdUseCase<GradeGroup, GradeGroupViewModel> getGradeGroupByIdUseCase,
            UpdateGradeGroupUseCase<UpdateGradeGroupRequestDto> updateGradeGroupUseCase,
            DeleteGradeGroupUseCase<GradeGroup> deleteGradeGroupUseCase
            )
        {
            _logger = logger;
            _getAllGradeGroupsUseCase = getAllGradeGroupsUseCase;
            _getGradeGroupByIdUseCase = getGradeGroupByIdUseCase;
            _createGradeGroupUseCase = createGradeGroupUseCase;
            _deleteGradeGroupUseCase = deleteGradeGroupUseCase;
            _updateGradeGroupUseCase = updateGradeGroupUseCase;
        }

        [HttpGet]
        [Route("GetAllGradeGroups")]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<GradeGroupViewModel>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<GradeGroupViewModel>>), 404)]
        public async Task<IActionResult> GetAllGradeGradeGroups()
        {
            var responseDto = await _getAllGradeGroupsUseCase.ExecuteAsync();
            if (responseDto.Data == null || !responseDto.Data.Any())
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpGet]
        [Route("GetAllGradesGroups/{gradeGroupId}")]
        [ProducesResponseType(typeof(ResponseDto<GradeViewModel>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GradeViewModel>), 404)]
        public async Task<IActionResult> GetAllGradesGroups([FromRoute] int gradeGroupId)
        {
            var responseDto = await _getGradeGroupByIdUseCase.ExecuteAsync(gradeGroupId);
            if (responseDto.Data == null)
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpPost]
        [Route("CreateGradeGroupRequestDto")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 500)]
        public async Task<IActionResult> CreateGrade([FromBody] CreateGradeGroupRequestDto createGradeGroupDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                responseDto = await _createGradeGroupUseCase.ExecuteAsync(createGradeGroupDto);
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, responseDto);
            }
            return Created(string.Empty, responseDto);
        }

        [HttpPut]
        [Route("UpdateGradeGroup")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 500)]
        public async Task<IActionResult> UpdateGradeGroup([FromBody] UpdateGradeGroupRequestDto updateGradeGroupDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                responseDto = await _updateGradeGroupUseCase.ExecuteAsync(updateGradeGroupDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, responseDto);
            }
            return Created(string.Empty, responseDto);
        }

        [HttpDelete]
        [Route("DeleteGradeGroup/{gradeGroupId}")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 404)]
        public async Task<IActionResult> DeleteGrade([FromRoute] int gradeGroupId)
        {
            var responseDto = await _deleteGradeGroupUseCase.ExecuteAsync(gradeGroupId);
            if (responseDto.Data == 0)
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }
    }
}
