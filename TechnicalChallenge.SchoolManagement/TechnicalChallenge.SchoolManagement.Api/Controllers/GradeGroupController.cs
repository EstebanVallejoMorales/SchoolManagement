using Microsoft.AspNetCore.Mvc;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
using TechnicalChallenge.SchoolManagement.UseCases.Grade;
using TechnicalChallenge.SchoolManagement.UseCases.GradeGroup;

namespace TechnicalChallenge.SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeGroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;
        private readonly GetAllGradeGroupsUseCase<GradeGroup, GradeGroupViewModel> _getAllGradeGroupsUseCase;

        public GradeGroupController(ILogger<GroupController> logger, GetAllGradeGroupsUseCase<GradeGroup, GradeGroupViewModel> getAllGradeGroupsUseCase)
        {
            _logger = logger;
            _getAllGradeGroupsUseCase = getAllGradeGroupsUseCase;            
        }

        [HttpGet]
        [Route("GetAllGradeGroups")]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<GradeGroupViewModel>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<GradeGroupViewModel>>), 404)]
        public async Task<IActionResult> GetAllGradeGroups()
        {
            var responseDto = await _getAllGradeGroupsUseCase.ExecuteAsync();
            if (responseDto.Data == null || !responseDto.Data.Any())
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }
    }
}
