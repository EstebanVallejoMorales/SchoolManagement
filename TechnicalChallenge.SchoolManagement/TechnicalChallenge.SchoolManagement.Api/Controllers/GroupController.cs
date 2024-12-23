using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.Dto.Group;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
using TechnicalChallenge.SchoolManagement.UseCases.Group;

namespace TechnicalChallenge.SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;
        private readonly GetAllGroupsUseCase<Group, GroupViewModel> _getAllGroupsUseCase;
        private readonly GetGroupByIdUseCase<Group, GroupViewModel> _getGroupByIdUseCase;
        private readonly DeleteGroupUseCase<Group> _deleteGroupUseCase;
        private readonly CreateGroupUseCase<CreateGroupRequestDto> _createGroupUseCase;
        private readonly UpdateGroupUseCase<UpdateGroupRequestDto> _updateGroupUseCase;

        public GroupController(
            ILogger<GroupController> logger, GetAllGroupsUseCase<Group,
            GroupViewModel> getAllGroupsUseCase,
            GetGroupByIdUseCase<Group, GroupViewModel> getGroupByIdUseCase,
            CreateGroupUseCase<CreateGroupRequestDto> createGroupUseCase,
            UpdateGroupUseCase<UpdateGroupRequestDto> updateGroupUseCase,
            DeleteGroupUseCase<Group> deleteGroupUseCase

            )
        {
            _logger = logger;
            _getAllGroupsUseCase = getAllGroupsUseCase;
            _getGroupByIdUseCase = getGroupByIdUseCase;
            _createGroupUseCase = createGroupUseCase;
            _deleteGroupUseCase = deleteGroupUseCase;
            _updateGroupUseCase = updateGroupUseCase;
        }

        [HttpGet]
        [Route("GetAllGroups")]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<GroupViewModel>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<GroupViewModel>>), 404)]
        public async Task<IActionResult> GetAllGroups()
        {
            var responseDto = await _getAllGroupsUseCase.ExecuteAsync();
            if (responseDto.Data == null || !responseDto.Data.Any())
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpGet]
        [Route("GetAllGroups/{groupId}")]
        [ProducesResponseType(typeof(ResponseDto<GroupViewModel>), 200)]
        [ProducesResponseType(typeof(ResponseDto<GroupViewModel>), 404)]
        public async Task<IActionResult> GetGroups([FromRoute] int groupId)
        {
            var responseDto = await _getGroupByIdUseCase.ExecuteAsync(groupId);
            if (responseDto.Data == null)
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }

        [HttpPost]
        [Route("CreateGroup")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 500)]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequestDto createGroupDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                responseDto = await _createGroupUseCase.ExecuteAsync(createGroupDto);
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, responseDto);
            }
            return Created(string.Empty, responseDto);
        }

        [HttpPut]
        [Route("UpdateGroup")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 500)]
        public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupRequestDto updateGroupDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                responseDto = await _updateGroupUseCase.ExecuteAsync(updateGroupDto);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, responseDto);
            }
            return Created(string.Empty, responseDto);
        }

        [HttpDelete]
        [Route("DeleteGroup/{groupId}")]
        [ProducesResponseType(typeof(ResponseDto<int>), 200)]
        [ProducesResponseType(typeof(ResponseDto<int>), 404)]
        public async Task<IActionResult> DeleteGroup([FromRoute] int groupId)
        {
            var responseDto = await _deleteGroupUseCase.ExecuteAsync(groupId);
            if (responseDto.Data == 0)
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }
    }
}
