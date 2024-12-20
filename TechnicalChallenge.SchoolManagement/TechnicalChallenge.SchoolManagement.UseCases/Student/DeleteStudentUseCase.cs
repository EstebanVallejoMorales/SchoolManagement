using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class DeleteStudentUseCase<TInputEntity>
    {
        private readonly IRepository<TInputEntity> _studentRepository;

        public DeleteStudentUseCase(IRepository<TInputEntity> studentRepository)
        {
            _studentRepository = studentRepository;            
        }

        public async Task<ResponseDto<int>> ExecuteAsync(int id)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                int result = await _studentRepository.DeleteAsync(id);
                responseDto.Data = result;
                if (result != 0)
                {
                    responseDto.Message = $"Estudiante eliminado exitosamente";
                }
                if (result != 0)
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
