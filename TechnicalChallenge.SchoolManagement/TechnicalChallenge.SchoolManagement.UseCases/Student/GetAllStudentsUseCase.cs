using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class GetAllStudentUseCase<TInputEntity, TOutput>
    {
        private readonly IRepository<TInputEntity> _studentRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetAllStudentUseCase(IRepository<TInputEntity> studentRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _studentRepository = studentRepository;
            _presenter = presenter;
        }

        public async Task<ResponseDto<IEnumerable<TOutput>>> ExecuteAsync()
        {
            ResponseDto<IEnumerable<TOutput>> responseDto = new ResponseDto<IEnumerable<TOutput>>();
            try
            {
                var students = await _studentRepository.GetAllAsync();

                var studentsViewModel = _presenter.Present(students);
                responseDto.Data = studentsViewModel;

            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto
                {
                    Message = $"Ocurrió un error al tratar de obtener los estudiantes"
                });
            }
            return responseDto;
        }
    }
}
