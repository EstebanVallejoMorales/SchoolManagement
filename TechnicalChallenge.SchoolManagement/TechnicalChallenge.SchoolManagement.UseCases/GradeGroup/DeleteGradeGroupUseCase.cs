using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.GradeGroup
{
    public class DeleteGradeGroupUseCase<TInputEntity>
    {
        private readonly IRepository<TInputEntity> _gradeGroupRepository;

        public DeleteGradeGroupUseCase(IRepository<TInputEntity> gradeGroupRepository)
        {
            _gradeGroupRepository = gradeGroupRepository;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(int id)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                int result = await _gradeGroupRepository.DeleteAsync(id);
                responseDto.Data = result;
                if (result != 0)
                {
                    responseDto.Message = $"Grado-Grupo eliminado exitosamente";
                }
                if (result != 0)
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
