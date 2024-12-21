using Microsoft.AspNetCore.Mvc;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Presenters.Student;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
using TechnicalChallenge.SchoolManagement.UseCases.Group;
using TechnicalChallenge.SchoolManagement.UseCases.Student;

namespace TechnicalChallenge.SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly GetAllGroupsUseCase<Group, GroupViewModel> _getAllGroupsUseCase;

        public GroupController(ILogger<StudentController> logger, GetAllGroupsUseCase<Group, GroupViewModel> getAllGroupsUseCase)
        {
            _logger = logger;
            _getAllGroupsUseCase = getAllGroupsUseCase;
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
    }
}
