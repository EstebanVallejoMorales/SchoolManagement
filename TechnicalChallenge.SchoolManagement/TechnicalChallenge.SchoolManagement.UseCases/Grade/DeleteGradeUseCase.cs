using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Grade
{
    public class DeleteGradeUseCase<TInputEntity>
    {
        private readonly IRepository<TInputEntity> _gradeRepository;

        public DeleteGradeUseCase(IRepository<TInputEntity> gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(int id)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                int result = await _gradeRepository.DeleteAsync(id);
                responseDto.Data = result;
                if (result != 0)
                {
                    responseDto.Message = $"Grado eliminado exitosamente";
                }
                if (result != 0)
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
