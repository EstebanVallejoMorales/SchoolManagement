using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Teacher
{
    public class GetTeacherByIdUseCase<TInputEntity, TOutput> where TInputEntity : class
    {
        private readonly IRepository<TInputEntity> _teacherRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetTeacherByIdUseCase(IRepository<TInputEntity> teacherRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _teacherRepository = teacherRepository;
            _presenter = presenter;
        }

        public async Task<ResponseDto<TOutput?>> ExecuteAsync(int id)
        {
            ResponseDto<TOutput?> responseDto = new ResponseDto<TOutput?>();
            try
            {
                var teacher = await _teacherRepository.GetByIdAsync(id);
                if (teacher != null)
                {
                    var teacherViewModel = _presenter.Present(teacher);
                    responseDto.Data = teacherViewModel;
                }
                else
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = $"No se encontró el profesor con id {id}." });
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto
                {
                    Message = $"Ocurrió un error al tratar de obtener el profesor con id {id}"
                });
            }
            return responseDto;
        }
    }
}
