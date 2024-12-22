using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.GradeGroup
{
    public class GetAllGradeGroupsUseCase<TInputEntity, TOutput>
    {
        private readonly IRepository<TInputEntity> _gradeGroupRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetAllGradeGroupsUseCase(IRepository<TInputEntity> gradeGroupRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _gradeGroupRepository = gradeGroupRepository;
            _presenter = presenter;
        }

        public async Task<ResponseDto<IEnumerable<TOutput>>> ExecuteAsync()
        {
            ResponseDto<IEnumerable<TOutput>> responseDto = new ResponseDto<IEnumerable<TOutput>>();
            try
            {
                var gradeGroups = await _gradeGroupRepository.GetAllAsync();

                var gradeGroupsViewModel = _presenter.Present(gradeGroups);
                responseDto.Data = gradeGroupsViewModel;
                if (!responseDto.Data.Any())
                {
                    responseDto.Message = "No se encontraron Grado-Grupos.";
                }
                else
                {
                    responseDto.Message = "Grado-Grupos cargados exitosamente.";
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto
                {
                    Message = $"Ocurrió un error al tratar de obtener los Grado-Grupos."
                });
            }
            return responseDto;
        }
    }
}
