using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Teacher
{
    public class DeleteTeacherUseCase<TInputEntity>
    {
        private readonly IRepository<TInputEntity> _teacherRepository;

        public DeleteTeacherUseCase(IRepository<TInputEntity> teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(int id)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                int result = await _teacherRepository.DeleteAsync(id);
                responseDto.Data = result;
                if (result != 0)
                {
                    responseDto.Message = $"Profesor eliminado exitosamente";
                }
                if (result == 0)
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = $"No se encontró el profesor con id {id}." });
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto
                {
                    Message = $"Ocurrió un error al tratar de obtener el profesor con id {id}. Por favor, verifique que el profesor no esté asociado a algún grupo."
                });
            }
            return responseDto;
        }
    }
}
