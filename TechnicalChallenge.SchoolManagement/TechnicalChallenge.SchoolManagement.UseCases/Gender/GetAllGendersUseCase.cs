using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Gender
{
    public class GetAllGendersUseCase<TInputEntity, TOutput>
    {
        private readonly IRepository<TInputEntity> _groupRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetAllGendersUseCase(IRepository<TInputEntity> groupRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _groupRepository = groupRepository;
            _presenter = presenter;
        }

        public async Task<ResponseDto<IEnumerable<TOutput>>> ExecuteAsync()
        {
            ResponseDto<IEnumerable<TOutput>> responseDto = new ResponseDto<IEnumerable<TOutput>>();
            try
            {
                var gender = await _groupRepository.GetAllAsync();

                var genderViewModel = _presenter.Present(gender);
                responseDto.Data = genderViewModel;
                if (!responseDto.Data.Any())
                {
                    responseDto.Message = "No se encontraron géneros.";
                }
                else
                {
                    responseDto.Message = "Géneros cargados exitosamente.";
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto
                {
                    Message = $"Ocurrió un error al tratar de obtener los géneros."
                });
            }
            return responseDto;
        }
    }
}
