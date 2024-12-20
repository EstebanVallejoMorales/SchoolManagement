using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class GetStudentByIdUseCase<TInputEntity, TOutput> where TInputEntity : class
    {
        private readonly IRepository<TInputEntity> _studentRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetStudentByIdUseCase(IRepository<TInputEntity> studentRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _studentRepository = studentRepository;
            _presenter = presenter;
        }

        public async Task<ResponseDto<TOutput?>> ExecuteAsync(int id)
        {
            ResponseDto<TOutput?> responseDto = new ResponseDto<TOutput?>();
            try
            {
                var student = await _studentRepository.GetByIdAsync(id);
                if (student != null)
                {
                    var studentViewModel = _presenter.Present(student);
                    responseDto.Data = studentViewModel;
                }
                else
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = $"No se encontró el estudiante con id {id}." });
                } 
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto
                {
                    Message = $"Ocurrió un error al tratar de obtener el estudiante con id {id}"
                });
            }
            return responseDto;
        }
    }
}
