using Microsoft.AspNetCore.Mvc;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Presenters.ViewModels;
using TechnicalChallenge.SchoolManagement.UseCases.Gender;
using TechnicalChallenge.SchoolManagement.UseCases.Grade;

namespace TechnicalChallenge.SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenderController : ControllerBase
    {
        private readonly ILogger<GenderController> _logger;
        private readonly GetAllGendersUseCase<Gender, GenderViewModel> _getAllGendersUseCase;

        public GenderController(ILogger<GenderController> logger, GetAllGendersUseCase<Gender, GenderViewModel> getAllGendersUseCase)
        {
            _logger = logger;
            _getAllGendersUseCase = getAllGendersUseCase;
        }

        [HttpGet]
        [Route("GetAllGenders")]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<GenderViewModel>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<GenderViewModel>>), 404)]
        public async Task<IActionResult> GetAllGenders()
        {
            var responseDto = await _getAllGendersUseCase.ExecuteAsync();
            if (responseDto.Data == null || !responseDto.Data.Any())
            {
                return NotFound(responseDto);
            }
            return Ok(responseDto);
        }
    }
}
