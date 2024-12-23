using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.GradeGroup
{
    public class GetGradeGroupByIdUseCase<TInputEntity, TOutput> where TInputEntity : class
    {
        private readonly IRepository<TInputEntity> _gradeGroupRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetGradeGroupByIdUseCase(IRepository<TInputEntity> gradeGroupRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _gradeGroupRepository = gradeGroupRepository;
            _presenter = presenter;
        }

        public async Task<ResponseDto<TOutput?>> ExecuteAsync(int id)
        {
            ResponseDto<TOutput?> responseDto = new ResponseDto<TOutput?>();
            try
            {
                var gradeGroup = await _gradeGroupRepository.GetByIdAsync(id);
                if (gradeGroup != null)
                {
                    var gradeGroupViewModel = _presenter.Present(gradeGroup);
                    responseDto.Data = gradeGroupViewModel;
                }
                else
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = $"No se encontró el Grado-Grupo con id {id}." });
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto
                {
                    Message = $"Ocurrió un error al tratar de obtener el Grado-Grupo con id {id}"
                });
            }
            return responseDto;
        }
    }
}
