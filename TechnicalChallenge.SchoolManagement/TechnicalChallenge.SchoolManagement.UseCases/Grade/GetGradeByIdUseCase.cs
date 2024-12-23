using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Grade
{
    public class GetGradeByIdUseCase<TInputEntity, TOutput> where TInputEntity : class
    {
        private readonly IRepository<TInputEntity> _gradeRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetGradeByIdUseCase(IRepository<TInputEntity> gradeRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _gradeRepository = gradeRepository;
            _presenter = presenter;
        }

        public async Task<ResponseDto<TOutput?>> ExecuteAsync(int id)
        {
            ResponseDto<TOutput?> responseDto = new ResponseDto<TOutput?>();
            try
            {
                var grade = await _gradeRepository.GetByIdAsync(id);
                if (grade != null)
                {
                    var gradeViewModel = _presenter.Present(grade);
                    responseDto.Data = gradeViewModel;
                }
                else
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = $"No se encontró el grado con id {id}." });
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto
                {
                    Message = $"Ocurrió un error al tratar de obtener el grado con id {id}"
                });
            }
            return responseDto;
        }
    }
}
