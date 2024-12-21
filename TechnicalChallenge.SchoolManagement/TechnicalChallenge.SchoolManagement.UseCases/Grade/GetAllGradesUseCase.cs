using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Grade
{
    public class GetAllGradesUseCase<TInputEntity, TOutput>
    {
        private readonly IRepository<TInputEntity> _gradeRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetAllGradesUseCase(IRepository<TInputEntity> studentRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _gradeRepository = studentRepository;
            _presenter = presenter;
        }

        public async Task<ResponseDto<IEnumerable<TOutput>>> ExecuteAsync()
        {
            ResponseDto<IEnumerable<TOutput>> responseDto = new ResponseDto<IEnumerable<TOutput>>();
            try
            {
                var grades = await _gradeRepository.GetAllAsync();

                var gradesViewModel = _presenter.Present(grades);
                responseDto.Data = gradesViewModel;
                if (!responseDto.Data.Any())
                {
                    responseDto.Message = "No se encontraron grados.";
                }
                else
                {
                    responseDto.Message = "Grados cargados exitosamente";
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto
                {
                    Message = $"Ocurrió un error al tratar de obtener los grupos"
                });
            }
            return responseDto;
        }
    }
}
