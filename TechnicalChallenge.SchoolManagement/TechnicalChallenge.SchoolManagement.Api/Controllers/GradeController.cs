using Microsoft.AspNetCore.Mvc;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
using TechnicalChallenge.SchoolManagement.UseCases.Grade;

namespace TechnicalChallenge.SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly GetAllGradesUseCase<Grade, GradeViewModel> _getAllGradesUseCase;

        public GradeController(ILogger<StudentController> logger, GetAllGradesUseCase<Grade, GradeViewModel> getAllGradesUseCase)
        {
            _logger = logger;
            _getAllGradesUseCase = getAllGradesUseCase;
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
    }
}
